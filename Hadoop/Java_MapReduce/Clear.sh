#!/bin/bash

thisdir=$(dirname "${BASH_SOURCE[0]}")

rm -f $thisdir/*.jar
rm -f $thisdir/*.class
rm -f $thisdir/*/*.class

