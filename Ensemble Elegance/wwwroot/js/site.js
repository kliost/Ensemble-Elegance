//let AddToCartButtons = document.querySelectorAll(".add-btn")
//let CartItemList = []

//let order = {
//    customerName: defaultname,
//    items:{
//        for
//    }
//}

//for (let i = 0; i < AddToCartButtons.length; i++) {
//    let AddToCartButton = AddToCartButtons[i]
//    AddToCartButton.addEventListener('click', AddToCartClick)
//}

//function AddToCartClick(event) {
//    let button = event.target
//    let item = button.parentElement

//    let itemName = item.getElementsByClassName('item-name')[0].innerText
//    let itemPrice = item.getElementsByClassName()[0].innerText


//    console.log(itemName)

//}

var cart = {
    items: []
};

// Додаємо товар до корзини
function addToCart(item) {
    cart.items.push(item);
    console.log(item.Id)
}

// Відправляємо список товарів на сервер
function sendCartToServer() {
    $.ajax({
        url: "/api/cart",
        method: "POST",
        data: JSON.stringify(cart.items),
        contentType: "application/json",
        success: function () {
            // Після успішної відправки на сервер можна виконати додаткові дії, наприклад, очистити список товарів в корзині
            cart.items = [];
        },
        error: function () {
            // Обробка помилки
        }
    });
}