Technical Documentation: Monolegal Challenge
1. Arquitectura del Sistema
El sistema se ha diseñado bajo los principios de Clean Architecture, permitiendo la separación de responsabilidades y facilitando la escalabilidad.

Capa de Dominio (Monolegal.Domain): Contiene la lógica de negocio pura, entidades (Invoice) y contratos de interfaces (IInvoiceRepository). No tiene dependencias externas.

Capa de Infraestructura (Monolegal.Infrastructure): Implementa la persistencia utilizando MongoDB. Aquí se realiza el mapeo (Traducción) entre las entidades de Dominio y los modelos de datos de la base de datos (MongoInvoiceModel), protegiendo al dominio de cambios en la capa de datos.

Capa de API (Monolegal.API): Expone los endpoints mediante Minimal APIs. Se encarga de la inyección de dependencias y la configuración de políticas de seguridad.

2. Decisiones Técnicas (Trade-offs)
Patrón de Repositorio: Implementado para desacoplar la lógica de acceso a datos de la lógica de negocio, permitiendo un fácil intercambio de la base de datos en el futuro.

Retro-propagación de IDs: Se implementó una lógica de sincronización donde el ID generado por MongoDB es inyectado de vuelta a la entidad de Dominio. Esto permite que el Frontend pueda realizar el track de los elementos en Angular sin problemas de referenciación nula.

Manejo de CORS: Se restringió el acceso a nivel de API específicamente al dominio de producción del frontend (https://monolegal-cobranzas.vercel.app) para mitigar ataques CSRF, evitando el uso de políticas permisivas (AllowAnyOrigin).

3. Pasos de Despliegue para Revisión Técnica
Para replicar el entorno de producción, se siguieron los siguientes pasos:

Persistencia: Se configuró un clúster de MongoDB Atlas con acceso global (0.0.0.0/0) para permitir conexiones desde servicios cloud.

Backend: Desplegado como Web Service en Render.com usando Docker (Dockerfile multi-stage) para optimizar el tamaño de la imagen.

Variables de entorno: PORT=8080, ASPNETCORE_ENVIRONMENT=Production.

Frontend: Desplegado como Static Web en Vercel.

Configuración de variables de entorno mediante el archivo environment.ts apuntando al endpoint de producción de Render.