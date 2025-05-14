"use client";

import { useAuth } from "@/utils/useAuth";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";

interface OrderItem {
    productId: string;
    productName: string;
    quantity: number;
    unitPrice: number;
    discount: number;
}

interface Order {
    orderId: string;
    customerId: string;
    customerName: string;
    customerPhone: string;
    paymentMethod: string;
    status: "Pending" | "Confirmed" | "Delivered" | "Cancelled";
    createdAt: string;
    total: number;
    orderItems: OrderItem[];
}

const ManageOrdersPage = () => {
    const { user } = useAuth();
    const router = useRouter();
    const [orders, setOrders] = useState<Order[]>([]);
    const [selectedOrder, setSelectedOrder] = useState<Order | null>(null);
    const [isModalOpen, setIsModalOpen] = useState(false);

    useEffect(() => {
        // Check if user is logged in and is staff
        if (!user || user.role !== "Staff") {
            router.push("/login");
            return;
        }

        // Mock data - In real app, this would be fetched from API
        const mockOrders: Order[] = [
            {
                orderId: "ORD001",
                customerId: "User99775533",
                customerName: "Nguyễn Kiệt",
                customerPhone: "99775533",
                paymentMethod: "Tiền mặt",
                status: "Pending",
                createdAt: "2024-03-10T15:45:00",
                total: 500000,
                orderItems: [
                    {
                        productId: "P003",
                        productName: "Thuốc trừ sâu Regen X2",
                        quantity: 1,
                        unitPrice: 300000,
                        discount: 0
                    },
                    {
                        productId: "P005",
                        productName: "Thuốc Regen",
                        quantity: 1,
                        unitPrice: 200000,
                        discount: 0
                    }
                ]
            },
            {
                orderId: "ORD002",
                customerId: "User99775533",
                customerName: "Nguyễn Kiệt",
                customerPhone: "99775533",
                paymentMethod: "Thẻ tín dụng",
                status: "Delivered",
                createdAt: "2024-03-10T15:45:00",
                total: 730000,
                orderItems: [
                    {
                        productId: "P003",
                        productName: "Trái cây theo mùa",
                        quantity: 1,
                        unitPrice: 250000,
                        discount: 0
                    },
                    {
                        productId: "P004",
                        productName: "Mật ong rừng",
                        quantity: 1,
                        unitPrice: 280000,
                        discount: 0
                    },
                    {
                        productId: "P005",
                        productName: "Hạt điều rang muối",
                        quantity: 1,
                        unitPrice: 200000,
                        discount: 0
                    }
                ]
            }
        ];

        setOrders(mockOrders);
    }, [user, router]);

    const getStatusColor = (status: Order["status"]) => {
        switch (status) {
            case "Pending":
                return "bg-yellow-100 text-yellow-800";
            case "Confirmed":
                return "bg-blue-100 text-blue-800";
            case "Delivered":
                return "bg-green-100 text-green-800";
            case "Cancelled":
                return "bg-red-100 text-red-800";
            default:
                return "bg-gray-100 text-gray-800";
        }
    };

    const handleStatusChange = (orderId: string, newStatus: Order["status"]) => {
        setOrders(orders.map(order => 
            order.orderId === orderId 
                ? { ...order, status: newStatus }
                : order
        ));
    };

    const handleViewDetails = (order: Order) => {
        setSelectedOrder(order);
        setIsModalOpen(true);
    };

    const handleConfirmOrder = async (orderId: string) => {
        try {
            // TODO: Implement API call to confirm order
            setOrders(orders.map(order => 
                order.orderId === orderId 
                    ? { ...order, status: "Confirmed" }
                    : order
            ));
            setIsModalOpen(false);
        } catch (error) {
            console.error("Error confirming order:", error);
        }
    };

    const OrderDetailModal = () => {
        if (!selectedOrder) return null;

        return (
            <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
                <div className="bg-white rounded-lg p-6 max-w-2xl w-full max-h-[90vh] overflow-y-auto">
                    <div className="flex justify-between items-center mb-4">
                        <h2 className="text-xl font-bold">Chi tiết đơn hàng #{selectedOrder.orderId}</h2>
                        <button
                            onClick={() => setIsModalOpen(false)}
                            className="text-gray-500 hover:text-gray-700"
                        >
                            <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M6 18L18 6M6 6l12 12" />
                            </svg>
                        </button>
                    </div>

                    <div className="space-y-4">
                        <div className="grid grid-cols-2 gap-4">
                            <div>
                                <p className="text-sm text-gray-600">Khách hàng</p>
                                <p className="font-medium">{selectedOrder.customerName}</p>
                                <p className="text-sm text-gray-600">{selectedOrder.customerPhone}</p>
                            </div>
                            <div>
                                <p className="text-sm text-gray-600">Thanh toán</p>
                                <p className="font-medium">{selectedOrder.paymentMethod}</p>
                            </div>
                        </div>

                        <div>
                            <p className="text-sm text-gray-600 mb-2">Sản phẩm</p>
                            <div className="border rounded-lg">
                                <table className="min-w-full">
                                    <thead>
                                        <tr className="bg-gray-50">
                                            <th className="px-4 py-2 text-left text-xs font-medium text-gray-500">Sản phẩm</th>
                                            <th className="px-4 py-2 text-left text-xs font-medium text-gray-500">Số lượng</th>
                                            <th className="px-4 py-2 text-left text-xs font-medium text-gray-500">Đơn giá</th>
                                            <th className="px-4 py-2 text-left text-xs font-medium text-gray-500">Thành tiền</th>
                                        </tr>
                                    </thead>
                                    <tbody className="divide-y divide-gray-200">
                                        {selectedOrder.orderItems.map((item) => (
                                            <tr key={item.productId}>
                                                <td className="px-4 py-2">{item.productName}</td>
                                                <td className="px-4 py-2">{item.quantity}</td>
                                                <td className="px-4 py-2">{item.unitPrice.toLocaleString()} VND</td>
                                                <td className="px-4 py-2">
                                                    {(item.quantity * item.unitPrice).toLocaleString()} VND
                                                </td>
                                            </tr>
                                        ))}
                                    </tbody>
                                </table>
                            </div>
                        </div>

                        <div className="flex justify-between items-center pt-4 border-t">
                            <p className="text-lg font-bold">Tổng tiền:</p>
                            <p className="text-lg font-bold text-green-600">
                                {selectedOrder.total.toLocaleString()} VND
                            </p>
                        </div>

                        {selectedOrder.status === "Pending" && (
                            <div className="flex justify-end pt-4">
                                <button
                                    onClick={() => handleConfirmOrder(selectedOrder.orderId)}
                                    className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600"
                                >
                                    Xác nhận đơn hàng
                                </button>
                            </div>
                        )}
                    </div>
                </div>
            </div>
        );
    };

    if (!user || user.role !== "Staff") {
        return null;
    }

    return (
        <div className="p-6">
            <h1 className="text-2xl font-bold mb-6">Quản lý đơn hàng</h1>
            
            <div className="space-y-6">
                {orders.map((order) => (
                    <div key={order.orderId} className="bg-white p-6 rounded-lg shadow-md">
                        <div className="flex justify-between items-start mb-4">
                            <div>
                                <h2 className="text-lg font-semibold">Đơn hàng #{order.orderId}</h2>
                                <p className="text-sm text-gray-600">
                                    {new Date(order.createdAt).toLocaleString()}
                                </p>
                            </div>
                            <div className="flex items-center gap-4">
                                <span className={`px-3 py-1 rounded-full text-sm ${getStatusColor(order.status)}`}>
                                    {order.status}
                                </span>
                                <button
                                    onClick={() => handleViewDetails(order)}
                                    className="text-blue-600 hover:text-blue-800"
                                >
                                    Xem chi tiết
                                </button>
                            </div>
                        </div>

                        <div className="grid grid-cols-2 gap-4 mb-4">
                            <div>
                                <p className="text-sm text-gray-600">Khách hàng</p>
                                <p className="font-medium">{order.customerName}</p>
                                <p className="text-sm text-gray-600">{order.customerPhone}</p>
                            </div>
                            <div>
                                <p className="text-sm text-gray-600">Thanh toán</p>
                                <p className="font-medium">{order.paymentMethod}</p>
                            </div>
                        </div>

                        <div className="border-t pt-4">
                            <h3 className="font-semibold mb-2">Chi tiết đơn hàng</h3>
                            <div className="space-y-2">
                                {order.orderItems.map((item) => (
                                    <div key={item.productId} className="flex justify-between text-sm">
                                        <span>
                                            {item.productName} x {item.quantity}
                                        </span>
                                        <span className="font-medium">
                                            {(item.quantity * item.unitPrice).toLocaleString()} VND
                                        </span>
                                    </div>
                                ))}
                            </div>
                            <div className="mt-4 pt-4 border-t flex justify-between items-center">
                                <span className="font-semibold">Tổng cộng:</span>
                                <span className="text-lg font-bold text-green-600">
                                    {order.total.toLocaleString()} VND
                                </span>
                            </div>
                        </div>
                    </div>
                ))}
            </div>

            {isModalOpen && <OrderDetailModal />}
        </div>
    );
};

export default ManageOrdersPage;