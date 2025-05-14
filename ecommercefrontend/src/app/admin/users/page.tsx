"use client";

import Button from "@/components/admin/Button";
import { Card, CardContent } from "@/components/admin/Card";
import Modal from "@/components/admin/Modal";
import { useState } from "react";

interface Profile {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  avatar: string;
  role: string;
}

type User = {
  id: string;
  name: string;
  isLocked: boolean;
  profile: Profile;
};

const UsersPage = () => {
  const [users, setUsers] = useState<User[]>([
    {
      id: "User99775533",
      name: "Nguyễn Kiệt",
      isLocked: false,
      profile: {
        firstName: "Kiệt",
        lastName: "Nguyễn",
        email: "user99775533@gmail.com",
        phoneNumber: "9977533",
        avatar: "",
        role: "Customer",
      },
    },
    {
      id: "User8888",
      name: "Staff Staff",
      isLocked: false,
      profile: {
        firstName: "Staff",
        lastName: "Staff",
        email: "staff@gmail.com",
        phoneNumber: "8888",
        avatar: "",
        role: "Staff",
      },
    },
  ]);

  const [selectedUser, setSelectedUser] = useState<Profile | null>(null);

  const handleToggleLock = (id: string) => {
    setUsers((prev) =>
      prev.map((user) =>
        user.id === id
          ? {
              ...user,
              isLocked: !user.isLocked,
            }
          : user
      )
    );

    const lockedUser = users.find((u) => u.id === id);
    if (lockedUser && !lockedUser.isLocked) {
      console.log(`Đã đăng xuất user ${lockedUser.name}`);
    }
  };

  return (
    <div className="p-6 space-y-6 max-w-4xl mx-auto">
      <Card>
        <CardContent className="p-4 space-y-4">
          <h2 className="text-xl font-semibold">Quản lý người dùng</h2>
          <div className="space-y-2">
            {users.map((user) => (
              <div
                key={user.id}
                className="flex justify-between items-center border rounded p-3"
              >
                <div>
                  <p className="font-medium">{user.name}</p>
                  <p className="text-sm text-gray-500">ID: {user.id}</p>
                </div>
                <div className="flex gap-2">
                  <Button onClick={() => setSelectedUser(user.profile)} variant="default">
                    Xem thông tin
                  </Button>
                  <Button
                    variant={user.isLocked ? "outline" : "destructive"}
                    onClick={() => handleToggleLock(user.id)}
                  >
                    {user.isLocked ? "Mở khóa" : "Khóa"}
                  </Button>
                </div>
              </div>
            ))}
          </div>
        </CardContent>
      </Card>

      <Modal isOpen={!!selectedUser} onClose={() => setSelectedUser(null)}>
        {selectedUser && (
          <div className="space-y-2">
            <img src={selectedUser.avatar} alt="Avatar" className="w-20 h-20 rounded-full mx-auto" />
            <p><strong>Họ:</strong> {selectedUser.firstName}</p>
            <p><strong>Tên:</strong> {selectedUser.lastName}</p>
            <p><strong>Email:</strong> {selectedUser.email}</p>
            <p><strong>Số điện thoại:</strong> {selectedUser.phoneNumber}</p>
            <p><strong>Vai trò:</strong> {selectedUser.role}</p>
          </div>
        )}
      </Modal>
    </div>
  );
};

export default UsersPage;
