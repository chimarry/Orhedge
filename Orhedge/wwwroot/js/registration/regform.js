let requiredMsg = "Ovo polje je obavezno";
$("#regForm").validate(
    {
        rules:
        {
            ConfirmPassword: { equalTo: "#password" },
            Username:
            {
                remote: {
                    url: "/api/registerapi/username-exists"
                }
            }
        },
        messages:
        {
            Username: {required: requiredMsg, remote: "Ovo korisničko ime je zauzeto"},
            Password: requiredMsg,
            ConfirmPassword: { required: requiredMsg, equalTo: "Lozinke nisu jednake" }
        },
        errorPlacement: (label, element) => { label.addClass("invalid-feedback"); label.insertAfter(element); }
    });