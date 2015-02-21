#!/bin/bash

function HandleAction
{

    local action=$1
    local callingscriptdir=$2
    local mainclass=$3
    local dfsoutfolder=$4

    local dfsoutpath=/user/$(whoami)/output/$dfsoutfolder

    if [ $action = "run" ] ; then

        dfsinpath=/user/$(whoami)/input
        
        echo "*** Deleting output directory (if exists) ***"
        hadoop fs -rm -r $dfsoutpath/
        
        echo "*** Running MapReduce job \"$mainclass\" ***"
        hadoop jar $callingscriptdir/AddressPartCounts.jar Jobs.$mainclass $dfsinpath/ $dfsoutpath/

    elif [ $action = "view" ] ; then

        hadoop fs -cat $dfsoutpath/part-r-00000

    else

        printf "ERROR: Parameter missing or not recognised (should be \"run\" or \"view\").\n"

    fi

}

