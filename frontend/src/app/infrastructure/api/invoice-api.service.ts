import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { InvoicePort } from '../../core/ports/invoice.port';
import { Invoice, ProcessRemindersResponse } from '../../core/models/invoice.model';

// Importamos el objeto de entorno
import { environment } from '../../../environments/environment';

@Injectable()
export class InvoiceApiService extends InvoicePort {
  private readonly http = inject(HttpClient);
  
  private readonly apiUrl = environment.apiUrl;

  override getAll(): Observable<Invoice[]> {
    return this.http.get<Invoice[]>(this.apiUrl);
  }

  override processReminders(): Observable<ProcessRemindersResponse> {
    return this.http.post<ProcessRemindersResponse>(`${this.apiUrl}/process-reminders`, {});
  }

  override create(invoice: Invoice): Observable<Invoice> {
    return this.http.post<Invoice>(this.apiUrl, invoice);
  }
}