#!/bin/bash
scriptdir=$(dirname "${BASH_SOURCE[0]}")
hadoop com.sun.tools.javac.Main -Xlint:unchecked $scriptdir/Jobs/*.java $scriptdir/AddressPartMappers/*.java $scriptdir/Reducers/IntSumReducer.java
jar cf $scriptdir/AddressPartCounts.jar $scriptdir/Jobs/*.class $scriptdir/AddressPartMappers/*.class  $scriptdir/Reducers/IntSumReducer.class
