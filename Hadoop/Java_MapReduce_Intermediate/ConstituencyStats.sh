
scriptdir=$(dirname "${BASH_SOURCE[0]}")
dfsinpath=/user/$(whoami)/output/postcodesplit/Postcodes-Wales*
dfsoutpath=/user/$(whoami)/output/stats

pushd $(pwd) > /dev/null

echo "*** Compiling Source ***"
cd $scriptdir/ConstituencyStats
mvn compile
cd target/classes
jar cf ../../ConstituencyStats.jar ConstituencyStats/*.class

echo "*** Deleting output directory (if exists) ***"
hadoop fs -rm -r $dfsoutpath/

echo "*** Running MapReduce job ***"
cd ../..
hadoop jar ConstituencyStats.jar ConstituencyStats.ConstituencyStats $dfsinpath/ $dfsoutpath/

popd > /dev/null

