*** Riak Demo Environment Set-Up ***

These notes are pretty scrappy at the moment and are mainly for my own benefit to keep track of the various steps required.  However, I may try and document the install process in greater detail (and possibly even get it scripted) as and when I have time

ON LINUX VM (SERVER):

Steps 1-6 are covered by the Basho install guide for Riak.  The latest version currently supported by the CourrugatedIron .NET client is 1.4 so see here - http://docs.basho.com/riak/1.4.1/quickstart/ - and follow further links on the page as required.

1) Install all prerequisites for Erlang and Riak via Yum - [scripted here](/Riak/server/SetUp/Get_Riak_Erlang_Dependencies.sh).
Note that at this stage the installing user already needs to have root or extensive sudo permissions - this is probably best set up manually as "step 0".

2) Install and build Erlang.
	If on Centos, may need to add a flag setting to ~/.kerlrc (export CFLAGS="-DOPENSSL_NO_EC=1")
	- see https://github.com/taylor/kiex/issues/7
	This is documented as part of [the Riak instructions](http://docs.basho.com/riak/latest/ops/building/installing/erlang/) but I missed it on first read-through...

3) Install Riak as per Basho instructions.

4) Install Riak dev cluster as per Basho instructions.

5) Increase upper limit for file handles for user who will run nodes (in config file - check which - probably need to document this further).

6) Start all nodes (script is provided in GitHub repo under Riak/server/SetUp/).

7) Join all nodes to cluster (script is provided in GitHub repo under Riak/server/SetUp/).

8) Switch VM to host-only networking (will it work using NAT? - need to check) and identify IP address.

9) Using system-config-firewall, add a rule allowing allow inbound connections to the VM on each of the ports used by Riak nodes.
	NOTE - this needs to be for both HTTP and Protocol Buffer connections, so ten ports in total.

10) Alter riak.conf or app.config (see note below) for each node so that listener.http.internal = 0.0.0.0:<PORT NUMBER>
	(rather than 127.0.0.1:<PORT NUMBER>, which will allow connections on loopback [localhost] only).
	Protocol buffer interface should also be changed - [see here](http://docs.basho.com/riak/latest/ops/running/nodes/adding-removing/#Change-the-HTTP-and-Protocol-Buffers-binding-address).
NB: Current versions of Riak use riak.conf for this.  However, for the version we need (1.4) this is done in app.config instead (but in same location riak-x.x.x/dev/dev(x)/etc/)

11) Stop and restart all nodes to apply config change from previous step,
	then verify that can be connected to over HTTP (browser connection will do)
	from host machine (i.e. Windows client).

ON WINDOWS HOST (CLIENT):

1) Add VM IP address to Windows "hosts" file as "linuxvm".
(Edit C:\Windows\System32\Drivers\etc\hosts as admin).

(NEED TO CHECK IF ANY MORE STEPS ARE NEEDED HERE)


If starting a new Visual Studio solution rather than using the one in this repo, please note:

When installing CorrugateIron Nuget package using "Install-Package CorrugatedIron", for some reason NuGet installs the previously-required version of Protocol Buffers (v2.0.0.602) - this causes errors.
So you will then need to "Install-Package protobuf-net -Version 2.0.0.640"

Connections to the cluster are specified in app.config - CorrugatedIron already puts in Riak entries,
these just need updating to point at the right number of nodes at right addresses (hostname:port).
