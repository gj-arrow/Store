function del(categoryId) {

    var elem = $(this).closest('.item');

    $.confirm({
        'title': 'Подтверждение удаления',
        'message': 'Если вы удалите категорию, все товары принадлежавшие данной категории будут удалены.Вы уверены?',
        'buttons': {
            'Да': {
                'class': 'green',
                'action': function deleteCategory() {
                    $.ajax({
                        url: '/Admin/DeleteCategory?CategoryId=' + categoryId,
                        type: 'POST',
                        success: location.reload()
                    })
                }
            },
            'Нет': {
                'class': 'red',
                'action': function () { }	// В данном случае ничего не делаем. Данную опцию можно просто опустить.
            }
        }
    });

};


function delUser(userEmail) {

    var elem = $(this).closest('.item');

    $.confirm({
        'title': 'Подтверждение удаления',
        'message': 'Вы точно хотите удалить данного пользователя?',
        'buttons': {
            'Да': {
                'class': 'green',
                'action': function deleteCategory() {
                    $.ajax({
                        url: '/Admin/DeleteUser?userEmail=' + userEmail,
                        type: 'POST',
                        success: location.reload()
                    })
                }
            },
            'Нет': {
                'class': 'red',
                'action': function () { }	// В данном случае ничего не делаем. Данную опцию можно просто опустить.
            }
        }
    });

};



function delProduct(ProductId) {

    var elem = $(this).closest('.item');

    $.confirm({
        'title': 'Подтверждение удаления',
        'message': 'Вы точно хотите удалить данный товар?',
        'buttons': {
            'Да': {
                'class': 'green',
                'action': function deleteCategory() {
                    $.ajax({
                        url: '/Admin/Delete?ProductId=' + ProductId,
                        type: 'POST',
                        success: location.reload()
                    })
                }
            },
            'Нет': {
                'class': 'red',
                'action': function () { }	// В данном случае ничего не делаем. Данную опцию можно просто опустить.
            }
        }
    });

};


