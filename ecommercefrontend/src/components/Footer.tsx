import Image from "next/image";
import Link from "next/link";

const Footer = () => {
    return (
        <div className="py-24 px-4 md:px-8 lg:px-16 xl:px-32 2xl:px-64 bg-gray-100 text-sm mt-24">
            {/* TOP */}
            <div className=" flex flex-col md:flex-row justify-between gap-24">
                {/* LEFT */}
                <div className="w-full md:w-1/2 lg:w-1/4 flex flex-col gap-8">
                    <Link href="/"><div className='text-2xl tracking-wide'>Agri</div></Link>
                        <p>72C/16 Binh Thoi, Ward 14, District 11, HCMC</p>
                        <span className="font-semibold">kietnguyen2226@gmail.com</span>
                        <span className="font-semibold">+84 0123 456 789</span>
                        <div className="flex gap-6">
                        <Image src="/facebook.png" alt="" width={16} height={16}/>
                        <Image src="/google.png" alt="" width={16} height={16}/>
                        <Image src="/instagram.png" alt="" width={16} height={16}/>
                        <Image src="/facebook.png" alt="" width={16} height={16}/>
                    </div>
                </div>
                {/* CENTER */}
                <div className="hidden lg:flex justify-between w-1/2">
                    <div className="flex flex-col justify-between">
                        <h1 className="font-medium text-lg mb-4">COMPANY</h1>
                        <div className="flex flex-col gap-6">
                            <Link href="">About Us</Link>
                            <Link href="">Contact Us</Link>
                            <Link href="">Careers</Link>
                            <Link href="">Terms & Conditions</Link>
                            <Link href="">Privacy Policy</Link>
                            <Link href="">Affiliates</Link>
                        </div>
                    </div>
                    <div className="flex flex-col justify-between">
                        <h1 className="font-medium text-lg">COMPANY</h1>
                        <div className="flex flex-col gap-6">
                            <Link href="">About Us</Link>
                            <Link href="">Contact Us</Link>
                            <Link href="">Careers</Link>
                            <Link href="">Terms & Conditions</Link>
                            <Link href="">Privacy Policy</Link>
                            <Link href="">Affiliates</Link>
                        </div>
                    </div>
                    <div className="flex flex-col justify-between">
                        <h1 className="font-medium text-lg">COMPANY</h1>
                        <div className="flex flex-col gap-6">
                            <Link href="">About Us</Link>
                            <Link href="">Contact Us</Link>
                            <Link href="">Careers</Link>
                            <Link href="">Terms & Conditions</Link>
                            <Link href="">Privacy Policy</Link>
                            <Link href="">Affiliates</Link>
                        </div>
                    </div>
                </div>
                {/* RIGHT */}
                <div className="w-full md:w-1/2 lg:w-1/4 flex flex-col gap-8">
                    <h1 className="font-medium text-lg">SUBSCRIBE</h1>
                    <p>This is text</p>
                    <div className="flex">
                        <input type="text" className="p-4 w-3/4" placeholder="Email Address" />
                        <button className="w-1/4 bg-red-300 text-white">JOIN</button>
                        </div>
                    <span className="font-semibold">Secure Payment</span>
                    <div className="flex justify-between">
                        <Image src="/google.png" alt="" width={16} height={16}/>
                        <Image src="/google.png" alt="" width={16} height={16}/>
                        <Image src="/google.png" alt="" width={16} height={16}/>
                        <Image src="/google.png" alt="" width={16} height={16}/>
                    </div>
                </div>
            </div>
            {/* BOTTOM */}
            <div className="flex flex-col md:flex-row items-center justify-between gap-8 mt-16">
                <div className="">Agri Shop</div>
                <div className="flex flex-col gap-8 md:flex-row">
                    <div className="">
                        <span className="text-gray-500 mr-4">Language</span>
                        <span className="font-medium">Vietnam | Vietnamese</span>
                    </div>
                    <div className="">
                        <span className="text-gray-500 mr-4">Currency</span>
                        <span className="font-medium">Dong (VND)</span>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Footer;