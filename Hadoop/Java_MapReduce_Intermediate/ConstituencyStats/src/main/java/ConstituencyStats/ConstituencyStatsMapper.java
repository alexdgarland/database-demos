package ConstituencyStats;

import java.io.IOException;
import org.apache.hadoop.mapreduce.Mapper;
import org.apache.hadoop.io.Text;
import org.apache.hadoop.io.IntWritable;
import org.apache.hadoop.io.MapWritable;
import java.util.List;
import java.util.ArrayList;


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
        List<String> columns = customSplit(value.toString());
        
        /* Set key (constituency) */
        outConstituency.set(columns.get(13).replace("\"", ""));

        String populationString = columns.get(18);
        population.set(populationString.replace("\"", "").equals("") ? 0 : Integer.parseInt(populationString));

        outMap = new MapWritable();
        outMap.put(population, ONE);

        context.write(outConstituency, outMap);
    }


    /*
    Function to handle commas within text-qualified CSV line input - hacked in from http://stackoverflow.com/a/3178092
    Possibly want to do something better (e.g. custom Input Format for CSV) but this will do for the moment.
    */
    private static List<String> customSplit(String input)
    {
        List<String> elements = new ArrayList<String>();       
        StringBuilder elementBuilder = new StringBuilder();

        boolean isQuoted = false;
        for (char c : input.toCharArray())
        {
            if (c == '\"')
            {
                isQuoted = !isQuoted;
            }
            if (c == ',' && !isQuoted)
            {
                elements.add(elementBuilder.toString().trim());
                elementBuilder = new StringBuilder();
                continue;
            }
            elementBuilder.append(c); 
        }
        elements.add(elementBuilder.toString().trim()); 
        return elements;
    }
}

