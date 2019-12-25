FROM node:13-alpine AS build-env
WORKDIR /app

COPY package.json package-lock.json ./
RUN npm install
COPY . .

ARG apiUrl
RUN npm run build -- --env.API_URL=${apiUrl}

FROM nginx:mainline-alpine
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build-env /app/dist /usr/share/nginx/html