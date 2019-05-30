using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

static internal class ByteToObject
{
    public static T ConvertTo<T>(byte[] byteData)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter ();
        using (MemoryStream ms = new MemoryStream())
        {
            ms.Write (byteData, 0, byteData.Length);
            ms.Seek (0, SeekOrigin.Begin);

            return (T)binaryFormatter.Deserialize (ms);
        }
    }
}