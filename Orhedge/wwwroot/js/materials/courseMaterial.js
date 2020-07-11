function showEditModal(studyMaterialParam) {
    document.getElementById('editStudyMaterialId').value = studyMaterialParam.studyMaterialId;
    document.getElementById('name').value = studyMaterialParam.name;
    $('#modalEditId').modal("show");
}

function showDeleteModal(studyMaterialId) {
    $('#modalDeleteId').modal("show");
    document.getElementById('deleteStudyMaterialId').value = studyMaterialId;
}

function showMoveModal(studyMaterialId) {
    $('#modalMoveId').modal("show");
    document.getElementById('moveStudyMaterialId').value = studyMaterialId;
}

$("#editCancel").click(() => $("#modalEditId").modal("hide"));
$("#deleteCancel").click(() => $("#modalDeleteId").modal("hide"));
$("#moveCancel").click(() => $("#modalMoveId").modal("hide"));
$("#modalEditId .close").click(() => $("#modalEditId").modal("hide"));
$("#modalMoveId .close").click(() => $("#modalMoveId").modal("hide"));
$("#modalDeleteId .close").click(() => $("#modalDeleteId").modal("hide"));

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
    $("#editSubmit").prop("disabled", true);
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
    $("#deleteSubmit").prop("disabled", true);
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

function moveStudyMaterial(courseId) {
    $("#moveSubmit").prop("disabled", true);

    let model = {
        "StudyMaterialId": parseInt($("#moveStudyMaterialId").val()),
        "CourseId": courseId,
        "CategoryId": parseInt($("#move-category-input").val())
    };

    $.ajax(
        {
            type: "PUT",
            url: "/api/StudyMaterialApi/move",
            data: JSON.stringify(model),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: response => {
                window.location.href = response;
            },
            error: response => {
                console.log(response);
            }
        });
}

function updateMoveCategories(selectedCourse) {

    let year = $("#move-year-input").val();
    let course = coursesByYear[parseInt(year) - 1].find(el => el.courseId === selectedCourse);
    if (course === undefined) {
        $("#move-category-input").empty();
        return;
    }
    opts = course.categories.map(cat => new Option(cat.name, cat.categoryId.toString(), false, false));
    $("#move-category-input").empty().append(opts);
}


loadCoursesPromise.then(() => {
    let selectedYear = parseInt($("#move-year-input").val()) - 1;
    console.log(coursesByYear);
    $("#move-course-input")
        .autocomplete({
            minLength: 0,
            select: (event, ui) => {
                $("#move-course-input").attr("data-selected-course-id", ui.item.courseId);
                updateMoveCategories(ui.item.courseId);
            },
            search: (event, ui) => {
                $("#move-category-input").empty();
            },
            source: coursesByYear[selectedYear]
        })
        .focus(ev => {
            $("#move-course-input").autocomplete("search", "");
        });
});


$("#move-year-input").change(ev => {
    let year = $("#move-year-input").val();

    $("#move-category-input").empty();
    let courseInput = $("#move-course-input");
    courseInput.autocomplete("disable");
    courseInput.autocomplete("option", "source", coursesByYear[parseInt(year) - 1]);
    courseInput.autocomplete("enable");
});
