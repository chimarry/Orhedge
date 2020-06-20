function showEditModal(studyMaterialParam) {
    document.getElementById('editStudyMaterialId').value = studyMaterialParam.studyMaterialId;
    document.getElementById('name').value = studyMaterialParam.name;
    document.getElementById('modalEditId').style.display = 'block';
}

function showDeleteModal(studyMaterialId) {
    document.getElementById('modalDeleteId').style.display = 'block';
    document.getElementById('deleteStudyMaterialId').value = studyMaterialId;
}

function searchSortFilter(paramsArray) {
    var numberOfElements = paramsArray.itemCount;
    var pageNumber = paramsArray.pageNumber;
    var courseId = paramsArray.courseId;
    var searchFor = document.getElementById("search").value;
    var selectBox = document.getElementById("sort");
    var selectedValue = selectBox.options[selectBox.selectedIndex].value;

    url = $("#SearchSortFilterRedirect").val() + "?categories=";
    for (i = 0; i < numberOfElements; ++i)
        if (document.getElementById(i + "filter").checked)
            url = url.concat("&categories=", document.getElementById(i + "filter").value);
    url = url.replace("=&categories", "");
    url = url + "&searchFor=" + searchFor + "&sortCriteria=" + selectedValue + "&courseId=" + courseId + "&pageNumber=" + pageNumber;
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