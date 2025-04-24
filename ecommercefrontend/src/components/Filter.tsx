const Filter = () => {
    return (
        <div className="mt-12 flex justify-between">
            <div className="flex gap-6 flex-wrap">
                <select aria-label="Filter Type" name="type" id="" className="py-2 px-4 rounded-2xl font-medium bg-gray-200">
                    <option value="Phan">Phan</option>
                    <option value="Code">Phan 1</option>
                    <option value="Phanw">Phan 2</option>
                    <option value="Phanb">Phan 3</option>
                </select>
                <input type="text" name="min" placeholder="min price" className="text-xs rounded-2xl pl-2 w-24 ring-1 ring-gray-400"/>
                <input type="text" name="max" placeholder="max price" className="text-xs rounded-2xl pl-2 w-24 ring-1 ring-gray-400"/>
                <select aria-label="Filter Type" name="type" id="" className="py-2 px-4 rounded-2xl font-medium bg-gray-200">
                    <option value="Phan">Phan</option>
                    <option value="Code">Phan 1</option>
                    <option value="Phanw">Phan 2</option>
                    <option value="Phanb">Phan 3</option>
                </select>
                <select aria-label="Filter Type" name="type" id="" className="py-2 px-4 rounded-2xl font-medium bg-gray-200">
                    <option value="Phan">Phan</option>
                    <option value="Code">Phan 1</option>
                    <option value="Phanw">Phan 2</option>
                    <option value="Phanb">Phan 3</option>
                </select>
                <select aria-label="Filter Type" name="type" id="" className="py-2 px-4 rounded-2xl font-medium bg-gray-200">
                    <option value="Phan">Phan</option>
                    <option value="Code">Phan 1</option>
                    <option value="Phanw">Phan 2</option>
                    <option value="Phanb">Phan 3</option>
                </select>
            </div>
            <div className="">
                <select aria-label="Filter Type" name="sort" id="" className="w-auto py-2 px-4 rounded-2xl text-xs font-medium bg-white ring-1 ring-gray-400">
                    <option>Sort By</option>
                    <option value="Code">Phan 1</option>
                    <option value="Phanw">Phan kiethas</option>
                    <option value="Phanb">Phan 3</option>
                </select>
            </div>
        </div>
    )
}

export default Filter;