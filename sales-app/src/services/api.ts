import axios from 'axios';
import { SalesOrder, Client, Item } from '../types';

const API_BASE_URL = 'http://localhost:5262/api';

const api = axios.create({
    baseURL: API_BASE_URL,
});

export const salesOrderService = {
    getOrders: () => api.get<SalesOrder[]>('/orders'),
    getOrder: (id: number) => api.get<SalesOrder>(`/orders/${id}`),
    createOrder: (order: SalesOrder) => api.post<SalesOrder>('/orders', order),
    updateOrder: (id: number, order: SalesOrder) => api.put<SalesOrder>(`/orders/${id}`, order),
    deleteOrder: (id: number) => api.delete(`/orders/${id}`),
    getClients: () => api.get<Client[]>('/orders/clients'),
    getItems: () => api.get<Item[]>('/orders/items'),
};
