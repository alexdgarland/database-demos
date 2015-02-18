#!/bin/bash

outdir=/user/$(whoami)/output/ward

echo "*** Deleting output directory (if exists) ***"
hadoop fs -rm -r $outdir
echo "*** Running MapReduce job ***"
hadoop jar $(dirname "${BASH_SOURCE[0]}")/WardCount.jar WardCount /user/$(whoami)/ $outdir

