export interface Invoice {
  id?: string;
  clientName: string;
  clientEmail: string;
  amount: number;
  status: string;
}

export interface ProcessRemindersResponse {
  message: string;
  details?: string[];
}

export interface InvoiceStatus {
  FirstRemember: 'primerrecordatorio';
  SegundoRecordatorio: 'segundorecordatorio';
  Desactivado: 'desactivado';
}