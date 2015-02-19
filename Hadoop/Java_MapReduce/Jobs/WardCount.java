package Jobs;

import AddressPartMappers.WardMapper;

public class WardCount
{
    public static void main(String[] args) throws Exception
    {
        AddressPartCountJob.Run
            (
            WardCount.class,
            WardMapper.class,
            "Ward count",
            args
            );
    }
}

