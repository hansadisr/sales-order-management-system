import React, { useEffect } from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { fetchOrders } from '../redux/salesSlice';
import { RootState, AppDispatch } from '../redux/store';
import { Plus, Edit2 } from 'lucide-react';

const HomeScreen: React.FC = () => {
    const dispatch = useDispatch<AppDispatch>();
    const navigate = useNavigate();
    const { orders, loading, error } = useSelector((state: RootState) => state.sales);

    useEffect(() => {
        dispatch(fetchOrders());
    }, [dispatch]);

    const handleEdit = (id: number) => {
        navigate(`/order/${id}`);
    };

    return (
        <div className="container mx-auto p-4 max-w-5xl">
            <div className="wf-window">
                {/* Title Bar */}
                <div className="wf-title-bar">
                    <div className="wf-dots">
                        <div className="wf-dot"></div>
                        <div className="wf-dot"></div>
                        <div className="wf-dot"></div>
                    </div>
                    <div className="absolute inset-0 flex justify-center items-center pointer-events-none">
                        <span className="font-bold">Home</span>
                    </div>
                </div>

                {/* Toolbar */}
                <div className="wf-toolbar">
                    <button
                        onClick={() => navigate('/order/new')}
                        className="wf-btn"
                    >
                        Add New
                    </button>
                </div>

                {/* Main Content */}
                <div className="p-4">
                    {loading ? (
                        <div className="text-center py-10 font-bold uppercase">Loading...</div>
                    ) : (
                        <div className="overflow-x-auto">
                            <table className="wf-table">
                                <thead>
                                    <tr>
                                        <th>▼ Invoice No.</th>
                                        <th>▼ Customer Name</th>
                                        <th>▼ Order Date</th>
                                        <th>▼ Total Amount</th>
                                        <th>▼ Reference No.</th>
                                        <th>▼ Note</th>
                                        <th>▼ Action</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {orders.map((order) => (
                                        <tr 
                                            key={order.orderId} 
                                            onDoubleClick={() => handleEdit(order.orderId!)}
                                            className="cursor-pointer"
                                        >
                                            <td>{order.invoiceNo}</td>
                                            <td>{order.clientName}</td>
                                            <td>{new Date(order.orderDate).toLocaleDateString()}</td>
                                            <td className="text-right font-bold">${order.totalIncl.toFixed(2)}</td>
                                            <td>{order.referenceNo}</td>
                                            <td>{order.note?.substring(0, 20)}...</td>
                                            <td className="text-center">
                                                <button 
                                                    onClick={() => handleEdit(order.orderId!)}
                                                    className="underline font-bold text-xs"
                                                >
                                                    EDIT
                                                </button>
                                            </td>
                                        </tr>
                                    ))}
                                    {orders.length === 0 && (
                                        <tr>
                                            <td colSpan={7} className="py-10 text-center font-bold italic">
                                                No orders found. Click "Add New" to create one.
                                            </td>
                                        </tr>
                                    )}
                                </tbody>
                            </table>
                        </div>
                    )}
                </div>
            </div>
        </div>
    );
};

export default HomeScreen;
