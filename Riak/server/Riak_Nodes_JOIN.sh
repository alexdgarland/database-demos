#!/bin/bash
RIAKDEVPATH=~/riak-2.0.2/dev/

for n in {2..5};
do
	$RIAKDEVPATH/dev$n/bin/riak-admin cluster join dev1@127.0.0.1;
done

