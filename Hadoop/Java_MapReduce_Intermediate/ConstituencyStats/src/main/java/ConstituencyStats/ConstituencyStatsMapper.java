package ConstituencyStats;

import java.io.IOException;
import org.apache.hadoop.mapreduce.Mapper;
import org.apache.hadoop.io.Text;
import org.apache.hadoop.io.IntWritable;
import org.apache.hadoop.io.MapWritable;

public class ConstituencyStatsMapper
    extends Mapper<Object, Text, Text, MapWritable>
{

    private static final IntWritable ONE = new IntWritable(1);
 
    private Text outConstituency = new Text();
    private IntWritable population = new IntWritable();

    private MapWritable outMap;

    @Override
    public void map (Object key, Text value, Context context)
        throws IOException, InterruptedException
    {
        String[] columns = value.toString().split(",");
        
        /* Set key (constituency) */
        outConstituency.set(columns[13].replace("\"", ""));

        String populationString = columns[18];
        population.set(populationString.equals("") ? 0 : Integer.parseInt(populationString));

        outMap = new MapWritable();
        outMap.put(population, ONE);

        context.write(outConstituency, outMap);
    }

}

