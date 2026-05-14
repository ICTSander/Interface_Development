

let cartContainerIsOpen = false;


document.addEventListener('DOMContentLoaded', () => {

    const cartImg = document.querySelector('.cart-img-container');

    cartImg.addEventListener('click', () => toggleCartConatiner())

    setAmoutofCart()

    hideBuyButtonIfNoItems()
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
    const cartSectionContainer = document.querySelector('.cart-section');

    // make the position far to the right to get the animations right
    // #JS quirk
    cartSectionContainer.style.right = '-500px';
    cartSectionContainer.style.display = 'flex';
    // set a timer of 1 sec to set the position back to 0 to get a total animation time of 0.5 with 0.5 sec buffer
    setTimeout( () => {
        cartSectionContainer.style.right = '0px';
    }, 1);
}
function closeCartContainer() {
    const cartSectionContainer = document.querySelector('.cart-section');

    // make the position far to the right to get the animations right
    // #JS quirk
    cartSectionContainer.style.right = '-500px'
    // set a timer of 1 sec to set the position back to 0 to get a total animation time of 0.5 with 0.5 sec buffer
    setTimeout(() => {
        cartSectionContainer.style.display = 'none';
    }, 1);   
}

function setAmoutofCart() {
    const cartCounter = document.querySelector('#cart-counter');

    const defaultCounterValue = ""; // empty

    // get how many objects are inside cart
    const cartItemAmmountValue = document.querySelectorAll('.cart-item-conainer').length;
    // set ammount inside cartCounter
    cartCounter.textContent = cartItemAmmountValue || defaultCounterValue;
}
function hideBuyButtonIfNoItems() {
    const buyContainer = document.querySelector('.cart-item-paying-Container');

    const cartItemAmmountValue = document.querySelectorAll('.cart-item-conainer').length;

    buyContainer.style.display = cartItemAmmountValue >= 1 ? 'block' : 'none';
}