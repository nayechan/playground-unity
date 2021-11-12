FROM nginx:1.21

ARG BUILD_NAME
ARG UNITY_DIR

ADD $UNITY_DIR/Builds/WebGL/$BUILD_NAME /usr/share/nginx/html

ARG BUILD_NAME
ARG UNITY_DIR

RUN ["echo", "${BUILD_NAME}"]

CMD ["nginx", "-g", "daemon off;"]

EXPOSE 80 443
