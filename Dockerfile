FROM nginx:1.21-alpine

ARG BUILD_NAME
ARG UNITY_DIR

RUN echo "BUILD_NAME: {$BUILD_NAME}"

ARG BUILD_NAME
ARG UNITY_DIR

RUN ls 

ARG BUILD_NAME
ARG UNITY_DIR

ADD Builds/WebGL/${BUILD_NAME} /usr/share/nginx/html

CMD ["nginx", "-g", "daemon off;"]

EXPOSE 80 443
