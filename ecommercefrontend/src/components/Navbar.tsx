"use client";

import { useAuth } from "@/utils/useAuth";
import Image from "next/image";
import Link from "next/link";
import { useState } from "react";
import Menu from "./Menu";
import NavIcons from "./NavIcons";
import SearchBar from "./SearchBar";

const Navbar = () => {
    const { user, isAuthenticated, logout } = useAuth();
    const [isDropdownOpen, setIsDropdownOpen] = useState(false);

    // ADMIN or STAFF
    if (isAuthenticated && user && (user.role === "Admin" || user.role === "Staff")) {
        return (
            <div className="h-20 px-4 md:px-16 xl:px-32 2xl:px-64 flex items-center justify-between">
                <Link href="/" className="flex items-center gap-3">
                    <Image src="/cart.png" alt="" width={24} height={24} />
                    <div className="text-2xl tracking-wide">AgriCode</div>
                </Link>

                <div className="flex items-center gap-6">
                    {user.role === "Staff" && (
                        <>
                            <Link href="/staff/manage-orders" className="hover:underline">
                                Quản lý hóa đơn
                            </Link>
                            <Link href="/staff/inventory" className="hover:underline">
                                Quản lý kho hàng
                            </Link>
                            <Link href="/staff/shift-end" className="hover:underline">
                                Kết ca
                            </Link>
                        </>
                    )}

                    {user.role === "Admin" && (
                        <>
                            <Link href="/admin/categories" className="hover:underline">
                                Quản lý danh mục
                            </Link>
                            <Link href="/admin/products" className="hover:underline">
                                Quản lý sản phẩm
                            </Link>
                            <Link href="/admin/users" className="hover:underline">
                                Quản lý người dùng
                            </Link>
                        </>
                    )}

                    <div className="relative">
                        <button 
                            className="flex items-center gap-2 cursor-pointer"
                            onClick={() => setIsDropdownOpen(!isDropdownOpen)}
                        >
                            {user.avatar ? (
                                <Image
                                    src={user.avatar}
                                    alt="Avatar"
                                    width={128}
                                    height={128}
                                    className="w-8 h-8 rounded-full object-cover"
                                />
                            ) : (
                                <div className="w-8 h-8 bg-green-600 rounded-full flex items-center justify-center text-white">
                                    {user.firstName.charAt(0)}
                                </div>
                            )}
                            <span className="hidden lg:block">{user.firstName}</span>
                        </button>

                        {isDropdownOpen && (
                            <div className="absolute right-0 mt-2 w-56 bg-white rounded-md shadow-lg py-1 z-10">
                                <Link
                                    href="/profile"
                                    className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100"
                                    onClick={() => setIsDropdownOpen(false)}
                                >
                                    Tài khoản
                                </Link>
                                <button
                                    onClick={() => {
                                        logout();
                                        setIsDropdownOpen(false);
                                    }}
                                    className="block w-full text-left px-4 py-2 text-sm text-gray-700 hover:bg-gray-100"
                                >
                                    Đăng xuất
                                </button>
                            </div>
                        )}
                    </div>
                </div>
            </div>
        );
    }

    // CUSTOMER or UNAUTHENTICATED
    return (
        <div className="h-20 px-4 md:px-16 xl:px-32 2xl:px-64 relative">
            <div className="h-full flex items-center justify-between md:hidden">
                <Link href="/" className="text-2xl tracking-wide">Agri</Link>
                <Menu />
            </div>

            <div className="hidden md:flex items-center justify-between gap-8 h-full">
                {/* LEFT */}
                <div className="w-1/3 xl:w-1/2 flex items-center gap-12">
                    <Link href="/" className="flex items-center gap-3">
                        <Image src="/cart.png" alt="" width={24} height={24} />
                        <div className="text-2xl tracking-wide">AgriCode</div>
                    </Link>
                    <div className="hidden xl:flex gap-4">
                        <Link href="/">Trang chủ</Link>
                        <Link href="/product">Sản phẩm</Link>
                        <Link href="/category">Danh mục</Link>
                        <Link href="/about">Giới thiệu</Link>
                        <Link href="/contact">Liên hệ</Link>
                    </div>
                </div>

                {/* RIGHT */}
                <div className="w-2/3 xl:w-1/2 flex items-center justify-end gap-8">
                    <SearchBar />
                    <div className="flex items-center gap-4">
                        <NavIcons />
                        {isAuthenticated && user ? (
                            <div className="relative">
                                <button 
                                    className="flex items-center gap-2 cursor-pointer"
                                    onClick={() => setIsDropdownOpen(!isDropdownOpen)}
                                >
                                    {user.avatar ? (
                                        <Image
                                            src={user.avatar}
                                            alt="Avatar"
                                            width={128}
                                            height={128}
                                            className="w-8 h-8 rounded-full object-cover"
                                        />
                                    ) : (
                                        <div className="w-8 h-8 bg-green-600 rounded-full flex items-center justify-center text-white">
                                            {user.firstName.charAt(0)}
                                        </div>
                                    )}
                                    <span className="hidden lg:block">{user.firstName}</span>
                                </button>

                                {isDropdownOpen && (
                                    <div className="absolute right-0 mt-2 w-56 bg-white rounded-md shadow-lg py-1 z-10">
                                        <Link
                                            href="/profile"
                                            className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100"
                                            onClick={() => setIsDropdownOpen(false)}
                                        >
                                            Tài khoản
                                        </Link>
                                        <Link
                                            href="/order/list"
                                            className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100"
                                            onClick={() => setIsDropdownOpen(false)}
                                        >
                                            Đơn hàng
                                        </Link>
                                        <button
                                            onClick={() => {
                                                logout();
                                                setIsDropdownOpen(false);
                                            }}
                                            className="block w-full text-left px-4 py-2 text-sm text-gray-700 hover:bg-gray-100"
                                        >
                                            Đăng xuất
                                        </button>
                                    </div>
                                )}
                            </div>
                        ) : (
                            <div className="flex gap-2">
                                <Link
                                    href="/login"
                                    className="py-1 px-3 bg-black-500 text-black rounded "
                                >
                                    Đăng nhập
                                </Link>
                                <Link
                                    href="/register"
                                    className="py-1 px-3 bg-green-500 text-white rounded hover:bg-green-600"
                                >
                                    Đăng ký
                                </Link>
                            </div>
                        )}
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Navbar;
