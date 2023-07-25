function showConfirmation(productId) {
    $("#deleteBtn_" + productId).hide();
    $("#confirmBtn_" + productId).show();
    $("#cancelBtn_" + productId).show();
}

function confirmDelete(productId) {

    window.location.href = "/admin/delete?id=" + productId;
}

function cancelDelete(productId) {
    $("#deleteBtn_" + productId).show();
    $("#confirmBtn_" + productId).hide();
    $("#cancelBtn_" + productId).hide();
}
