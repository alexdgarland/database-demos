#!/bin/bash

curl -XPUT http://localhost:10018/buckets/demo/keys/RiakLogo.jpg \
  -H 'Content-Type: image/jpeg' \
  --data-binary @/home/$(whoami)/Pictures/RiakLogo.jpg

