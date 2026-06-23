import { Observable } from 'rxjs';
import { Invoice, ProcessRemindersResponse } from '../models/invoice.model';

export abstract class InvoicePort {
  /**
   * Solicita la lista de todas las facturas registradas
   */
  abstract getAll(): Observable<Invoice[]>;
  abstract processReminders(): Observable<ProcessRemindersResponse>;

  abstract create(invoice: Invoice): Observable<Invoice>;
}