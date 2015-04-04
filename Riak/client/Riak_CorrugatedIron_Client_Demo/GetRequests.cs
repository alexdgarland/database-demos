using CorrugatedIron;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Riak_CorrugatedIron_Client_Demo
{
    class GetRequests
    {

        public static void Run()
        {

            IRiakClient client = DemoSetup.GetClient();

            Console.WriteLine("\nGetting message...\n");
            Console.WriteLine("\"" + GetDemoMessage(client) + "\"");

            Console.WriteLine("\nGetting Riak logo image...\n");
            String tempFilePath = GetImage(client, fileName: "RiakLogo.jpg");
            DisplayImage(tempFilePath);
            File.Delete(tempFilePath);

        }


        private static String GetDemoMessage(IRiakClient client)
        {
            // Simple GET based on bucket + key
            var result = client.Get(bucket: "demo", key: "message");

            // Further handling ...
            CheckContentType(result, "text/plain");
            return Encoding.Default.GetString(result.Value.Value);
        }

        private static String GetImage(IRiakClient client, String fileName)
        {
            // Simple GET based on bucket + key
            var result = client.Get(bucket: "demo", key: fileName);
            
            // Further handling ...
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

        private static void CheckContentType(RiakResult<CorrugatedIron.Models.RiakObject> result, String expectedType)
        {
            // You either need to be careful about requesting objects with the expected content type,
            // or do some kind of explicit header check - e.g. ...
            String actualType = result.Value.ContentType;
            if (actualType != expectedType)
            {
                var error = String.Format("Response content type error ({0} required, got {1})", expectedType, actualType);
                throw new Exception(error);
            }
        }

        private static void DisplayImage(String filePath)
        {
            Process process = new Process();
            process.StartInfo.FileName = filePath;
            process.Start();
            process.WaitForExit();
        }

    }
}