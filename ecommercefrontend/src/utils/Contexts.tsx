"use client";

import { createContext, useReducer, ReactNode, useEffect } from "react";
import { userService } from "./APIs";

// Định nghĩa kiểu dữ liệu cho User
export interface User {
    id: string;
    firstName: string;
    lastName: string;
    phoneNumber: string;
    email: string | null;
    address: string;
    avatar: string | null;
    // Thêm các trường khác nếu cần
}

// Định nghĩa kiểu dữ liệu cho state
interface State {
    user: User | null;
    isAuthenticated: boolean;
    isLoading: boolean;
}

// Định nghĩa kiểu dữ liệu cho action
type Action =
    | { type: "login"; payload: User }
    | { type: "logout" }
    | { type: "setLoading"; payload: boolean };

// Khởi tạo state ban đầu
const initialState: State = {
    user: null,
    isAuthenticated: false,
    isLoading: true,
};

// Tạo context
export const StateContext = createContext<State | null>(null);
export const DispatchContext = createContext<React.Dispatch<Action> | null>(null);

// Reducer để xử lý các action
function reducer(state: State, action: Action): State {
    switch (action.type) {
        case "login":
        return {
            ...state,
            user: action.payload,
            isAuthenticated: true,
            isLoading: false,
        };
        case "logout":
        // Xóa token khỏi localStorage khi logout
        localStorage.removeItem("token");
        return {
            ...state,
            user: null,
            isAuthenticated: false,
            isLoading: false,
        };
        case "setLoading":
        return {
            ...state,
            isLoading: action.payload,
        };
        default:
        return state;
    }
}

export function AuthProvider({ children }: { children: ReactNode }) {
    const [state, dispatch] = useReducer(reducer, initialState);

    useEffect(() => {
        const checkAuth = async () => {
        const token = localStorage.getItem("token");
        if (token) {
            try {
            const res = await userService.getCurrentUser();
            dispatch({ type: "login", payload: res.data });
            } catch (err) {
            console.error("Error fetching current user:", err);
            dispatch({ type: "logout" });
            }
        } else {
            dispatch({ type: "setLoading", payload: false });
        }
        };

        checkAuth();
    }, []);

    return (
        <StateContext.Provider value={state}>
        <DispatchContext.Provider value={dispatch}>
            {children}
        </DispatchContext.Provider>
        </StateContext.Provider>
    );
}