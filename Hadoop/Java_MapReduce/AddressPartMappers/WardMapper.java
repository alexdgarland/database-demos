package AddressPartMappers;

import java.io.IOException;
import org.apache.hadoop.io.IntWritable;
import org.apache.hadoop.io.Text;
import org.apache.hadoop.mapreduce.Mapper;

public class WardMapper extends Mapper<Object, Text, Text, IntWritable>
{
    private final static IntWritable one = new IntWritable(1);
    private Text ward = new Text();

    public void map (Object key, Text value, Context context) throws IOException, InterruptedException
    {
        String wardColumn = value.toString().split(",")[8];
        ward.set(wardColumn.replace("\"", ""));
        context.write(ward, one);
    }
}

