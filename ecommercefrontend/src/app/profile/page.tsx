"use client";

import ChangePasswordModal from "@/components/ChangePasswordModal";
import UploadAvatarModal from "@/components/UploadAvatarModal";
import { userService } from "@/utils/APIs";
import { User } from "@/utils/Contexts";
import { useAuth } from "@/utils/useAuth";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";

const Profile = () => {
    const { user, isAuthenticated, isLoading, login } = useAuth();
    const [fetching, setFetching] = useState(false);
    const router = useRouter();
    const [showAvatarModal, setShowAvatarModal] = useState(false);
    const [showPasswordModal, setShowPasswordModal] = useState(false);

    useEffect(() => {
        const fetchUser = async () => {
        if (isAuthenticated && !user) {
            setFetching(true);
            try {
            const res = await userService.getCurrentUser();
            login(res.data as User);
            } catch (error) {
            console.error("Lỗi lấy thông tin người dùng:", error);
            } finally {
            setFetching(false);
            }
        }
        };

        fetchUser();
    }, [isAuthenticated, user, login]);

    if (isLoading || fetching) {
        return <div>Đang tải thông tin người dùng...</div>;
    }

    if (!isAuthenticated || !user) {
        return (
        <div className="text-center mt-10">
            Bạn chưa đăng nhập. Vui lòng{" "}
            <a href="/login" className="text-blue-500 underline">đăng nhập</a>.
        </div>
        );
    }

    return (
        <div className="p-6 max-w-md mx-auto bg-white rounded-lg shadow-md mt-8">
            <div className="flex items-center gap-4">
                <img
                    src={user.avatar || "/default-avatar.png"}
                    alt="Avatar"
                    className="w-24 h-24 rounded-full border object-cover"
                />
                <div>
                    <h2 className="text-xl font-semibold">
                        {user.firstName} {user.lastName}
                    </h2>
                    <p className="text-gray-500">{user.email || "Chưa có email"}</p>
                    <p className="text-gray-500">{user.phoneNumber || "Chưa có số điện thoại"}</p>
                </div>
            </div>
    
            {/* Nút hành động đặt phía dưới */}
            <div className="mt-6 flex gap-4 justify-center">
                <button
                    onClick={() => setShowAvatarModal(true)}
                    className="flex-1 bg-indigo-600 text-white py-2 px-4 rounded hover:bg-indigo-700 transition"
                >
                    Cập nhật ảnh
                </button>
                <button
                    onClick={() => setShowPasswordModal(true)}
                    className="flex-1 bg-red-500 text-white py-2 px-4 rounded hover:bg-red-600 transition"
                >
                    Đổi mật khẩu
                </button>
            </div>
    
            {/* Modals */}
            <UploadAvatarModal isOpen={showAvatarModal} onClose={() => setShowAvatarModal(false)} />
            <ChangePasswordModal isOpen={showPasswordModal} onClose={() => setShowPasswordModal(false)} />
        </div>
    );
}

export default Profile;
