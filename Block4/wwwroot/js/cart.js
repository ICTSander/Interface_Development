

let cartContainerIsOpen = false;


document.addEventListener('DOMContentLoaded', () => {

    const cartImg = document.querySelector('.cart-img-container');
    const cartItemsContainer = document.querySelectorAll('.cart-items-container');
    const cartItemsRemoveBtn = document.querySelectorAll('.cart-item-removeBtn');
    const allcartAmountAddBtn = document.querySelectorAll('.cart-item-amount-add');
    const allcartAmountReduceBtn = document.querySelectorAll('.cart-item-amount-reduce')

    cartImg.addEventListener('click', () => toggleCartConatiner())

    // initial change ( does it only once at page load )
    setAmoutofCart()
    hideBuyButtonIfNoItems()

    for (const cartAmountAddBtn of allcartAmountAddBtn) cartAmountAddBtn.addEventListener('click', (e) => addAantal(e));
    for (const cartAmountReduceBtn of allcartAmountReduceBtn) cartAmountReduceBtn.addEventListener('click', (e) => removeAantal(e));

    // update live changes
    for (const cartItemContainer of cartItemsContainer) {

        cartItemContainer.addEventListener('change', () => {
            setAmoutofCart()
            hideBuyButtonIfNoItems()
        });
    }

    // remove parent
    for (const cartItemRemoveBtn of cartItemsRemoveBtn) {
        cartItemRemoveBtn.addEventListener('click', (e) => removeCartItem(e));
    }
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

function removeCartItem(e) {
    // get parent
    const target = e.target;
    const parent = target.parentNode;
    const grandParent = parent.parentNode;

    // remove parent
    grandParent.remove();
    // reload
    setAmoutofCart()
    hideBuyButtonIfNoItems()
}
function removeAantal(e) {
    const target = e.target;
    const parent = target.parentNode;
    const textNode = parent.querySelector('.cart-item-amount-text');
    const originalNumber = Number.parseFloat(textNode.textContent);
    const newNumber = originalNumber - 1;
    if (newNumber <= 0) {
        removeCartItem(e);
    }
    textNode.textContent = newNumber;
}
function addAantal(e) {
    const target = e.target;
    const parent = target.parentNode;
    const textNode = parent.querySelector('.cart-item-amount-text');
    const originalNumber = Number.parseFloat(textNode.textContent);
    const newNumber = originalNumber + 1;
    textNode.textContent = newNumber;
}