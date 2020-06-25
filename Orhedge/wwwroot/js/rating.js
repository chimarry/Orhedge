function rate(parameters) {
    var studentId = parameters.studentId;
    var studyMaterialId = parameters.studyMaterialId;
    var rating = parameters.rating;
    for (i = 1; i < rating + 1; ++i) {
        var button = document.getElementById(studyMaterialId + "" + i);
        button.style.background = "#4facac";
        button.style.backgroundRepeat = "no-repeat";
    }
    for (i = rating + 1; i < 6; ++i) {
        var button = document.getElementById(studyMaterialId + "" + i);
        button.style.background = "transparent";
        button.style.backgroundRepeat = "no-repeat";
    }
    var model = JSON.stringify({
        'StudyMaterialId': studyMaterialId,
        'Rating': rating,
        'AuthorId': studentId
    });
    $.ajax({
        type: "PUT",
        url: "/api/StudyMaterialApi/rate",
        data: model,
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: response => {
            location.href = location.href;
        },
        error: response => {
            console.log(response);
        }
    })
}
