using Microsoft.Extensions.AI;
using Microsoft.Maui.Networking;
using Microsoft.Maui.Storage;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Onnx;
using System;
using System.Buffers;
using System.IO;
using System.Net.Http;
using System.Numerics.Tensors;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace QSF.Examples.DataGridControl.SemanticSearchExample;

public sealed class LocalEmbeddingService : IDisposable
{
    private const int MaxTokensCount = 256;
    private const string ModelFolderName = "ai-models";
    private const string ModelName = "all-MiniLM-L12-v2";
    private const string Arm64ModelUrl = "https://huggingface.co/sentence-transformers/all-MiniLM-L12-v2/resolve/main/onnx/model_qint8_arm64.onnx";
    private const string WindowsX64ModelUrl = "https://huggingface.co/sentence-transformers/all-MiniLM-L12-v2/resolve/main/onnx/model_quint8_avx2.onnx";
    private const string VocabUrl = "https://huggingface.co/sentence-transformers/all-MiniLM-L12-v2/resolve/main/vocab.txt";

    private static readonly TimeSpan IoTimeout = TimeSpan.FromSeconds(5);
    private static readonly HttpClient HttpClient = new HttpClient();

    private IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator;

    public bool IsModelDownloaded
    {
        get
        {
            var paths = GetModelPaths();
            return File.Exists(paths.ModelPath) && File.Exists(paths.VocabPath);
        }
    }

    public void Dispose()
    {
        this.embeddingGenerator?.Dispose();
    }

    public EmbeddingF32 Embed(string inputText) => this.EmbedAsync(inputText).Result;

    public async Task<EmbeddingF32> EmbedAsync(string inputText)
    {
        if (this.embeddingGenerator == null)
        {
            var (modelPath, vocabPath) = GetModelPaths();

            var builder = Kernel.CreateBuilder();

            var options = new BertOnnxOptions { CaseSensitive = false, MaximumTokens = MaxTokensCount };
            var kernel = builder.AddBertOnnxEmbeddingGenerator(modelPath, vocabPath, options).Build();

            this.embeddingGenerator = kernel.GetRequiredService<IEmbeddingGenerator<string, Embedding<float>>>();
        }

        if (this.embeddingGenerator == null)
        {
            throw new InvalidOperationException("The embedding generator was not initialized properly.");
        }

        var embedding = await this.embeddingGenerator.GenerateVectorAsync(inputText);
        return EmbeddingF32.FromModelOutput(embedding.Span, new byte[EmbeddingF32.GetBufferByteLength(embedding.Span.Length)]);
    }

    public async Task DownloadModelAsync(IProgress<double> progress = null, CancellationToken cancellationToken = default)
    {
        if (this.IsModelDownloaded)
        {
            progress?.Report(1);
            return;
        }

        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            throw new InvalidOperationException("No internet connection is available.");
        }

        var modelUrl = GetModelUrl();
        var (modelPath, vocabPath) = GetModelPaths();

        progress?.Report(0);

        if (File.Exists(modelPath))
        {
            Directory.Delete(Path.GetDirectoryName(modelPath)!, true);
        }

        var modelProgress = progress == null ? null : new Progress<double>(p => progress.Report(p * 0.5));
        await DownloadFileAsync(modelUrl, modelPath, modelProgress, cancellationToken).ConfigureAwait(false);

        if (File.Exists(vocabPath))
        {
            Directory.Delete(Path.GetDirectoryName(vocabPath)!, true);
        }

        var vocabProgress = progress == null ? null : new Progress<double>(p => progress.Report(0.5 + (p * 0.5)));
        await DownloadFileAsync(VocabUrl, vocabPath, vocabProgress, cancellationToken).ConfigureAwait(false);
    }

    private static string GetModelUrl()
    {
        if (OperatingSystem.IsWindows() && RuntimeInformation.OSArchitecture == Architecture.X64)
        {
            return WindowsX64ModelUrl;
        }

        return Arm64ModelUrl;
    }

    private static (string ModelPath, string VocabPath) GetModelPaths()
    {
        var modelUrl = GetModelUrl();
        var modelFileName = Path.GetFileName(new Uri(modelUrl).AbsolutePath);
        var vocabFileName = Path.GetFileName(new Uri(VocabUrl).AbsolutePath);
        var basePath = Path.Combine(FileSystem.AppDataDirectory, ModelFolderName, ModelName);

        return (Path.Combine(basePath, modelFileName), Path.Combine(basePath, vocabFileName));
    }

    private static async Task DownloadFileAsync(string url, string destinationPath, IProgress<double> progress, CancellationToken cancellationToken)
    {
        if (File.Exists(destinationPath))
        {
            progress?.Report(1);
            return;
        }

        using var response = await HttpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        var contentLength = response.Content.Headers.ContentLength;
        await using var source = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);

        var directory = Path.GetDirectoryName(destinationPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        try
        {
            await using var target = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 16 * 1024, useAsync: true);

            var buffer = new byte[16 * 1024];
            long totalRead = 0;
            int bytesRead;

            while (true)
            {
                using var readCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                readCts.CancelAfter(IoTimeout);
                bytesRead = await source.ReadAsync(buffer.AsMemory(0, buffer.Length), readCts.Token).ConfigureAwait(false);

                if (bytesRead == 0)
                {
                    break;
                }

                using var writeCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                writeCts.CancelAfter(IoTimeout);
                await target.WriteAsync(buffer.AsMemory(0, bytesRead), writeCts.Token).ConfigureAwait(false);
                totalRead += bytesRead;

                if (contentLength.HasValue && contentLength.Value > 0)
                {
                    progress?.Report((double)totalRead / contentLength.Value);
                }
            }
        }
        catch
        {
            if (File.Exists(destinationPath))
            {
                File.Delete(destinationPath);
            }

            throw;
        }

        progress?.Report(1);
    }

    public readonly struct EmbeddingF32
    {
        private readonly ReadOnlyMemory<byte> buffer;
        private readonly ReadOnlyMemory<float> values;

        public EmbeddingF32(ReadOnlyMemory<byte> buffer)
        {
            this.buffer = buffer;
            this.values = Utils.Cast<byte, float>(MemoryMarshal.AsMemory(buffer));
        }

        public ReadOnlyMemory<byte> Buffer => buffer;

        public ReadOnlyMemory<float> Values => values;

        public static EmbeddingF32 FromModelOutput(ReadOnlySpan<float> input, Memory<byte> buffer)
        {
            var requiredBufferLength = GetBufferByteLength(input.Length);
            if (buffer.Length != requiredBufferLength)
            {
                throw new InvalidOperationException($"For an input with {input.Length} dimensions, the buffer length must be equal to {requiredBufferLength}, but it was {buffer.Length}.");
            }

            MemoryMarshal.AsBytes(input).CopyTo(buffer.Span);
            return new EmbeddingF32(buffer);
        }

        public static int GetBufferByteLength(int dimensions) => dimensions * sizeof(float);

        public float Similarity(EmbeddingF32 other) => TensorPrimitives.CosineSimilarity(values.Span, other.values.Span);

        internal static class Utils
        {
            public static Memory<TTo> Cast<TFrom, TTo>(Memory<TFrom> from) where TFrom : unmanaged where TTo : unmanaged
            {
                if (typeof(TFrom) == typeof(TTo))
                {
                    return (Memory<TTo>)(object)from;
                }

                return new CastMemoryManager<TFrom, TTo>(from).Memory;
            }

            private sealed class CastMemoryManager<TFrom, TTo> : MemoryManager<TTo> where TFrom : unmanaged where TTo : unmanaged
            {
                private readonly Memory<TFrom> from;

                public CastMemoryManager(Memory<TFrom> from)
                {
                    this.from = from;
                }

                public override Span<TTo> GetSpan() => MemoryMarshal.Cast<TFrom, TTo>(from.Span);

                protected override void Dispose(bool disposing)
                {
                }

                public override MemoryHandle Pin(int elementIndex = 0) => throw new NotSupportedException();

                public override void Unpin() => throw new NotSupportedException();
            }
        }
    }
}
