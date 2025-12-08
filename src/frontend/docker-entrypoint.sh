#!/bin/sh
# Replace template placeholders with environment variables
envsubst < /usr/share/nginx/html/config/appSettings.template.yaml > /usr/share/nginx/html/config/appSettings.yaml

# Start frontend
exec "$@"