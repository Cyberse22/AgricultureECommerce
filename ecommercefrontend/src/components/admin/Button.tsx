import React from "react";

interface ButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
  variant?: "default" | "outline" | "destructive";
}

const Button: React.FC<ButtonProps> = ({ children, variant = "default", className = "", ...props }) => {
  const baseStyle = "px-4 py-2 rounded text-white font-semibold";
  const variants: Record<string, string> = {
    default: "bg-blue-600 hover:bg-blue-700",
    outline: "bg-green-600 border border-gray-400 text-gray-800 hover:bg-gray-100",
    destructive: "bg-red-600 hover:bg-red-700",
  };

  return (
    <button className={`${baseStyle} ${variants[variant]} ${className}`} {...props}>
      {children}
    </button>
  );
};

export default Button;
