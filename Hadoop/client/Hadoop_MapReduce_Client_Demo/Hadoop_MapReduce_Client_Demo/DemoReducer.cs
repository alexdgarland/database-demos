using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Hadoop.MapReduce;

namespace Hadoop_MapReduce_Client_Demo
{
    public class DemoReducer : ReducerCombinerBase
    {

        public override void Reduce(string key, IEnumerable<string> values, ReducerCombinerContext context)
        {
            ////initialize counters
            //int myCount = 0;
            //int mySum = 0;

            ////count and sum incoming values
            //foreach (string value in values)
            //{
            //    mySum += int.Parse(value);
            //    myCount++;
            //}

            ////output results
            //context.EmitKeyValue(key, myCount + "\t" + mySum);
        }

    }

}
