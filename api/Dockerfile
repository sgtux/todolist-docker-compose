FROM ubuntu
ARG DEBIAN_FRONTEND=noninteractive
COPY files /root/app
RUN apt update && \
  apt install -y wget && \
  wget https://packages.microsoft.com/config/ubuntu/19.10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && \
  dpkg -i packages-microsoft-prod.deb && \
  apt update && \
  apt install -y aspnetcore-runtime-3.1 dotnet-sdk-3.1 && \
  dotnet publish -o /root/publish /root/app/ && \
  apt remove dotnet-sdk-3.1 nodejs -y && \
  rm -rf app .nuget .local && \
  apt clean
WORKDIR /root/publish
CMD [ "/usr/bin/dotnet", "/root/publish/Api.dll", "--urls=https://*" ]