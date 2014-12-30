#!/bin/bash
RIAKDEVPATH=~/riak-2.0.2/dev/

for node in $RIAKDEVPATH*;
do
	echo Pinging node $node
	$node/bin/riak ping
done

