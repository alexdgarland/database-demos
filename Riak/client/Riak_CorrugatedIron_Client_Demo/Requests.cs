using CorrugatedIron;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System;

namespace Riak_CorrugatedIron_Client_Demo
{
    partial class Program
    {

        static String GetDemoMessage(IRiakClient client)
        {
            var result = client.Get(bucket: "demo", key: "message");
            CheckContentType(result, "text/plain");
            return Encoding.Default.GetString(result.Value.Value);
        }

        static String GetImage(IRiakClient client, String fileName)
        {
            var result = client.Get(bucket: "demo", key: fileName);
            CheckContentType(result, "image/jpeg");
            Byte[] bytes = result.Value.Value;

            String tempFilePath = Path.Combine(Path.GetTempPath(), fileName);

            using (MemoryStream stream = new MemoryStream(bytes))
            {
                using (Image image = Image.FromStream(stream))
                {
                    image.Save(tempFilePath, ImageFormat.Jpeg);
                }
            }

            return tempFilePath;
        }

        static void CheckContentType(RiakResult<CorrugatedIron.Models.RiakObject> result, String expectedType)
        {
            String actualType = result.Value.ContentType;
            if (actualType != expectedType)
            {
                var error = String.Format("Resonse content type error ({0} required, got {1})", expectedType, actualType);
                throw new Exception(error);
            }
        }
    }
}