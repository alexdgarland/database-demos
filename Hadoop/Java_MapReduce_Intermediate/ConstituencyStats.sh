
scriptdir=$(dirname "${BASH_SOURCE[0]}")
dfsinpath=/user/$(whoami)/output/postcodesplit/Postcodes-Wales*
dfsoutpath=/user/$(whoami)/output/stats

echo "*** Deleting output directory (if exists) ***"
hadoop fs -rm -r $dfsoutpath/

echo "*** Running MapReduce job ***"
hadoop jar $scriptdir/ConstituencyStats.jar ConstituencyStats.ConstituencyStats $dfsinpath/ $dfsoutpath/

