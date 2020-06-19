
$.validator.addMethod("validOldPass", (val, element) => {
    let result = null;
    console.log(val);
    $.ajax({
        url: `/api/studentapi/validate-pass/${val}`,
        method: "GET",
        async: false,
        success: response =>
        {
            result = response;
        },
        dataType: "json"
    });

    console.log(`Old pass validation result: ${typeof result}`);

    return result;
});

let validator = $("#passform").validate
    ({
        rules: {
            confirmpass: {
                equalTo: "#newpass"
            },
            oldpass: "validOldPass"
        },
        messages: {
            oldpass: {
                required: "Ovo polje je obavezno",
                validOldPass: "Neispravna lozinka"
            },
            newpass: "Ovo polje je obavezno",
            confirmpass: {
                required: "Ovo polje je obavezno",
                equalTo: "Stara i nova lozinka nisu jednake"
            }
        },
        errorPlacement: function (label, element) {
            label.addClass('error-field');
            label.insertAfter(element);
        },
        onfocusout: false,
        onkeyup: false
    });


$("#passform #save").click(e => {
    e.preventDefault();

    if (!$("#passform").valid())
        return;

    let data =
    {
        oldpassword: $("#oldpass").val(),
        newpassword: $("#newpass").val(),
        confirmpassword: $("#confirmpass").val()
    };

    console.log(data);


    $.ajax({
        url: "/api/studentapi/update-password",
        data: JSON.stringify(data),
        method: "PUT",
        contentType: "application/json",
        success: () =>
        {
            console.log("Update succesful");
            // TODO: Close the modal
        }
    })
    
});

$("#passform #cancel").click(e => {
    validator.resetForm();
    //TODO: Close modal here
});



$("#btnShowModal").click(function() {
    $("#passModal").modal('show');
});  

$("#cancel").click(function () {
    $("#passModal").modal('hide');
}); 