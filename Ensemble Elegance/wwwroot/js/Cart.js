




//
//function noenter() {
//    return !(window.event && window.event.keyCode == 13);
//}


const formElement = document.querySelector("form");

const quantityInputs = document.querySelectorAll(".quantity");
quantityInputs.forEach(function (quantityInput) {
    quantityInput.addEventListener("blur", function () {
        const quantity = quantityInput.value;
        const trElement = quantityInput.closest("tr");
        const itemIdInput = trElement.querySelector("input[name='Id']");
        const itemId = itemIdInput.value;

        window.location.href = "/Cart/UpdateQuantity?newQuantity=" + quantity + "&itemId=" + itemId;
    });
});