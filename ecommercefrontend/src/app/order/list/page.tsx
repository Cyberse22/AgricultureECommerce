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
    orderItems: OrderItem[];
    status: "Pending" | "Confirmed" | "Delivered" | "Cancelled";
    createdAt: string;
    total: number;
}

const OrderPage = () => {
    const { user } = useAuth();
    const router = useRouter();
    const [orders, setOrders] = useState<Order[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        if (!user) {
            router.push("/login");
            return;
        }

        // TODO: Replace with actual API call
        // Mock data for demonstration
        const mockOrders: Order[] = [
            {
                orderId: "ORD001",
                customerId: user.id,
                customerName: user.firstName + " " + user.lastName,
                customerPhone: user.phoneNumber || "",
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
                customerId: user.id,
                customerName: user.firstName + " " + user.lastName,
                customerPhone: user.phoneNumber || "",
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
            },
        ];

        setOrders(mockOrders);
        setLoading(false);
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

    const getStatusText = (status: Order["status"]) => {
        switch (status) {
            case "Pending":
                return "Chờ xác nhận";
            case "Confirmed":
                return "Đã xác nhận";
            case "Delivered":
                return "Đã giao hàng";
            case "Cancelled":
                return "Đã hủy";
            default:
                return status;
        }
    };

    if (loading) {
        return <div className="p-6">Loading...</div>;
    }

    return (
        <div className="p-6 max-w-6xl mx-auto">
            <h1 className="text-2xl font-bold mb-6">Lịch sử đơn hàng</h1>

            {orders.length === 0 ? (
                <div className="text-center py-8">
                    <p className="text-gray-600">Bạn chưa có đơn hàng nào.</p>
                </div>
            ) : (
                <div className="space-y-6">
                    {orders.map((order) => (
                        <div key={order.orderId} className="bg-white rounded-lg shadow-md overflow-hidden">
                            <div className="p-6">
                                <div className="flex justify-between items-start mb-4">
                                    <div>
                                        <h2 className="text-lg font-semibold">Đơn hàng #{order.orderId}</h2>
                                        <p className="text-sm text-gray-600">
                                            {new Date(order.createdAt).toLocaleString("vi-VN")}
                                        </p>
                                    </div>
                                    <span
                                        className={`px-3 py-1 rounded-full text-sm font-medium ${getStatusColor(
                                            order.status
                                        )}`}
                                    >
                                        {getStatusText(order.status)}
                                    </span>
                                </div>

                                <div className="space-y-4">
                                    {order.orderItems.map((item) => (
                                        <div
                                            key={item.productId}
                                            className="flex justify-between items-center border-b pb-4 last:border-0"
                                        >
                                            <div>
                                                <p className="font-medium">{item.productName}</p>
                                                <p className="text-sm text-gray-600">
                                                    {item.quantity} x {item.unitPrice.toLocaleString()} VND
                                                </p>
                                            </div>
                                            <p className="font-medium">
                                                {(item.quantity * item.unitPrice).toLocaleString()} VND
                                            </p>
                                        </div>
                                    ))}
                                </div>

                                <div className="mt-4 pt-4 border-t">
                                    <div className="flex justify-between items-center">
                                        <div>
                                            <p className="text-sm text-gray-600">Phương thức thanh toán</p>
                                            <p className="font-medium">{order.paymentMethod}</p>
                                        </div>
                                        <div className="text-right">
                                            <p className="text-sm text-gray-600">Tổng cộng</p>
                                            <p className="text-lg font-bold text-green-600">
                                                {order.total.toLocaleString()} VND
                                            </p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    ))}
                </div>
            )}
        </div>
    );
};

export default OrderPage;