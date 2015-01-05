*** Riak Demo Environment Set-Up ***
To be documented in greater detail (and possibly scripted) as and when I have time

ON LINUX VM (SERVER):

NB: For steps 1-6, see here - http://docs.basho.com/riak/latest/quickstart/ -
and further links provided from that page.

ACTUALLY - this should be Riak 1.4 (at latest) to use with CorrugatedIron.  So link is:
http://docs.basho.com/riak/1.4.1/quickstart/

1) Install all prerequisites for Erlang and Riak via Yum - [scripted here](/Riak/server/SetUp/Get_Riak_Erlang_Dependencies.sh).  Note that at this stage the installing user needs to have high sudo permissions - this is probably best set up manually as "step 0".

2) Install and build Erlang.
	If on Centos, may need to add a flag setting to ~/.kerlrc (export CFLAGS="-DOPENSSL_NO_EC=1")
	- see https://github.com/taylor/kiex/issues/7
	This is documented as part of the Riak instructions
	(http://docs.basho.com/riak/latest/ops/building/installing/erlang/)
	but I missed it on first read-through...

3) Install Riak.

4) Install Riak dev cluster.

5) Increase upper limit for file handles for user who will run nodes (in config file - check which).

6) Start all nodes (script is provided in GitHub repo under Riak/server/SetUp/).

7) Join all nodes to cluster (script is provided in GitHub repo under Riak/server/SetUp/).

8) Switch VM to host-only networking (will it work using NAT? - need to check) and identify IP address.

9) Using system-config-firewall, add a rule allowing allow inbound connections to the VM on each of the ports used by Riak nodes.
	NOTE - this needs to be for both HTTP and Protocol Buffer connections, so ten ports in total.

10) Alter riak.conf for each node so that listener.http.internal = 0.0.0.0:<PORT NUMBER>
	(rather than 127.0.0.1:<PORT NUMBER>, which will allow connections on loopback [localhost] only).
	Protocol buffer interface should also be changed.
	SEE: http://docs.basho.com/riak/latest/ops/running/nodes/adding-removing/#Change-the-HTTP-and-Protocol-Buffers-binding-address
	NB: for Riak 1.4 this is done in app.config instead (but in same location riak-x.x.x/dev/dev(x)/etc/)

11) Stop and restart all nodes to apply config change from previous step,
	then verify that can be connected to over HTTP (browser connection will do)
	from host machine (i.e. Windows client).

ON WINDOWS HOST (CLIENT):

1) Add VM IP address to Windows "hosts" file as "linuxvm".
(Edit C:\Windows\System32\Drivers\etc\hosts as admin).
This is important as will be connecting for Cassandra examples and possibly others,
don't want to be changing IP address in multiple places if setting up on further environments.

(REST OF FORMAL PROCESS IS TO DO)

So far have created new C# Console app solution
and got CorrugateIron Nuget package using "Install-Package CorrugatedIron".
Install-Package protobuf-net -Version 2.0.0.640 - for some reason NuGet installs previously-required v2.0.0.602 but this is now not correct???
Then altered app.config - CorrugatedIron already puts in Riak entries,
just need updating to point at right number of nodes at right addresses.



