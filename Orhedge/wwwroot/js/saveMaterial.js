function saveMaterial() {
    var myData = $(document.getElementById("uploadForm")).serialize();
    var basicInfo = myData.split("&");
    var category = basicInfo[0].replace("category=", "");
    var course = basicInfo[1].replace("+", "").replace("course=", "");
    course = course.replace(new RegExp("%20", "g"), " ");
    var url = $(document.getElementById("output")).attr('href');
    var urlInfo = url.split(";");
    var fileType = urlInfo[0].substring(5).split("/")[1];
    var fileData = urlInfo[1];
    var fileEncodingIndex = fileData.indexOf(",");
    var fileEncoding = fileData.substring(0, fileEncodingIndex);
    fileData = fileData.substring(fileEncodingIndex + 1);
    var model = JSON.stringify({
        'Category': category,
        'Course': course,
        'FileExtension': fileType,
        'FileData': fileData,
        'FileEncoding': fileEncoding
    });
    $.ajax({
        type: "POST",
        url: "/api/StudyMaterialApi",
        data: model,
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    })
}

var openFile = function (event) {
    var input = event.target;

    var reader = new FileReader();
    reader.onload = function () {
        var dataURL = reader.result;
        var output = document.getElementById('output');

        output.href = dataURL;
    };
    reader.readAsDataURL(input.files[0]);
}