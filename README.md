# VodeGamesCharacterApi

Ejemplo de proyecto que muestra cómo crear una API REST con:
- .NET 10
- ASP.NET Core Web API
- Entity Framework Core (EF Core)
- SQL Server

Este código fue desarrollado en base al siguiente video: https://www.youtube.com/watch?v=RwQVRXEs370

## Descripción
Proyecto de ejemplo para gestionar entidades de personajes (Characters) con operaciones CRUD a través de una API Web. Ideal para aprender la estructura básica de una Web API en .NET 10 y la integración con EF Core y SQL Server.

## Tecnologías
- .NET 10 (SDK)
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server (LocalDB, SQL Server Express o instancia remota)
- Herramientas: dotnet CLI, EF Core Tools

## Requisitos
- .NET 10 SDK instalado: https://dotnet.microsoft.com
- SQL Server (o LocalDB) accesible desde la máquina de desarrollo
- (Opcional) Visual Studio 2022/2023 o VS Code

## Configuración rápida

1. Clona el repositorio:
   git clone https://github.com/darkderes/VodeGamesCharacterApi.git

2. Configura la conexión a la base de datos:
   - Abre `appsettings.json` (o el archivo de configuración correspondiente).
   - Actualiza `ConnectionStrings:DefaultConnection` con la cadena de conexión a tu SQL Server. Ejemplo:
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=VodeGamesCharacterApiDb;Trusted_Connection=True;"
     }

3. Instala las herramientas de EF Core (si no las tienes):
   dotnet tool install --global dotnet-ef
   dotnet add package Microsoft.EntityFrameworkCore.Design

4. Crea y aplica migraciones:
   - Crear migración inicial:
     dotnet ef migrations add InitialCreate
   - Aplicar migraciones y crear la base de datos:
     dotnet ef database update

5. Ejecuta la API:
   dotnet run --project ./Ruta/Al/ProyectoApi.csproj
   Por defecto la API escuchará en los puertos configurados (ej. https://localhost:5001).

## Endpoints básicos (ejemplos)
Suponiendo un controlador `CharactersController`, algunos endpoints típicos:

- GET /api/characters
  - Obtiene todos los personajes.
- GET /api/characters/{id}
  - Obtiene un personaje por id.
- POST /api/characters
  - Crea un nuevo personaje. Body: JSON con los campos del modelo.
- PUT /api/characters/{id}
  - Actualiza un personaje existente.
- DELETE /api/characters/{id}
  - Elimina un personaje.

Ejemplo de JSON para POST/PUT:
{
  "name": "Nombre",
  "hitPoints": 100,
  "strength": 10,
  "defense": 5,
  "intelligence": 7,
  "class": "Mage"
}

(adapta los nombres de campos según tu modelo real)

## Consideraciones
- Este repositorio está pensado como recurso educativo; ajusta prácticas (validación, manejo de errores, DTOs, autenticación/autorization, tests) antes de usarlo en producción.
- Usa DTOs y AutoMapper si quieres separar entidades de la capa de transporte.
- Habilita HTTPS en desarrollo y producción según corresponda.

## Recursos
- Video base: https://www.youtube.com/watch?v=RwQVRXEs370
- Documentación EF Core: https://learn.microsoft.com/ef/core/
- .NET: https://dotnet.microsoft.com/

## Contribuir
Si quieres mejorar este ejemplo:
- Abre issues para bugs o mejoras.
- Envía pull requests con descripciones claras de los cambios.

## Licencia
Añade la licencia que prefieras (MIT, Apache 2.0, etc.). Actualmente sin licencia explícita.
