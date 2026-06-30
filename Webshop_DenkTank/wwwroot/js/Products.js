const searchInput = document.getElementById("search");
const sortSelect = document.getElementById("sort");
const grid = document.getElementById("product-grid");

searchInput.addEventListener("input", () => {
    const term = searchInput.value.toLowerCase();
    document.querySelectorAll(".product-card").forEach(card => {
        const name = card.querySelector("h3").textContent.toLowerCase();
        card.style.display = name.includes(term) ? "block" : "none";
    });
});

sortSelect.addEventListener("change", () => {
    const cards = [...document.querySelectorAll(".product-card")];
    const sort = sortSelect.value;

    cards.sort((a, b) => {
        const priceA = parseFloat(a.querySelector(".price").textContent.replace("€", ""));
        const priceB = parseFloat(b.querySelector(".price").textContent.replace("€", ""));

        return sort === "price-asc" ? priceA - priceB : priceB - priceA;
    });

    cards.forEach(card => grid.appendChild(card));
});