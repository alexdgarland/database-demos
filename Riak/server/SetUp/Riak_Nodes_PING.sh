#!/bin/bash
RIAKDEVPATH=~/riak-1.4.1/dev/

for node in $RIAKDEVPATH*;
do
	echo Pinging node $node
	$node/bin/riak ping
done

