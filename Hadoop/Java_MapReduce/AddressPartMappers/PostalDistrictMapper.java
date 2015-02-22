package AddressPartMappers;

public class PostalDistrictMapper extends AbstractAddressPartMapper
{
    @Override
    String _getKeyFromColumns(String[] columns)
    {
        // Get postal district from first column (Postcode)
        return columns[0].split(" ")[0];
    }
}

