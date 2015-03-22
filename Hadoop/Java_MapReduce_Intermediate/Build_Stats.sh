#!/bin/bash

# NB: Temporarily (?) removed "-Xlint:unchecked" option

scriptdir=$(dirname "${BASH_SOURCE[0]}")

pushd $(pwd) > /dev/null
cd $scriptdir

hadoop com.sun.tools.javac.Main ConstituencyStats/*.java
jar cf ConstituencyStats.jar ConstituencyStats/*.class
rm ConstituencyStats/*.class

popd > /dev/null

