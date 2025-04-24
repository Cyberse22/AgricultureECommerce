"use client";

import { useAuth } from "@/utils/useAuth";
import { useRouter } from "next/navigation";
import { useEffect } from "react";

export default function ProtectedRoute({ children }: { children: React.ReactNode }) {
    const { isAuthenticated, isLoading } = useAuth();
    const router = useRouter();

    useEffect(() => {
        if (!isLoading && !isAuthenticated) {
        router.push("/login");
        }
    }, [isAuthenticated, isLoading, router]);

    if (isLoading) {
        return <div>Đang tải...</div>;
    }

    return isAuthenticated ? <>{children}</> : null;
}