#!/bin/bash
RIAKDEVPATH=~/riak-1.4.1/dev/

for node in $RIAKDEVPATH*;
do
	echo Stopping node $node
	$node/bin/riak stop
done
