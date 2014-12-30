#!/bin/bash

curl -XPUT http://localhost:10018/buckets/demo/keys/message   -H 'Content-Type: text/plain'   -d 'Hello, Leeds Sharpers!'

