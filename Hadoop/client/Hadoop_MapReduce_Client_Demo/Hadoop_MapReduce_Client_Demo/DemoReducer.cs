using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Hadoop.MapReduce;

namespace Hadoop_MapReduce_Client_Demo
{
    public class CountReducer : ReducerCombinerBase
    {

        public override void Reduce(string key, IEnumerable<string> values, ReducerCombinerContext context)
        {
            // Initialize counter
            int count = 0;
            // Add each value
            foreach (string value in values)
            {
                count += int.Parse(value);
            }

            context.EmitKeyValue(key, count.ToString());
        }

    }

}
