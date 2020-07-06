function showAddCategoryModal() {
    document.getElementById('modalAddCategoryId').style.display = 'block';
}

function showDeleteCategoryModal(categoryId) {
    document.getElementById('deleteCategoryId').value = categoryId;
    document.getElementById('modalDeleteCategoryId').style.display = 'block';
}

function addCategory(courseId) {
    var formData = $("#addCategoryFormId").serializeArray();
    var model = JSON.stringify({
        'CourseId': courseId,
        'Category': formData[0].value,
    });
    $.ajax({
        type: "POST",
        url: `/api/CourseCategoryApi`,
        data: model,
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: response => {
            document.getElementById('modalAddCategoryId').style.display = 'none';
            window.location.href = response;
        },
        error: response => {
            console.log(response);
        }
    })
}

function deleteCategory(courseId) {
    var formData = $("#deleteCategoryFormId").serializeArray();
    var model = JSON.stringify({
        'CategoryId': formData[0].value,
        'CourseId': courseId
    });
    $.ajax({
        type: "PUT",
        url: "/api/CourseCategoryApi/category",
        data: model,
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: response => {
            window.location.href = response;
        },
        error: response => {
            console.log(response);
        }
    })
}