package ConstituencyStats;

import java.io.IOException;
import org.apache.hadoop.io.Writable;
import java.io.DataOutput;
import java.io.DataInput;
import java.util.TreeMap;
import java.util.Map.Entry;
import java.lang.Math;


public class PopulationStatsTuple implements Writable
{

    private int _obsCount = 0;
    private int _total = 0;
	private float _median = 0f;
	private float _stddev = 0f;

    /* Basic getters and setters */
	public int getObsCount()                { return this._obsCount; }
	public void setObsCount(int obsCount)   { this._obsCount = obsCount; }
	public int getTotal()                   { return this._total; }
	public void setTotal(int total)         { this._total = total; }
	public float getMedian()                { return this._median; }
	public float getStdDev()                { return this._stddev; }

    /* More complex data access methods */

    public float getMean()
    {
        return (float)this._total / this._obsCount;
    }

    public void reset()
    {
        this._obsCount = 0;
        this._total = 0;
        this._median = 0f;
        this._stddev = 0f;
    }

    public void incrementTotals(int value, int obsCount)
    {
        this._obsCount += obsCount;
        this._total += (value * obsCount);
    }


    public void setMedian(TreeMap<Integer, Integer> valueCounts)
    {
        int medianIndex = this._obsCount / 2;
        int previousCumulativeObs = 0;
        int cumulativeObs = 0;
        int prevKey = 0;

        for (Entry<Integer, Integer> entry : valueCounts.entrySet())
        {
            cumulativeObs = previousCumulativeObs + entry.getValue();

            if (previousCumulativeObs <= medianIndex && medianIndex < cumulativeObs)
            {
                if (this._obsCount % 2 == 0 && previousCumulativeObs == medianIndex)
                {
                    this._median = (float)(entry.getKey() + prevKey) / 2.0f;
                }
                else
                {
                    this._median = (float)(entry.getKey());
                }
                break;
            }

            previousCumulativeObs = cumulativeObs;
            prevKey = entry.getKey();
        }
    }
    

    public void setStdDev(TreeMap<Integer, Integer> valueCounts)
    {
        float mean = this.getMean();
        float sumOfSquares = 0.0f;

        for (Entry<Integer, Integer> entry : valueCounts.entrySet())
        {
            float delta = entry.getKey() - mean;
            sumOfSquares += (delta * delta * entry.getValue());
        }
        this._stddev = (float)Math.sqrt(sumOfSquares / (this._obsCount - 1));
        
    }


    /* Serialisation */
	@Override
	public void readFields(DataInput in) throws IOException
    {
	    this._obsCount = in.readInt();
        this._total = in.readInt();	
        this._median = in.readFloat();
		this._stddev = in.readFloat();
	}

	@Override
	public void write(DataOutput out) throws IOException
    {
		out.writeInt(this._obsCount);
        out.writeInt(this._total);
        out.writeFloat(this._median);
		out.writeFloat(this._stddev);
	}

    
	@Override
	public String toString()
    {
		return  this._obsCount + "\t"
                + this._total + "\t"
                + this.getMean() + "\t"                
                + this._median + "\t"
                + this._stddev;
	}

}

