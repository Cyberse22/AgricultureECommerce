"use client";

import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";

interface OrderItem {
    productId: string;
    productName: string;
    quantity: number;
    unitPrice: number;
    discount: number;
}

interface OrderData {
    customerId: string;
    customerName: string;
    customerPhone: string;
    paymentMethod: string;
    orderItems: OrderItem[];
}

const OrderSuccessPage = () => {
    const router = useRouter();
    const [orderData, setOrderData] = useState<OrderData | null>(null);

    useEffect(() => {
        // Get order data from localStorage
        const storedOrder = localStorage.getItem("pendingOrder");
        if (!storedOrder) {
            router.push("/");
            return;
        }

        setOrderData(JSON.parse(storedOrder));
    }, [router]);

    if (!orderData) {
        return <div className="p-6">Loading...</div>;
    }

    const total = orderData.orderItems.reduce(
        (sum, item) => sum + item.quantity * item.unitPrice,
        0
    );

    return (
        <div className="p-6 max-w-4xl mx-auto">
            {/* Success Message */}
            <div className="text-center mb-8">
                <div className="w-20 h-20 bg-green-100 rounded-full flex items-center justify-center mx-auto mb-4">
                    <svg
                        className="w-10 h-10 text-green-600"
                        fill="none"
                        stroke="currentColor"
                        viewBox="0 0 24 24"
                    >
                        <path
                            strokeLinecap="round"
                            strokeLinejoin="round"
                            strokeWidth={2}
                            d="M5 13l4 4L19 7"
                        />
                    </svg>
                </div>
                <h1 className="text-3xl font-bold text-green-600 mb-2">
                    Đặt hàng thành công!
                </h1>
                <p className="text-gray-600">
                    Cảm ơn bạn đã đặt hàng. Chúng tôi sẽ xử lý đơn hàng của bạn ngay.
                </p>
            </div>

            {/* Order Summary */}
            <div className="bg-white p-6 rounded-lg shadow-md mb-6">
                <h2 className="text-xl font-semibold mb-4">Thông tin đơn hàng</h2>
                <div className="grid grid-cols-2 gap-4 mb-6">
                    <div>
                        <p className="text-gray-600">Họ tên</p>
                        <p className="font-medium">{orderData.customerName}</p>
                    </div>
                    <div>
                        <p className="text-gray-600">Số điện thoại</p>
                        <p className="font-medium">{orderData.customerPhone}</p>
                    </div>
                    <div>
                        <p className="text-gray-600">Phương thức thanh toán</p>
                        <p className="font-medium">{orderData.paymentMethod}</p>
                    </div>
                    <div>
                        <p className="text-gray-600">Tổng tiền</p>
                        <p className="font-medium text-green-600">
                            {total.toLocaleString()} VND
                        </p>
                    </div>
                </div>

                <div className="border-t pt-4">
                    <h3 className="font-semibold mb-3">Chi tiết sản phẩm</h3>
                    <div className="space-y-3">
                        {orderData.orderItems.map((item) => (
                            <div
                                key={item.productId}
                                className="flex justify-between items-center"
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
                </div>
            </div>

            {/* Action Buttons */}
            <div className="flex justify-center gap-4">
                <button
                    onClick={() => router.push("/")}
                    className="bg-green-600 text-white px-8 py-3 rounded-lg hover:bg-green-700 transition-colors"
                >
                    Tiếp tục mua sắm
                </button>
                <button
                    onClick={() => router.push("/order")}
                    className="bg-gray-200 text-gray-800 px-8 py-3 rounded-lg hover:bg-gray-300 transition-colors"
                >
                    Xem đơn hàng
                </button>
            </div>
        </div>
    );
};

export default OrderSuccessPage; 