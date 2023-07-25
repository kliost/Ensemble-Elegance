function NextStatus(Id) {
    var xhr = new XMLHttpRequest();
    xhr.open("GET", "/Admin/SetNextOrderStatus?Id=" + Id, true);
    xhr.onload = function () {
        if (xhr.status === 200) {
            callback();
        }
    };
    xhr.send();
}