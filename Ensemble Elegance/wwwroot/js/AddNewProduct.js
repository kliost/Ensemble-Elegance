$(document).ready(function () {
    var categoryInput = $('#categoryInput');
    var categoryDropdown = $('#categoryDropdown');
    var selectedCategories = $('#selectedCategories');
    var categories = [];

    var hideDropdownTimeout;

    categoryInput.on('input', function () {
        var input = categoryInput.val();
        if (input.length > 0) {
            $.ajax({
                url: '/categories/search',
                method: 'GET',
                data: { input: input },
                dataType: 'json',
                success: function (response) {
                    categoryDropdown.empty();
                    var filteredCategories = response.filter(function (category) {
                        return !categories.includes(category);
                    });
                    $.each(filteredCategories, function (index, category) {
                        var listItem = $('<li/>').text(category);
                        listItem.on('click', function () {
                            addSelectedCategory(category);
                            categoryInput.val('');
                            categoryDropdown.empty();
                            categoryDropdown.hide();
                        });
                        categoryDropdown.append(listItem);
                    });
                    categoryDropdown.show();
                },
                error: function (xhr, status, error) {
                    console.error(error);
                }
            });
        } else {
            categoryDropdown.empty();
            clearTimeout(hideDropdownTimeout);
            hideDropdownTimeout = setTimeout(function () {
                categoryDropdown.hide();
            }, 200);
        }
    });

    categoryInput.on('focusout', function () {
        clearTimeout(hideDropdownTimeout);
        hideDropdownTimeout = setTimeout(function () {
            categoryDropdown.hide();
        }, 200);
    });

    function addSelectedCategory(category) {
        categories.push(category);
        // Останні три рядки оновлені
        var selectedCategory = $('<div class="selected-category"/>');
        var categoryName = $('<span class="category-name"/>').text(category);
        var deleteButton = $('<span class="delete-category"/>').text('x');

        deleteButton.on('click', function () {
            var index = categories.indexOf(category);
            if (index > -1) {
                categories.splice(index, 1);
            }
            selectedCategory.remove();
            updateSelectedCategoriesInput(); // Оновлення сховища даних
        });

        selectedCategory.append(categoryName);
        selectedCategory.append(deleteButton);
        selectedCategories.append(selectedCategory);

        updateSelectedCategoriesInput(); // Оновлення сховища даних
    }

    function updateSelectedCategoriesInput() {
        var selectedCategoriesInput = $('#selectedCategoriesInput');
        selectedCategoriesInput.val(categories.join(','));
    }
});