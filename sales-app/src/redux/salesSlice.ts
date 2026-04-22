import { createSlice, createAsyncThunk, PayloadAction } from '@reduxjs/toolkit';
import { SalesOrder, Client, Item } from '../types';
import { salesOrderService } from '../services/api';

interface SalesState {
    orders: SalesOrder[];
    clients: Client[];
    items: Item[];
    loading: boolean;
    error: string | null;
}

const initialState: SalesState = {
    orders: [],
    clients: [],
    items: [],
    loading: false,
    error: null,
};

export const fetchOrders = createAsyncThunk('sales/fetchOrders', async () => {
    const response = await salesOrderService.getOrders();
    return response.data;
});

export const fetchClients = createAsyncThunk('sales/fetchClients', async () => {
    const response = await salesOrderService.getClients();
    return response.data;
});

export const fetchItems = createAsyncThunk('sales/fetchItems', async () => {
    const response = await salesOrderService.getItems();
    return response.data;
});

const salesSlice = createSlice({
    name: 'sales',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(fetchOrders.pending, (state) => { state.loading = true; })
            .addCase(fetchOrders.fulfilled, (state, action) => {
                state.loading = false;
                state.orders = action.payload;
            })
            .addCase(fetchOrders.rejected, (state, action) => {
                state.loading = false;
                state.error = action.error.message || 'Failed to fetch orders';
            })
            .addCase(fetchClients.fulfilled, (state, action) => {
                state.clients = action.payload;
            })
            .addCase(fetchItems.fulfilled, (state, action) => {
                state.items = action.payload;
            });
    },
});

export default salesSlice.reducer;
