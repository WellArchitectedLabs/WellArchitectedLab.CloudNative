FROM node:20 AS build
WORKDIR /src

# Install envsubst
RUN apt-get update && apt-get install -y gettext-base && rm -rf /var/lib/apt/lists/*

# Copy frontend source
COPY package*.json ./

# Copy template config and entrypoint script
COPY config/appSettings.template.yaml /src/config/appSettings.template.yaml
COPY docker-entrypoint.sh /src/docker-entrypoint.sh
RUN chmod +x /src/docker-entrypoint.sh

# Install dependencies
RUN npm install
COPY . .

# Grant access to vite modules
RUN chmod +x ./node_modules/.bin/vite

# Build (optional if your image serves static files)
RUN npm run build

# packaging stage
FROM nginx:alpine

#Copy source files and config into /usr/share/nginx/html folder
COPY --from=build /src/dist /usr/share/nginx/html
COPY --from=build /src/config/appSettings.template.yaml /usr/share/nginx/html/config/appSettings.template.yaml

# Copy entrypoint into app folder (or any separate folder from source files)
COPY --from=build /src/docker-entrypoint.sh /app/docker-entrypoint.sh
RUN chmod +x /app/docker-entrypoint.sh

EXPOSE 80

# Set entrypoint
ENTRYPOINT ["/app/docker-entrypoint.sh"]
CMD ["nginx", "-g", "daemon off;"]