"use client";

import { useContext } from "react";
import { CartContext, CartDispatchContext } from "./CartContext";

export function useCart() {
    const context = useContext(CartContext);
    if (!context) throw new Error("useCart must be used within a CartProvider");
    return context;
}

export function useCartDispatch() {
    const context = useContext(CartDispatchContext);
    if (!context) throw new Error("useCartDispatch must be used within a CartProvider");
    return context;
}
