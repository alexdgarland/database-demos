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
        // - Implement custom partitioner to split while maintaining counties as single files.
        // - Implement body of reducer - writing out to separate filenames per Country-County using MultipleOutputs.
        // - Complete Main method to call all the above correctly.
        // - Implement additional MR job which uses pre-partitioned data
        //      to efficiently run stats for one geo area (Yorkshire?) only.
        // - Link it all together and prepare to demo!

        // Also:
        //  - Unit Test (especially given that country/ county creates easy source of error)
        //  - Use better IDE/ build tool!

        // job.setReducerClass(FilterSplitReducer.class);
        // job.setOutputKeyClass(Text.class);
        // job.setOutputValueClass(IntWritable.class);

        FileInputFormat.addInputPath(job, new Path(args[0]));
        FileOutputFormat.setOutputPath(job, new Path(args[1]));

        System.exit(job.waitForCompletion(true) ? 0 : 1);
    }


    private class FilterSplitMapper
        extends Mapper<Object, Text, PostcodeFileSplitKeyWritable, Text>
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


    private class FilterSplitReducer
        extends Reducer<PostcodeFileSplitKeyWritable, Text, NullWritable, Text>
    {
        @Override
        public void reduce(PostcodeFileSplitKeyWritable key, Iterable<Text> values, Context context)
            throws IOException, InterruptedException
        {
            // TO DO : Implement body; write text values out unaltered
            //          *** BUT *** split out to different files based on Country-County.
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
        // We include district as I potentially want to use this in custom partitioner
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

        // Implement comparison operations

        @Override
        public int compareTo(PostcodeFileSplitKeyWritable other)
        {
            // Compare by Country and County elements, with Country taking precedence.
            int CountryCompareResult = this._country.compareTo(other.getCountry());
            if (CountryCompareResult == 0)
            {
                return this._county.compareTo(other.getCounty());
            }
            else
            {
                return CountryCompareResult; 
            }
        }

        @Override
        public int hashCode()
        {
            return (this._country + this._county).hashCode();
        }
        
    }


}

