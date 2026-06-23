```markdown
# Monolegal - Solución al Reto del Arquitecto (.NET 8 + Angular 18)

Solución técnica para el proceso de selección de **Monolegal**. Este proyecto está estructurado bajo un enfoque **Monorepo**, integrando un motor backend de automatización bajo estricta **Arquitectura Hexagonal** y un cliente web SPA moderno.

## Arquitectura del Backend (.NET 8)
Se ha garantizado el desacople total de la lógica de negocio mediante la separación en capas:

1. **`Monolegal.Domain`**: Reglas de negocio puras (Máquina de estados de facturación). Sin dependencias externas.
2. **`Monolegal.Infrastructure`**: Adaptadores de salida para **MongoDB Atlas** y servicio de notificaciones **SMTP (Gmail/Brevo)**.
3. **`Monolegal.API`**: Exposición de servicios mediante **Minimal APIs** con documentación en **Swagger**.
4. **`Monolegal.Tests`**: Suite de pruebas unitarias (xUnit + Moq) garantizando el 100% de cobertura en la lógica de estados.

## Arquitectura del Frontend (Angular 18)
El cliente web ha sido diseñado priorizando la escalabilidad:

- **Gestor de Paquetes:** `pnpm` (Eficiencia en resolución de dependencias).
- **UI Kit:** Angular Material v18 + SCSS estructurado bajo arquitectura modular.
- **Capa de Red:** Configuración de Proxy de desarrollo para comunicación fluida con el backend.

## Logros del Desarrollo
- **Motor de Estados:** Automatización asíncrona (`primerrecordatorio` -> `segundorecordatorio` -> `desactivado`).
- **Resiliencia:** Manejo robusto de errores en adaptadores SMTP y persistencia NoSQL.
- **Seguridad:** Implementación de **UserSecrets** para la gestión de credenciales sensibles, evitando exposición de datos en control de versiones.

---

## Instrucciones de Auditoría y Ejecución

Para auditar el proyecto, necesitarás [SDK .NET 8.0](https://dotnet.microsoft.com/es-es/download/dotnet/8.0), **Node.js (v20+)** y `pnpm`.

### Paso 1: Levantar el Backend
En la raíz del proyecto:
```bash
cd backend
dotnet build
# Nota: Configura tus credenciales mediante UserSecrets:
# dotnet user-secrets set "MongoDbSettings:ConnectionString" "TU_CADENA"
# dotnet user-secrets set "EmailConfiguration:SmtpPassword" "TU_CLAVE"
dotnet run --project Monolegal.API/Monolegal.API.csproj

```

*API escuchando en: `http://localhost:5184/swagger*`

### Paso 2: Levantar el Frontend

En una segunda terminal:

```bash
cd frontend
pnpm install
pnpm ng serve

```

*App escuchando en: `http://localhost:4200*`

---

*Solución desarrollada bajo estándares de industria, enfocada en mantenibilidad, seguridad y excelente Experiencia de Desarrollador (DX).*

```

***