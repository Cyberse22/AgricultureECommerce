"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import { userService } from "@/utils/APIs";
import { useAuth } from "@/utils/useAuth";

const LoginPage = () => {
  const { login } = useAuth();
  const router = useRouter();

  const [phoneNumber, setPhoneNumber] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");
    setLoading(true);

    try {
      const res = await userService.login({ phoneNumber, password });
      const token = res.data;

      localStorage.setItem("token", token);

      const userRes = await userService.getCurrentUser();

      login(userRes.data);

      router.push("/");
    } catch (err) {
      console.error("Login error:", err);
      setError("Số điện thoại hoặc mật khẩu không chính xác.");
    } finally {
      setLoading(false);
    }
  };

    return (
        <div className="flex items-center justify-center min-h-screen bg-gray-100">
            <div className="w-full max-w-md p-8 bg-white rounded-2xl shadow-md">
                <h2 className="text-2xl font-bold mb-6 text-center">Đăng nhập</h2>
                <form className="space-y-4" onSubmit={handleLogin}>
                <div>
                    <label htmlFor="phoneNumber" className="block text-sm font-medium text-gray-700">
                    Số điện thoại
                    </label>
                    <input
                    type="text"
                    id="phoneNumber"
                    value={phoneNumber}
                    onChange={(e) => setPhoneNumber(e.target.value)}
                    className="mt-1 w-full px-4 py-2 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
                    placeholder="Nhập số điện thoại"
                    required
                    />
                </div>
                <div>
                    <label htmlFor="password" className="block text-sm font-medium text-gray-700">
                    Mật khẩu
                    </label>
                    <input
                    type="password"
                    id="password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    className="mt-1 w-full px-4 py-2 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
                    placeholder="Nhập mật khẩu"
                    required
                    />
                </div>
                {error && <p className="text-red-600 text-sm">{error}</p>}
                <button
                    type="submit"
                    disabled={loading}
                    className="w-full py-2 bg-blue-600 hover:bg-blue-700 text-white font-semibold rounded-lg transition duration-200"
                >
                    {loading ? "Đang đăng nhập..." : "Đăng nhập"}
                </button>
                </form>
            </div>
        </div>
    );
};

export default LoginPage;