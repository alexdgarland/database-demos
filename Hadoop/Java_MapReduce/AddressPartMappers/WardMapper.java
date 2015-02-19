package AddressPartMappers;

public class WardMapper extends AbstractAddressPartMapper
{
    String _getKeyFromColumns(String[] columns)
    {
        // Get ward from ninth column and strip quotes
        return columns[8].replace("\"", "");
    }
}

