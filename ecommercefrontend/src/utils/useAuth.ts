"use client";

import { useContext } from "react";
import { StateContext, DispatchContext, User } from "@/utils/Contexts";
import { useRouter } from "next/navigation";

export function useAuth() {
    const state = useContext(StateContext);
    const dispatch = useContext(DispatchContext);
    const router = useRouter();

    const login = (user: User) => {
        dispatch?.({ type: "login", payload: user });
    };

    const logout = () => {
        dispatch?.({ type: "logout" });
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