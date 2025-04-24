"use client";

import { productService, cartService } from "@/utils/APIs";
import { Product } from "@/types/product";
import { useEffect, useState } from "react";
import { useParams } from "next/navigation";
import Image from "next/image";
import { useAuth } from "@/utils/useAuth";
import { useCartDispatch } from "@/utils/useCart";

const ProductDetailPage = () => {
  const { productId } = useParams();
  const [product, setProduct] = useState<Product | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const { user } = useAuth();
  const cartDispatch = useCartDispatch();

  useEffect(() => {
    const fetchProduct = async () => {
      try {
        setLoading(true);
        const res = await productService.getById(productId as string);
        setProduct(res.data);
      } catch (err) {
        setError("Không thể tải thông tin sản phẩm.");
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    if (productId) fetchProduct();
  }, [productId]);

  const handleAddToCart = async () => {
    if (!user || !product) return;

    const item = {
      productId: product.productId,
      productName: product.productName,
      price: product.productPrice,
      quantity: 1,
    };

    try {
      await cartService.addItemsToCart(user.id, item);
      cartDispatch({ type: "addItem", payload: item });
      alert("✅ Đã thêm vào giỏ hàng!");
    } catch (err) {
      console.error("Lỗi khi thêm vào giỏ:", err);
      alert("❌ Thêm vào giỏ hàng thất bại.");
    }
  };

  if (loading) return <p className="mt-12 text-center">Đang tải...</p>;
  if (error) return <p className="mt-12 text-center text-red-500">{error}</p>;
  if (!product) return <p className="mt-12 text-center">Không tìm thấy sản phẩm.</p>;

  return (
    <div className="mt-12 max-w-5xl mx-auto flex flex-col md:flex-row gap-8 p-4">
      <div className="relative w-full md:w-1/2 h-[500px]">
        <Image
          src={product.productImage[0]}
          alt={product.productName}
          fill
          className="object-cover rounded-xl"
        />
      </div>
      <div className="flex-1 flex flex-col gap-4">
        <h1 className="text-3xl font-bold">{product.productName}</h1>
        <p className="text-gray-700">{product.productDescription}</p>
        <span className="text-2xl font-semibold text-green-600">{product.productPrice} VND</span>
        <button
          onClick={handleAddToCart}
          className="mt-4 w-max px-6 py-2 rounded-xl bg-blue-600 text-white hover:bg-blue-700"
        >
          Thêm vào giỏ hàng
        </button>
      </div>
    </div>
  );
};

export default ProductDetailPage;
