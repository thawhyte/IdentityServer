$(document).ready(function () {
    deleteItems();
    //initFormValidations();

    $(".modal").perfectScrollbar();

});
function deleteItems() {
    if (localStorage) { // Check if the localStorage object exists

        localStorage.clear();  //clears the localstorage

    } else {

        alert("Sorry, no local storage."); //an alert if localstorage is non-existing
    }
}


function clear() {
    $('#PcCode').val('');
    $('#ddlCompany').val('');
    $('#StaffNo').val('');
    $('#ddlMis').val('');
    $('#ddlBranch').val('');
    $('#StaffName').val('');
    $('#ddlDept').val('');
    $('#ddlMis').val('');
    $('#JobTitle').val('');
    $('#Rank').val('');
    $('#ddlUnit').val('');
    $('#ddlGender').val('');
    $('#Age').val('');
    $('#Email').val('');
    $('#Phone').val('');
    $('#Address').val('');
    $('#State').val('');
    $('#Nationality').val('');
    $('#Staffsignature').val('');
    $('#NextOfKinName').val('');
    $('#ddlNextGender').val('');
    $('#NextOfKinAddress').val('');
    $('#NextOfKinEmail').val('');
    $('#NextOfKinPhone').val('');
    $('#RelationShip').val('');
    $('#Username').val('');
    $('#Password').val('');
    $('#ConfirmPassword').val('');
}



$("#Spousephone").change(function myfunction() {
    var form = $("#frmPersonalPrimary");
    var phoneno = /^[+]*[(]{0,1}[0-9]{1,3}[)]{0,1}[-\s\./0-9]*$/g;

    debugger

    var data = {
        Phone: form.find("#Phone").val(),
    };

    if ((data.Phone.match(phoneno))) {
        return true;
    }
    else {
        $('#Phone').val('');
        swal({
            text: 'Alphabets are not allowed for phone numbers!',

        });

        return false;
    }

})




$("#Spousephone").change(function myfunction() {
    var form = $("#frmPersonalPrimary");
    var phoneno = /^[+]*[(]{0,1}[0-9]{1,3}[)]{0,1}[-\s\./0-9]*$/g;

    debugger

    var data = {
        NextOfKinPhone: form.find("#NextOfKinPhone").val(),
    };

    if ((data.NextOfKinPhone.match(phoneno))) {
        return true;
    }
    else {
        $('#NextOfKinPhone').val('');
        swal({
            text: 'Alphabets are not allowed for phone numbers!',

        });

        return false;
    }

})





//Notification


//---------------------------------
$(document).ready(function ($) {

    $('#btnRegisters').on('click', function () {
        debugger
       //tblStaffInformation();
       addRegisters();

    });

});


function addRegisters() {
    debugger
    $("input[type=submit]").attr("disabled", "disabled");
    $('#frmRegister').validate({
        messages: {
            StaffNames: { required: "Fullname is required" },
            StaffNos: { required: "Staff No. is required" },
            JobTitles: { required: "Job Title is required" },
            Ages: { required: "Age is required" },
            Phones: { required: "Phone No. is required" },
            Addresss: { required: "Address is required" },
            Nationalitys: { required: "Nationality is required" },
            Usernames: { required: "Username is required" },
            Passwords: { required: "Password is required" },
            Age: "Date of birth is required",
            //staffsignature: { required: "Signature is required" }

        },
        //errorPlacement: function (error, element) {
        //    $.notify({
        //        icon: "now-ui-icons travel_info",
        //        message: error.text(),
        //    }, {
        //            type: 'danger',
        //            placement: {
        //                from: 'top',
        //                align: 'right'
        //            }
        //        });
        //},
        submitHandler: function (form) {
            swal({
                title: "Are you sure?",
                text: "New staff will be created!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#ff9800",
                confirmButtonText: "Yes, continue",
                cancelButtonText: "No, stop!",
                showLoaderOnConfirm: true,
                preConfirm: function () {
                    return new Promise(function (resolve) {
                        setTimeout(function () {
                            resolve();
                        }, 4000);
                    });
                }
            }).then(function (isConfirm) {
                if (isConfirm) {
                    var Staffsignature = $("#staffsignature").get(0).files;
                    debugger;
                    //if (!Staffsignature) {
                    //return;
                    //}                   
                    var signatureimgData = new FormData();

                    //for (var signatureCounter = 0; signatureCounter < Staffsignature.length; signatureCounter++) {
                    //    signatureimgData.append("Staffsignature", staffsignature[signatureCounter])
                    //};

                    signatureimgData.append("Staffsignature", $("#staffsignature").get(0).files[0]);

                    var staffinformation = {
                        Miscode: $('#miscode').val(),
                        companyId: $('#ddlCompany').val(),
                        staffNo: $('#StaffNo').val(),
                        designationCode: $('#ddlMis').val(),
                        BranchId: $('#ddlBranch').val(),
                        staffName: $('#StaffName').val(),
                        DeptCode: $('#ddlDept').val(),
                        Rank: $('#ddlRanks').val(),
                        jobTitle: $('#JobTitle').val(),
                        rank: $('#Rank').val(),
                        Unit: $('#ddlUnit').val(),
                        gender: $('#ddlGender').val(),
                        age: $('#Age').val(),
                        Email: $('#Email').val(),
                        phone: $('#Phone').val(),
                        address: $('#Address').val(),
                        state: $('#ddlState').val(),
                        nationality: $('#ddlCountry').val(),
                        staffsignature: $("#Staffsignature").val(),
                        nextOfKinName: $('#NextOfKinName').val(),
                        nextOfKinGender: $('#ddlNextGender').val(),
                        nextOfKinAddress: $('#NextOfKinAddress').val(),
                        nextOfKinEmail: $('#NextOfKinEmail').val(),
                        nextOfKinPhone: $('#NextOfKinPhone').val(),
                        relationShip: $('#RelationShip').val()
                    };
                    var register_datas = {
                        username: $('#Username').val(),
                        password: $('#password').val(),
                        confirmPassword: $('#ConfirmPassword').val()
                    }
                    signatureimgData.append("staffInformation", JSON.stringify(staffinformation));
                    signatureimgData.append("Username", $('#Username').val());
                    signatureimgData.append("Password", $('#password').val());
                    signatureimgData.append("ConfirmPassword", $('#ConfirmPassword').val());
                    debugger
                    $.ajax({
                        url: "../Administration/Register",
                        type: "POST",
                        data: signatureimgData,
                        contentType: false,
                        processData: false,

                        success: function (result) {
                            debugger
                            if (result !== '') {

                                //tblStaffInformation();
                                swal({ title: 'Add New Staff', text: 'New Staff added successfully!', type: 'success' }).then(
                                    setTimeout(function () { window.location.replace("/Account/Login"); }, 3000)
                                    //{ clear(); }
                                )
                            }

                            else {
                                swal({
                                    title: 'Add New Staff', text: 'Something went wrong: </br>' + result.toString(), type: 'error'
                                }).then(function () { });
                                $("#btnRegisters").removeAttr("disabled");
                                deleteItems();
                            }

                            $("#btnRegisters").removeAttr("disabled");
                            // window.location.href="../Account/login"

                        },
                        error: function (e) {

                            swal({ title: 'Add New Staff', text: 'Adding new staff encountered an error', type: 'error' }).then(function () { });
                            $("#btnRegisters").removeAttr("disabled");
                            deleteItems();
                        }
                    });
                }
            });
        },
        function(dismiss) {
            swal('Add New Staff', 'You cancelled add new staff.', 'error');
            $("#btnRegister").removeAttr("disabled");
        }

    })

}

$(document).ready(function () {

        $("#ddlCompany").select2({
            theme: "bootstrap4",
            placeholder: "Loading..."
        });
        $.ajax({
            url: "loadCompany",
        }).then(function (response) {

            $("#ddlCompany").select2({
                theme: "bootstrap4",
                placeholder: "Select Company",
                data: response.results
            });
        });


    $('#ddlCompany').on('change', function () {
        debugger
        $("#ddlBranch").select2({
            theme: "bootstrap4",
            placeholder: "Loading..."
        });
        $.ajax({
            url: "loadBranch",
            data: { CoyId: $('#ddlCompany').val() },
            type: "POST"
        }).then(function (response) {
            $("#ddlBranch").empty().trigger('change');
            $("#ddlBranch").select2({               
                theme: "bootstrap4",
                placeholder: "Select Branch",
                data: response.results
            });
        });
    });


});

$(document).ready(function ($) {
    debugger
    //for dropdown list control for Comapny (id=ddlCompany)starts here

    $("#ddlDept").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",

    });

    $.ajax({
        url: "../Administration/loadDept",
    }).then(function (response) {
        debugger
        $("#ddlDept").select2({
            theme: "bootstrap4",
            // placeholder: "Select Company...",  
            width: '100%',
            data: response.results
        });
    });
});

$(document).ready(function ($) {
    debugger

    $("#ddlRanks").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",

    });
    $.ajax({
        url: "loadRank",
    }).then(function (response) {
        debugger
        $("#ddlRanks").select2({
            theme: "bootstrap4",
            placeholder: "Select Rank...",  
            width: '100%',
            data: response.results
        });
    });
});

$(document).ready(function ($) {
    debugger
    $("#miscode").select2({
        theme: "bootstrap4",
        placeholder: "Loading..."
    });
    $.ajax({
        url: "/administration/loadMisCode"
    }).then(function (response) {

        $("#miscode").select2({
            theme: "bootstrap4",
            placeholder: "Select MisCode",
            width: '100%',
            data: response.results
        });
    });
    //for dropdown list control for Comapny (id=ddlCompany)starts here

    $("#ddlMis").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",

    });

    $.ajax({
        url: "/Administration/loadMis",
    }).then(function (response) {
        debugger
        $("#ddlMis").select2({
            theme: "bootstrap4",
            // placeholder: "Select Company...",  
            width: '100%',
            data: response.results
        });
    });
});


$(document).ready(function ($) {
    debugger
    //for dropdown list control for Comapny (id=ddlCompany)starts here

    $("#ddlUnit").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",

    });

    $.ajax({
        url: "../Administration/loadUnit",
    }).then(function (response) {
        debugger
        $("#ddlUnit").select2({
            theme: "bootstrap4",
            // placeholder: "Select Company...",  
            width: '100%',
            data: response.results
        });
    });
});

//-Email Validation
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
function validateEmail(email) {
    return emailReg.test(email);
}
var clicked = jQuery("button");
//$(document).ready(function ($) {
//    $("#Email").mouseout(function (event) {
//        debugger
//        var email = jQuery("input[type='email']").val();

//        if (!validateEmail(email)) {

//            $.notify({ message:"Invalid email address", type:"warning" });
//        }

//        else {
//            $.notify({ message: "Email address is valid", type: "success" });
//        }

//    });
//});

$(document).ready(function ($) {
    debugger 
    $("#ddlCountry").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",
    });
    $.ajax({
        url: "loadCountry",
    }).then(function (response) {
        debugger
        $("#ddlCountry").select2({
            theme: "bootstrap4",
            placeholder: "Select Country",
            width: '100%',
            data: response.results
        });
    });


    $("#ddlCountry").on("select2:select", function (e) {
       
        debugger
        $.ajax({
            url: "LoadState",
            data: { country: $('#ddlCountry').val() },
            type: "POST",
        }).then(function (response) {
            debugger
            
            if (e.params.data.text == "Nigeria") {
                $("#ddlState").empty().trigger('change');
                $("#ddlState").select2({
                    theme: "bootstrap4", 
                    placeholder: "Select State",
                    width: '100%',
                    data: response.results
                });
            }
            else {
                $("#ddlState").empty().trigger('change');
                $("#ddlState").select2({
                    theme: "bootstrap4",
                    placeholder: "Select State",
                    width: '100%',
                    data: [ e.params.data.text] 
                });

            }
        });

    });
});


//$(document).ready(function () {
   //debugger

    $('#Age').datetimepicker({
        format: "YYYY-MM-DD",
         //changeMonth: true,
          //  changeYear: true,
            minDate: new Date(1940, 0, 1),
            maxDate: new Date(2001, 11, 31),

        icons: {
            time: "now-ui-icons tech_watch-time",
            date: "now-ui-icons ui-1_calendar-60",
            up: "fa fa-chevron-up",
            down: "fa fa-chevron-down",
            previous: 'now-ui-icons arrows-1_minimal-left',
            next: 'now-ui-icons arrows-1_minimal-right',
            today: 'fa fa-screenshot',
            clear: 'fa fa-trash',
            close: 'fa fa-remove',
            }


    });

//$(document).ready(function () {
//    debugger


    //$('#Age')
    //    .datetimepicker({                        
    //    })
    //    .on('changeDate', function (e) {
    //        // Revalidate the date field
    //        $('#eventForm').formValidation('revalidateField', 'date');
    //    });


//    var yearRange = $("#Age").datepicker("option", "yearRange");

//    // Setter
//    $("#Age").datepicker("option", "yearRange", "2002:2012");

//});


//Image Upload


function tblStaffInformation() {
    $("input[type=submit]").attr("disabled", "disabled");
    var Staffsignature = $("#staffsignature").get(0).files;    
    debugger;
    if (!Staffsignature) {
        return;
    }
    var register_datas = {
        staffInformation: {
            pcCode: $('#PcCode').val(),
            companyId: $('#ddlCompany').val(),
            staffNo: $('#StaffNo').val(),
            designationCode: $('#ddlMis').val(),
            BranchId: $('#ddlBranch').val(),
            staffName: $('#StaffName').val(),
            DeptCode: $('#ddlDept').val(),
            Rank: $('#ddlRanks').val(),
            jobTitle: $('#JobTitle').val(),
            rank: $('#Rank').val(),
            Unit: $('#ddlUnit').val(),
            gender: $('#ddlGender').val(),
            age: $('#Age').val(),
            Email: $('#Email').val(),
            phone: $('#Phone').val(),
            address: $('#Address').val(),
            state: $('#ddlState').val(),
            nationality: $('#ddlCountry').val(),
            staffsignature: $("#Staffsignature").val(),
            nextOfKinName: $('#NextOfKinName').val(),
            nextOfKinGender: $('#ddlNextGender').val(),
            nextOfKinAddress: $('#NextOfKinAddress').val(),
            nextOfKinEmail: $('#NextOfKinEmail').val(),
            nextOfKinPhone: $('#NextOfKinPhone').val(),
            relationShip: $('#RelationShip').val()
        },
        //Staffsignature: $("#Staffsignature").get(0).files, 
        staffsignature: $("#Staffsignature").val(),

        username: $('#Username').val(),
        password: $('#password').val(),
        confirmPassword: $('#ConfirmPassword').val()

    }; 
    var signatureimgData = new FormData();  

    for (var signatureCounter = 0; signatureCounter < Staffsignature.length; signatureCounter++) {
        signatureimgData.append("Staffsignature", staffsignature[signatureCounter]);
    }
    debugger;
    $.ajax("AddSignature", {
        method: "POST",
        contentType: false,
        processData: false,
        data: { signatureimgData },
        success: function (data) {
            swal({ title: 'Add Signature ', text: 'Signature  added successfully!', type: 'success' }).then(function () { clear(); });

            //$('#AddNewLoanBooking').modal('hide');
        },
        error: function (e) {
            swal({ title: 'Signature ', text: 'Signature  encountered an error', type: 'error' }).then(function () { clear(); });

        }
    });
}




$(document).ready(function ($) {
    debugger
    $("input[required]").parent("label").addClass("required");  
});













