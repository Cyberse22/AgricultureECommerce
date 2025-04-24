import ProductList from "@/components/ProductList";

const Product = () => {
    return (
        <main className="p-4">
            <div className="max-w-screen-xl mx-auto">
                <h1 className="text-3xl font-bold mb-6">Danh sách sản phẩm</h1>
                <ProductList />
            </div>
        </main>
    );
};

export default Product;
