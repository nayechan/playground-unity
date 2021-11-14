FROM nginx:1.21-alpine

ARG BUILD_NAME

ADD Builds/WebGL/${BUILD_NAME} /usr/share/nginx/html

CMD ["nginx", "-g", "daemon off;"]

EXPOSE 80 443
