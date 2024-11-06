<a id="readme-top"></a>
# Willinn Backend API Template - Prueba Técnica Willinn

Willinn Backend API Template es una API RESTful para la prueba técnica en Trainee de Willinn, está desarrollada con .NET 8 donde se proporciona servicios para la gestión de usuarios. Se implementó autenticación JWT, operaciones CRUD de usuarios y una arquitectura utilizando Entity Framework Core.

## Tabla de Contenidos

- [Tecnologías Utilizadas](#tecnologías-utilizadas)
- [Requisitos Previos](#requisitos-previos)
- [Instalación y Configuración](#instalación-y-configuración)
- [Ejecución de la Aplicación](#ejecución-de-la-aplicación)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Uso de Swagger](#uso-de-swagger)
- [Documentación de Endpoints](#documentación-de-endpoints)
    - [Endpoints de Autenticación](#endpoints-de-autenticación)
    - [Endpoints CRUD de Usuarios](#endpoints-crud-de-usuarios)
- [Descripción de los Controladores](#descripción-de-los-controladores)
    - [AccessController](#accesscontroller)
    - [UserController](#usercontroller)
- [Autenticación](#autenticación)
- [Gestión de Usuarios](#gestión-de-usuarios)
- [Seguridad](#seguridad)
- [Autor](#autor)

## Características Principales
- 🔐 Sistema de autenticación JWT completo
- 👥 Gestión de usuarios (CRUD)
- 🏗️ Arquitectura modular
- 🔍 Documentación Swagger
- 🐳 Containerización con Docker
- 🔄 Migraciones automáticas

## Tecnologías Utilizadas
- .NET 8
- C# 12.0
- Entity Framework Core
- SQL Server
- Docker & Docker Compose
- Swagger/OpenAPI
- JWT Authentication

## Requisitos Previos

- **.NET SDK**: [Descargar e instalar](https://dotnet.microsoft.com/es-es/download).
- **Docker y Docker Compose**: [Descargar e instalar](https://www.docker.com/).
- **SQL Server** (local o containerizado)
- **IDE recomendado**: Se recomienda **Rider** para un desarrollo más eficiente. [Más información](https://www.jetbrains.com/es-es/rider/).
<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Instalación y Configuración

1. Clona el repositorio:
    ```bash
    git clone https://github.com/GaboAfk/Willinn-backend-api-template.git
    ```
2. Navega al directorio del proyecto:
    ```bash
    cd Willinn-backend-api-template
    ```
3. Restaura las dependencias:
    ```bash
    dotnet restore
    ```
4. Configura la cadena de conexión a SQL Server en `Api/appsettings.json`:
    ```
    "ConnectionStrings": {
       "DefaultConnection": "Server=localhost,1433;Database=UserDb;User=sa;Password=sqlpass*1234;TrustServerCertificate=true;"
    }
    ```
<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Ejecución de la Aplicación

### 🐳 Uso de Contenedores Docker

1. Asegúrate de que Docker esté ejecutándose y ejecuta el siguiente comando:
    ```bash
    docker-compose -f docker-compose-development.yml up --build
    ```

### Ejecución Local

1. Aplica las migraciones de la base de datos:
    ```bash
    dotnet ef migrations add --project Data\Data.csproj --startup-project Api\Api.csproj --context Data.AppDbContext --configuration Debug Initial --output-dir Migrations
    ```
2. Actualiza la base de datos con la migración. Reemplaza `_idMigration_Initial_` con el ID de la migración creada en `Data/Migrations`:
    ```bash
    dotnet ef database update --project Data\Data.csproj --startup-project Api\Api.csproj --context Data.AppDbContext --configuration Debug _idMigration_Initial_
    ```
3. Ejecuta la aplicación:
    ```bash
    dotnet run --project Api\Api.csproj
    ```
<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Estructura del Proyecto

```plaintext
├── Api/
│   ├── Controllers/
│   ├── Extensions/
│   ├── Properties/
│   └── Program.cs
├── Core/
│   ├── Models/
│   └── Services/
├── Data/
│   ├── Migrations/
│   └── AppDbContext.cs
├── Services/
│   ├── UserService.cs
│   └── JwtService.cs
└── Tests/
    └── UnitTests/
```
<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Uso de Swagger

Puedes acceder a la documentación de Swagger para probar los endpoints:

- **Local**: [http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html)
- **Contenedor**: [http://localhost:5001/swagger/index.html](http://localhost:5001/swagger/index.html)
<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Documentación de Endpoints

### Endpoints de Autenticación

#### `POST /registerUser `

Registra un nuevo usuario.

- **Request Body**:
    ```json
    {
      "name": "string",
      "email": "string",
      "password": "string"
    }
    ```
- **Respuesta**:
    ```json
    {
      "isSuccess": true
    }
    ```

#### `POST /login`

Inicia sesión con las credenciales del usuario.

- **Request Body**:
    ```json
    {
      "email": "string",
      "password": "string"
    }
    ```
- **Respuesta**:
    ```json
    {
      "isSuccess": true,
      "token": "your_jwt_token_here"
    }
    ```

### Endpoints CRUD de Usuarios

**Nota**: Es necesario autenticarse con un token JWT válido para poder utilizar los siguientes endpoints CRUD de usuarios. Asegúrate de incluir el token en el encabezado de autorización de tus solicitudes.

#### `POST /users`

Crea un nuevo usuario.

- **Request Body**:
    ```json
    {
      "name": "string",
      "email": "string",
      "password": "string"
    }
    ```
- **Respuesta**:
    ```json
    {
      "id": 1,
      "name": "John Doe",
      "email": "john.doe@example.com",
      "isActive": true
    }
    ```

#### `GET /users`

Obtiene todos los usuarios.

- **Respuesta**:
    ```json
    [
      {
        "id": 1,
        "name": "John Doe",
        "email": "john.doe@example.com",
        "isActive": true
      },
      {
        "id": 2,
        "name": "Jane Doe",
        "email": "jane.doe@example.com",
        "isActive": true
      }
    ]
    ```

#### `GET /users/{id}`

Obtiene un usuario específico por ID.

- **Parámetros**:
   - `id`: ID del usuario (integer).

- **Respuesta**:
    ```json
    {
      "id": 1,
      "name": "John Doe",
      "email": "john.doe@example.com",
      "isActive": true
    }
    ```

#### `PUT /users/{id}`

Actualiza un usuario existente.

- **Parámetros**:
   - `id`: ID del usuario (string).

- **Request Body**:
    ```json
    {
      "id": 1,
      "name": "John Doe Updated",
      "email": "john.doe.updated@example.com",
      "password": "newpassword",
      "isActive": true
    }
    ```
- **Respuesta**:
    ```json
    {
      "id": 1,
      "name": "John Doe Updated",
      "email": "john.doe.updated@example.com",
      "isActive": true
    }
    ```

#### `DELETE /users/{id}`

Marca un usuario como inactivo por ID.

- **Parámetros**:
    - `id`: ID del usuario (integer).

- **Respuesta**:
    ```json
    {
      "id": 1,
      "name": "John Doe",
      "email": "john.doe@example.com",
      "isActive": false
    }
    ```
<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Descripción de los Controladores

### AccessController

Maneja la autenticación y el registro de usuarios. Contiene métodos como:

- `POST /registerUser `: Registra un nuevo usuario.
- `POST /login`: Inicia sesión con las credenciales del usuario.

### UserController

Gestiona las operaciones CRUD sobre los usuarios. Contiene métodos como:

- `POST /users`: Crea un nuevo usuario.
- `GET /users`: Obtiene todos los usuarios.
- `GET /users/{id}`: Obtiene un usuario específico por ID.
- `PUT /users/{id}`: Actualiza un usuario existente.
- `DELETE /users/{id}`: Marca un usuario como inactivo por ID.
<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Autenticación
- Registro de usuarios
- Login con JWT
- Recuperación de contraseña
- Encriptación SHA256 para contraseñas
## Gestión de Usuarios
- Listado de usuarios
- Creación de usuarios
- Actualización de datos
- Eliminación lógica
- Búsqueda por ID y email
## Seguridad
- Autenticación JWT
- Protección de endpoints
- Validación de datos
- CORS configurado
- Manejo seguro de contraseñas

## Autor
Gabriel - [GaboAfk](https://github.com/GaboAfk)
<p align="right">(<a href="#readme-top">back to top</a>)</p>
