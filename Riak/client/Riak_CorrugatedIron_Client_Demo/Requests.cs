using CorrugatedIron;
using System;
using System.Text;

namespace Riak_CorrugatedIron_Client_Demo
{
    partial class Program
    {

        static String GetDemoMessage(IRiakClient client)
        {
            var result = client.Get(bucket: "demo", key: "message");
            return Encoding.Default.GetString(result.Value.Value);
        }

    }
}