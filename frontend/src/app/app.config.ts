import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { routes } from './app.routes';

import { InvoicePort } from './core/ports/invoice.port';
import { InvoiceApiService } from './infrastructure/api/invoice-api.service';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    
    provideHttpClient(withFetch()), 
    
    { provide: InvoicePort, useClass: InvoiceApiService }
  ]
};