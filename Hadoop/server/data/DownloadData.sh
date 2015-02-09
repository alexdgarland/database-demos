#!/bin/bash

# Script to download and unzip postcode data for use in Hadoop MapReduce demo.

startdir=$(pwd);
datadir=`dirname $0`;

echo "*** Switching to data directory $datadir ***";
cd $datadir;
echo "*** Downloading postcode data zip ***";
wget http://www.doogal.co.uk/files/postcodes.zip;
echo "*** Extracting csv file ***";
unzip postcodes.zip;
echo "*** Deleting zip ***";
rm postcodes.zip;
echo "*** Reverting to previous directory ***";
cd $startdir;

