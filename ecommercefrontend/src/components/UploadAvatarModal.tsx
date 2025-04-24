"use client";

import { useState } from "react";
import { userService } from "@/utils/APIs";

interface Props {
  isOpen: boolean;
  onClose: () => void;
}

const UploadAvatarModal = ({ isOpen, onClose }: Props) => {
  const [file, setFile] = useState<File | null>(null);
  const [loading, setLoading] = useState(false);

  const handleUpload = async () => {
    if (!file) return;

    const formData = new FormData();
    formData.append("avatar", file);

    try {
      setLoading(true);
      await userService.updateAvatar(formData);
      alert("Cập nhật ảnh đại diện thành công!");
      onClose();
    } catch (err) {
      console.error("Upload error:", err);
      alert("Lỗi khi cập nhật ảnh đại diện.");
    } finally {
      setLoading(false);
    }
  };

  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 z-50 bg-black/30 flex items-center justify-center">
      <div className="bg-white p-6 rounded-xl shadow-md w-full max-w-md">
        <h2 className="text-lg font-semibold mb-4">Cập nhật ảnh đại diện</h2>
        <input placeholder="Chọn ảnh đại diện"
        type="file"
        accept="image/*"
        onChange={(e) => setFile(e.target.files?.[0] || null)}
        className="w-full border rounded p-2 text-sm"
        />
        <div className="mt-4 flex justify-end gap-2">
            <button
                onClick={onClose}
                className="px-4 py-2 bg-gray-200 text-gray-800 rounded hover:bg-gray-300 transition"
                >
                Hủy
            </button>
            <button
                onClick={handleUpload}
                disabled={!file || loading}
                className="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600 transition disabled:opacity-50"
                >
                {loading ? "Đang tải..." : "Cập nhật"}
            </button>
        </div>
      </div>
    </div>
  );
};

export default UploadAvatarModal;
