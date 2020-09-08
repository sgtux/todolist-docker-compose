FROM ubuntu:latest
ARG DEBIAN_FRONTEND=noninteractive
COPY files/* /root/
RUN apt update && \
  apt install -y postgresql-12 net-tools uuid-runtime iputils-ping wget curl && \
  useradd -m -s /bin/bash -p $(openssl passwd -1 123) site && \
  cp /root/pg_hba.conf /etc/postgresql/12/main/pg_hba.conf && \
  cp /root/postgresql.conf /etc/postgresql/12/main/postgresql.conf && \
  cp /root/pre-script.sql /var/lib/postgresql && \
  service postgresql start && \
  runuser -l postgres -c "psql -f /var/lib/postgresql/pre-script.sql" && \
  cp /root/script.sql /home/site && \
  runuser -l site -c "psql -f /home/site/script.sql" && \
  apt autoremove -y && \
  apt clean
USER postgres
CMD [ "/usr/lib/postgresql/12/bin/postgres", "--config-file=/etc/postgresql/12/main/postgresql.conf" ]