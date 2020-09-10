## **Exemplo de Todo List com infraestrutura em containers docker**


### 1. Requisitos:
* <a href="https://docs.docker.com/get-started/">Docker</a>.
* <a href="https://docs.docker.com/compose/install/">Docker Compose</a>.

### 2. Configuração do ambiente
* Subir os containers com o docker compose, para isso, no diretório do arquivo docker-compose.yaml, um dos commandos deve ser executado. O parâmetro **-d** é para não travar o terminal e omitir os logs:
  ```
  $ docker-compose up
  $ docker-compose up -d
  ```