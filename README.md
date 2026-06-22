
---

```markdown
# Monolegal - Solución al Reto del Arquitecto (.NET 8 + Angular 18)

Solución técnica para el proceso de selección de **Monolegal**. Este proyecto está estructurado bajo un enfoque **Monorepo**, integrando un motor backend de automatización bajo estricta **Arquitectura Hexagonal** y un cliente web SPA moderno.

## Arquitectura del Backend (.NET 8)
Se ha garantizado el desacople total de la lógica de negocio mediante la separación en 4 capas:

1. **`Monolegal.Domain`**: Reglas de negocio puras (Máquina de estados de facturación). Sin dependencias de frameworks.
2. **`Monolegal.Infrastructure`**: Adaptadores de salida para **MongoDB Atlas** y servicio de notificaciones **SMTP (Mailtrap)**.
3. **`Monolegal.API`**: Adaptadores de entrada vía **Minimal APIs** con documentación automática en **Swagger**.
4. **`Monolegal.Tests`**: Suite de pruebas unitarias implementada con **xUnit** y **Moq**.

## Arquitectura del Frontend (Angular 18)
El cliente web ha sido inicializado priorizando rendimiento y buenas prácticas de maquetación:

- **Gestor de Paquetes:** `pnpm` (Elegido por su eficiencia de almacenamiento en disco y resolución estricta de dependencias *non-flat*).
- **UI Kit:** Angular Material v18 + SCSS estructurado bajo metodología **BEM**.
- **Capa de Red:** Configuración de Proxy de desarrollo integrado (`proxy.conf.json`) para comunicación directa con el backend en el puerto `:5184` anulando errores de CORS.

## Logros del Desarrollo
- **Motor de Estados:** Implementación de transiciones automáticas asíncronas (`primerrecordatorio` -> `segundorecordatorio` -> `desactivado`).
- **Resiliencia:** Aplicación de patrones de manejo de errores y Throttling (`Task.Delay`) para proteger los límites de tasa del proveedor SMTP.
- **Calidad de Código:** 100% de cobertura lógica validada mediante pruebas unitarias.

---

## Instrucciones de Auditoría y Ejecución

Para auditar el proyecto en tu máquina local, necesitarás el [SDK de .NET 8.0](https://dotnet.microsoft.com/es-es/download/dotnet/8.0) y **Node.js (v20+)** con `pnpm`.

### Paso 1: Levantar el Backend
Abre una terminal en la raíz del proyecto:
```bash
cd backend
dotnet build
dotnet run --project Monolegal.API/Monolegal.API.csproj

```

*La API quedará escuchando en `http://localhost:5184/swagger*`

### Paso 2: Levantar el Frontend

Abre una **segunda terminal** en la raíz del proyecto:

```bash
cd frontend
pnpm install
pnpm ng serve

```

*Abre tu navegador en `http://localhost:4200*`

---

*Solución construida con estándares de industria, enfocada en alta cohesión, bajo acoplamiento y excelente Experiencia de Desarrollador (DX).*

