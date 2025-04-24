import axios from "axios";

interface LoginCredentials {
    phoneNumber: string;
    password: string;
}

interface RegisterData {
    firstName: string;
    lastName: string;
    phoneNumber: string;
    password: string;
    passwordConfirmation: string;
    email: string | null;
    address: string;
    dateOfBirth: string;
    gender: string;
}

interface UserCartItems{
    userId: string;
    productId: string;
}

interface Item{
    productId: string;
    productName: string;
    quantity: number;
    price: number;
}

interface Product{
    productId: string;
    productName: string;
    productDescription: string;
    productPrice: number;
    productImage: string[];
    categoryIds: string[];
    inventoryId: string;
    quantity: number;
}

export interface CreateOrderModel {
    customerId?: string;
    customerName: string;
    customerPhone: string;
    paymentMethod: string;
    orderItems: {
        productId: string;
        productName: string;
        quantity: number;
        unitPrice: number;
        discount?: number;
    }[];
}

const API_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:2001';

const apiClient = axios.create({
    baseURL: API_URL,
    headers: {
        'Content-Type': 'application/json',
    },
});

apiClient.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem('token');
        if (token) {
            config.headers['Authorization'] = `Bearer ${token}`;
        }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

// API Services
export const userService = {
    login: (credentials: LoginCredentials) => apiClient.post("/account/SignIn", credentials),
    register: (userData: RegisterData) => apiClient.post("/account/SignUp", userData),
    changePassword: (data: any) => apiClient.post("/account/ChangePassword", data),
    updateAvatar: (data: any) => apiClient.post("/account/update-avatar", data),
    getCurrentUser: () => apiClient.get("/account/currentUser"),
};

export const productService = {
    getAll: () => apiClient.get("/product/get-all"),
    getById: (productId: string) => apiClient.get(`/product/${productId}`),
    getByCategory: (categoryId: string) => apiClient.get(`/product/by-category/${categoryId}`),
    getByInventory: (inventoryId: string) => apiClient.get(`/product/by-inventory/${inventoryId}`),
    search: (keyword: string) => apiClient.get(`/product/search/${keyword}`),
    create: (data: Product) => apiClient.post("/product/create", data),
    update: (productId: string, data: Product) => apiClient.put(`/product/update/${productId}`, data),
    updateImage: (data: any) => apiClient.put(`/product/update-image`, data),
};

export const categoryService = {
    getAll: () => apiClient.get("/category/all"),
    getById: (categoryId: string) => apiClient.get(`/category/${categoryId}`),
    getByName: (categoryName: string) => apiClient.get(`/category/name/${categoryName}`),
    getByParent: (parentName: string) => apiClient.get(`/category/parent/${parentName}`),
    create: (data: any) => apiClient.post("/category/create", data),
    update: (data: any) => apiClient.put("/category/update", data),
    uploadImage: (data: any) => apiClient.post("/category/upload-image", data),
};

export const inventoryService = {
    getAll: () => apiClient.get("/inventory/all"),
    create: (data: any) => apiClient.post("/inventory/create", data),
    getById: (inventoryId: string) => apiClient.get(`/inventory/${inventoryId}`),
    update: (inventoryId: string, data: any) => apiClient.put(`/inventory/update/${inventoryId}`, data),
    importStock: (data: any) => apiClient.post("/inventory/import", data),
    exportStock: (data: any) => apiClient.post("/inventory/export", data),
    getTransactionById: (transactionId: string) => apiClient.get(`/inventory/transactions/${transactionId}`),
    getTransactionsByInventoryId: (inventoryId: string) => apiClient.get(`/inventory/${inventoryId}/transactions`),
};
export const cartService = {
    getUserCart: (userId: string) => apiClient.get(`/cart/${userId}`),
    getUserCartItems: (userId: string) => apiClient.get(`/cart/${userId}/items`),
    deleteCartItems: (userCartItems: UserCartItems) => apiClient.delete(`/cart/${userCartItems.userId}/items/${userCartItems.productId}`),
    clearCart: (userId: string) => apiClient.delete(`/cart/${userId}`),
    addItemsToCart: (userId: string, item: Item) => apiClient.post(`/cart/${userId}/items`, item),
}

export const orderService = {
    getAll: () => apiClient.get("/order"),
    getById: (orderId: string) => apiClient.get(`/order/order-id`, { params: { orderId } }),
    create: (data: CreateOrderModel) => apiClient.post("/order", data),
    getByDate: (date: string) => apiClient.get("/order/by-date", { params: { date } }),
    getByDateWithIndex: (date: string) => apiClient.get("/order/by-date-with-index", { params: { date } }),
};