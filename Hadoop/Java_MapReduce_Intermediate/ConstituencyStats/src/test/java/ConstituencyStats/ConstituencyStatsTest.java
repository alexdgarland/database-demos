package ConstituencyStats;


import org.apache.hadoop.io.IntWritable;
import org.apache.hadoop.io.Text;
import org.apache.hadoop.io.MapWritable;
import org.apache.hadoop.mrunit.mapreduce.MapDriver;
import org.apache.hadoop.mrunit.mapreduce.MapReduceDriver;
import org.apache.hadoop.mrunit.mapreduce.ReduceDriver;
import org.junit.Before;
import org.junit.Test;
import org.junit.Assert;
import org.apache.commons.lang3.StringUtils;

import java.util.ArrayList;
import java.util.List;

import java.io.IOException;


public class ConstituencyStatsTest
{
    
    MapReduceDriver<Object, Text, Text, MapWritable, Text, Text> mapReduceDriver;
    MapDriver<Object, Text, Text, MapWritable> mapDriver;
    ReduceDriver<Text, MapWritable, Text, MapWritable> combineDriver; 
    ReduceDriver<Text, MapWritable, Text, Text> reduceDriver;
    String key = "Aberdeen South";

    @Before
    public void setUp()
    {
        ConstituencyStatsMapper mapper = new ConstituencyStatsMapper();
        ConstituencyStatsCombiner combiner = new ConstituencyStatsCombiner();
        ConstituencyStatsReducer reducer = new ConstituencyStatsReducer();

        mapDriver = MapDriver.newMapDriver(mapper);
        combineDriver = ReduceDriver.newReduceDriver(combiner);
        reduceDriver = ReduceDriver.newReduceDriver(reducer);

        mapReduceDriver = MapReduceDriver.newMapReduceDriver(mapper, reducer).withCombiner(combiner);
    }

    private Text getMapInputRow(int population)
    {
        return new Text
                    (
                    StringUtils.repeat("\"\",", 13)
                    + "\"" + key + "\","
                    + StringUtils.repeat("\"\",", 4)
                    + Integer.toString(population) + ","
                    + StringUtils.repeat("\"\",", 6)
                    );
    }

    @Test
    public void testMapper()
        throws IOException
    {
        Text dummyKey = new Text("Not a real key!");
        mapDriver.withInput(dummyKey, getMapInputRow(2));
        MapWritable expectedMap = new MapWritable();
        expectedMap.put(new IntWritable(2), new IntWritable(1));        
        mapDriver.withOutput(new Text(key), expectedMap);
        mapDriver.runTest();
    }

    private MapWritable getSingleCountMapFromInts(int[] values)
    {
        MapWritable map = new MapWritable();
        for (int val : values) { map.put(new IntWritable(val), new IntWritable(1)); }
        return map;
    }

    @Test
    public void testCombiner()
        throws IOException
    {
        List<MapWritable> values = new ArrayList<MapWritable>();
        values.add(getSingleCountMapFromInts(new int[]{1, 2, 3}));
        values.add(getSingleCountMapFromInts(new int[]{2, 3, 4}));

        MapWritable expectedOutMap = new MapWritable();
        expectedOutMap.put(new IntWritable(1), new IntWritable(1));        
        expectedOutMap.put(new IntWritable(2), new IntWritable(2));
        expectedOutMap.put(new IntWritable(3), new IntWritable(2));
        expectedOutMap.put(new IntWritable(4), new IntWritable(1));
        
        combineDriver.withInput(new Text(key), values);
        combineDriver.withOutput(new Text(key), expectedOutMap);
        combineDriver.runTest();
    }

    @Test
    public void testReducer()
        throws IOException
    {
        List<MapWritable> values = new ArrayList<MapWritable>();
        values.add(getSingleCountMapFromInts(new int[]{1, 2, 5}));
        values.add(getSingleCountMapFromInts(new int[]{3, 4, 5}));
        
        reduceDriver.withInput(new Text(key), values);
        reduceDriver.withOutput(new Text(key), new Text("6\t20\t3.3333333\t3.5\t1.490712"));
        reduceDriver.runTest();
   }

    @Test
    public void testMapReduce()
        throws IOException
    {
        Text dummyKey = new Text("Not a real key!");
        mapReduceDriver.addInput(dummyKey, getMapInputRow(1));
        mapReduceDriver.addInput(dummyKey, getMapInputRow(2));
        mapReduceDriver.addInput(dummyKey, getMapInputRow(3));
        mapReduceDriver.addInput(dummyKey, getMapInputRow(4));
        mapReduceDriver.addInput(dummyKey, getMapInputRow(5));
        mapReduceDriver.addInput(dummyKey, getMapInputRow(5));
        mapReduceDriver.withOutput(new Text(key), new Text("6\t20\t3.3333333\t3.5\t1.490712"));
        mapReduceDriver.runTest();
    }

    @Test
    public void testStatsBuilder()
    {
        StatsBuilder builder = new StatsBuilder();
        builder.addMap(getSingleCountMapFromInts(new int[]{1, 2, 5}));
        builder.addMap(getSingleCountMapFromInts(new int[]{3, 4, 5}));
        
        Assert.assertEquals(6, builder.getObsCount());
        Assert.assertEquals(20, builder.getTotal());
        Assert.assertEquals(3.333, builder.getMean(), 0.001);
        Assert.assertEquals(3.5, builder.getMedian(), 0.01);
        Assert.assertEquals(1.49, builder.getStdDev(), 0.001);
        Assert.assertEquals("6\t20\t3.3333333\t3.5\t1.490712", builder.toString());
    }

}

