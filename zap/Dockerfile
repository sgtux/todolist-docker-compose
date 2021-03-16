FROM ubuntu:latest
ARG DEBIAN_FRONTEND=noninteractive
WORKDIR /root
RUN apt update -y && \
    apt install -y unzip wget openjdk-8-jdk && \
    wget https://github.com/zaproxy/zaproxy/releases/download/w2020-08-17/ZAP_WEEKLY_D-2020-08-17.zip && \
    unzip ZAP_WEEKLY_D-2020-08-17.zip && \
    mv ZAP_D-2020-08-17 zap && \
    rm ZAP_WEEKLY_D-2020-08-17.zip && \
    apt autoremove -y && \
    apt clean
# CMD ["java" "-jar" "/usr/share/jenkins/jenkins.war" "--httpPort=80"]