<!-- PROYECTO -->
<br />
<div align="center">
  <h3 align="center">Sistema Hotelero: Reservas</h3>
</div>

## Tabla de Contenidos
- [Introducción](#introducción)
- [Tecnologías Utilizadas 🖥️](#tecnologías-utilizadas)
- [Pasos de Ejecución](#pasos-de-ejecución)
- [Migraciones](#migraciones)
- [Iniciar la Aplicación](#iniciar-la-aplicación)
- [Versionado 📈](#versionado)
- [Autores ✂️](#autores)
- [Licencia 📄](#licencia)

# Sistema Hotelero - Reservas y Clientes

## Introducción

Este sistema hotelero consta de cuatro componentes principales: 
1. **Servicio gRPC de Reservas**: Gestiona la comunicación de reservas con el sistema de clientes.
2. **Aplicación Web de Reservas**: Permite listar habitaciones, crear reservas y asignarlas a clientes.

## Tecnologías Utilizadas 🖥️
- [ASP.NET Core](https://dotnet.microsoft.com/en-us/): Framework principal.
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/): ORM para la base de datos MySQL.
- [gRPC](https://grpc.io/): Comunicación entre servicios.
- [MySQL](https://www.mysql.com): Base de datos relacional.
- [Visual Studio Code](https://code.visualstudio.com/): Editor de código.

Asegúrate de tener instalada la herramienta de Entity Framework Core CLI:
```bash
dotnet tool install --global dotnet-ef
```

## Pasos de Ejecución

### Configuración de la Base de Datos

Cambia los siguientes parámetros en el archivo `.env` con las variables de entorno de la base de datos:

- **server**: Es la dirección del servidor MySQL. Puede utilizar `localhost` si tiene el servidor MySQL en la misma máquina que la aplicación web.
- **port**: Es el puerto donde se realiza la conexión a la base de datos.
- **database**: Aquí va el nombre de la base de datos creada en nuestro administrador de base de datos preferido (Ej: MySQL Workbench).
- **user**: El nombre de usuario que utiliza para acceder a la base de datos.
- **password**: Es la contraseña del usuario.

Además, debes ingresar la dirección la cual debe ser del servidor gRPC de clientes, en el apartado de `GRPC_CLIENT_URL=`


Ejecuta el siguiente comando en una terminal para instalar las dependencias:
```bash
dotnet restore
```

## Migraciones
Ejecuta los siguientes comandos para aplicar las migraciones y crear las bases de datos:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Iniciar la Aplicación

1. **Ejecutar los Servicios gRPC**:
   - Para el servicio de Reservas:
     ```bash
     dotnet run --project ReservasGrpcService
     ```

2. **Ejecutar las Aplicaciones Web**:
   - Para la aplicación web de Reservas:
     ```bash
     dotnet run --project ReservasWebApp
     ```

3. Accede a las aplicaciones.

## Versionado 📈
Usamos [GitHub](https://github.com) para el versionado.

## Autores ✂️

###### José Bautista

###### Nicolas Mardones

## Licencia 📄

Este proyecto está bajo la Licencia MIT - consulta el archivo [LICENSE](LICENSE) para más detalles.

