package Jobs;

import AddressPartMappers.PostalDistrictMapper;

public class PostalDistrictCount
{
    public static void main(String[] args) throws Exception
    {
        AddressPartCountJob.Run
            (
            PostalDistrictCount.class,
            PostalDistrictMapper.class,
            "Postal district count",
            args
            );
    }
}
