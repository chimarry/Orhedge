function showEditModal(studentParam) {
    document.getElementById('studentId').value = studentParam.studentId;
    document.getElementById('nameId').value = studentParam.name;
    document.getElementById('lastNameId').value = studentParam.lastName;
    document.getElementById('indexNumberId').value = studentParam.index;
    document.getElementById('modalEditId').style.display = 'block';
}

function showDeleteModal(studentId) {
    document.getElementById('modalDeleteId').style.display = 'block';
    document.getElementById('deleteStudentId').value = studentId;
}

function editUser() {
    var formData = $("#editFormId").serializeArray();
    var model = JSON.stringify({
        'StudentId': formData[0].value,
        'Name': formData[1].value,
        'LastName': formData[2].value,
        'Index': formData[3].value,
        'Privilege': formData[4].value
    });
    $.ajax({
        type: "PUT",
        url: "/api/AdminApi",
        data: model,
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    })
}
function deleteUser() {
    var formData = $("#deleteFormId").serializeArray();
    var model = JSON.stringify({
        'StudentId': formData[0].value
    });
    $.ajax({
        type: "PUT",
        url: "/api/AdminApi/delete",
        data: model,
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    })
}

function searchSortFilter(numberOfElements) {
    var searchFor = document.getElementById("search").value;
    var selectBox = document.getElementById("sort");
    var selectedValue = selectBox.options[selectBox.selectedIndex].value;
    var url = "SearchSortFilter?privileges=";

    // Check if redirected
    var pathname = window.location.pathname;
    if (pathname.indexOf("Admin") < 0)
        url = "Admin/" + url;

    for (i = 0; i < numberOfElements; ++i)
        if (document.getElementById(i + "filter").checked)
            url = url.concat("&privileges=", document.getElementById(i + "filter").value);
    url = url.replace("=&privileges", "");
    url = url + "&searchFor=" + searchFor + " &sortCriteria=" + selectedValue;
    window.location.href = url;
}

let requiredMsg = "Ovo polje je obavezno";
$("#regForm").validate(
    {
        messages:
        {
            firstname: requiredMsg,
            lastname: requiredMsg,
            email: {
                required: requiredMsg,
                email: "Neispravna email adresa"
            },
            indexnumber: {
                required: requiredMsg,
                pattern: "Neispravan broj indeksa"
            }
        },
        errorPlacement: function (label, element) {
            label.addClass('error-msg');
            label.insertAfter(element);
        },
    });

$("#sendEmailBttn").click(e => {
    e.preventDefault();

    if (!$("#regForm").valid())
        return;

    // Add code which submits the form via ajax
});
