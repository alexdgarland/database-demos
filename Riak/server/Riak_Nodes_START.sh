#!/bin/bash
RIAKDEVPATH=~/riak-2.0.2/dev/

for node in $RIAKDEVPATH*;
do
	echo Starting node $node
	$node/bin/riak start
done
