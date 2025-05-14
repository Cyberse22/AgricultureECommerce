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

interface OrderData {
    customerId: string;
    customerName: string;
    customerPhone: string;
    paymentMethod: string;
    orderItems: OrderItem[];
}

const OrderPage = () => {
    const { user } = useAuth();
    const router = useRouter();
    const [orderData, setOrderData] = useState<OrderData | null>(null);
    const [selectedPaymentMethod, setSelectedPaymentMethod] = useState("Tiền mặt");
    const [isSubmitting, setIsSubmitting] = useState(false);

    useEffect(() => {
        // Check if user is logged in
        if (!user) {
            router.push("/login");
            return;
        }

        // Get order data from localStorage
        const storedOrder = localStorage.getItem("pendingOrder");
        if (!storedOrder) {
            router.push("/");
            return;
        }

        setOrderData(JSON.parse(storedOrder));
    }, [user, router]);

    const handlePaymentMethodChange = (method: string) => {
        setSelectedPaymentMethod(method);
        if (orderData) {
            setOrderData({
                ...orderData,
                paymentMethod: method
            });
        }
    };

    const handleConfirmOrder = async () => {
        if (!orderData || isSubmitting) return;

        try {
            setIsSubmitting(true);
            // TODO: Call API to create order
            console.log("Creating order:", orderData);
            
            // First redirect to success page
            router.push("/order/success");
            
            // Then clear cart and localStorage after a short delay
            setTimeout(() => {
                localStorage.removeItem("pendingOrder");
            }, 100);
        } catch (error) {
            console.error("Error creating order:", error);
            setIsSubmitting(false);
        }
    };

    if (!orderData) {
        return <div className="p-6">Loading...</div>;
    }

    const total = orderData.orderItems.reduce(
        (sum, item) => sum + item.quantity * item.unitPrice,
        0
    );

    return (
        <div className="p-6 max-w-4xl mx-auto">
            <h1 className="text-2xl font-bold mb-6">Xác nhận đơn hàng</h1>

            {/* Customer Information */}
            <div className="bg-white p-6 rounded-lg shadow-md mb-6">
                <h2 className="text-xl font-semibold mb-4">Thông tin khách hàng</h2>
                <div className="grid grid-cols-2 gap-4">
                    <div>
                        <p className="text-gray-600">Họ tên</p>
                        <p className="font-medium">{orderData.customerName}</p>
                    </div>
                    <div>
                        <p className="text-gray-600">Số điện thoại</p>
                        <p className="font-medium">{orderData.customerPhone}</p>
                    </div>
                </div>
            </div>

            {/* Order Items */}
            <div className="bg-white p-6 rounded-lg shadow-md mb-6">
                <h2 className="text-xl font-semibold mb-4">Chi tiết đơn hàng</h2>
                <div className="space-y-4">
                    {orderData.orderItems.map((item) => (
                        <div key={item.productId} className="flex justify-between items-center border-b pb-4">
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
                        <p className="text-lg font-semibold">Tổng cộng:</p>
                        <p className="text-xl font-bold text-green-600">
                            {total.toLocaleString()} VND
                        </p>
                    </div>
                </div>
            </div>

            {/* Payment Method */}
            <div className="bg-white p-6 rounded-lg shadow-md mb-6">
                <h2 className="text-xl font-semibold mb-4">Phương thức thanh toán</h2>
                <div className="space-y-4">
                    <label className="flex items-center space-x-3">
                        <input
                            type="radio"
                            name="payment"
                            value="Tiền mặt"
                            checked={selectedPaymentMethod === "Tiền mặt"}
                            onChange={() => handlePaymentMethodChange("Tiền mặt")}
                            className="form-radio h-5 w-5 text-green-600"
                        />
                        <span>Tiền mặt</span>
                    </label>
                    <label className="flex items-center space-x-3">
                        <input
                            type="radio"
                            name="payment"
                            value="Thẻ tín dụng"
                            checked={selectedPaymentMethod === "Thẻ tín dụng"}
                            onChange={() => handlePaymentMethodChange("Thẻ tín dụng")}
                            className="form-radio h-5 w-5 text-green-600"
                        />
                        <span>Thẻ tín dụng</span>
                    </label>
                </div>
            </div>

            {/* Confirm Button */}
            <div className="flex justify-end">
                <button
                    onClick={handleConfirmOrder}
                    disabled={isSubmitting}
                    className={`bg-green-600 text-white px-8 py-3 rounded-lg transition-colors ${
                        isSubmitting ? "opacity-50 cursor-not-allowed" : "hover:bg-green-700"
                    }`}
                >
                    {isSubmitting ? "Đang xử lý..." : "Xác nhận đơn hàng"}
                </button>
            </div>
        </div>
    );
};

export default OrderPage;