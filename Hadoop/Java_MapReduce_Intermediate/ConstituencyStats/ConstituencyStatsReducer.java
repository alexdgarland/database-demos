package ConstituencyStats;

import java.io.IOException;
import org.apache.hadoop.mapreduce.Reducer;
import org.apache.hadoop.io.Writable;
import org.apache.hadoop.io.WritableComparable;
import org.apache.hadoop.io.Text;
import org.apache.hadoop.io.IntWritable;
import org.apache.hadoop.io.SortedMapWritable;
import org.apache.hadoop.io.Text;
import java.util.TreeMap;
import java.util.Map.Entry;


public class ConstituencyStatsReducer
    extends Reducer<Text, SortedMapWritable, Text, PopulationStatsTuple>
{

    @Override
    public void reduce(Text key, Iterable<SortedMapWritable> values, Context context)
        throws IOException, InterruptedException
    {

        PopulationStatsTuple statistics = new PopulationStatsTuple();
        TreeMap<Integer, Integer> populationCounts = new TreeMap<Integer, Integer>();

        /*
        Build full TreeMap by combining each incoming (partially pre-combined) sorted map.
        Also get total observation count and overall population as these are
        straightforward to calculate as we iterate through the incoming maps.
        */
        
        populationCounts.clear();
        statistics.reset();
        
        for (SortedMapWritable v : values)
        {
            for(Entry<WritableComparable, Writable> entry : v.entrySet())
            {
                int population = ((IntWritable)entry.getKey()).get();
                int obsCount = ((IntWritable)entry.getValue()).get();
                
                statistics.incrementTotals(population, obsCount);

                Integer storedCount = populationCounts.get(population); 
                if (storedCount == null)
                {
                    populationCounts.put(population, obsCount);
                }
                else
                {
                    populationCounts.put(population, storedCount + obsCount);
                }
            }
        }
        
        /*
        Iterate through full TreeMap and calculate median and standard deviation.
        For the moment at least have moved these methods into the stats object itself.
        */

        statistics.setMedian(populationCounts);
        statistics.setStdDev(populationCounts);
        
        context.write(key, statistics);
        
    }



}

