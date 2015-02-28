#!/bin/bash

# Find location of JDK so we can use it to manually set JAVA_HOME correctly if needed.
for path in `find / -name tools.jar 2>/dev/null` ; do echo $(dirname $(dirname $path)) ; done

