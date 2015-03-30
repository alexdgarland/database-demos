package ConstituencyStats;

import java.io.IOException;
import org.apache.hadoop.mapreduce.Reducer;
import org.apache.hadoop.io.Text;
import org.apache.hadoop.io.MapWritable;


public class ConstituencyStatsReducer
    extends Reducer<Text, MapWritable, Text, Text>
{

    @Override
    public void reduce(Text key, Iterable<MapWritable> values, Context context)
        throws IOException, InterruptedException
    {

        StatsBuilder builder = new StatsBuilder();
        
        for (MapWritable inMap : values)
        {
            builder.addMap(inMap);
        }
        
        context.write(key, new Text(builder.toString()));        
    }

}

