using System;
using System.IO;

namespace QSF.Services
{
    public interface ISerializationService
    {
        T XmlDeserializeFromStream<T>(Stream stream);
    }
}
