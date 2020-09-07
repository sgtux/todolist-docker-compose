#!/bin/sh
echo $API_HOST > apihost
exec python3 -m http.server 80