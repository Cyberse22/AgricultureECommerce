import CategoryList from "@/components/CategoryList";

const Category = () => {
    return (
        <main className="p-4">
            <div className="max-w-screen-xl mx-auto">
                <h1 className="text-3xl font-bold mb-6">Danh mục sản phẩm</h1>
                <CategoryList />
            </div>
        </main>
    );
};

export default Category;
