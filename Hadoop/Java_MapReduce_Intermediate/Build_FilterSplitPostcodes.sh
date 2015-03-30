#!/bin/bash

# NB: Temporarily (?) removed "-Xlint:unchecked" option

scriptdir=$(dirname "${BASH_SOURCE[0]}")

pushd $(pwd) > /dev/null
cd $scriptdir

hadoop com.sun.tools.javac.Main FilterSplitPostcodes/*.java
jar cf FilterSplitPostcodes.jar FilterSplitPostcodes/*.class
rm FilterSplitPostcodes/*.class

popd> /dev/null

