"use client";

import { DispatchContext, StateContext, User } from "@/utils/Contexts";
import { useRouter } from "next/navigation";
import { useContext, useEffect } from "react";

export function useAuth() {
    const state = useContext(StateContext);
    const dispatch = useContext(DispatchContext);
    const router = useRouter();

    useEffect(() => {
        // Check localStorage for user data on initial load
        const storedUser = localStorage.getItem("user");
        if (storedUser && !state?.user) {
            const parsedUser = JSON.parse(storedUser);
            dispatch?.({ type: "login", payload: parsedUser });
        }
    }, [state?.user, dispatch]);

    const login = (user: User) => {
        dispatch?.({ type: "login", payload: user });
        // Store user data in localStorage
        localStorage.setItem("user", JSON.stringify(user));
    };

    const logout = () => {
        dispatch?.({ type: "logout" });
        // Remove user data from localStorage
        localStorage.removeItem("user");
        router.push("/login");
    };

    return {
        user: state?.user,
        isAuthenticated: state?.isAuthenticated,
        isLoading: state?.isLoading,
        login,
        logout,
    };
}