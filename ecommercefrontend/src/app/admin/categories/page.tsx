"use client";

import Button from "@/components/admin/Button";
import { Card, CardContent } from "@/components/admin/Card";
import { Dialog, DialogContent, DialogHeader, DialogTitle } from "@/components/admin/Dialog";
import { Category } from "@/types/category";
import Image from "next/image";
import { useState } from "react";

const CategoriesPage = () => {
  const [categories] = useState<Category[]>([
    {
      categoryId: "Cate1",
      categoryName: "Regen",
      categoryDescription: "Thuốc trừ sâu hãng Regen",
      categoryParent: "Brand",
      categoryImage: "/images/vegetables.jpg",
    },
    {
      categoryId: "Cate2",
      categoryName: "Pesticide",
      categoryDescription: "Thuốc trừ sâu",
      categoryParent: "Pesticides",
      categoryImage: "/images/fruits.jpg",
    },
  ]);

  const [selectedCategory, setSelectedCategory] = useState<Category | null>(null);
  const [dialogOpen, setDialogOpen] = useState(false);
  const [lockedCategories, setLockedCategories] = useState<string[]>([]);

  const handleView = (category: Category) => {
    setSelectedCategory(category);
    setDialogOpen(true);
  };

  const toggleLock = (id: string) => {
    setLockedCategories((prev) =>
      prev.includes(id) ? prev.filter((c) => c !== id) : [...prev, id]
    );
  };

  return (
    <div className="p-6 max-w-6xl mx-auto space-y-6">
      <Card>
        <CardContent className="p-4 space-y-4">
          <h2 className="text-xl font-semibold">Quản lý danh mục</h2>
          <div className="space-y-2">
            {categories.map((cat) => (
              <div
                key={cat.categoryId}
                className="flex justify-between items-center border rounded p-3"
              >
                <div className="flex gap-4 items-center">
                  <Image
                    src={cat.categoryImage}
                    alt={cat.categoryName}
                    width={60}
                    height={60}
                    className="rounded object-cover"
                  />
                  <div>
                    <p className="font-medium">{cat.categoryName}</p>
                    <p className="text-sm text-gray-500">
                      ID: {cat.categoryId} - {cat.categoryDescription}
                    </p>
                  </div>
                </div>
                <div className="flex gap-2">
                  <Button variant="default" onClick={() => handleView(cat)}>
                    Xem chi tiết
                  </Button>
                  <Button
                    variant="destructive"
                    onClick={() => toggleLock(cat.categoryId)}
                  >
                    {lockedCategories.includes(cat.categoryId)
                      ? "Mở khóa"
                      : "Khóa"}
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
            <DialogTitle>Thông tin danh mục</DialogTitle>
          </DialogHeader>
          {selectedCategory && (
            <div className="space-y-3">
              <Image
                src={selectedCategory.categoryImage}
                alt={selectedCategory.categoryName}
                width={200}
                height={200}
                className="rounded object-cover mx-auto"
              />
              <p>
                <strong>ID:</strong> {selectedCategory.categoryId}
              </p>
              <p>
                <strong>Tên:</strong> {selectedCategory.categoryName}
              </p>
              <p>
                <strong>Mô tả:</strong> {selectedCategory.categoryDescription}
              </p>
              <p>
                <strong>Danh mục cha:</strong> {selectedCategory.categoryParent || "Không có"}
              </p>
            </div>
          )}
        </DialogContent>
      </Dialog>
    </div>
  );
};

export default CategoriesPage;
