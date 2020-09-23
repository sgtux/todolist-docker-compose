FROM ubuntu:latest
ARG DEBIAN_FRONTEND=noninteractive
WORKDIR /root
COPY files/* /root/
RUN apt update -y && \
    apt install -y net-tools wget software-properties-common curl git unzip iputils-ping && \
    wget https://github.com/zaproxy/zaproxy/releases/download/w2020-08-17/ZAP_WEEKLY_D-2020-08-17.zip && \
    mv ZAP_WEEKLY_D-2020-08-17.zip / && \
    unzip /ZAP_WEEKLY_D-2020-08-17.zip && \
    wget -q -O - https://pkg.jenkins.io/debian-stable/jenkins.io.key | apt-key add - && \
    sh -c 'echo deb https://pkg.jenkins.io/debian-stable binary/ > /etc/apt/sources.list.d/jenkins.list' && \
    wget https://packages.microsoft.com/config/ubuntu/19.10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && \
    dpkg -i packages-microsoft-prod.deb && \
    apt update && \
    apt install -y jenkins dotnet-sdk-3.1 openjdk-8-jdk && \
    apt autoremove -y && \
    apt clean && \
    chmod +x init.sh
CMD ["/root/init.sh"]