"use client";

import { useState } from "react";
import { userService } from "@/utils/APIs";

interface Props {
  isOpen: boolean;
  onClose: () => void;
}

const ChangePasswordModal = ({ isOpen, onClose }: Props) => {
  const [oldPassword, setOldPassword] = useState("");
  const [newPassword, setNewPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [loading, setLoading] = useState(false);

  const handleChangePassword = async () => {
    if (newPassword !== confirmPassword) {
      alert("Mật khẩu mới không khớp!");
      return;
    }

    try {
      setLoading(true);
      await userService.changePassword({ oldPassword, newPassword });
      alert("Đổi mật khẩu thành công!");
      onClose();
    } catch (err) {
      console.error("Change password error:", err);
      alert("Lỗi khi đổi mật khẩu.");
    } finally {
      setLoading(false);
    }
  };

  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 z-50 bg-black/30 flex items-center justify-center">
      <div className="bg-white p-6 rounded-xl shadow-md w-full max-w-md">
        <h2 className="text-lg font-semibold mb-4">Đổi mật khẩu</h2>
        <input
          type="password"
          placeholder="Mật khẩu cũ"
          value={oldPassword}
          onChange={(e) => setOldPassword(e.target.value)}
          className="w-full mb-2 p-2 border rounded"
        />
        <input
          type="password"
          placeholder="Mật khẩu mới"
          value={newPassword}
          onChange={(e) => setNewPassword(e.target.value)}
          className="w-full mb-2 p-2 border rounded"
        />
        <input
          type="password"
          placeholder="Xác nhận mật khẩu"
          value={confirmPassword}
          onChange={(e) => setConfirmPassword(e.target.value)}
          className="w-full mb-4 p-2 border rounded"
        />
        <div className="flex justify-end gap-2">
          <button
            onClick={onClose}
            className="px-4 py-2 bg-gray-300 rounded hover:bg-gray-400"
          >
            Hủy
          </button>
          <button
            onClick={handleChangePassword}
            disabled={loading}
            className="px-4 py-2 bg-green-600 text-white rounded hover:bg-green-700"
          >
            {loading ? "Đang xử lý..." : "Xác nhận"}
          </button>
        </div>
      </div>
    </div>
  );
};

export default ChangePasswordModal;
