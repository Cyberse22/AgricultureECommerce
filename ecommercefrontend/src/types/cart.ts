export interface Cart {
    userId: string;
    cartItems: CartItem[];
}

export interface CartItem {
    productId: string;
    productName: string;
    quantity: number;
    price: number;
}