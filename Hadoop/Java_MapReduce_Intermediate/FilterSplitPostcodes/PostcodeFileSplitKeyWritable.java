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
        if (this._county.length() == 0)
        {
            return "MissingCounty";
        }
        return this._county;
    }

    public String getDistrict()
    {
        if (this._district.length() == 0)
        {
            return "MissingDistrict";
        }
        return this._district;
    }

    public String getSubdivision()
    {
        if (this._country.equals("England"))
        {
            return this.getCounty();
        }
        return this.getDistrict();
    }


    // Implement read-write (serialisation) operations

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
        this._country = in.readLine().replaceAll("\\W", "");
        this._county = in.readLine().replaceAll("\\W", "");
        this._district = in.readLine().replaceAll("\\W", "");
    }

    // Implement comparison operations

    @Override
    public int compareTo(PostcodeFileSplitKeyWritable other)
    {
        // Compare by Country, County, District elements (in that order of precedence).
        int CountryCompareResult = this.getCountry().compareTo(other.getCountry());
        if (CountryCompareResult != 0)
        {
            return CountryCompareResult; 
        }
        int CountyCompareResult = this.getCounty().compareTo(other.getCounty());
        if (CountyCompareResult != 0)
        {
             return CountyCompareResult;
        }
        return this.getDistrict().compareTo(other.getDistrict());
    }

    @Override
    public int hashCode()
    {
        return (this._country + "-" + this._county + "-" + this._district).hashCode();
    }

    @Override
    public String toString()
    {
        return String.format
                (
                "Postcode File Split Key: Country - \"%s\", County - \"%s\", District - \"%s\"",
                this.getCountry(), this.getCounty(), this.getDistrict()
                ); 
   }    
}
