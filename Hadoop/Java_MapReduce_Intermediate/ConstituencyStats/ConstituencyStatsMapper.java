package ConstituencyStats;

import java.io.IOException;
import org.apache.hadoop.mapreduce.Mapper;
import org.apache.hadoop.io.Text;
import org.apache.hadoop.io.IntWritable;
import org.apache.hadoop.io.SortedMapWritable;

public class ConstituencyStatsMapper
    extends Mapper<Object, Text, Text, SortedMapWritable>
{

    private static final IntWritable ONE = new IntWritable(1);
    private Text outConstituency = new Text();
    private IntWritable population = new IntWritable();

    @Override
    public void map (Object key, Text value, Context context)
        throws IOException, InterruptedException
    {
        String[] columns = value.toString().split(",");
        
        /* Set key (constituency) */
        outConstituency.set(columns[13].replace("\"", ""));        

        /* Set up value (sorted map of (key) population size per postcode, (value) frequency */
        String populationString = columns[19].replace("\"", "");
        population.set(populationString.equals("") ? 0 : Integer.parseInt(populationString));
        SortedMapWritable outPopulation = new SortedMapWritable();
        outPopulation.put(population, ONE);

        context.write(outConstituency, outPopulation);
    }

}

