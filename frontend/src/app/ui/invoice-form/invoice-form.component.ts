import { Component, EventEmitter, Output, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { InvoicePort } from '../../core/ports/invoice.port';
import { Invoice } from '../../core/models/invoice.model';

@Component({
  selector: 'app-invoice-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './invoice-form.component.html',
  styleUrl: './invoice-form.component.scss'
})
export class InvoiceFormComponent {
  private readonly fb = inject(FormBuilder);
  private readonly invoicePort = inject(InvoicePort);

  @Output() readonly invoiceCreated = new EventEmitter<void>();

  readonly form: FormGroup = this.fb.nonNullable.group({
    clientName: ['', [Validators.required, Validators.minLength(3)]],
    clientEmail: ['', [Validators.required, Validators.email]],
    amount: [1000, [Validators.required, Validators.min(1)]]
  });

  isSubmitting = false;
  successMessage = '';
  errorMessage = '';

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;
    this.successMessage = '';
    this.errorMessage = '';

    const raw = this.form.getRawValue();
    const newInvoice: Invoice = {
      clientName: raw.clientName.trim(),
      clientEmail: raw.clientEmail.trim(),
      amount: Number(raw.amount),
      status: 'primerrecordatorio'
    };

    this.invoicePort.create(newInvoice).subscribe({
      next: () => {
        this.form.reset({ amount: 1000 });
        this.successMessage = 'Expediente creado exitosamente.';
        this.invoiceCreated.emit();
      },
      error: () => this.errorMessage = 'Error de conexión con MongoDB.',
      complete: () => {
        this.isSubmitting = false;
        setTimeout(() => this.successMessage = '', 4000);
      }
    });
  }
}