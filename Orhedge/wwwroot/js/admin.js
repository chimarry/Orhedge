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
        dataType: "json",
        success: response => {
            window.location.href = response;
        },
        error: response => {
            console.log(response);
        }
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
        dataType: "json",
        success: response => {
            window.location.href = response;
        },
        error: response => {
            console.log(response);
        }
    })
}

function searchSortFilter(paramsArray) {
    var numberOfElements = paramsArray.itemCount;
    var pageNumber = paramsArray.pageNumber;
    var searchFor = document.getElementById("search").value;
    var selectBox = document.getElementById("sort");
    var selectedValue = selectBox.options[selectBox.selectedIndex].value;

    url = $("#SearchSortFilterRedirect").val() + "?privileges=";
    for (i = 0; i < numberOfElements; ++i)
        if (document.getElementById(i + "filter").checked)
            url = url.concat("&privileges=", document.getElementById(i + "filter").value);
    url = url.replace("=&privileges", "");
    url = url + "&searchFor=" + searchFor + " &sortCriteria=" + selectedValue + " &pageNumber=" + pageNumber;
    window.location.href = url;
}

let requiredMsg = "Ovo polje je obavezno";
let regFormValidator = $("#regForm").validate(
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
        submitHandler: (form, e) => {
            $("#sendEmailBttn").prop("disabled", true);

            let data = {
                firstName: $("#regFirstName").val(),
                lastName: $("#regLastName").val(),
                email: $("#regEmail").val(),
                indexNumber: $("#regIndexNumber").val(),
                privilege: $("#regPrivilege").val()
            }

            $.ajax({
                url: "/api/adminapi/send-confirmation-email",
                method: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                statusCode: {
                    400: xhr => {
                        console.log(xhr.responseJSON);
                        if (xhr.responseJSON.error === "EmailAlreadyExists")
                            regFormValidator.showErrors({ email: "Korisnik sa datom email adresom već postoji" });
                        else if (xhr.responseJSON.error === "IndexAlreadyExists")
                            regFormValidator.showErrors({ indexnumber: "Korisnik sa datim indeksom već postoji" });
                        $("#sendEmailBttn").prop("disabled", false);
                    }
                },
                success: () => {
                    alert(`Email uspješno poslan na ${data.email}`);
                    $("#regForm").trigger('reset');
                    $("#sendEmailBttn").prop("disabled", false);
                },
                data: JSON.stringify(data)
            });
        }
    });


