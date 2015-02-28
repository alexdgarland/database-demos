package FilterSplitPostcodes;

import org.apache.hadoop.mapreduce.Partitioner;
import org.apache.hadoop.io.Text;

/*
    Partition records so we can use multiple reducers, while still only producing
    a single output file for each Country-Subdivision (County or District) we've chosen to use
    (other than "Missing County" data in England, which makes sense to split).
    This is not really needed but an okay "toy" requirement to try out syntax etc.!
    
    For the postcode file, I've set partitions up statically based on manual analysis
    (in SQL Server) of what gives relatively even distribution.
    For many use-cases, distribution would shift so need to do dynamically and over larger volumes.
    In which case would probably want some MR job or at least counter set up prior,
    generating "good enough" percentile figures in a way that is as cheap as possible.
*/

public class FilterSplitPartitioner
    extends Partitioner<PostcodeFileSplitKeyWritable, Text>
{
    @Override
    public int getPartition(PostcodeFileSplitKeyWritable key, Text value, int numPartitions)
    {
        return partitionPostcodeKey(key) % numPartitions;
    }

    private int partitionPostcodeKey(PostcodeFileSplitKeyWritable key)
    {
        if (!(key.getCountry().equals("England")))
        {
            return 0;   // All areas outside England can go in one partition
        }
        
        // Rest of the method is a finer-grained split for England (>80% of all UK postcodes).

        if (key.getCounty() == "MissingCounty")
        {
            /*
                NB The getter masks empty district strings as "MissingDistrict",
                which will very slightly skew the figures vs the SQL analysis,
                but nowhere near enough to make a meaningful difference.
            */

            // Grab first char, ensure uppercase just for caution
            char firstChar_District = Character.toUpperCase(key.getDistrict().charAt(0));
            
            if (firstChar_District < 'F')
            {
                return 1;
            }
            if (firstChar_District < 'R')
            {
                return 2;
            }
            return 3;
        }

        // Non-missing English counties

        // Grab first char, ensure uppercase just for caution
        char firstChar_County = Character.toUpperCase(key.getCounty().charAt(0));

        if (firstChar_County < 'L')
        {
            return 4;
        }
        return 5;

    }
}

