FROM alpine
RUN mkdir /app-temp
COPY files /app-temp
RUN apk add --update python3 nodejs yarn && \
    cd /app-temp && \
    yarn install && \
    yarn build && \
    mv build /app && \
    mv /app-temp/server.sh /app && \
    rm -rf /app-temp && \
    chmod +x /app/server.sh
WORKDIR /app
CMD [ "/app/server.sh" ]