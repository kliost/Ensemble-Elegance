function decrementQuantity(productId) {
    var quantityInput = document.getElementById("quantityInput-" + productId);
    var currentValue = parseInt(quantityInput.value);
    var newValue = currentValue - 1;

    if (newValue >= 0) {
        quantityInput.value = newValue;
        updateQuantityOnServer(productId, newValue, function () {
            updateSumCell(productId, newValue);
        });
    }
}

function incrementQuantity(productId) {
    var quantityInput = document.getElementById("quantityInput-" + productId);
    var currentValue = parseInt(quantityInput.value);
    var newValue = currentValue + 1;
    console.log(productId);
    if (!isNaN(newValue)) {
        quantityInput.value = newValue;
        updateQuantityOnServer(productId, newValue, function () {
            updateSumCell(productId, newValue);
        });
    }
}

function updateQuantityOnServer(productId, quantity, callback) {
    var xhr = new XMLHttpRequest();
    xhr.open("GET", "/Cart/UpdateQuantity?productId=" + productId + "&newQuantity=" + quantity, true);
    xhr.onload = function () {
        if (xhr.status === 200) {
            callback();
        }
    };
    xhr.send();
}


function updateSumCell(productId, quantity) {
    var sumCell = document.getElementById("sumCell-" + productId);
    var priceCell = document.getElementById("priceCell-" + productId)
    var price = parseFloat(priceCell.innerHTML.replace("$", ""));
    var sum = price * quantity;




    if (!isNaN(sum)) {
        sumCell.innerHTML = sum + "$";
        updateTotal();
    }

}
function updateTotal() {
    var sumCells = document.getElementsByClassName("product-sum-cell");
    var totalCell = document.getElementById("total");
    var total = 0;
    for (var i = 0; i < sumCells.length; i++) {
        total += parseFloat(sumCells[i].innerHTML);
    }
    totalCell.innerHTML = total + "$";
}

const quantityInputs = document.querySelectorAll(".quantity");
quantityInputs.forEach(function (quantityInput) {
    quantityInput.addEventListener("blur", function () {
        const quantity = quantityInput.value;
        const trElement = quantityInput.closest("tr");
        const productIdInput = trElement.querySelector("input[name='Id']");
        const productId = productIdInput.value;

        updateSumCell(productId, quantity);
    });
});

