package FilterSplitPostcodes;

import java.io.IOException;
import org.apache.hadoop.io.WritableComparable;
import java.io.DataOutput;
import java.io.DataInput;


public class PostcodeFileSplitKeyWritable
    implements WritableComparable<PostcodeFileSplitKeyWritable>
{
    private String _country;
    private String _county;
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
        // Compare by Country, County, District elements (in that order of precedence).

        int CountryCompareResult = this._country.compareTo(other.getCountry());
        if (CountryCompareResult != 0)
        {
            return CountryCompareResult; 
        }

        int CountyCompareResult = this._county.compareTo(other.getCounty());
        if (CountyCompareResult != 0)
        {
             return CountyCompareResult;
        }

        return this._district.compareTo(other.getDistrict());

    }

    @Override
    public int hashCode()
    {
        return (this._country + "-" + this._county + "-" + this._district).hashCode();
    }
    
}
