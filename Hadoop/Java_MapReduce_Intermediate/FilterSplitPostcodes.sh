
scriptdir=$(dirname "${BASH_SOURCE[0]}")
dfsinpath=/user/$(whoami)/input
dfsoutpath=/user/$(whoami)/output/postcodesplit

echo "*** Deleting output directory (if exists) ***"
hadoop fs -rm -r $dfsoutpath/

echo "*** Running MapReduce job ***"
hadoop jar $scriptdir/FilterSplitPostcodes.jar FilterSplitPostcodes.FilterSplitPostcodes $dfsinpath/ $dfsoutpath/

