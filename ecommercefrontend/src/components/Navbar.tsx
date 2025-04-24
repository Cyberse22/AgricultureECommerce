"use client"

import { useAuth } from '@/utils/useAuth';
import Image from 'next/image';
import Link from 'next/link';
import { useState } from 'react';
import Menu from './Menu';
import NavIcons from './NavIcons';
import SearchBar from './SearchBar';

const Navbar = () => {
    
    const {user, isAuthenticated, logout} = useAuth();
    const [userMenuOpen, setUserMenuOpen] = useState(false)
    
    const toggleUserMenu = () => {
        setUserMenuOpen(!userMenuOpen);
    }

    return (
        <div className="h-20 px-4 md:px-16 xl:px-32 2xl:px-64 relative">
            <div className='h-full flex items-center justify-between md:hidden'>
                {/* MOBILE */}
                <Link href="/">
                    <div className='text-2xl tracking-wide'>Agri</div>
                </Link>
                <Menu />
            </div>
            {/* BIGGER SCREENS*/}
            <div className='hidden md:flex items-center justify-between gap-8 h-full'>
                {/* LEFT */}
                <div className='w-1/3 xl:w-1/2 flex items-center gap-12'>
                    <Link href="/" className='flex items-center gap-3'>
                        <Image src="/cart.png" alt='' width={24} height={24} />
                        <div className='text-2xl tracking-wide'>AgriCode</div>
                    </Link>
                    <div className='hidden xl:flex gap-4'>
                        <Link href="/">Trang chủ</Link>
                        <Link href="/product">Sản phẩm</Link>
                        <Link href="/category">Danh mục</Link>
                        <Link href="/about">Giới thiệu</Link>
                        <Link href="/contact">Liên hệ</Link>
                    </div>
                </div>
                {/* RIGHT */}
                <div className='w-2/3 xl:w-1/2 flex items-center justify-end gap-8'>
                    <SearchBar />
                    <div className="flex items-center gap-4">
                        <NavIcons />
                        {isAuthenticated ? (
                            <div className="relative">
                                <button
                                    onClick={toggleUserMenu}
                                    className="flex items-center gap-2 cursor-pointer"
                                >
                                {user?.avatar ? (
                                    <Image
                                        src={user.avatar}
                                        alt="Avatar"
                                        width={128}
                                        height={128}
                                        className="w-8 h-8 rounded-full object-cover"
                                    />
                                ) : (
                                    <div className="w-8 h-8 bg-green-600 rounded-full flex items-center justify-center text-white">
                                        {user?.firstName?.charAt(0)}
                                    </div>
                                )}
                                    <span className="hidden lg:block">{user?.firstName}</span>
                                </button>
                                
                                {userMenuOpen && (
                                    <div className="absolute right-0 mt-2 w-48 bg-white rounded-md shadow-lg py-1 z-10">
                                        <Link href="/profile" className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100">
                                            Tài khoản
                                        </Link>
                                        <Link href="/order" className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100">
                                            Đơn hàng
                                        </Link>
                                        <button
                                            onClick={logout}
                                            className="block w-full text-left px-4 py-2 text-sm text-gray-700 hover:bg-gray-100"
                                        >
                                            Đăng xuất
                                        </button>
                                    </div>
                                )}
                            </div>
                        ) : (
                            <div className="flex items-center gap-2">
                                <Link href="/login" className="py-2 px-3 text-sm rounded hover:bg-gray-100">
                                    Đăng nhập
                                </Link>
                                <Link href="/register" className="py-2 px-3 text-sm bg-green-600 text-white rounded hover:bg-green-700">
                                    Đăng ký
                                </Link>
                            </div>
                        )}
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Navbar;