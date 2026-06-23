```markdown
# Monolegal - Solución al Reto Técnico (.NET 8 + Angular 18)

Solución técnica para el proceso de selección de **Monolegal**. Este proyecto implementa un sistema robusto de gestión de cobranzas, estructurado bajo **Clean Architecture** (Arquitectura Hexagonal), con un backend de alto rendimiento en .NET 8 y una interfaz SPA moderna en Angular 18.

## Despliegue en Producción
El sistema ha sido desplegado en la nube para una auditoría inmediata:

* **Frontend (Angular):** [https://monolegal-cobranzas.vercel.app](https://monolegal-cobranzas.vercel.app)
* **Backend (API .NET 8 + Swagger):** [https://monolegal-arquitectura-reto.onrender.com/](https://monolegal-arquitectura-reto.onrender.com/)
    * *Nota:* La documentación interactiva (Swagger) está disponible directamente en la raíz para probar los endpoints.

## Arquitectura del Backend (.NET 8)
El backend garantiza el desacople total de la lógica mediante capas definidas:

1. **`Monolegal.Domain`**: Reglas de negocio puras (Entidades y Contratos). Sin dependencias externas.
2. **`Monolegal.Infrastructure`**: Adaptadores de persistencia para **MongoDB Atlas**. Implementación de patrón repositorio para proteger al dominio de cambios en la fuente de datos.
3. **`Monolegal.API`**: Exposición de servicios mediante **Minimal APIs** y documentación integrada en **Swagger/OpenAPI**.
4. **`Monolegal.Tests`**: Suite de pruebas unitarias implementando xUnit + Moq.

## Arquitectura del Frontend (Angular 18)
Cliente web diseñado para la escalabilidad:

- **Gestor de Paquetes:** `pnpm`.
- **UI Kit:** Angular Material v18 + SCSS estructurado.
- **Comunicación:** Integración RESTful con manejo centralizado de configuración de entornos.
- **Seguridad:** Implementación de políticas de CORS restrictivas para permitir exclusivamente el tráfico del dominio de producción.

## Instrucciones de Ejecución Local

Para auditar el código fuente, asegúrate de tener instalado [SDK .NET 8.0](https://dotnet.microsoft.com/es-es/download/dotnet/8.0), **Node.js (v20+)** y `pnpm`.

### Paso 1: Configurar el Backend
Desde la raíz del proyecto:
```bash
cd backend
# Restaurar dependencias y compilar
dotnet build
# Ejecutar API
dotnet run --project Monolegal.API/Monolegal.API.csproj

```

### Paso 2: Configurar el Frontend

En una nueva terminal:

```bash
cd frontend
pnpm install
pnpm ng serve

```

---

## Documentación Técnica

Para una explicación detallada de las decisiones arquitectónicas (Trade-offs, manejo de persistencia y estrategia de despliegue), por favor consulte el archivo: [DOCUMENTACION_TECNICA.md](https://www.google.com/search?q=./DOCUMENTACION_TECNICA.md)

---

*Solución desarrollada con estándares de industria, priorizando la mantenibilidad, seguridad y la experiencia del usuario final.*

```