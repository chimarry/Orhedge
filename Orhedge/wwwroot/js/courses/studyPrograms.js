function showAddInStudyProgramModal() {
    document.getElementById('modalAddSpId').style.display = 'block';
}

function showDeleteFromStudyProgramModal(param) {
    document.getElementById('deleteSemesterId').value = param.semesterEnum;
    document.getElementById('deleteStudyProgramId').value = param.studyProgramEnum;
    document.getElementById('modalDeleteSpId').style.display = 'block';
}

function addInStudyProgram(courseId) {
    var formData = $("#addInSpFormId").serializeArray();
    var model = JSON.stringify({
        'CourseId': courseId,
        'Semester': formData[0].value,
        'StudyProgram': formData[1].value
    });
    $.ajax({
        type: "POST",
        url: "/api/CourseCategoryApi/studyProgram",
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

function deleteFromStudyProgram(courseId) {
    var formData = $("#deleteFromStudyProgramFormId").serializeArray();
    var model = JSON.stringify({
        'Semester': formData[0].value,
        'StudyProgram': formData[1].value,
        'CourseId': courseId
    });
    $.ajax({
        type: "PUT",
        url: "/api/CourseCategoryApi/studyProgram",
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