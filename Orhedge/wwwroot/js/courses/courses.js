function showEditModal(courseParam) {
    document.getElementById('editCourseId').value = courseParam.courseId;
    document.getElementById('modalEditId').style.display = 'block';
}

function showDeleteModal(courseId) {
    document.getElementById('modalDeleteId').style.display = 'block';
    document.getElementById('deleteCourseId').value = courseId;
}

function searchFilter(paramsArray) {
    var numberOfElements = paramsArray.itemCount;
    var pageNumber = paramsArray.pageNumber;
    var searchFor = document.getElementById("search").value;
    url = $("#SearchFilterRedirect").val() + "?studyPrograms=";
    for (i = 0; i < numberOfElements; ++i)
        if (document.getElementById(i + "filter").checked)
            url = url.concat("&studyPrograms=", document.getElementById(i + "filter").value);
    url = url.replace("=&studyPrograms", "");
    url = url + "&searchFor=" + searchFor + "&pageNumber=" + pageNumber;
    location.href = url;
}
function editStudyMaterial(courseId) {
    var formData = $("#editFormId").serializeArray();
    var model = JSON.stringify({
        'StudyMaterialId': formData[0].value,
        'Name': formData[1].value,
        'CategoryId': formData[2].value,
        'CourseId': courseId
    });
    $.ajax({
        type: "PUT",
        url: "/api/StudyMaterialApi",
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
function deleteStudyMaterial(courseId) {
    var formData = $("#deleteFormId").serializeArray();
    var model = JSON.stringify({
        'StudyMaterialId': formData[0].value,
        'CourseId': courseId
    });
    $.ajax({
        type: "PUT",
        url: `/api/StudyMaterialApi/delete`,
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

function appendCategory() {
    var name = document.getElementById('regCategoryId').value;
    document.getElementById('addedCategoriesId').innerHTML = document.getElementById('addedCategoriesId').innerHTML + "<br/>" + name;
}

function saveCourse() {
    var formData = $("#addForm").serializeArray();
    var model = JSON.stringify({
        'Name': formData[0].value,
        'Categories': document.getElementById('addedCategoriesId').innerHTML.split("<br>"),
        'Semester': formData[1].value,
        'StudyProgram': formData[2].value
    });
    $.ajax({
        type: "POST",
        url: `/api/CourseCategoryApi/save`,
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

function deleteCourse() {
    var courseId = document.getElementById('deleteCourseId').value;
    $.ajax({
        type: "PUT",
        url: `/api/CourseCategoryApi/delete/${courseId}`,
        async: false,
        success: response => {
            window.location.href = response;
        },
        error: response => {
            console.log(response);
        }
    })
}