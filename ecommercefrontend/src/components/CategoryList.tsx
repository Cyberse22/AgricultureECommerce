"use client"

import { Category } from "@/types/category";
import { categoryService } from "@/utils/APIs";
import Image from "next/image";
import Link from "next/link";
import { useEffect, useState } from "react";

const CategoryList = () => {
    const [groupedCategories, setGroupedCategories] = useState<Record<string, Category[]>>({});

    useEffect(() => {
        const fetchCategories = async () => {
            try {
                const res = await categoryService.getAll();
                const allCategories: Category[] = res.data;

                // Nhóm theo categoryParent
                const grouped: Record<string, Category[]> = {};
                allCategories.forEach((cat) => {
                    const parent = cat.categoryParent || "Khác";
                    if (!grouped[parent]) grouped[parent] = [];
                    grouped[parent].push(cat);
                });

                setGroupedCategories(grouped);
            } catch (err) {
                console.error("Failed to fetch categories", err);
            }
        };

        fetchCategories();
    }, []);

    return (
        <div className="px-4">
            {Object.entries(groupedCategories).map(([parentName, categories]) => (
                <div key={parentName} className="mb-10">
                    <h2 className="text-2xl font-bold mb-4">{parentName}</h2>
                    <div className="flex gap-4 md:gap-8 overflow-x-auto scrollbar-hide">
                        {categories.map((cat) => (
                            <Link
                                href={`/list?cat=${cat.categoryId}`}
                                key={cat.categoryId}
                                className="flex-shrink-0 w-full sm:w-1/2 lg:w-1/4 xl:w-1/6"
                            >
                                <div className="relative bg-slate-100 w-full h-96">
                                    <Image
                                        src={cat.categoryImage || "https://i.pinimg.com/736x/4b/6e/f9/4b6ef9efaa54f5d66ec5f67df0967dc4.jpg"}
                                        alt={cat.categoryName}
                                        fill
                                        sizes="20vw"
                                        className="object-cover"
                                    />
                                </div>
                                <h1 className="mt-8 font-light text-xl tracking-wide">{cat.categoryName}</h1>
                            </Link>
                        ))}
                    </div>
                </div>
            ))}
        </div>
    );
};

export default CategoryList;