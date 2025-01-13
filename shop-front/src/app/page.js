const navItems = [
  { name: "Products", href: "/products" },
  { name: "Users", href: "/users" },
];

export default function Home() {
  return (
    <div>
      <h1>Shop Front</h1>
      <nav className="flex gap-4 items-center justify-center font-semibold">
        {navItems.map((item) => (
          <a key={item.name} href={item.href} className="hover:underline">
            {item.name}
          </a>
        ))}
      </nav>
    </div>
  );
}