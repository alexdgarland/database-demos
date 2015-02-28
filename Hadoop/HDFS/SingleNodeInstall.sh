#!/bin/bash

# Setup actions for HDFS based on:
# http://www.cloudera.com/content/cloudera/en/documentation/cdh4/latest/CDH4-Quick-Start/cdh4qs_topic_3_2.html

scriptdir=$(dirname ${BASH_SOURCE[0]})

# Grab Cloudera repo
startdir=$(pwd)
cd /etc/yum.repos.d
sudo wget http://archive.cloudera.com/cdh4/redhat/6/x86_64/cdh/cloudera-cdh4.repo
sudo rpm --import http://archive.cloudera.com/cdh4/redhat/6/x86_64/cdh/RPM-GPG-KEY-cloudera
cd $startdir

# Install with single-node config
sudo yum install hadoop-0.20-conf-pseudo
sudo update-alternatives --set hadoop-conf /etc/hadoop/conf.pseudo.mr1

# Reformat namenode
sudo -u hdfs hdfs namenode -format

# Start HDFS Daemons
. $scriptdir/../HadoopDaemons.sh start dfs

# Create/ set permissions on tmp dir
sudo -u hdfs hadoop fs -mkdir /tmp 
sudo -u hdfs hadoop fs -chmod -R 1777 /tmp

# Create/ set permissions on mapred staging dir
sudo -u hdfs hadoop fs -mkdir -p /var/lib/hadoop-hdfs/cache/mapred/mapred/staging
sudo -u hdfs hadoop fs -chmod 1777 /var/lib/hadoop-hdfs/cache/mapred/mapred/staging
sudo -u hdfs hadoop fs -chown -R mapred /var/lib/hadoop-hdfs/cache/mapred

# Start MapReduce
. $scriptdir/../HadoopDaemons.sh start mapred

# Create user directory
sudo -u hdfs hadoop fs -mkdir /user/$(whoami)
sudo -u hdfs hadoop fs -chown $(whoami) /user/$(whoami)

# Create my directories (nothing to do with the Cloudera setup)
. $scriptdir/CreateFolders.sh
