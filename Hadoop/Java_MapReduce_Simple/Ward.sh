#!/bin/bash

action=$1

scriptdir=$(dirname "${BASH_SOURCE[0]}")

source $scriptdir/Bash/MapReduceHelperFunctions.sh

HandleAction $action $scriptdir WardCount ward

