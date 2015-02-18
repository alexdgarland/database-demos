#!/bin/bash

scriptdir=$(dirname "${BASH_SOURCE[0]}")

hadoop com.sun.tools.javac.Main $scriptdir/WardCount.java $scriptdir/AddressPartMappers/WardMapper.java $scriptdir/Reducers/IntSumReducer.java
jar cf $scriptdir/WardCount.jar WardCount.class AddressPartMappers/WardMapper.class  Reducers/IntSumReducer.class

