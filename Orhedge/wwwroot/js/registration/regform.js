let requiredMsg = "Ovo polje je obavezno";
$("#regForm").validate(
    {
        rules:
        {
            ConfirmPassword: { equalTo: "#password"}
        },
        messages:
        {
            Username: requiredMsg,
            Password: requiredMsg,
            ConfirmPassword: { required: requiredMsg, equalTo: "Lozinke nisu jednake" }
        },
        errorPlacement: (label, element) => { label.addClass("invalid-feedback"); label.insertAfter(element); },
        submitHandler: (form, event) =>
        {
            if (!$(form).isValid())
                event.preventDefault();
        }
    });