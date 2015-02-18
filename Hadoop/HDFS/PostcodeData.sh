#!/bin/bash

filename=postcodes.csv
dfsdatapath=/user/$(whoami)/input/$filename

if [ $1 = "del" ] || [ $1 = "delete" ] ; then

    echo "*** Deleting postcode data from HDFS ***"
    hdfs dfs -rm $dfsdatapath

elif [ $1 = "put" ] ; then

    echo "*** Copying postcode data to HDFS from local filesystem ***"
    localdatadir=`dirname $0`/../data;
    hdfs dfs -put $localdatadir/$filename $dfsdatapath

elif [ $1 = "sample" ] ; then

    echo "*** Displaying sample of input data in HDFS: ***"
    hadoop fs -cat $dfsdatapath | head

fi
else

    printf "ERROR: Argument missing or not recognised (should be \"del\"/\"delete\", \"put\" or \"sample\").\n"

fi

