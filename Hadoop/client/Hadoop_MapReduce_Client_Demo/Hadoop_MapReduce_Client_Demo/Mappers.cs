using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Hadoop.MapReduce;

namespace Hadoop_MapReduce_Client_Demo
{


    public abstract class DemoCountMapper : MapperBase
    {
        private Boolean _isHeader(String line)
        {
            // Dead simple method used to identify (and hence skip) header row
            return (line.Substring(0, 8) == "Postcode");
        }

        public override void Map(String inputLine, MapperContext context)
        {
            if (!_isHeader(inputLine))
            {
                String[] columns = inputLine.Split(',');
                // We'll always do something based on a split of the line.
                // but exactly what depends on the concrete subclass.
                String key = this._getKeyFromColumns(columns);
                context.EmitKeyValue(key, "1");
            }
        }

        // Override this!
        protected abstract String _getKeyFromColumns(String[] columns);

    }


    public class PostcodeDistrictCountMapper : DemoCountMapper
    {
        protected override String _getKeyFromColumns(String[] columns)
        {
            // Get postcode district - postcode is first column,
            // take everything before space.
            return columns[0].Split(' ')[0];
        }
    }


    public class WardCountMapper : DemoCountMapper
    {
        protected override String _getKeyFromColumns(String[] columns)
        {
            // Get Ward - ninth column, strip double quotes.
            return columns[8].Trim('"');
        }
    }


}
