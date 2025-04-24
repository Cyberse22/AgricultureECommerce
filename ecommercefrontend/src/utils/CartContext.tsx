"use client";

import { createContext, useReducer, ReactNode, useEffect } from "react";
import { cartService } from "./APIs";
import { useAuth } from "./useAuth";
import { CartItem } from "@/types/cart";

interface CartState {
  cartItems: CartItem[];
  isLoading: boolean;
}

type CartAction =
  | { type: "setItems"; payload: CartItem[] }
  | { type: "addItem"; payload: CartItem }
  | { type: "removeItem"; payload: string }
  | { type: "clearCart" }
  | { type: "setLoading"; payload: boolean };

const initialState: CartState = {
  cartItems: [],
  isLoading: false,
};

export const CartContext = createContext<CartState | null>(null);
export const CartDispatchContext = createContext<React.Dispatch<CartAction> | null>(null);

function reducer(state: CartState, action: CartAction): CartState {
  switch (action.type) {
    case "setItems":
      return { ...state, cartItems: action.payload, isLoading: false };
    case "addItem":
      const existing = state.cartItems.find(item => item.productId === action.payload.productId);
      if (existing) {
        return {
          ...state,
          cartItems: state.cartItems.map(item =>
            item.productId === action.payload.productId
              ? { ...item, quantity: item.quantity + action.payload.quantity }
              : item
          ),
        };
      }
      return { ...state, cartItems: [...state.cartItems, action.payload] };
    case "removeItem":
      return {
        ...state,
        cartItems: state.cartItems.filter(item => item.productId !== action.payload),
      };
    case "clearCart":
      return { ...state, cartItems: [] };
    case "setLoading":
      return { ...state, isLoading: action.payload };
    default:
      return state;
  }
}

export function CartProvider({ children }: { children: ReactNode }) {
  const [state, dispatch] = useReducer(reducer, initialState);
  const { user } = useAuth();

  useEffect(() => {
    const fetchCart = async () => {
      if (!user) return;
      dispatch({ type: "setLoading", payload: true });
      try {
        const res = await cartService.getUserCartItems(user.id);
        dispatch({ type: "setItems", payload: res.data });
      } catch (err) {
        console.error("Error fetching cart:", err);
        dispatch({ type: "setItems", payload: [] });
      }
    };

    fetchCart();
  }, [user]);

  return (
    <CartContext.Provider value={state}>
      <CartDispatchContext.Provider value={dispatch}>
        {children}
      </CartDispatchContext.Provider>
    </CartContext.Provider>
  );
}
