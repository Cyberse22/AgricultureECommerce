import CategoryList from "@/components/CategoryList";
import ProductList from "@/components/ProductList";
import Slider from "@/components/Slider";

const HomePage = () => {
  return (
    <div className="">
      <Slider/>
      <div className="mt-24 px-4 md:px-16 xl:px-32 2xl:px-64">
        <h1 className="text-2xl">Feature Products</h1>
        <ProductList/>
      </div>
      <div className="mt-24">
        <h1 className="text-2xl px-4 md:px-16 xl:px-32 2xl:px-64 mb-12">Categories</h1>
        <CategoryList/>
      </div>
      <div className="mt-24 px-4 md:px-16 xl:px-32 2xl:px-64">
        <h1 className="text-2xl">Feature Products</h1>
        <ProductList/>
      </div>
    </div>
  )
}

export default HomePage;