#!/bin/python

# General imports
import os
import os.path as op
import sys
import zipfile as z
import time
# Version-specific import and method declaration for URL download
if sys.version_info[0] >= 3:
    import urllib.request as ur
    def downloadurl(url, targetpath):
        ur.retrieve(url, targetpath)
else:
    import urllib2
    def downloadurl(url, targetpath):
        source = urllib2.urlopen(url)
        data = source.read()
        with open(targetpath, "wb") as file:
            file.write(data)


datapath = op.dirname(op.realpath(__file__))
zippath = op.join(datapath, 'postcodes.zip')

print('Downloading zip file to {0}\n...'.format(datapath))
downloadurl('http://www.doogal.co.uk/files/postcodes.zip', zippath)
print('DONE')

print('Unzipping data\n...')
with z.ZipFile(zippath) as zip:
    zip.extract('postcodes.csv', datapath)
print('DONE')

print('Deleting zip file\n...')
os.remove(zippath)
print('DONE')
