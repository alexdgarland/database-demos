#!/bin/bash

datadir=`dirname $0`/data;

hdfs dfs -put $datadir/postcodes.csv /user/centosadmin/postcodes.csv

