package AddressPartMappers;

import java.io.IOException;
import org.apache.hadoop.io.IntWritable;
import org.apache.hadoop.io.Text;
import org.apache.hadoop.mapreduce.Mapper;

public class PostalDistrictMapper extends Mapper<Object, Text, Text, IntWritable>
{
    private final static IntWritable one = new IntWritable(1);
    private Text postalDistrict = new Text();

    public void map (Object key, Text value, Context context) throws IOException, InterruptedException
    {
        String postcodeColumn = value.toString().split(",")[0];
        postalDistrict.set(postcodeColumn.split(" ")[0]);
        context.write(postalDistrict, one);
    }
}

