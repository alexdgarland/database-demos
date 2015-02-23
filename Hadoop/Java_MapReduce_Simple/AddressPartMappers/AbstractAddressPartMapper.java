package AddressPartMappers;

import java.io.IOException;
import org.apache.hadoop.io.IntWritable;
import org.apache.hadoop.io.Text;
import org.apache.hadoop.mapreduce.Mapper;

abstract class AbstractAddressPartMapper extends Mapper<Object, Text, Text, IntWritable>
{
    private final static IntWritable one = new IntWritable(1);
    private Text key = new Text();

    @Override
    public void map (Object key, Text value, Context context) throws IOException, InterruptedException
    {
        String line = value.toString();
        if (!this._isHeader(line))
        {
            String[] columns = line.split(",");
            this.key.set(this._getKeyFromColumns(columns));
            context.write(this.key, one);
        }
    }

    private static boolean _isHeader(String line)
    {
        return (line.substring(0,8).equals("Postcode"));
    }

    // Concrete AddressPartMapper classes need to implement this method
    abstract String _getKeyFromColumns(String[] columns);
}

