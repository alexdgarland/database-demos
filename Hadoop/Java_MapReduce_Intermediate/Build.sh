#!/bin/bash
scriptdir=$(dirname "${BASH_SOURCE[0]}")
# NB: Temporarily (?) removed "-Xlint:unchecked" option
hadoop com.sun.tools.javac.Main $scriptdir/FilterSplitPostcodes/*.java
jar cf $scriptdir/FilterSplitPostcodes.jar $scriptdir/FilterSplitPostcodes/*.class
rm $scriptdir/FilterSplitPostcodes/*.class

