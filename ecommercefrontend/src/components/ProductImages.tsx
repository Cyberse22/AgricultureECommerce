"use client"

import Image from "next/image";
import { useState } from "react";

type Props = {
    images: string[];
};

const ProductImages = ({ images }: Props) => {
    const [index, setIndex] = useState(0);

    if (!images || images.length === 0) {
        return <div className="text-gray-500">Không có hình ảnh</div>;
    }

    return (
        <div>
            <div className="h-[500px] relative">
                <Image
                    src={images[index]}
                    alt={`Ảnh sản phẩm ${index + 1}`}
                    fill
                    className="object-cover rounded-md"
                    sizes="50vw"
                />
            </div>
            <div className="flex justify-between gap-4 mt-8">
                {images.map((img, i) => (
                    <div
                        className="w-1/4 h-32 relative cursor-pointer"
                        key={i}
                        onClick={() => setIndex(i)}
                    >
                        <Image
                            src={img}
                            alt={`Ảnh ${i + 1}`}
                            fill
                            className="object-cover rounded-md"
                            sizes="30vw"
                        />
                    </div>
                ))}
            </div>
        </div>
    );
};

export default ProductImages;
