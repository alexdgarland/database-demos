package FilterSplitPostcodes;

import java.io.IOException;
import org.apache.hadoop.mapreduce.Reducer;
import org.apache.hadoop.mapreduce.lib.output.MultipleOutputs;
import org.apache.hadoop.io.Text;
import org.apache.hadoop.io.NullWritable;


public class FilterSplitReducer
    extends Reducer<PostcodeFileSplitKeyWritable, Text, NullWritable, Text>
{
    private MultipleOutputs mos;
    private NullWritable nullkey; 

    @Override        
    public void setup(Context context)
    {
        mos = new MultipleOutputs(context);
    }

    @Override
    public void reduce(PostcodeFileSplitKeyWritable key, Iterable<Text> values, Context context)
        throws IOException, InterruptedException
    {
        for (Text value : values)
        {
            mos.write(nullkey, value, generateFileName(key));
        }

    }

    private static String generateFileName(PostcodeFileSplitKeyWritable key)
    {
        String missingCountyDefault = key.getCountry().equals("England") ? "MissingCounty" : key.getDistrict();
        return key.getCountry() + "-" + (key.getCounty().length() == 0 ? missingCountyDefault : key.getCounty());  
    }

}

