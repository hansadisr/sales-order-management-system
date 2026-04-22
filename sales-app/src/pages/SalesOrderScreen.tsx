import React, { useState, useEffect, useMemo, useRef } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { useSelector, useDispatch } from 'react-redux';
import { fetchClients, fetchItems } from '../redux/salesSlice';
import { RootState, AppDispatch } from '../redux/store';
import { SalesOrder, SalesOrderDetail, Client, Item } from '../types';
import { salesOrderService } from '../services/api';
import { Trash2, Save, Printer, ArrowLeft, Plus } from 'lucide-react';
import { useReactToPrint } from 'react-to-print';

const SalesOrderScreen: React.FC = () => {
    const { id } = useParams<{ id: string }>();
    const navigate = useNavigate();
    const dispatch = useDispatch<AppDispatch>();
    const printRef = useRef<HTMLDivElement>(null);

    const { clients, items } = useSelector((state: RootState) => state.sales);

    const [order, setOrder] = useState<SalesOrder>({
        clientId: 0,
        orderDate: new Date().toISOString().split('T')[0],
        invoiceNo: '',
        referenceNo: '',
        note: '',
        totalExcl: 0,
        totalTax: 0,
        totalIncl: 0,
        orderDetails: []
    });

    const [selectedClient, setSelectedClient] = useState<Client | null>(null);

    useEffect(() => {
        dispatch(fetchClients());
        dispatch(fetchItems());

        if (id && id !== 'new') {
            salesOrderService.getOrder(parseInt(id)).then(res => {
                setOrder(res.data);
                const client = clients.find(c => c.clientId === res.data.clientId);
                if (client) setSelectedClient(client);
            });
        }
    }, [id, dispatch]);

    useEffect(() => {
        if (order.clientId) {
            const client = clients.find(c => c.clientId === order.clientId);
            setSelectedClient(client || null);
        }
    }, [order.clientId, clients]);

    const handleClientChange = (clientId: number) => {
        setOrder(prev => ({ ...prev, clientId }));
    };

    const addLineItem = () => {
        const newItem: SalesOrderDetail = {
            itemId: 0,
            itemCode: '',
            itemDescription: '',
            quantity: 1,
            price: 0,
            taxRate: 0,
            exclAmount: 0,
            taxAmount: 0,
            inclAmount: 0,
            note: ''
        };
        setOrder(prev => ({
            ...prev,
            orderDetails: [...prev.orderDetails, newItem]
        }));
    };

    const removeLineItem = (index: number) => {
        setOrder(prev => ({
            ...prev,
            orderDetails: prev.orderDetails.filter((_, i) => i !== index)
        }));
    };

    const updateLineItem = (index: number, updates: Partial<SalesOrderDetail>) => {
        setOrder(prev => {
            const newDetails = [...prev.orderDetails];
            const item = { ...newDetails[index], ...updates };

            // Recalculate amounts
            if (updates.itemId) {
                const catalogItem = items.find(i => i.itemId === updates.itemId);
                if (catalogItem) {
                    item.itemCode = catalogItem.itemCode;
                    item.itemDescription = catalogItem.description;
                    item.price = catalogItem.price;
                    item.taxRate = catalogItem.taxRate;
                }
            }

            item.exclAmount = item.quantity * item.price;
            item.taxAmount = (item.exclAmount * item.taxRate) / 100;
            item.inclAmount = item.exclAmount + item.taxAmount;

            newDetails[index] = item;
            return { ...prev, orderDetails: newDetails };
        });
    };

    const totals = useMemo(() => {
        return order.orderDetails.reduce((acc, curr) => ({
            excl: acc.excl + curr.exclAmount,
            tax: acc.tax + curr.taxAmount,
            incl: acc.incl + curr.inclAmount
        }), { excl: 0, tax: 0, incl: 0 });
    }, [order.orderDetails]);

    useEffect(() => {
        setOrder(prev => ({
            ...prev,
            totalExcl: totals.excl,
            totalTax: totals.tax,
            totalIncl: totals.incl
        }));
    }, [totals]);

    const handleSave = async () => {
        if (!order.clientId) {
            alert("Please select a customer");
            return;
        }
        if (order.orderDetails.length === 0) {
            alert("Please add at least one item");
            return;
        }

        try {
            if (id && id !== 'new') {
                await salesOrderService.updateOrder(parseInt(id), order);
            } else {
                await salesOrderService.createOrder(order);
            }
            navigate('/');
        } catch (err) {
            alert("Failed to save order");
        }
    };

    const handlePrint = useReactToPrint({
        content: () => printRef.current,
    });

    return (
        <div className="container mx-auto p-4 max-w-6xl">
            <div className="wf-window">
                {/* Title Bar */}
                <div className="wf-title-bar">
                    <div className="wf-dots">
                        <div className="wf-dot"></div>
                        <div className="wf-dot"></div>
                        <div className="wf-dot"></div>
                    </div>
                    <div className="absolute inset-0 flex justify-center items-center pointer-events-none">
                        <span className="font-bold">Sales Order</span>
                    </div>
                </div>

                {/* Toolbar */}
                <div className="wf-toolbar flex justify-between items-center">
                    <div className="flex gap-2">
                        <button onClick={handleSave} className="wf-btn">
                            <span className="w-5 h-5 border-2 border-black rounded flex items-center justify-center text-[10px]">✓</span> 
                            Save Order
                        </button>
                        <button onClick={handlePrint} className="wf-btn">
                            <Printer className="w-4 h-4" />
                            Print
                        </button>
                    </div>
                    <button onClick={() => navigate('/')} className="wf-btn text-xs">
                        Back
                    </button>
                </div>

                {/* Form Area to Print */}
                <div ref={printRef} className="p-8">
                    <div className="grid grid-cols-1 md:grid-cols-2 gap-x-12 gap-y-2 mb-8">
                        {/* Left Column: Labels + Inputs */}
                        <div className="space-y-1">
                            <div className="wf-field-group">
                                <label className="wf-label">Customer Name</label>
                                <select 
                                    className="wf-input" 
                                    value={order.clientId} 
                                    onChange={(e) => handleClientChange(parseInt(e.target.value))}
                                >
                                    <option value={0}>Select Customer...</option>
                                    {clients.map(c => (
                                        <option key={c.clientId} value={c.clientId}>{c.name}</option>
                                    ))}
                                </select>
                            </div>
                            <div className="wf-field-group">
                                <label className="wf-label">Address 1</label>
                                <input className="wf-input" value={selectedClient?.address1 || ''} readOnly />
                            </div>
                            <div className="wf-field-group">
                                <label className="wf-label">Address 2</label>
                                <input className="wf-input" value={selectedClient?.address2 || ''} readOnly />
                            </div>
                            <div className="wf-field-group">
                                <label className="wf-label">Address 3</label>
                                <input className="wf-input" value={selectedClient?.address3 || ''} readOnly />
                            </div>
                            <div className="wf-field-group">
                                <label className="wf-label">Suburb</label>
                                <input className="wf-input" value={selectedClient?.suburb || ''} readOnly />
                            </div>
                            <div className="wf-field-group">
                                <label className="wf-label">State</label>
                                <input className="wf-input" value={selectedClient?.state || ''} readOnly />
                            </div>
                            <div className="wf-field-group">
                                <label className="wf-label">Post Code</label>
                                <input className="wf-input" value={selectedClient?.postCode || ''} readOnly />
                            </div>
                        </div>

                        {/* Right Column */}
                        <div className="space-y-1">
                            <div className="wf-field-group">
                                <label className="wf-label">Invoice No.</label>
                                <input className="wf-input" value={order.invoiceNo || ''} onChange={(e) => setOrder({...order, invoiceNo: e.target.value})} />
                            </div>
                            <div className="wf-field-group">
                                <label className="wf-label">Invoice Date</label>
                                <input type="date" className="wf-input" value={order.orderDate.split('T')[0]} onChange={(e) => setOrder({...order, orderDate: e.target.value})} />
                            </div>
                            <div className="wf-field-group">
                                <label className="wf-label">Reference no</label>
                                <input className="wf-input" value={order.referenceNo || ''} onChange={(e) => setOrder({...order, referenceNo: e.target.value})} />
                            </div>
                            <div className="pt-2">
                                <label className="wf-label mb-1">Note</label>
                                <textarea 
                                    className="wf-input h-32" 
                                    value={order.note || ''} 
                                    onChange={(e) => setOrder({...order, note: e.target.value})} 
                                />
                            </div>
                        </div>
                    </div>

                    {/* Items Table Section */}
                    <div className="mt-8 border-t-2 border-dashed border-gray-200 pt-8">
                        <div className="flex justify-between items-center mb-4">
                             <h2 className="font-bold text-lg uppercase tracking-tight">Line Items</h2>
                             <button onClick={addLineItem} className="wf-btn text-xs">+ Add Item</button>
                        </div>
                        <div className="overflow-x-auto">
                            <table className="wf-table">
                                <thead>
                                    <tr>
                                        <th>Item Code</th>
                                        <th>Description</th>
                                        <th>Note</th>
                                        <th>Quantity</th>
                                        <th>Price</th>
                                        <th>Tax</th>
                                        <th>Excl Amount</th>
                                        <th>Tax Amount</th>
                                        <th>Incl Amount</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {order.orderDetails?.map((item, idx) => (
                                        <tr key={idx}>
                                            <td className="w-32">
                                                <select 
                                                    className="w-full outline-none"
                                                    value={item.itemId}
                                                    onChange={(e) => updateLineItem(idx, { itemId: parseInt(e.target.value) })}
                                                >
                                                    <option value={0}></option>
                                                    {items.map(i => (
                                                        <option key={i.itemId} value={i.itemId}>{i.itemCode}</option>
                                                    ))}
                                                </select>
                                            </td>
                                            <td className="w-48">
                                                <select 
                                                    className="w-full outline-none"
                                                    value={item.itemId}
                                                    onChange={(e) => updateLineItem(idx, { itemId: parseInt(e.target.value) })}
                                                >
                                                    <option value={0}></option>
                                                    {items.map(i => (
                                                        <option key={i.itemId} value={i.itemId}>{i.description}</option>
                                                    ))}
                                                </select>
                                            </td>
                                            <td>
                                                <input 
                                                    className="w-full outline-none" 
                                                    value={item.note || ''} 
                                                    onChange={(e) => updateLineItem(idx, { note: e.target.value })}
                                                />
                                            </td>
                                            <td className="w-20">
                                                <input 
                                                    type="number" 
                                                    className="w-full text-center outline-none" 
                                                    value={item.quantity}
                                                    onChange={(e) => updateLineItem(idx, { quantity: parseInt(e.target.value) || 0 })}
                                                />
                                            </td>
                                            <td className="w-24 text-right">
                                                {item.price.toFixed(2)}
                                            </td>
                                            <td className="w-20 text-right">
                                                {item.taxRate.toFixed(2)}
                                            </td>
                                            <td className="w-28 text-right">
                                                {item.exclAmount.toFixed(2)}
                                            </td>
                                            <td className="w-28 text-right">
                                                {item.taxAmount.toFixed(2)}
                                            </td>
                                            <td className="w-28 text-right font-bold">
                                                {item.inclAmount.toFixed(2)}
                                            </td>
                                        </tr>
                                    ))}
                                    {/* Empty rows to match wireframe look */}
                                    {[...Array(Math.max(0, 4 - (order.orderDetails?.length || 0)))].map((_, i) => (
                                        <tr key={`empty-${i}`}>
                                            {[...Array(9)].map((_, j) => <td key={j} className="h-8"></td>)}
                                        </tr>
                                    ))}
                                </tbody>
                            </table>
                        </div>
                    </div>

                    {/* Totals Section */}
                    <div className="mt-8 flex justify-end border-t-2 border-black pt-8">
                        <div className="w-80 space-y-2">
                            <div className="flex items-center justify-between">
                                <span className="font-bold text-sm uppercase">Total Excl</span>
                                <input className="wf-input w-40 text-right font-bold" value={order.totalExcl.toFixed(2)} readOnly />
                            </div>
                            <div className="flex items-center justify-between">
                                <span className="font-bold text-sm uppercase">Total Tax</span>
                                <input className="wf-input w-40 text-right font-bold" value={order.totalTax.toFixed(2)} readOnly />
                            </div>
                            <div className="flex items-center justify-between pt-2 border-t border-gray-300">
                                <span className="font-black text-lg uppercase">Total Incl</span>
                                <input className="wf-input w-40 text-right font-black text-xl bg-gray-50" value={order.totalIncl.toFixed(2)} readOnly />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default SalesOrderScreen;
