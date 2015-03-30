package ConstituencyStats;

import java.io.IOException;
import org.apache.hadoop.mapreduce.Reducer;
import org.apache.hadoop.io.Writable;
import org.apache.hadoop.io.Text;
import org.apache.hadoop.io.IntWritable;
import org.apache.hadoop.io.MapWritable;
import java.util.Map.Entry;


public class ConstituencyStatsCombiner
    extends Reducer<Text, MapWritable, Text, MapWritable>
{

    @Override
    public void reduce (Text key, Iterable<MapWritable> values, Context context)
        throws IOException, InterruptedException
    {
        MapWritable combinedMap = new MapWritable();
        
        for (MapWritable inMap : values)
        {
            for (Entry<Writable, Writable> entry : inMap.entrySet())
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

