"use client";

import { cartService } from "@/utils/APIs";
import { useAuth } from "@/utils/useAuth";
import Image from "next/image";
import Link from "next/link";
import { useRouter } from "next/navigation";
import { useEffect, useState } from "react";
import CartModal from "./CartModal";

const NavIcons = () => {
    const [isProfileOpen, setIsProfileOpen] = useState(false);
    const [isCartOpen, setIsCartOpen] = useState(false);
    const [cartCount, setCartCount] = useState(0);

    const router = useRouter();
    const { isAuthenticated, user, logout } = useAuth();

    const handleProfile = () => {
        if (!isAuthenticated) {
            router.push("/login");
            return;
        }
        setIsProfileOpen((prev) => !prev);
    };

    useEffect(() => {
        const fetchCart = async () => {
            if (user) {
                try {
                    const res = await cartService.getUserCart(user.id);
                    setCartCount(res.data.items.length);
                } catch (err) {
                    console.error("Error loading cart", err);
                }
            }
        };
        fetchCart();
    }, [user]);

    return (
        <div className="flex items-center gap-4 xl:gap-6 relative">
            {/* Profile
            <Image
                src="/profile.png"
                alt="Profile"
                width={22}
                height={22}
                className="cursor-pointer"
                onClick={handleProfile}
            /> */}

            {/* Profile Dropdown */}
            {isProfileOpen && isAuthenticated && (
                <div className="absolute p-4 rounded-md top-12 left-0 text-sm shadow-[0-3px_10px_rgb(0,0,0,0.2)] z-20 bg-white">
                    <Link href="/">Profile</Link>
                    <div className="mt-2 cursor-pointer" onClick={logout}>Logout</div>
                </div>
            )}

            {/* Notification */}
            <Image
                src="/notification.png"
                alt="Notification"
                width={22}
                height={22}
                className="cursor-pointer"
            />

            {/* Cart */}
            <div className="relative cursor-pointer" onClick={() => setIsCartOpen((prev) => !prev)}>
                <Image src="/cart.png" alt="Cart" width={22} height={22} className="cursor-pointer" />
                {cartCount > 0 && (
                    <div className="absolute -top-4 -right-4 w-6 h-6 bg-[#F35C7A] rounded-full text-white text-sm flex items-center justify-center">
                        {cartCount}
                    </div>
                )}
                {isCartOpen && <CartModal />}
            </div>
        </div>
    );
};

export default NavIcons;