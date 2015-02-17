#!/bin/bash

hadoop com.sun.tools.javac.Main $(dirname "${BASH_SOURCE[0]}")/AddressCounts.java
jar cf AddressCounts.jar AddressCounts*.class

