package FilterSplitPostcodes;

import java.io.IOException;
import org.apache.hadoop.io.WritableComparable;
import java.io.DataOutput;
import java.io.DataInput;


public class PostcodeFileSplitKeyWritable
    implements WritableComparable<PostcodeFileSplitKeyWritable>
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
        out.writeChars(this._country + '\n');
        out.writeChars(this._county + '\n');
        out.writeChars(this._district + '\n');
    }

    public void readFields(DataInput in)
        throws IOException
    {
        this._country = in.readLine();
        this._county = in.readLine();
        this._district = in.readLine();
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
