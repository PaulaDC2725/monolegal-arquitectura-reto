# Monolegal - Architectural Technical Challenge (.NET 8 + Angular)

Solución al **"Reto del Arquitecto"** para el proceso de selección de **Monolegal**. Este proyecto está estructurado bajo un enfoque de **Monorepo**, separando el ciclo de vida del *Backend* y el *Frontend*, pero manteniendo una única fuente de la verdad para el control de versiones e integración continua.

---

## Arquitectura del Backend: Hexagonal (Ports & Adapters)

El backend ha sido diseñado aplicando **Domain-Driven Design (DDD)** y **Arquitectura Hexagonal** con el objetivo de aislar por completo la lógica de negocio de las tecnologías de infraestructura (Bases de datos, servicios de e-mail, frameworks HTTP).

La solución de C# (`MonolegalChallenge.sln`) se divide en 4 capas estrictas:

1. `Monolegal.Domain` **(Núcleo / Capa de Dominio):** Contiene la entidad pura `Invoice` y los contratos (Interfaces) de los repositorios y servicios. **Cero dependencias de frameworks externos.**
2. `Monolegal.Infrastructure` **(Adaptadores de Salida):** Implementa la persistencia hacia **MongoDB Atlas** utilizando el driver oficial y un modelo de datos espejo (`MongoInvoiceModel`) para no contaminar el dominio con decoradores de persistencia (`[BsonId]`).
3. `Monolegal.API` **(Adaptadores de Entrada):** Capa de presentación REST *(Próxima a implementar)*.
4. `Monolegal.Tests` **(Capa de Testing):** Contenedor para pruebas unitarias con `xUnit`.

---

## Roadmap y Estado Actual (v0.1.0 - Sprint 1)

Aplicando un desarrollo iterativo por entregas atómicas:

* [x] **Fase 0:** Andamiaje de la Solución `.sln` y enlazado de referencias de proyectos en C#.
* [x] **Fase 1:** Modelado de la Entidad de Dominio e implementación del Repositorio conectado a un clúster de **MongoDB Atlas** en la nube.
* [ ] **Fase 2:** Motor asíncrono de evaluación de estados de facturas y cliente SMTP simulado con *Mailtrap*. **(Siguiente paso)**
* [ ] **Fase 3:** Exposición de Controladores HTTP / API REST.
* [ ] **Fase 4:** Suite de Pruebas Unitarias (`xUnit` + `Moq`).
* [ ] **Fase 5:** Desarrollo del cliente web en **Angular**.
* [ ] **Fase 6:** Automatización de Pipeline CI/CD (GitHub Actions) y despliegue Cloud.

---

## Instrucciones de Auditoría (Fase 1)

Si deseas verificar la salud del código en este punto del desarrollo, necesitas el [SDK de .NET 8.0](https://dotnet.microsoft.com/es-es/download/dotnet/8.0).

Parado sobre la raíz de la carpeta `/backend`, ejecuta:

```bash
dotnet build
