"use client";

import { Product } from "@/types/product";
import { productService } from "@/utils/APIs";
import Image from "next/image";
import Link from "next/link";
import { useEffect, useState } from "react";

interface ProductListProps {
    keyword?: string;
}

const ProductList: React.FC<ProductListProps> = ({ keyword }) => {
    const [products, setProducts] = useState<Product[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
    const [error, setError] = useState<string>("");

    useEffect(() => {
        fetchProducts();
    }, [keyword]);

    const fetchProducts = async () => {
        try {
            setLoading(true);
            const res = keyword
                ? await productService.search(keyword)
                : await productService.getAll();
            setProducts(res.data);
        } catch (err) {
            setError("Error fetching products");
            console.error(err);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="mt-12 flex flex-wrap gap-x-8 gap-y-16 justify-between">
            {loading && <p className="text-gray-500">Đang tải sản phẩm...</p>}
            {error && <p className="text-red-500">{error}</p>}

            {!loading && !error && products.length === 0 && (
                <p className="text-gray-500">Không có sản phẩm nào.</p>
            )}
            
            {!loading &&
                !error &&
                products.map((product) => (
                    <Link
                        key={product.productId}
                        href={`/product/${product.productId}`}
                        className="w-full flex flex-col gap-4 sm:w-[45%] lg:w-[22%]"
                    >
                        <div className="relative w-full h-80">
                            <Image
                                src={product.productImage[0]}
                                alt={product.productName}
                                fill
                                sizes="25vw"
                                className="absolute object-cover rounded-md z-10 hover:opacity-0 transition-opacity ease-in-out duration-500"
                            />
                            <Image
                                src={product.productImage[1]}
                                alt={product.productName}
                                fill
                                sizes="25vw"
                                className="absolute object-cover rounded-md z-0 opacity-0 hover:opacity-100 transition-opacity ease-in-out duration-500"
                            />
                        </div>
                        <div className="flex-col gap-1 flex">
                            <span className="font-medium">{product.productName}</span>
                            <span className="font-semibold">{product.productPrice} VND</span>
                        </div>
                        <div className="text-sm text-gray-500">{product.productDescription}</div>
                        <button className="rounded-2xl ring-1 ring-red-300 text-red-400 w-max py-2 px-4 text-xs hover:bg-red-500 hover:text-white">
                            Add To Cart
                        </button>
                    </Link>
                ))}
        </div>
    );
};

export default ProductList;
