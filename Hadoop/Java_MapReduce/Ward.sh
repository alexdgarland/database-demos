#!/bin/bash

action=$1
scriptdir=$(dirname "${BASH_SOURCE[0]}")

if [ $action = "run" ] ; then

    indir=/user/$(whoami)/input/
    outdir=/user/$(whoami)/output/ward/

    echo "*** Deleting output directory (if exists) ***"
    hadoop fs -rm -r $outdir
    echo "*** Running MapReduce job ***"
    hadoop jar $scriptdir/AddressPartCounts.jar Jobs.WardCount $indir $outdir

elif [ $action = "view" ] ; then

    hadoop fs -cat /user/$(whoami)/output/ward/part-r-00000

else

    printf "Parameter missing or not recognised (should be \"run\" or \"view\"\n"

fi

