import { Component } from '@angular/core';
import { InvoiceListComponent } from './ui/invoice-list/invoice-list.component';
import { InvoiceFormComponent } from './ui/invoice-form/invoice-form.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [InvoiceListComponent, InvoiceFormComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {}