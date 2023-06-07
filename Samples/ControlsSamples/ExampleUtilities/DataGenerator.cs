using QSF.Services;
using Microsoft.Maui.Controls;

namespace QSF.ExampleUtilities;

public static class DataGenerator
{
    public static T GetItems<T>(string path)
    {
        var resourceService = DependencyService.Get<IResourceService>();
        var serializationService = DependencyService.Get<ISerializationService>();

        var stream = resourceService.GetResourceStream(path);
        T items = serializationService.XmlDeserializeFromStream<T>(stream);

        return items;
    }
}
