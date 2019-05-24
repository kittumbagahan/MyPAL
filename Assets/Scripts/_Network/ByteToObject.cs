using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

static internal class ByteToObject
{
    public static T ConvertToObject<T>(byte[] byteData)
    {
        BinaryFormatter bin = new BinaryFormatter ();
        MemoryStream ms = new MemoryStream ();
        ms.Write (byteData, 0, byteData.Length);
        ms.Seek (0, SeekOrigin.Begin);

        return (T)bin.Deserialize (ms);
    }
}