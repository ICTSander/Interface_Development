document.addEventListener("DOMContentLoaded", function () {
    var form = document.getElementById("checkout-form");
    var errorBox = document.getElementById("checkout-error");

    if (!form) return;

    form.addEventListener("submit", function (e) {
        errorBox.style.display = "none";
        errorBox.textContent = "";

        var naam = form.querySelector("[name='Input.Naam']");
        var adres = form.querySelector("[name='Input.Adres']");
        var betaalwijze = form.querySelector("[name='Input.Betaalwijze']");
        var errors = [];

        if (!naam || !naam.value.trim()) errors.push("Vul je naam in.");
        if (!adres || !adres.value.trim()) errors.push("Vul je bezorgadres in.");
        if (!betaalwijze || !betaalwijze.value) errors.push("Kies een betaalwijze.");

        if (errors.length > 0) {
            e.preventDefault();
            errorBox.textContent = errors.join(" ");
            errorBox.style.display = "block";
            window.scrollTo({ top: 0, behavior: "smooth" });
        }
    });
});
