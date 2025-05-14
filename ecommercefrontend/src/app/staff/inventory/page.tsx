"use client";

import { useEffect, useState } from "react";

interface Product {
    id: string;
    name: string;
    totalQuantity: number;
    availableQuantity: number;
    exportedQuantity: number;
}

const InventoryPage = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [selectedProduct, setSelectedProduct] = useState<Product | null>(null);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [importQuantity, setImportQuantity] = useState<number>(0);

    useEffect(() => {
        // Sample data
        setProducts([
            {
                id: "P001",
                name: "Thuốc Regen",
                totalQuantity: 1000,
                availableQuantity: 800,
                exportedQuantity: 200
            },
            {
                id: "P002",
                name: "Thuốc trừ sâu Regen X2",
                totalQuantity: 1000,
                availableQuantity: 800,
                exportedQuantity: 200
            },
            {
                id: "P003",
                name: "Trái cây theo mùa",
                totalQuantity: 75,
                availableQuantity: 60,
                exportedQuantity: 15
            },
            {
                id: "P004",
                name: "Mật ong rừng",
                totalQuantity: 40,
                availableQuantity: 25,
                exportedQuantity: 15
            },
            {
                id: "P005",
                name: "Hạt điều rang muối",
                totalQuantity: 60,
                availableQuantity: 45,
                exportedQuantity: 15
            },
        ]);
    }, []);

    const handleImport = (product: Product) => {
        setSelectedProduct(product);
        setImportQuantity(0);
        setIsModalOpen(true);
    };

    const handleConfirmImport = () => {
        if (selectedProduct && importQuantity > 0) {
            setProducts(products.map(product => 
                product.id === selectedProduct.id
                    ? {
                        ...product,
                        totalQuantity: product.totalQuantity + importQuantity,
                        availableQuantity: product.availableQuantity + importQuantity
                    }
                    : product
            ));
            setIsModalOpen(false);
        }
    };

    const ImportModal = () => {
        if (!selectedProduct) return null;

        return (
            <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
                <div className="bg-white rounded-lg p-6 w-96">
                    <div className="flex justify-between items-center mb-4">
                        <h2 className="text-xl font-bold">Nhập hàng</h2>
                        <button
                            onClick={() => setIsModalOpen(false)}
                            className="text-gray-500 hover:text-gray-700"
                        >
                            <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M6 18L18 6M6 6l12 12" />
                            </svg>
                        </button>
                    </div>

                    <div className="space-y-4">
                        <div>
                            <p className="text-sm text-gray-500">Sản phẩm</p>
                            <p className="font-medium">{selectedProduct.name}</p>
                        </div>

                        <div>
                            <p className="text-sm text-gray-500 mb-2">Số lượng hiện tại</p>
                            <div className="grid grid-cols-3 gap-4">
                                <div>
                                    <p className="text-xs text-gray-500">Tổng số lượng</p>
                                    <p className="font-medium">{selectedProduct.totalQuantity}</p>
                                </div>
                                <div>
                                    <p className="text-xs text-gray-500">Khả dụng</p>
                                    <p className="font-medium">{selectedProduct.availableQuantity}</p>
                                </div>
                                <div>
                                    <p className="text-xs text-gray-500">Đã xuất</p>
                                    <p className="font-medium">{selectedProduct.exportedQuantity}</p>
                                </div>
                            </div>
                        </div>

                        <div>
                            <label className="block text-sm text-gray-500 mb-2">
                                Số lượng nhập thêm
                            </label>
                            <input
                                type="number"
                                min="1"
                                value={importQuantity}
                                onChange={(e) => setImportQuantity(parseInt(e.target.value) || 0)}
                                className="w-full px-3 py-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-green-500"
                            />
                        </div>

                        <div className="flex justify-end pt-4">
                            <button
                                onClick={handleConfirmImport}
                                disabled={importQuantity <= 0}
                                className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600 disabled:bg-gray-300 disabled:cursor-not-allowed"
                            >
                                Xác nhận nhập hàng
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        );
    };

    return (
        <div className="p-6 max-w-6xl mx-auto space-y-6">
            <h1 className="text-2xl font-bold">Quản lý kho hàng</h1>
            
            <div className="overflow-x-auto">
                <table className="min-w-full bg-white border border-gray-200">
                    <thead>
                        <tr className="bg-gray-50">
                            <th className="px-6 py-3 border-b text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                Mã sản phẩm
                            </th>
                            <th className="px-6 py-3 border-b text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                Tên sản phẩm
                            </th>
                            <th className="px-6 py-3 border-b text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                Tổng số lượng
                            </th>
                            <th className="px-6 py-3 border-b text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                Số lượng khả dụng
                            </th>
                            <th className="px-6 py-3 border-b text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                Số lượng đã xuất
                            </th>
                            <th className="px-6 py-3 border-b text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                                Thao tác
                            </th>
                        </tr>
                    </thead>
                    <tbody className="divide-y divide-gray-200">
                        {products.map((product) => (
                            <tr key={product.id}>
                                <td className="px-6 py-4 whitespace-nowrap">
                                    {product.id}
                                </td>
                                <td className="px-6 py-4">
                                    {product.name}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap">
                                    {product.totalQuantity}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap">
                                    {product.availableQuantity}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap">
                                    {product.exportedQuantity}
                                </td>
                                <td className="px-6 py-4 whitespace-nowrap">
                                    <button
                                        onClick={() => handleImport(product)}
                                        className="text-green-600 hover:text-green-900"
                                    >
                                        Nhập hàng
                                    </button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>

            {isModalOpen && <ImportModal />}
        </div>
    );
};

export default InventoryPage;