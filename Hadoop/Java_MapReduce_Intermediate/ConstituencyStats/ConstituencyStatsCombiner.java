package ConstituencyStats;

import java.io.IOException;
import org.apache.hadoop.mapreduce.Reducer;
import org.apache.hadoop.io.Writable;
import org.apache.hadoop.io.WritableComparable;
import org.apache.hadoop.io.Text;
import org.apache.hadoop.io.IntWritable;
import org.apache.hadoop.io.SortedMapWritable;
import java.util.Map.Entry;


public class ConstituencyStatsCombiner
    extends Reducer<Text, SortedMapWritable, Text, SortedMapWritable>
{

    @Override
    public void reduce (Text key, Iterable<SortedMapWritable> values, Context context)
        throws IOException, InterruptedException
    {
        SortedMapWritable combinedMap = new SortedMapWritable();
        
        for (SortedMapWritable v : values)
        {
            for (Entry<WritableComparable, Writable> entry : v.entrySet())
            {
                IntWritable count = (IntWritable)combinedMap.get(entry.getKey());
                if (count != null)
                {
                    count.set(count.get() + ((IntWritable) entry.getValue()).get());
                }
                else
                {
                    combinedMap.put
                        (
                        entry.getKey(),
                        new IntWritable(((IntWritable)entry.getValue()).get())
                        );
                }
            }
        }
        
        context.write(key, combinedMap);

    }

}

