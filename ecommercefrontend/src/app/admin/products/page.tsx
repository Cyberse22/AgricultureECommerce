"use client";

import Button from "@/components/admin/Button";
import { Card, CardContent } from "@/components/admin/Card";
import { Dialog, DialogContent, DialogHeader, DialogTitle } from "@/components/admin/Dialog";
import { Input } from "@/components/admin/Input";
import { Textarea } from "@/components/admin/Textarea";
import Image from "next/image";
import { useState } from "react";

interface Product {
  id: string;
  name: string;
  description: string;
  price: number;
  stock: number;
  category: string;
  image: string;
  isActive: boolean;
}

const ProductsPage = () => {
  const [products, setProducts] = useState<Product[]>([
    {
      id: "P001",
      name: "Thuốc Regen",
      description: "Thuốc trừ sâu Regen",
      price: 200000,
      stock: 1000,
      category: "Regen",
      image: "/images/vegetables.jpg",
      isActive: true,
    },
    {
      id: "P002",
      name: "Thuốc trừ sâu Regen X2",
      description: "Thuốc loại X2",
      price: 300000,
      stock: 1000,
      category: "Regen",
      image: "/images/fruits.jpg",
      isActive: true,
    },
  ]);

  const [selectedProduct, setSelectedProduct] = useState<Product | null>(null);
  const [dialogOpen, setDialogOpen] = useState(false);
  const [addProductDialogOpen, setAddProductDialogOpen] = useState(false);
  const [newProduct, setNewProduct] = useState<Partial<Product>>({
    name: "",
    description: "",
    price: 0,
    stock: 0,
    category: "",
    image: "",
    isActive: true,
  });

  const handleView = (product: Product) => {
    setSelectedProduct(product);
    setDialogOpen(true);
  };

  const toggleActive = (id: string) => {
    // TODO: Implement product activation/deactivation
    console.log(`Toggle product ${id} active status`);
  };

  const handleAddProduct = () => {
    // TODO: Implement add product logic
    console.log("Adding new product:", newProduct);
    setAddProductDialogOpen(false);
    setNewProduct({
      name: "",
      description: "",
      price: 0,
      stock: 0,
      category: "",
      image: "",
      isActive: true,
    });
  };

  return (
    <div className="p-6 max-w-6xl mx-auto space-y-6">
      <Card>
        <CardContent className="p-4 space-y-4">
          <div className="flex justify-between items-center">
            <h2 className="text-xl font-semibold">Quản lý sản phẩm</h2>
            <Button variant="default" onClick={() => setAddProductDialogOpen(true)}>
              Thêm sản phẩm
            </Button>
          </div>
          <div className="space-y-2">
            {products.map((product) => (
              <div
                key={product.id}
                className="flex justify-between items-center border rounded p-3"
              >
                <div className="flex gap-4 items-center">
                  <Image
                    src={product.image}
                    alt={product.name}
                    width={60}
                    height={60}
                    className="rounded object-cover"
                  />
                  <div>
                    <p className="font-medium">{product.name}</p>
                    <p className="text-sm text-gray-500">
                      ID: {product.id} - {product.category}
                    </p>
                    <p className="text-sm text-gray-500">
                      Giá: {product.price.toLocaleString('vi-VN')}đ - Tồn kho: {product.stock}
                    </p>
                  </div>
                </div>
                <div className="flex gap-2">
                  <Button variant="default" onClick={() => handleView(product)}>
                    Xem chi tiết
                  </Button>
                  <Button
                    variant={product.isActive ? "destructive" : "outline"}
                    onClick={() => toggleActive(product.id)}
                  >
                    {product.isActive ? "Khóa" : "Mở khóa"}
                  </Button>
                </div>
              </div>
            ))}
          </div>
        </CardContent>
      </Card>

      <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
        <DialogContent>
          <DialogHeader>
            <DialogTitle>Thông tin sản phẩm</DialogTitle>
          </DialogHeader>
          {selectedProduct && (
            <div className="space-y-3">
              <Image
                src={selectedProduct.image}
                alt={selectedProduct.name}
                width={200}
                height={200}
                className="rounded object-cover mx-auto"
              />
              <p>
                <strong>ID:</strong> {selectedProduct.id}
              </p>
              <p>
                <strong>Tên:</strong> {selectedProduct.name}
              </p>
              <p>
                <strong>Mô tả:</strong> {selectedProduct.description}
              </p>
              <p>
                <strong>Giá:</strong> {selectedProduct.price.toLocaleString('vi-VN')}đ
              </p>
              <p>
                <strong>Tồn kho:</strong> {selectedProduct.stock}
              </p>
              <p>
                <strong>Danh mục:</strong> {selectedProduct.category}
              </p>
              <p>
                <strong>Trạng thái:</strong> {selectedProduct.isActive ? "Đang bán" : "Đã ẩn"}
              </p>
            </div>
          )}
        </DialogContent>
      </Dialog>

      <Dialog open={addProductDialogOpen} onOpenChange={setAddProductDialogOpen}>
        <DialogContent>
          <DialogHeader>
            <DialogTitle>Thêm sản phẩm mới</DialogTitle>
          </DialogHeader>
          <div className="space-y-4">
            <div>
              <label className="block text-sm font-medium mb-1">Tên sản phẩm</label>
              <Input
                value={newProduct.name}
                onChange={(e) => setNewProduct({ ...newProduct, name: e.target.value })}
                placeholder="Nhập tên sản phẩm"
              />
            </div>
            <div>
              <label className="block text-sm font-medium mb-1">Mô tả</label>
              <Textarea
                value={newProduct.description}
                onChange={(e) => setNewProduct({ ...newProduct, description: e.target.value })}
                placeholder="Nhập mô tả sản phẩm"
              />
            </div>
            <div className="grid grid-cols-2 gap-4">
              <div>
                <label className="block text-sm font-medium mb-1">Giá</label>
                <Input
                  type="number"
                  value={newProduct.price}
                  onChange={(e) => setNewProduct({ ...newProduct, price: Number(e.target.value) })}
                  placeholder="Nhập giá"
                />
              </div>
              <div>
                <label className="block text-sm font-medium mb-1">Tồn kho</label>
                <Input
                  type="number"
                  value={newProduct.stock}
                  onChange={(e) => setNewProduct({ ...newProduct, stock: Number(e.target.value) })}
                  placeholder="Nhập số lượng"
                />
              </div>
            </div>
            <div>
              <label className="block text-sm font-medium mb-1">Danh mục</label>
              <Input
                value={newProduct.category}
                onChange={(e) => setNewProduct({ ...newProduct, category: e.target.value })}
                placeholder="Nhập danh mục"
              />
            </div>
            <div>
              <label className="block text-sm font-medium mb-1">Hình ảnh</label>
              <Input
                value={newProduct.image}
                onChange={(e) => setNewProduct({ ...newProduct, image: e.target.value })}
                placeholder="Nhập URL hình ảnh"
              />
            </div>
            <div className="flex justify-end gap-2">
              <Button variant="destructive" onClick={() => setAddProductDialogOpen(false)}>
                Hủy
              </Button>
              <Button variant="default" onClick={handleAddProduct}>
                Thêm sản phẩm
              </Button>
            </div>
          </div>
        </DialogContent>
      </Dialog>
    </div>
  );
};

export default ProductsPage;
