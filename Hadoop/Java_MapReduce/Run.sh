#!/bin/bash

echo "*** Deleting output directory (if exists) ***"
hadoop fs -rm -r /user/$(whoami)/output

echo "*** Running MapReduce job ***"
hadoop jar $(dirname "${BASH_SOURCE[0]}")/AddressCounts.jar AddressCounts /user/$(whoami)/ /user/$(whoami)/output/

