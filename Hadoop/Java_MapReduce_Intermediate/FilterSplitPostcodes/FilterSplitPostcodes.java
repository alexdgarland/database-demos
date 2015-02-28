
/* **** WORK IN PROGRESS **** */ 
// TO DO:
// - Implement custom partitioner to split while maintaining counties as single files.
// - Implement additional MR job which uses pre-partitioned data
//      to efficiently run stats for one geo area (Yorkshire?) only.
// - Link it all together and prepare to demo!

// Also:
//  - Unit Test (especially given that country/ county creates easy source of error)
//  - Use better IDE/ build tool!


package FilterSplitPostcodes;

import java.io.IOException;
import org.apache.hadoop.conf.Configuration;
import org.apache.hadoop.fs.Path;
import org.apache.hadoop.io.Text;
import org.apache.hadoop.mapreduce.Job;
import org.apache.hadoop.mapreduce.lib.input.FileInputFormat;
import org.apache.hadoop.mapreduce.lib.output.FileOutputFormat;
import org.apache.hadoop.io.NullWritable;


public class FilterSplitPostcodes
{

    public static void main (String[] args)
        throws IOException, InterruptedException, ClassNotFoundException
    {
        Configuration conf = new Configuration();
        Job job = Job.getInstance(conf, "Filter and split postcodes");

        job.setJarByClass(FilterSplitPostcodes.class);

        job.setMapperClass(FilterSplitMapper.class);
        job.setPartitionerClass(FilterSplitPartitioner.class);
        job.setReducerClass(FilterSplitReducer.class);
        job.setNumReduceTasks(3);
        job.setOutputKeyClass(NullWritable.class);
        job.setMapOutputKeyClass(PostcodeFileSplitKeyWritable.class);
        job.setOutputValueClass(Text.class);

        FileInputFormat.addInputPath(job, new Path(args[0]));
        FileOutputFormat.setOutputPath(job, new Path(args[1]));

        System.exit(job.waitForCompletion(true) ? 0 : 1);
    }

}

