#!/bin/bash

outdir=/user/$(whoami)/output/postaldistrict

echo "*** Deleting output directory (if exists) ***"
hadoop fs -rm -r $outdir
echo "*** Running MapReduce job ***"
hadoop jar $(dirname "${BASH_SOURCE[0]}")/PostalDistrictCount.jar PostalDistrictCount /user/$(whoami)/ $outdir

