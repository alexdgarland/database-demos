#!/bin/bash

scriptdir="$(dirname "${BASH_SOURCE[0]}")"

curl -XPUT http://localhost:10018/buckets/demo/keys/RiakLogo.jpg \
  -H 'Content-Type: image/jpeg' \
  --data-binary @$scriptdir/RiakLogo.jpg

