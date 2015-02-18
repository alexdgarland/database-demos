#!/bin/bash

scriptdir=$(dirname "${BASH_SOURCE[0]}")

hadoop com.sun.tools.javac.Main $scriptdir/PostalDistrictCount.java $scriptdir/AddressPartMappers/PostalDistrictMapper.java $scriptdir/Reducers/IntSumReducer.java
jar cf $scriptdir/PostalDistrictCount.jar PostalDistrictCount.class AddressPartMappers/PostalDistrictMapper.class  Reducers/IntSumReducer.class

