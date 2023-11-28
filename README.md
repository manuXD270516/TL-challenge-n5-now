TEST N5 Tech Lead

-- Servicios externos
Iniciar los servicios del archivo docker-compose con el comando
docker-compose -f docker-compose-2.yml up -d

-- Reconocimiento del kafka server
Agregar la siguiente linea

127.0.0.1    		 kafka

Todo ello en el archivo de sistema que haga referencia a los hosts (en caso de windows en el siguiente directorio: C:\Windows\System32\drivers\etc\hosts)
en caso de utilizar linux, encontrar este archivo y realizar el mismo paso 


el archivo se encuentra en el directorio:
external-resources\docker

-- Para correr las migraciones 
Descomentar el constructor sin parametros de la class DatabaseContextFactory y comentar el constructor que inyecta el atributo de clae para setear la estrategia. (Por defecto levantara una instancia de SQLServer), no obstante es capaz de soportar tambien MySQL al cambiar la estrategia

-- Iniciar el proyecto en modo local
Dejar el constructor con parametros inyectados de la clase DatabaseContextFactory y comentar el que se utilizo para correr la migracion. de Esta forma se instanciara correctamente la estrategia inyectada.

-- Logs de proyecto
se encuentra en el directorio.
rest-server\Logs


-- Para generar la imagen de docker 
Situarse en el directorio /rest-server/ y ejecutar el siguiente comando.

dotnet publish --os linux --arch x64 -p:PublishProfile=DefaultContainer -p:ContainerImageName:docker-net-n5-now
