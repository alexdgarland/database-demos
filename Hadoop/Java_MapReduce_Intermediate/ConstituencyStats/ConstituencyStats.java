package ConstituencyStats;

import java.io.IOException;
import org.apache.hadoop.conf.Configuration;
import org.apache.hadoop.fs.Path;
import org.apache.hadoop.io.Text;
import org.apache.hadoop.io.SortedMapWritable;
import org.apache.hadoop.mapreduce.Job;
import org.apache.hadoop.mapreduce.lib.input.FileInputFormat;
import org.apache.hadoop.mapreduce.lib.output.FileOutputFormat;


public class ConstituencyStats
{

    public static void main (String[] args)
        throws IOException, InterruptedException, ClassNotFoundException
    {
        Configuration conf = new Configuration();
        Job job = Job.getInstance(conf, "Calculate summary stats");

        job.setJarByClass(ConstituencyStats.class);

        job.setMapperClass(ConstituencyStatsMapper.class);
        job.setCombinerClass(ConstituencyStatsCombiner.class);
        job.setReducerClass(ConstituencyStatsReducer.class);
        job.setOutputKeyClass(Text.class);
        job.setOutputValueClass(SortedMapWritable.class);

        FileInputFormat.addInputPath(job, new Path(args[0]));
        FileOutputFormat.setOutputPath(job, new Path(args[1]));

        System.exit(job.waitForCompletion(true) ? 0 : 1);
    }

}

