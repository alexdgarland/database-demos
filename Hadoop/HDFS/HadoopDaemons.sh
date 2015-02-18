#!/bin/bash

# First param - whether to stop or start services - mandatory
action=$1
# Second param - which set of daemons to act on (defaults to all [DFS + MAPRED])
hdpsvc=${2:-all}

if [ $action != "start" ] && [ $action != "stop" ] ; then
    printf "First param value (action) must be \"start\" or \"stop\" - exiting.\n"
exit -1
fi

if [ $hdpsvc = "dfs" ] || [ $hdpsvc = "hdfs" ] || [ $hdpsvc = "all" ] ; then
    echo "*** HDFS - $action ***"
    for x in `cd /etc/init.d ; ls hadoop-hdfs-*` ; do sudo service $x $action ; done
fi

if [ $hdpsvc = "mapred" ] || [ $hdpsvc = "mapreduce" ] || [ $hdpsvc = "all" ] ; then
    echo "*** MapReduce - $action ***"
    for x in `cd /etc/init.d ; ls hadoop-*-mapreduce-*` ; do sudo service $x $action ; done
fi

