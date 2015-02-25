package FilterSplitPostcodes;

import java.io.IOException;
import org.apache.hadoop.mapreduce.Mapper;
import org.apache.hadoop.io.Text;


public class FilterSplitMapper
    extends Mapper<Object, Text, PostcodeFileSplitKeyWritable, Text>
{
    private PostcodeFileSplitKeyWritable outKey = new PostcodeFileSplitKeyWritable();

    @Override
    public void map (Object key, Text value, Context context)
        throws IOException, InterruptedException
    {
        if (!_isHeader(value.toString()))
        {
            String[] columns = value.toString().split(",");
            // Filter on terminated date (exclude [do not emit] if populated) 
            String terminatedDate = columns[15];
            if (terminatedDate.equals(""))
            {                
                // Set key
                String country = columns[11];
                String county = columns[6].replace("\"", "");
                String district = columns[7].replace("\"", "");
                outKey.set(country, county, district);
                // Emit key, plus unchanged value
                context.write(outKey, value);
            }
        }
    }

    private static boolean _isHeader(String line)
    {
        return (line.substring(0,8).equals("Postcode"));
    }
}

