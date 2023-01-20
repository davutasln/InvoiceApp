# InvoiceApp

## Data Base Tables

### TableName : Invoice
 - ID             int
 - CustomerId     int
 - InvoiceNumber  string
 - InvoiceDate    string
 - TotalAmount    decimal/number

### TableName : Customer
 - Id         int
 - TaxNumber  string
 - Title      string
 - Address    string
 - EMail      string

### TableName : InvoiceLine
 - Id         int
 - InvoiceId  int
 - ItemName   string
 - Quantity   int
 - Price      decimal/number

## [InvoiceAppFrontend](https://github.com/davutasln/InvoiceAppFrontEnd) link bağlantısı.
