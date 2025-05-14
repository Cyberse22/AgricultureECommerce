"use client";

import { CartItem } from "@/types/cart";
import { Product } from "@/types/product";
import { cartService, productService } from "@/utils/APIs";
import { useAuth } from "@/utils/useAuth";
import Image from "next/image";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";

const CartModal = () => {
    const { user } = useAuth();
    const router = useRouter();
    const [cartItems, setCartItems] = useState<CartItem[]>([]);
    const [productMap, setProductMap] = useState<Record<string, Product>>({});
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        fetchCart();
    }, [user]);

    const fetchCart = async () => {
        if (!user) return;

        try {
            setLoading(true);
            const res = await cartService.getUserCartItems(user.id);
            const items: CartItem[] = res.data;
            setCartItems(items);

            const productResponses = await Promise.all(
                items.map((item) => productService.getById(item.productId))
            );

            const productData: Record<string, Product> = {};
            productResponses.forEach((res) => {
                const product: Product = res.data;
                productData[product.productId] = product;
            });

            setProductMap(productData);
        } catch (err) {
            console.error("Error loading cart or products:", err);
        } finally {
            setLoading(false);
        }
    };

    const handleDeleteItem = async (productId: string) => {
        if (!user) return;

        try {
            await cartService.deleteCartItems({ userId: user.id, productId });
            setCartItems(cartItems.filter((item) => item.productId !== productId));
        } catch (err) {
            console.error("Error deleting item:", err);
        }
    };

    const handleClearCart = async () => {
        if (!user) return;

        try {
            await cartService.clearCart(user.id);
            setCartItems([]);
        } catch (err) {
            console.error("Error clearing cart:", err);
        }
    };

    const handlePayment = () => {
        if (!user) {
            router.push("/login");
            return;
        }

        // Create sample order data
        const orderData = {
            customerId: user.id,
            customerName: user.firstName + " " + user.lastName,
            customerPhone: user.phoneNumber || "",
            paymentMethod: "Tiền mặt",
            orderItems: cartItems.map(item => ({
                productId: item.productId,
                productName: item.productName,
                quantity: item.quantity,
                unitPrice: item.price,
                discount: 0
            }))
        };

        // Store order data in localStorage for the order page
        localStorage.setItem("pendingOrder", JSON.stringify(orderData));
        router.push("/order");
    };

    const total = cartItems.reduce((sum, item) => sum + item.quantity * item.price, 0);

    return (
        <div className="absolute right-0 mt-2 w-96 bg-white p-6 rounded-xl shadow-xl z-50">
            <h2 className="text-xl font-bold mb-4">🛒 Giỏ hàng của bạn</h2>

            {loading ? (
                <p>Đang tải...</p>
            ) : cartItems.length === 0 ? (
                <p>Không có sản phẩm nào trong giỏ hàng.</p>
            ) : (
                <div className="flex flex-col gap-6 max-h-[400px] overflow-y-auto">
                    {cartItems.map((item) => {
                        const product = productMap[item.productId];
                        return (
                            <div
                                key={item.productId}
                                className="flex items-start gap-4 border-b pb-4"
                            >
                                <Image
                                    src={product?.productImage?.[0] || "/placeholder.png"}
                                    alt={item.productName}
                                    width={72}
                                    height={96}
                                    className="object-cover rounded-md"
                                />
                                <div className="flex-1">
                                    <h3 className="font-semibold text-lg">{item.productName}</h3>
                                    <p className="text-sm text-gray-600">Số lượng: {item.quantity}</p>
                                    <p className="text-sm text-gray-600">Giá: {item.price} VND</p>
                                    <p className="text-sm font-medium mt-1">
                                        Tổng: {item.quantity * item.price} VND
                                    </p>
                                </div>
                                <button
                                    onClick={() => handleDeleteItem(item.productId)}
                                    className="text-sm px-3 py-1 bg-red-500 text-white rounded hover:bg-red-600"
                                >
                                    Xóa
                                </button>
                            </div>
                        );
                    })}
                </div>
            )}

            {cartItems.length > 0 && (
                <>
                    <div className="flex justify-between items-center mt-6">
                        <span className="text-lg font-semibold">Tổng cộng:</span>
                        <span className="text-xl font-bold text-green-600">{total} VND</span>
                    </div>
                    <div className="flex justify-between mt-4 gap-4">
                        <button
                            onClick={handleClearCart}
                            className="flex-1 bg-gray-200 text-gray-800 py-2 rounded hover:bg-gray-300"
                        >
                            Xóa tất cả
                        </button>
                        <button
                            onClick={handlePayment}
                            className="flex-1 bg-blue-600 text-white py-2 rounded hover:bg-blue-700"
                        >
                            Thanh toán
                        </button>
                    </div>
                </>
            )}
        </div>
    );
}

export default CartModal;
