# generate artifact stage (build stage)
FROM node:20-alpine AS build
WORKDIR /src
COPY package*.json ./
RUN npm install
COPY . .
RUN chmod +x ./node_modules/.bin/vite
RUN npm run build

# packaging stage
FROM nginx:alpine
COPY --from=build /src/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]