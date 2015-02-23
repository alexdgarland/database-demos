package FilterSplitPostcodes;

import java.io.IOException;

// Generic MapReduce stuff
import org.apache.hadoop.conf.Configuration;
import org.apache.hadoop.fs.Path;
import org.apache.hadoop.io.IntWritable;
import org.apache.hadoop.io.Text;
import org.apache.hadoop.mapreduce.Job;
import org.apache.hadoop.mapreduce.lib.input.FileInputFormat;
import org.apache.hadoop.mapreduce.lib.output.FileOutputFormat;
import org.apache.hadoop.mapreduce.Mapper;

public class FilterSplitPostcodes
{

    public static void Main (String[] args)
        throws IOException, InterruptedException
    {
        Configuration conf = new Configuration();
        Job job = Job.getInstance(conf, JobName);

        job.setJarByClass(FilterSplitPostcodes.class);
        job.setMapperClass(FilterSplitMapper.class);

        /* **** WORK IN PROGRESS **** */ 
        // TO DO:
        // - Finish Mapper and custom Writable (key) - see below
        // - Implement customer partitioner to split while maintaining counties as single files.
        // - Implement reducer - writing out to separate filenames per Country-County using MultipleOutputs.
        // - Complete Main method to call all the above correctly.
        // - Implement additional MR job which uses pre-partitioned data
        //      to efficiently run stats for one geo area (Yorkshire?) only.
        // - Link it all together and prepare to demo!

        // Also:
        //  - Unit Test (especially given that country/ county created easy source of error)
        //  - Use better IDE/ build tool!

        // job.setReducerClass(FilterSplitReducer.class);
        // job.setOutputKeyClass(Text.class);
        // job.setOutputValueClass(IntWritable.class);

        FileInputFormat.addInputPath(job, new Path(args[0]));
        FileOutputFormat.setOutputPath(job, new Path(args[1]));

        System.exit(job.waitForCompletion(true) ? 0 : 1);
    }

    private class FilterSplitMapper
        extends Mapper<Object, Text, Text, Text>
    {
        PostcodeFileSplitKeyWritable key = new PostcodeFileSplitKeyWritable();

        @Override
        public void map (Object key, Text value, Context context)
            throws IOException, InterruptedException
        {
            String[] columns = value.split(",");
            
            // Filter on terminated date (exclude [do not emit] if populated) 
            String terminatedDate = columns[15];
            if (terminatedDate.equals('')
            {
                // Set key
                String country = columns[11];
                String county = columns[6];
                String district = columns[7];
                key.set(country, county, district);
                
                // Emit key, plus unchanged value
                context.emit(key, value);
            }
        }

        private static boolean _isHeader(String line)
        {
            return (line.substring(0,8).equals("Postcode"));
        }

    }

    private class PostcodeFileSplitKeyWritable
        implements WritableComparable
    {
        // These two fields are the actual "key", i.e. used for comparison operations
        private String _country;
        private String _county;
        // We include district as I potentially want to use this in customer partitioner
        // and is easier to have as a named field here than split out columns again later.
        private String _district;

        // Access methods
        public void set(String Country, String County, String District)
        {
            this._country = Country;
            this._county = County;
            this._district = District;
        }

        public String getCountry()
        {
            return this._country;
        }

        public String getCounty()
        {
            return this._county;
        }

        public String getDistrict()
        {
            return this._district;
        }

        // Implement read-write operations

        public void write(DataOutput out)
            throws IOException
        {
            out.writeChars(this._country);
            out.writeChars(this._county);
            out.writeChars(this._district);
        }

        public void readFields(DataInput in)
            throws IOException
        {
            this._country = in.readChars();
            this._county = in.readChars();
            this._district = in.readChars();
        }

        // TO DO: Implement comparison operations

    }


}

