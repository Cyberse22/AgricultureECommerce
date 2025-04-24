"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import { userService } from "@/utils/APIs";
import Link from "next/link";
import axios from "axios";

const Register = () => {
    const router = useRouter();
    const [formData, setFormData] = useState({
        firstName: "",
        lastName: "",
        dateOfBirth: "",
        gender: "",
        email: "",
        phoneNumber: "",
        address: "",
        password: "",
        passwordConfirmation: "",
    });
    const [error, setError] = useState("");
    const [loading, setLoading] = useState(false);
    const [success, setSuccess] = useState(false);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const { name, value } = e.target;
        setFormData((prev) => ({ ...prev, [name]: value }));
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setError("");

        if (formData.password !== formData.passwordConfirmation) {
            setError("Mật khẩu không khớp");
            return;
        }

        setLoading(true);

        try {
            const registerData = {
                firstName: formData.firstName,
                lastName: formData.lastName,
                dateOfBirth: formData.dateOfBirth,
                gender: formData.gender,
                email: formData.email,
                phoneNumber: formData.phoneNumber,
                address: formData.address,
                password: formData.password,
                passwordConfirmation: formData.passwordConfirmation,
            };

            await userService.register(registerData);
            setSuccess(true);

            setTimeout(() => {
                router.push("/login");
            }, 1500);
        } catch (err) {
            console.error("Register error:", err);
            if (axios.isAxiosError(err)) {
                setError(err.response?.data?.message || "Đã xảy ra lỗi khi đăng ký");
            } else {
                setError("Đã xảy ra lỗi khi đăng ký");
            }
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="flex justify-center items-center min-h-screen bg-gray-100 py-8">
            <div className="w-full max-w-md p-8 bg-white rounded-2xl shadow-md">
                <h1 className="text-2xl font-bold mb-6 text-center">Đăng ký tài khoản</h1>

                <form onSubmit={handleSubmit} className="space-y-4">
                    <div className="grid grid-cols-2 gap-4">
                        <div>
                            <label htmlFor="lastName" className="block text-sm font-medium text-gray-700">Họ</label>
                            <input
                                type="text"
                                id="lastName"
                                name="lastName"
                                value={formData.lastName}
                                onChange={handleChange}
                                required
                                className="mt-1 w-full px-4 py-2 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-green-500"
                            />
                        </div>
                        <div>
                            <label htmlFor="firstName" className="block text-sm font-medium text-gray-700">Tên</label>
                            <input
                                type="text"
                                id="firstName"
                                name="firstName"
                                value={formData.firstName}
                                onChange={handleChange}
                                required
                                className="mt-1 w-full px-4 py-2 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-green-500"
                            />
                        </div>
                    </div>

                    <div>
                        <label htmlFor="phoneNumber" className="block text-sm font-medium text-gray-700">Số điện thoại</label>
                        <input
                            type="tel"
                            id="phoneNumber"
                            name="phoneNumber"
                            value={formData.phoneNumber}
                            onChange={handleChange}
                            className="mt-1 w-full px-4 py-2 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-green-500"
                        />
                    </div>

                    <div>
                        <label htmlFor="email" className="block text-sm font-medium text-gray-700">Email</label>
                        <input
                            type="email"
                            id="email"
                            name="email"
                            value={formData.email}
                            onChange={handleChange}
                            required
                            className="mt-1 w-full px-4 py-2 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-green-500"
                        />
                    </div>

                    <div>
                        <label htmlFor="address" className="block text-sm font-medium text-gray-700">Địa chỉ</label>
                        <input
                            type="text"
                            id="address"
                            name="address"
                            value={formData.address}
                            onChange={handleChange}
                            className="mt-1 w-full px-4 py-2 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-green-500"
                        />
                    </div>

                    <div>
                        <label htmlFor="dateOfBirth" className="block text-sm font-medium text-gray-700">Ngày sinh</label>
                        <input
                            type="date"
                            id="dateOfBirth"
                            name="dateOfBirth"
                            value={formData.dateOfBirth}
                            onChange={handleChange}
                            required
                            className="mt-1 w-full px-4 py-2 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-green-500"
                        />
                    </div>

                    <div>
                        <label htmlFor="gender" className="block text-sm font-medium text-gray-700">Giới tính</label>
                        <select
                            id="gender"
                            name="gender"
                            value={formData.gender}
                            onChange={handleChange}
                            required
                            className="mt-1 w-full px-4 py-2 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-green-500"
                        >
                            <option value="" disabled hidden>-- Chọn giới tính --</option>
                            <option value="Nam">Nam</option>
                            <option value="Nữ">Nữ</option>
                            <option value="Khác">Khác</option>
                        </select>
                    </div>

                    <div>
                        <label htmlFor="password" className="block text-sm font-medium text-gray-700">Mật khẩu</label>
                        <input
                            type="password"
                            id="password"
                            name="password"
                            value={formData.password}
                            onChange={handleChange}
                            required
                            minLength={6}
                            className="mt-1 w-full px-4 py-2 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-green-500"
                        />
                    </div>

                    <div>
                        <label htmlFor="passwordConfirmation" className="block text-sm font-medium text-gray-700">Xác nhận mật khẩu</label>
                        <input
                            type="password"
                            id="passwordConfirmation"
                            name="passwordConfirmation"
                            value={formData.passwordConfirmation}
                            onChange={handleChange}
                            required
                            minLength={6}
                            className="mt-1 w-full px-4 py-2 border rounded-lg shadow-sm focus:outline-none focus:ring-2 focus:ring-green-500"
                        />
                    </div>

                    {error && <p className="text-red-600 text-sm">{error}</p>}

                    <button
                        type="submit"
                        disabled={loading}
                        className="w-full py-2 bg-green-600 hover:bg-green-700 text-white font-semibold rounded-lg transition duration-200"
                    >
                        {success ? "Đăng ký thành công!" : loading ? "Đang đăng ký..." : "Đăng ký"}
                    </button>
                </form>

                <p className="mt-4 text-center text-sm text-gray-600">
                    Đã có tài khoản?{" "}
                    <Link href="/login" className="text-green-600 hover:underline">
                        Đăng nhập
                    </Link>
                </p>
            </div>
        </div>
    );
};

export default Register;
