#!/bin/bash

# Python download script doesn't work on all environments yet (FIX IT!)
# But for the moment - do a very quick bash script instead for some Linux envs.

cd $(dirname ${BASH_SOURCE[0]})
wget http://www.doogal.co.uk/files/postcodes.zip
unzip -o postcodes.zip
rm postcodes.zip

