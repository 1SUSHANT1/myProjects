#!/bin/bash

if openssl x509 -checkend 2592000 -noout -in /etc/letsencrypt/live/sushantadk.com/fullchain.pem
	then echo "Valid"

else
	echo "Renewing Certificate now"

	/sbin/rc-service apache2 stop || exit 1
	trap '/sbin/rc-service apache2 start' EXIT

	if /usr/bin/certbot renew
	then 
		echo "renewal successful"
	else
		echo "renewal failed"
	fi
fi
