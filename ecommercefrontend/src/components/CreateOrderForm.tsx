"use client";

import { useState } from "react";
import toast from "react-hot-toast";
import { PaymentMethod } from "@/types/paymentMethod";
import { CreateOrderModel, orderService } from "@/utils/APIs";
import { useCart, useCartDispatch } from "@/utils/useCart";

// Danh sách lựa chọn thanh toán
const paymentOptions = [
    { label: "Tiền mặt", value: PaymentMethod.Cash },
    { label: "Ví MoMo", value: PaymentMethod.Momo },
    { label: "ZaloPay", value: PaymentMethod.ZaloPay },
];

const CreateOrderForm = () => {
    const { cartItems } = useCart();
    const dispatch = useCartDispatch();

    const [form, setForm] = useState({
        customerName: "",
        customerPhone: "",
        paymentMethod: PaymentMethod.Cash,
    });

    const [isSubmitting, setIsSubmitting] = useState(false);

    const handleChange = (
        e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
    ) => {
        const { name, value } = e.target;
        setForm((prev) => ({ ...prev, [name]: value }));
    };

    const handleSubmit = async () => {
    const { customerName, customerPhone, paymentMethod } = form;

    if (!customerName || !customerPhone) {
        toast.error("Vui lòng điền đầy đủ thông tin.");
        return;
    }

    if (cartItems.length === 0) {
        toast.error("Giỏ hàng đang trống.");
        return;
    }

    const orderData: CreateOrderModel = {
        customerName,
        customerPhone,
        paymentMethod: paymentMethod as PaymentMethod,
        orderItems: cartItems.map((item) => ({
            productId: item.productId,
            productName: item.productName,
            quantity: item.quantity,
            unitPrice: item.price,
            discount: 0,
        })),
    };

        try {
        setIsSubmitting(true);
        await orderService.create(orderData);
        toast.success("🎉 Đặt hàng thành công!");
        dispatch({ type: "clearCart" });
        setForm({
            customerName: "",
            customerPhone: "",
            paymentMethod: PaymentMethod.Cash,
        });
        } catch (error) {
        console.error("❌ Đặt hàng thất bại:", error);
        toast.error("Đặt hàng thất bại. Vui lòng thử lại.");
        } finally {
        setIsSubmitting(false);
        }
    };

    return (
        <div className="bg-white p-6 rounded-xl shadow-md space-y-4 max-w-md mx-auto">
        <h2 className="text-2xl font-bold text-center">Thông tin đặt hàng</h2>

        <div>
            <label className="block mb-1 font-medium">Tên khách hàng</label>
            <input
            name="customerName"
            value={form.customerName}
            onChange={handleChange}
            className="w-full p-3 border rounded-xl focus:outline-none focus:ring-2 focus:ring-green-500"
            placeholder="Nhập tên"
            />
        </div>

        <div>
            <label className="block mb-1 font-medium">Số điện thoại</label>
            <input
            name="customerPhone"
            value={form.customerPhone}
            onChange={handleChange}
            className="w-full p-3 border rounded-xl focus:outline-none focus:ring-2 focus:ring-green-500"
            placeholder="Nhập số điện thoại"
            />
        </div>

        <div>
            <label className="block mb-1 font-medium">Phương thức thanh toán</label>
            <select
                title="Phương thức thanh toán"
            name="paymentMethod"
            value={form.paymentMethod}
            onChange={handleChange}
            className="w-full p-3 border rounded-xl focus:outline-none focus:ring-2 focus:ring-green-500"
            >
            {paymentOptions.map((opt) => (
                <option key={opt.value} value={opt.value}>
                {opt.label}
                </option>
            ))}
            </select>
        </div>

        <button
            onClick={handleSubmit}
            disabled={isSubmitting}
            className="w-full bg-green-600 text-white p-3 rounded-xl hover:bg-green-700 transition disabled:opacity-50"
        >
            {isSubmitting ? "Đang xử lý..." : "Xác nhận đặt hàng"}
        </button>
        </div>
    );
};

export default CreateOrderForm;
