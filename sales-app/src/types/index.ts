export interface Client {
    clientId: number;
    name: string;
    address1: string;
    address2: string;
    address3: string;
    suburb: string;
    state: string;
    postCode: string;
}

export interface Item {
    itemId: number;
    itemCode: string;
    description: string;
    price: number;
    taxRate: number;
}

export interface SalesOrderDetail {
    orderDetailId?: number;
    itemId: number;
    itemCode: string;
    itemDescription: string;
    quantity: number;
    price: number;
    taxRate: number;
    exclAmount: number;
    taxAmount: number;
    inclAmount: number;
    note: string;
}

export interface SalesOrder {
    orderId?: number;
    clientId: number;
    clientName?: string;
    orderDate: string;
    invoiceNo: string;
    referenceNo: string;
    note: string;
    totalExcl: number;
    totalTax: number;
    totalIncl: number;
    orderDetails: SalesOrderDetail[];
}
