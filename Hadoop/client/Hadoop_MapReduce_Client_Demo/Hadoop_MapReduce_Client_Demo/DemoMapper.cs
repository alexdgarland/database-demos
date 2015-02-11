using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Hadoop.MapReduce;

namespace Hadoop_MapReduce_Client_Demo
{

    public class DemoMapper : MapperBase
    {

        public override void Map(string inputLine, MapperContext context)
        {

            ////interpret the incoming line as an integer value
            //int value = int.Parse(inputLine);

            ////determine whether value is even or odd
            //string key = (value % 2 == 0) ? "even" : "odd";

            ////output key assignment with value
            //context.EmitKeyValue(key, value.ToString());

        }

    }

}
