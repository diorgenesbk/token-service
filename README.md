Serviço de Geração e Validação de Token JWT

How To

1 - dotnet publish -c Release.
2 - docker build -t token-service.
3 - docker volume create token-dbvolume
4 - docker run --name token-database -e MYSQL_ROOT_PASSWORD={yourpasswordhere} -e MYSQL_DATABASE=TokenDb -e MYSQL_USER=token-user -e MYSQL_PASSWORD=token!@# -v token-dbvolume:/var/lib/mysql -p 3306:3306 -d mysql/mysql-server:5.7
5 - docker run -d -p 8080:80 --name token-service --link token-database -it token-service
6 - run Scripts into database
