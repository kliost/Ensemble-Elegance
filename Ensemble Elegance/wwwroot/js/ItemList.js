function showConfirmation(itemId) {
    $("#deleteBtn_" + itemId).hide();
    $("#confirmBtn_" + itemId).show();
    $("#cancelBtn_" + itemId).show();
}

function confirmDelete(itemId) {
    // Виконати видалення, наприклад, перейти за посиланням /delete?id=itemId
    window.location.href = "/admin/delete?id=" + itemId;
}

function cancelDelete(itemId) {
    $("#deleteBtn_" + itemId).show();
    $("#confirmBtn_" + itemId).hide();
    $("#cancelBtn_" + itemId).hide();
}
