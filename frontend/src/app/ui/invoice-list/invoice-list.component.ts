import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BehaviorSubject, switchMap } from 'rxjs';
import { InvoicePort } from '../../core/ports/invoice.port';

@Component({
  selector: 'app-invoice-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './invoice-list.component.html',
  styleUrl: './invoice-list.component.scss'
})
export class InvoiceListComponent implements OnInit {
  private readonly invoicePort = inject(InvoicePort);

  private readonly refreshTrigger$ = new BehaviorSubject<void>(undefined);

  readonly invoices$ = this.refreshTrigger$.pipe(
    switchMap(() => this.invoicePort.getAll())
  );

  isTriggering = false;
  cronReport: { success?: boolean; text?: string } | null = null;
  
  toastMessage: string | null = null;
  private toastTimer: any;

  ngOnInit(): void {}

  reload(): void {
    this.refreshTrigger$.next();
  }

  onExecuteCron(): void {
    this.isTriggering = true;
    this.cronReport = null;

    this.invoicePort.processReminders().subscribe({
      next: (res) => {
        this.cronReport = { success: true, text: res.message };
        
        // Disparamos el popup de advertencia de SPAM
        this.showToast('En los próximos minutos le deben estar llegando un correo a cada responsable de factura (no olvides que deben revisar SPAM). Puede tardar varios minutos.');

        this.reload(); // Pedimos a Mongo los datos actualizados
      },
      error: () => {
        this.cronReport = { success: false, text: 'Falló la orden de vigilancia.' };
      },
      complete: () => {
        this.isTriggering = false;
      }
    });
  }

  private showToast(msg: string): void {
    this.toastMessage = msg;
    if (this.toastTimer) {
      clearTimeout(this.toastTimer);
    }
    
    this.toastTimer = setTimeout(() => {
      this.toastMessage = null;
    }, 7500);
  }
}