"use client"; // Đảm bảo component là client-side

import { useRouter } from "next/router"; // Import đúng
import { useEffect, useState } from "react";

const CheckoutPage = () => {
  const router = useRouter();
  const [totalAmount, setTotalAmount] = useState<string | null>(null);

  useEffect(() => {
    // Chỉ set giá trị totalAmount khi trang đã được render
    if (router.query.totalAmount) {
      setTotalAmount(router.query.totalAmount as string);
    }
  }, [router.query.totalAmount]);

  if (!totalAmount) {
    return <p>Không có thông tin thanh toán. Vui lòng quay lại giỏ hàng.</p>;
  }

  return (
    <div>
      <h1>Trang Thanh Toán</h1>
      <p>Tổng số tiền cần thanh toán: {totalAmount} VND</p>
      {/* Các bước thanh toán khác */}
    </div>
  );
};

export default CheckoutPage;
