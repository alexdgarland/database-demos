#!/bin/bash
RIAKDEVPATH=~/riak-2.0.2/dev/

for node in $RIAKDEVPATH*;
do
	echo Stopping node $node
	$node/bin/riak stop
done
