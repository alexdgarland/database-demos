package ConstituencyStats;

import org.apache.hadoop.io.Writable;
import org.apache.hadoop.io.MapWritable;
import org.apache.hadoop.io.IntWritable;
import java.util.TreeMap;
import java.util.Map.Entry;
import java.lang.Math;


public class StatsBuilder
{
    private int _obsCount = 0;
    private int _total = 0;
    
    public TreeMap<Integer, Integer> populationCounts = new TreeMap<Integer, Integer>();	

    public int getObsCount() { return this._obsCount; }
	public int getTotal()    { return this._total; }
    public float getMean()   { return (float)this._total / this._obsCount; }

    public void reset()
    {
        this._obsCount = 0;
        this._total = 0;
        this.populationCounts.clear();
    }

    public void addMap(MapWritable inMap)
    {        
        for(Entry<Writable, Writable> entry : inMap.entrySet())
        {
            /* Pre-increment totals */
            int population = ((IntWritable)entry.getKey()).get();
            int obsCount = ((IntWritable)entry.getValue()).get();
            this._obsCount += obsCount;
            this._total += (population * obsCount);

            /* Merge keys and values into internal TreeMap */
            Integer storedCount = populationCounts.get(population);
            if (storedCount == null)
            {
                populationCounts.put(population, obsCount);
            }
            else
            {
                populationCounts.put(population, storedCount + obsCount);
            }
         }
    }

    public float getMedian()
    {
        int medianIndex = this._obsCount / 2;
        int previousCumulativeObs = 0;
        int cumulativeObs = 0;
        int prevKey = 0;
        float median = 0;

        for (Entry<Integer, Integer> entry : this.populationCounts.entrySet())
        {
            cumulativeObs = previousCumulativeObs + entry.getValue();
            if (previousCumulativeObs <= medianIndex && medianIndex < cumulativeObs)
            {
                if (this._obsCount % 2 == 0 && previousCumulativeObs == medianIndex)
                {
                    median = (float)(entry.getKey() + prevKey) / 2.0f;
                }
                else
                {
                    median = (float)(entry.getKey());
                }
                break;
            }
            previousCumulativeObs = cumulativeObs;
            prevKey = entry.getKey();
        }
        return median;
    }
    
    public float getStdDev()
    {
        float mean = this.getMean();
        float sumOfSquares = 0.0f;

        for (Entry<Integer, Integer> entry : this.populationCounts.entrySet())
        {
            float delta = entry.getKey() - mean;
            sumOfSquares += (delta * delta * entry.getValue());
        }
        return (float)Math.sqrt(sumOfSquares / (this._obsCount));
    }

    
	@Override
	public String toString()
    {
		return this.getObsCount()   + "\t"
                + this.getTotal()   + "\t"
                + this.getMean()    + "\t"
                + this.getMedian()  + "\t"
                + this.getStdDev();
	}

}

