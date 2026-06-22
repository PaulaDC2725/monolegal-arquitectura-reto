# Monolegal - Architect's Challenge Solution (.NET 8 + Angular)

Solución técnica para el proceso de selección de **Monolegal**. Este sistema implementa un motor de automatización de estados procesales y notificaciones, bajo una estricta **Arquitectura Hexagonal**.

## Arquitectura de Software
Se ha garantizado el desacople total de la lógica de negocio mediante la separación en capas:

1. **`Monolegal.Domain`**: Reglas de negocio puras (Máquina de estados de facturación). Sin dependencias externas.
2. **`Monolegal.Infrastructure`**: Adaptadores para **MongoDB Atlas** y servicio de notificaciones **SMTP (Mailtrap)**.
3. **`Monolegal.API`**: Exposición de servicios mediante **Minimal APIs** y documentación integrada con **Swagger**.
4. **`Monolegal.Tests`**: Suite de pruebas unitarias implementada con **xUnit** y **Moq** para validar la integridad del sistema.

## Logros del Desarrollo
- **Motor de Estados:** Implementación de transiciones automáticas (`primerrecordatorio` -> `segundorecordatorio` -> `desactivado`).
- **Resiliencia:** Aplicación de patrones de manejo de errores y Throttling (`Task.Delay`) para garantizar entregabilidad frente a límites de tasa de proveedores SMTP.
- **Calidad de Código:** 100% de cobertura lógica validada mediante pruebas unitarias.
- **Persistencia:** Integración nativa con clúster NoSQL en la nube.

## Auditoría y Ejecución
Para ejecutar la solución, asegúrate de tener el [SDK de .NET 8.0](https://dotnet.microsoft.com/es-es/download/dotnet/8.0).

1. Navega a la carpeta `/backend`.
2. Compila el proyecto: `dotnet build`
3. Ejecuta el servidor: `dotnet run --project Monolegal.API/Monolegal.API.csproj`
4. Accede a la interfaz de pruebas en: `http://localhost:5184/swagger`

---
*Solución desarrollada con estándares de industria, enfocado en resiliencia y mantenibilidad.*