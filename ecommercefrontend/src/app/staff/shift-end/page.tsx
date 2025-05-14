"use client";

import { useState } from "react";

interface PaymentSummary {
    method: string;
    count: number;
    total: number;
}

interface ShiftSummary {
    startTime: string;
    endTime: string;
    totalOrders: number;
    totalAmount: number;
    payments: PaymentSummary[];
}

const ShiftEndPage = () => {
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [shiftSummary, setShiftSummary] = useState<ShiftSummary>({
        startTime: "2024-03-20 08:00:00",
        endTime: "2024-03-20 16:00:00",
        totalOrders: 2,
        totalAmount: 1230000,
        payments: [
            {
                method: "Tiền mặt",
                count: 2,
                total: 500000
            },
            {
                method: "Thẻ tín dụng",
                count: 3,
                total: 730000
            }
        ]
    });

    const formatDateTime = (dateTimeStr: string) => {
        const date = new Date(dateTimeStr);
        return date.toLocaleString('vi-VN', {
            year: 'numeric',
            month: '2-digit',
            day: '2-digit',
            hour: '2-digit',
            minute: '2-digit'
        });
    };

    const formatCurrency = (amount: number) => {
        return amount.toLocaleString('vi-VN') + 'đ';
    };

    const ShiftSummaryModal = () => {
        return (
            <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
                <div className="bg-white rounded-lg p-6 w-[600px]">
                    <div className="flex justify-between items-center mb-6">
                        <h2 className="text-xl font-bold">Chi tiết kết ca</h2>
                        <button
                            onClick={() => setIsModalOpen(false)}
                            className="text-gray-500 hover:text-gray-700"
                        >
                            <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M6 18L18 6M6 6l12 12" />
                            </svg>
                        </button>
                    </div>

                    <div className="space-y-6">
                        <div className="grid grid-cols-2 gap-4">
                            <div>
                                <p className="text-sm text-gray-500">Thời gian bắt đầu</p>
                                <p className="font-medium">{formatDateTime(shiftSummary.startTime)}</p>
                            </div>
                            <div>
                                <p className="text-sm text-gray-500">Thời gian kết thúc</p>
                                <p className="font-medium">{formatDateTime(shiftSummary.endTime)}</p>
                            </div>
                        </div>

                        <div className="border-t pt-4">
                            <h3 className="text-lg font-semibold mb-4">Tổng quan</h3>
                            <div className="grid grid-cols-2 gap-4">
                                <div>
                                    <p className="text-sm text-gray-500">Tổng số đơn hàng</p>
                                    <p className="text-xl font-bold">{shiftSummary.totalOrders}</p>
                                </div>
                                <div>
                                    <p className="text-sm text-gray-500">Tổng doanh thu</p>
                                    <p className="text-xl font-bold">{formatCurrency(shiftSummary.totalAmount)}</p>
                                </div>
                            </div>
                        </div>

                        <div className="border-t pt-4">
                            <h3 className="text-lg font-semibold mb-4">Chi tiết thanh toán</h3>
                            <div className="space-y-4">
                                {shiftSummary.payments.map((payment, index) => (
                                    <div key={index} className="flex justify-between items-center p-3 bg-gray-50 rounded-lg">
                                        <div>
                                            <p className="font-medium">{payment.method}</p>
                                            <p className="text-sm text-gray-500">{payment.count} đơn hàng</p>
                                        </div>
                                        <p className="font-bold">{formatCurrency(payment.total)}</p>
                                    </div>
                                ))}
                            </div>
                        </div>

                        <div className="border-t pt-4">
                            <div className="flex justify-end space-x-4">
                                <button
                                    onClick={() => setIsModalOpen(false)}
                                    className="px-4 py-2 border border-gray-300 rounded hover:bg-gray-50"
                                >
                                    Đóng
                                </button>
                                <button
                                    onClick={() => {
                                        // TODO: Implement shift end confirmation
                                        setIsModalOpen(false);
                                    }}
                                    className="px-4 py-2 bg-green-500 text-white rounded hover:bg-green-600"
                                >
                                    Xác nhận kết ca
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    };

    return (
        <div className="p-6 max-w-6xl mx-auto space-y-6">
            <h1 className="text-2xl font-bold">Kết ca</h1>

            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
                <div className="bg-white p-6 rounded-lg shadow">
                    <p className="text-sm text-gray-500">Thời gian bắt đầu</p>
                    <p className="text-lg font-semibold">{formatDateTime(shiftSummary.startTime)}</p>
                </div>

                <div className="bg-white p-6 rounded-lg shadow">
                    <p className="text-sm text-gray-500">Tổng số đơn hàng</p>
                    <p className="text-lg font-semibold">{shiftSummary.totalOrders}</p>
                </div>

                <div className="bg-white p-6 rounded-lg shadow">
                    <p className="text-sm text-gray-500">Tổng doanh thu</p>
                    <p className="text-lg font-semibold">{formatCurrency(shiftSummary.totalAmount)}</p>
                </div>

                <div className="bg-white p-6 rounded-lg shadow">
                    <button
                        onClick={() => setIsModalOpen(true)}
                        className="w-full py-2 bg-green-500 text-white rounded hover:bg-green-600"
                    >
                        Xem chi tiết
                    </button>
                </div>
            </div>

            {isModalOpen && <ShiftSummaryModal />}
        </div>
    );
};

export default ShiftEndPage;