#!/bin/bash
RIAKDEVPATH=~/riak-1.4.1/dev/

for n in {2..5};
do
	$RIAKDEVPATH/dev$n/bin/riak-admin cluster join dev1@127.0.0.1;
done

$RIAKDEVPATH/dev1/bin/riak-admin cluster plan
$RIAKDEVPATH/dev1/bin/riak-admin cluster commit

printf "\n\nCHECKING STATUS:\n\n"

$RIAKDEVPATH/dev1/bin/riak-admin member-status

