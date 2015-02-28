/*
Final stage of static partition analysis for MR reducer split, with results,
having already done some cumulative percentage numbers for individual parts.
*/

WITH CTE_Partition AS
        (
        SELECT  CASE
                    WHEN Country <> 'England' THEN 'Non-English'
                    WHEN County = '' AND LEFT(District, 1) < 'F' THEN 'English Missing County, Districts 1'
                    WHEN County = '' AND LEFT(District, 1) < 'R' THEN 'English Missing County, Districts 2'
                    WHEN County = '' THEN 'English Missing County, Districts 3'
                    WHEN County < 'L' THEN 'English Counties 1'
                    ELSE 'English Counties 2'
                END AS PartitionGroup
    FROM	dbo.Postcodes
    WHERE   Terminated = ''
    )
SELECT	PartitionGroup, COUNT(*) AS PartitionRowCount
FROM    CTE_Partition
GROUP BY PartitionGroup;

/*
PartitionGroup                        PartitionRowCount
English Counties 1                    303359
English Counties 2                    332831
English Missing County, Districts 1   267148
English Missing County, Districts 2   256290
English Missing County, Districts 3   278658
Non-English                           298444
*/
