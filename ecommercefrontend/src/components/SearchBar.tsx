"use client";

import { productService } from "@/utils/APIs";
import Image from "next/image";
import { useRouter } from "next/navigation";
import React, { useState } from "react";

const SearchBar = () => {
    const router = useRouter();
    const [keyword, setKeyword] = useState("");
    const [loading, setLoading] = useState(false);

    const handleSearch = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        if (!keyword.trim()) return;

        setLoading(true);

        try {
            // Gọi API (nếu cần), nhưng ở đây chủ yếu để điều hướng
            const res = await productService.search(keyword);
            console.log("Search result: ", res.data);

            // ✅ Route đến trang list kèm query
            router.push(`/list?name=${encodeURIComponent(keyword)}`);
        } catch (error) {
            console.error("Search error:", error);
        } finally {
            setLoading(false);
        }
    };

    return (
        <form
            className="flex items-center justify-between gap-4 bg-gray-100 p-2 rounded-md flex-1"
            onSubmit={handleSearch}
        >
            <input
                type="text"
                name="name"
                placeholder="Search"
                value={keyword}
                onChange={(e) => setKeyword(e.target.value)} // ✅ cập nhật keyword
                className="flex-1 bg-transparent outline-none"
            />
            <button type="submit" aria-label="Search" className="cursor-pointer">
                <Image src="/search.png" alt="" width={16} height={16} />
            </button>
        </form>
    );
};

export default SearchBar;