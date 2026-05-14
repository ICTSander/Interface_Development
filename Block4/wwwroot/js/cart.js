

let cartContainerIsOpen = false;


document.addEventListener('DOMContentLoaded', () => {

    const cartImg = document.querySelector('.cart-img-container');

    cartImg.addEventListener('click', () => toggleCartConatiner())
})




function toggleCartConatiner() {
    if (cartContainerIsOpen) {
        cartContainerIsOpen = false;
        closeCartContainer()
    } else {
        cartContainerIsOpen = true;
        openCartContainer()
    }
}

function openCartContainer() {
    const cartSectionContainer = document.querySelector < HTMLDivElement > ('.cart-section');

    cartSectionContainer.style.display = 'flex';
    // make the position far to the right to get the animations right
    // #JS quirk
    cartSectionContainer.style.right = '-500px';
    setTimeout(cartSectionContainer.style.right = '0px', 1);

}
function closeCartContainer() {
    const cartSectionContainer = document.querySelector<HTMLDivElement>('.cart-section');

    cartSectionContainer.style.display = 'none';
    // make the position far to the right to get the animations right
    // #JS quirk
    cartSectionContainer.style.right = '-500px';
}