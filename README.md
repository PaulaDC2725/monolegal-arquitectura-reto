```markdown
# Monolegal - Solución al Reto Técnico (.NET 8 + Angular 18)

Solución técnica para el proceso de selección de **Monolegal**. Este proyecto implementa un sistema robusto de gestión de cobranzas y automatización de recordatorios procesales, estructurado bajo **Clean Architecture** (Arquitectura Hexagonal), con un backend de alto rendimiento en .NET 8 y una interfaz SPA moderna en Angular 18.

## Despliegue en Producción

El sistema ha sido desplegado en entornos Cloud para facilitar una auditoría inmediata sin necesidad de configuración local:

* **Frontend (Angular):** [https://monolegal-cobranzas.vercel.app](https://monolegal-cobranzas.vercel.app)
* **Backend (API .NET 8 + Swagger):** [https://monolegal-arquitectura-reto.onrender.com/](https://monolegal-arquitectura-reto.onrender.com/)
    * *Nota:* Al acceder a la URL del backend, la documentación interactiva de **Swagger** está habilitada en la raíz para probar y explorar los endpoints directamente.

---

## Arquitectura del Backend (.NET 8)

El backend garantiza el desacople total de la lógica de negocio mediante un diseño estricto por capas:

1. **`Monolegal.Domain`**: Reglas de negocio puras (Entidades, Interfaces y Contratos de Repositorio). No posee ninguna dependencia externa ni referencias a frameworks de persistencia.
2. **`Monolegal.Infrastructure`**: Implementación del acceso a datos con **MongoDB Atlas** y servicios de mensajería (Brevo SMTP). Actúa como adaptador de salida, protegiendo al dominio de cambios en la base de datos.
3. **`Monolegal.API`**: Capa de presentación expuesta mediante **Minimal APIs**. Gestiona la inyección de dependencias, configuración de CORS y documentación OpenAPI.
4. **`Monolegal.Tests`**: Suite de pruebas unitarias para validar la lógica de los servicios implementando `xUnit` + `Moq`.

## Arquitectura del Frontend (Angular 18)

Cliente web optimizado para rendimiento y escalabilidad:

- **Gestor de Paquetes:** `pnpm`.
- **UI Kit:** Angular Material v18 + SCSS modular.
- **Comunicación HTTP:** Integración RESTful basada en `HttpClient` con tipado estricto (TypeScript).
- **Seguridad:** Políticas de CORS configuradas para permitir exclusivamente el tráfico del dominio de producción.

---

## Instrucciones de Ejecución Local

Para auditar el código fuente en tu PC, asegúrate de tener instalado [SDK de .NET 8.0](https://dotnet.microsoft.com/es-es/download/dotnet/8.0), **Node.js (v20+)** y el gestor de paquetes `pnpm`.

### Paso 1: Configurar y Ejecutar el Backend

Abre una terminal en la raíz del proyecto, entra a la capa de la API e inyecta los secretos de desarrollo:

```bash
cd backend/Monolegal.API

# 1. Configurar cadena de conexión de MongoDB (Clúster de pruebas de lectura/escritura)
dotnet user-secrets set "MongoDbSettings:ConnectionString" "mongodb+srv://Paula:Monolegal2026@monolegalcluster.5jb1jxr.mongodb.net/?appName=MonolegalCluster"

# 2. Configurar clave SMTP de Brevo para envío de notificaciones
dotnet user-secrets set "EmailConfiguration:SmtpPassword" "PEGA_AQUI_LA_CLAVE_SMTP"

```

*(Nota de seguridad: Por normativas de GitHub Secret Scanning, la clave privada de Brevo no se expone en el repositorio público. **La credencial fue adjuntada en el cuerpo del correo de entrega**; por favor reemplaza `PEGA_AQUI_LA_CLAVE_SMTP` con dicho valor).*

```bash
# Compilar y levantar la API
dotnet build
dotnet run

```

La API quedará escuchando en `http://localhost:5184` (o el puerto asignado en tu consola).

### Paso 2: Configurar y Ejecutar el Frontend

Abre una **segunda terminal** en la raíz del proyecto:

```bash
cd frontend
pnpm install
pnpm ng serve

```

La aplicación web estará disponible de forma local en `http://localhost:4200`.

---

## Documentación Técnica Detallada

Para una lectura exhaustiva sobre las decisiones de diseño, trade-offs adoptados (como la retro-propagación de IDs de MongoDB hacia el dominio), manejo de resiliencia en red y estrategia de despliegue, por favor consulta el archivo:

**[DOCUMENTACION_TECNICA.md](https://www.google.com/search?q=./DOCUMENTACION_TECNICA.md)**

---

*Solución desarrollada con estándares de la industria, priorizando la mantenibilidad del código, la seguridad de la infraestructura y la experiencia del usuario final.*

```

```