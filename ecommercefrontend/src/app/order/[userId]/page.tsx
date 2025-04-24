// app/checkout/page.tsx

import { useRouter } from 'next/router';

const CheckoutPage = () => {
  const router = useRouter();
  const { totalAmount } = router.query; // Lấy tổng số tiền từ query

  return (
    <div>
      <h1>Trang Thanh Toán</h1>
      <p>Tổng số tiền cần thanh toán: {totalAmount} VND</p>
      {/* Các bước thanh toán khác */}
    </div>
  );
};

export default CheckoutPage;
