
interface User {
  id: string;
  name: string;
}

interface Props {
  users: User[];
}

export const UserManagement = ({ users }: Props) => {
  return (
    <div className="space-y-2">
      <h2 className="text-xl font-semibold">Quản lý người dùng</h2>
      {users.map((user) => (
        <div
          key={user.id}
          className="flex justify-between items-center border p-2 rounded"
        >
          <div>
            <p className="font-medium">{user.name}</p>
            <p className="text-sm text-gray-500">ID: {user.id}</p>
          </div>
          <div className="flex gap-2">
            <button className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600">Xem thông tin</button>
            <button className="bg-red-500 text-white px-4 py-2 rounded hover:bg-red-600">Khóa</button>
          </div>
        </div>
      ))}
    </div>
  );
};