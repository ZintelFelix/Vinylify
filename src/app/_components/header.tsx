import Link from "next/link";

const Header = () => {
  return (
    <div>
    <h2 className="text-2xl md:text-4xl font-bold tracking-tight md:tracking-tighter leading-tight mb-20 mt-8">
      <Link href="/" className="hover:underline">
        Vinylify
      </Link>
      .
    </h2>
    </div>
  );
};

export default Header;
