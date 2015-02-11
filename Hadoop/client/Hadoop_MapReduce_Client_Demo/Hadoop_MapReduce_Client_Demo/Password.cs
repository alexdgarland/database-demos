using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;

namespace Hadoop_MapReduce_Client_Demo
{

    public class Password
    {
        public static string Get(string userName)
        {
            Console.WriteLine("Please enter password for Hadoop cluster user \"{0}\".", userName);

            var sbPwd = new StringBuilder();

            while (true)
            {
                ConsoleKeyInfo i = Console.ReadKey(true);
                if (i.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (i.Key == ConsoleKey.Backspace && sbPwd.Length > 0)
                {
                    sbPwd.Length -= 1;
                    Console.Write("\b \b");
                }
                else
                {
                    sbPwd.Append(i.KeyChar);
                    Console.Write("*");
                }
            }
            return sbPwd.ToString();
        }
    }

}
