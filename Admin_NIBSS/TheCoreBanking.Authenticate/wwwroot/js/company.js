
function companyFormatter(value, row, index) {
    return [
       
        '<a style="color:white" class="edit btn btn-sm  btn-danger"  title="Edit Company">'
        + '<i class="now-ui-icons ui-2_settings-90"></i>' +
        '<a style="color:white"  title="Remove company" class="remove btn btn-sm btn-danger">'
        + '<i class="now-ui-icons ui-1_simple-remove"></i></a>' +
        '</a> '
    ].join('');
}

window.companyEvents = {
    'click .edit': function (e, value, row, index) {
        if (row.state = true) {
            var data = JSON.stringify(row);
            $('#Id').val(row.id);
            $('#CoyId').val(row.coyId);
            $('#CoyName').val(row.coyName);
            $('#Address').val(row.address);
            $('#Telephone').val(row.telephone);
            $('#Fax').val(row.fax);
            $('#Email').val(row.email);
            $('#DateOfIncorporation').val(row.dateOfIncorporation);
            $('#Manager').val(row.manager);
            $('#NatureOfBusiness').val(row.natureOfBusiness);
            $('#NameOfScheme').val(row.nameOfScheme);
            $('#FunctionsRegistered').val(row.functionsRegistered);
            $('#AuthorisedShareCapital').val(row.authorisedShareCapital);
            $('#NameOfRegistrar').val(row.nameOfRegistrar);       
            $('#NameOfTrustees').val(row.nameOfTrustees);
            $('#FormerManagersTrustees').val(row.formerManagersTrustees);
            $('#DateOfRenewalOfRegistration').val(row.dateOfRenewalOfRegistration);
            $('#DateOfCommencement').val(row.dateOfCommencement);
            $('#InitialFloatation').val(row.initialFloatation);
            $('#InitialSubscription').val(row.initialSubscription);
            $('#CoyRegisteredBy').val(row.coyRegisteredBy);
            $('#TrusteesAddress').val(row.trusteesAddress);
            $('#InvestmentObjective').val(row.investmentObjective);
            $('#MgtType').val(row.mgtType);
            $('#Webbsite').val(row.webbsite);
            $('#CoyClass').val(row.coyClass);
            $('#AccountStand').val(row.accountStand);
            $('#ManagementType').val(row.managementType);
            $('#EoyprofitAndLossGl').val(row.eoyprofitAndLossGl);       
            $('#NameOfRegistrar').val(row.nameOfRegistrar);
            $('#AddNewCompany').modal('show');
            $('#btnCompanyUpdate').html('  <i class="now-ui-icons ui-1_check"></i> Update Record');
        }
    },
    'click .remove': function (e, value, row, index) {
        info = JSON.stringify(row);
        console.log(info);

        debugger
        $('#ID').val(row.id);
        $.ajax({
            url: 'CompanySetup/RemoveCompany',
            type: 'POST',
            data: { ID: row.id },
            success: function (data) {
                swal({
                    title: "Are you sure?",
                    text: "You are about to delete this record!",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#ff9800",
                    confirmButtonText: "Yes, proceed",
                    cancelButtonText: "No, cancel!",
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



                        swal("Deleted succesfully");
                        //alert('Deleted succesfully');
                        $('#companyTable').
                            bootstrapTable(
                                'refresh', { url: 'CompanySetup/listcompany' });

                        //return false;
                    }
                    else {
                        swal("Company", "You cancelled add company.", "error");
                    }
                    $('#companyTable').
                        bootstrapTable(
                            'refresh', { url: 'CompanySetup/listcompany' });
                });
                return

            },

            error: function (e) {
                //alert("An exception occured!");
                swal("An exception occured!");
            }
        });
    }

};
$(document).ready(function ($) {

    $('#btnCompanyUpdate').on('click', function () {
        debugger
        updateCompany();
    });

});
function updateCompany() {
    debugger
    var json_data = {};
    json_data.Id=$('#Id').val();
    json_data.CoyId=$('#CoyId').val();
    json_data.CoyName=$('#CoyName').val();
    json_data.Address = $('#Address').val();
    json_data.Telephone=$('#Telephone').val();
    json_data.Fax=$('#Fax').val();
    json_data.Email=$('#Email').val();
    json_data.DateOfIncorporation=$('#DateOfIncorporation').val();
    json_data.Manager=$('#Manager').val();
    json_data.NatureOfBusiness=$('#NatureOfBusiness').val();
    json_data.NameOfScheme=$('#NameOfScheme').val();
    json_data.FunctionsRegistered=$('#FunctionsRegistered').val();
    json_data.AuthorisedShareCapital=$('#AuthorisedShareCapital').val();
    json_data.NameOfRegistrar=$('#NameOfRegistrar').val();
    json_data.NameOfTrustees=$('#NameOfTrustees').val();
    json_data.FormerManagersTrustees=$('#FormerManagersTrustees').val();
    json_data.DateOfRenewalOfRegistration=$('#DateOfRenewalOfRegistration').val();
    json_data.DateOfCommencement=$('#DateOfCommencement').val();
    json_data.InitialFloatation=$('#InitialFloatation').val();
    json_data.InitialSubscription=$('#InitialSubscription').val();
    json_data.CoyRegisteredBy=$('#CoyRegisteredBy').val();
    json_data.TrusteesAddress=$('#TrusteesAddress').val();
    json_data.InvestmentObjective=$('#InvestmentObjective').val();
    json_data.MgtType=$('#MgtType').val();
    json_data.Webbsite=$('#Webbsite').val();
    json_data.CoyClass=$('#CoyClass').val();
    json_data.AccountStand=$('#AccountStand').val();
    json_data.ManagementType=$('#ManagementType').val();
    json_data.EoyprofitAndLossGl=$('#EoyprofitAndLossGl').val();
    json_data.NameOfRegistrar=$('#NameOfRegistrar').val();
  

    $("input[type=submit]").attr("disabled", "disabled");

    $('#frmCompany').validate({
        //rule: {
        //    coyid: {greaterThanZero:true}
        //},
        messages: {
            coyID: { required: "Company ID is required" },
            coyName: { required: "Company Name is required" },
            Address: { required: "Address is required" },
            Telephone: { required: "Telephone is required" },
            Email: { required: "Email is required" }
        },
        errorPlacement: function (error, element) {
            $.notify({
                icon: "now-ui-icons travel_info",
                message: error.text(),
            }, {
                    type: 'danger',
                    placement: {
                        from: 'top',
                        align: 'right'
                    }
                });
        },
        submitHandler: function (form) {
            swal({
                title: "Are you sure?",
                text: "Company will be updated!",
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
                    $("#btnCompanyUpdate").attr("disabled", "disabled");
                    debugger
                    $.ajax({
                        url: 'CompanySetup/UpdateCompany',
                        type: 'POST',
                        data: json_data,
                        dataType: "json",
                        //headers: {
                        //    'VerificationToken': forgeryId
                        //},
                        success: function (result) {

                            if (result.toString != '' && result != null) {
                                swal({ title: 'Update Company', text: 'Company updated successfully!', type: 'success' }).then(function () { clear(); });

                                $('#companyTable').
                                    bootstrapTable(
                                        'refresh', { url: 'CompanySetup/listcompany' });

                                $("#btnCompanyUpdate").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Update Company', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnCompanyUpdate").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Update Company', text: 'Company update encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnCompanyUpdate").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Update Company', 'You cancelled company update.', 'error');
            $("#btnCompanyUpdate").removeAttr("disabled");
        });

}



$(document).ready(function ($) {

    $('#btnCompany').on('click', function () {
        debugger
        addCompany();
    });

});
function addCompany() {
    debugger
    $('#btnCompanyUpdate').hide();

    $("input[type=submit]").attr("disabled", "disabled");
    $('#frmCompany').validate({
        messages: {
            coyID: { required: "Company ID is required" },
            coyName: { required: "Company Name is required" },
            Address: { required: "Address is required" },
            Telephone: { required: "Telephone is required" },
            Email: { required: "Email is required" }
        },
        errorPlacement: function (error, element) {
            $.notify({
                icon: "now-ui-icons travel_info",
                message: error.text(),
            }, {
                    type: 'danger',
                    placement: {
                        from: 'top',
                        align: 'right'
                    }
                });
        },
        submitHandler: function (form) {
            swal({
                title: "Are you sure?",
                text: "Company will be Added!",
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
                    $("#btnCompany").attr("disabled", "disabled");

                    debugger      
                    var json_data = {};
                    json_data.Id = $('#Id').val();
                    json_data.CoyId = $('#CoyId').val();
                    json_data.CoyName = $('#CoyName').val();
                    json_data.Address = $('#Address').val();
                    json_data.Telephone = $('#Telephone').val();
                    json_data.Fax = $('#Fax').val();
                    json_data.Email = $('#Email').val();
                    json_data.DateOfIncorporation = $('#DateOfIncorporation').val();
                    json_data.Manager = $('#Manager').val();
                    json_data.NatureOfBusiness = $('#NatureOfBusiness').val();
                    json_data.NameOfScheme = $('#NameOfScheme').val();
                    json_data.FunctionsRegistered = $('#FunctionsRegistered').val();
                    json_data.AuthorisedShareCapital = $('#AuthorisedShareCapital').val();
                    json_data.NameOfRegistrar = $('#NameOfRegistrar').val();
                    json_data.NameOfTrustees = $('#NameOfTrustees').val();
                    json_data.FormerManagersTrustees = $('#FormerManagersTrustees').val();
                    json_data.DateOfRenewalOfRegistration = $('#DateOfRenewalOfRegistration').val();
                    json_data.DateOfCommencement = $('#DateOfCommencement').val();
                    json_data.InitialFloatation = $('#InitialFloatation').val();
                    json_data.InitialSubscription = $('#InitialSubscription').val();
                    json_data.CoyRegisteredBy = $('#CoyRegisteredBy').val();
                    json_data.TrusteesAddress = $('#TrusteesAddress').val();
                    json_data.InvestmentObjective = $('#InvestmentObjective').val();
                    json_data.MgtType = $('#MgtType').val();
                    json_data.Webbsite = $('#Webbsite').val();
                    json_data.CoyClass = $('#CoyClass').val();
                    json_data.AccountStand = $('#AccountStand').val();
                    json_data.ManagementType = $('#ManagementType').val();
                    json_data.EoyprofitAndLossGl = $('#EoyprofitAndLossGl').val();
                    json_data.NameOfRegistrar = $('#NameOfRegistrar').val();

                    $.ajax({
                        url: 'CompanySetup/AddCompany',
                        type: 'POST',
                        data: json_data,
                        dataType: "json",
                        //headers: {
                        //    'VerificationToken': forgeryId
                        //},
                        success: function (result) {
                           
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Add Company', text: 'Company added successfully!', type: 'success' }).then(function () { clear(); });

                                $('#companyTable').
                                    bootstrapTable(
                                        'refresh', { url: 'CompanySetup/listcompany' });

                                $("#btnCompany").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Add Company', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnCompany").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Add Company', text: 'Adding company encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnCompany").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Add Company', 'You cancelled add company.', 'error');
            $("#btnCompany").removeAttr("disabled");
        });

}


function reloadpage() {
    location.reload();
}

function clear() {
    $('#Id').val('');
    $('#coyID').val('');
    $('#coyName').val('');
    $('#Address').val('');
    $('#Telephone').val('');
    $('#Fax').val('');
    $('#Email').val('');
    $('#DateOfIncorporation').val('');
    $('#Manager').val('');
    $('#NatureOfBusiness').val('');
    $('#NameOfScheme').val('');
    $('#FunctionsRegistered').val('');
    $('#AuthorisedShareCapital').val('');
    $('#NameOfRegistrar').val('');
    $('#NameOfTrustees').val('');
    $('#FormerManagers_Trustees').val('');
    $('#DateOfRenewalOfRegistration').val('');
    $('#DateOfCommencement').val('');
    $('#InitialFloatation').val('');
    $('#InitialSubscription').val('');
    $('#CoyRegisteredBy').val('');
    $('#TrusteesAddress').val('');
    $('#InvestmentObjective').val('');
    $('#CompanyClass').val('');
    $('#CompanyType').val('');
    $('#AccountingStandard').val('');
    $('#MgtType').val('');
    $('#Webbsite').val('');
    $('#CoyClass').val('');
    $('#AccountStand').val('');
    $('#ManagementType').val('');
    $('#EOYProfitAndLossGL').val('');
  
}



$('#companyTable').on('expand-row.bs.table', function (e, index, row, $detail) {
    $detail.html('Loading request...');

    var htmlData = '';
    var header = '<div>';
    var footer = '</div>';
    htmlData = htmlData + header;

    debugger

    var html =
        '<h8>' +
        '<p style="text-align:left">' +
        '<strong> Company Name:</strong> &nbsp' + row.coyName + '' +
        '<strong> Address:</strong>  &nbsp' + row.address + '' +
        '<strong> Telephone:</strong>  &nbsp' + row.telephone + '' +
        '<strong> Email:</strong>  &nbsp' + row.email + '' +
        '<strong> Website:</strong>  &nbsp' + row.webbsite + '' +
        '<strong>Date Of Incorporation:</strong>  &nbsp' + row.dateOfIncorporation + '' + '<p>' +

        '<strong> Investment Objective:</strong> &nbsp' + row.investmentObjective + '' +
        '<strong> Company Class:</strong>  &nbsp' + row.companyClass + '' +
        '<strong> Company Type:</strong>  &nbsp' + row.companyType + '' +
        '<strong> Management Type:</strong>  &nbsp' + row.managementType + '' +
        '<strong> Manager:</strong>  &nbsp' + row.manager + '' +
        '<strong> Nature Of Business:</strong>  &nbsp' + row.natureOfBusiness + ''+
        '<strong> Name Of Scheme :</strong>  &nbsp' + row.nameOfScheme + '' +
        '<strong> Functions Registered:</strong>  &nbsp' + row.functionsRegistered + '' +
        '<strong> Authorised Share Capital:</strong>  &nbsp' + row.authorisedShareCapital + '' +
        '<strong> Name Of Registrar:</strong>  &nbsp' + row.nameOfRegistrar + '' + '<p>' +

        '<strong> Name Of Trustees:</strong>  &nbsp' + row.nameOfTrustees + '' +
        '<strong> Former Managers Trustees:</strong>  &nbsp' + row.formerManagersTrustees + '' +
        '<strong> Date Of Renewal Registration:</strong>  &nbsp' + row.dateOfRenewalOfRegistration + '' +
        '<strong> Date Of Commencement:</strong>  &nbsp' + row.dateOfCommencement + '' +
        '<strong> Initial Floatation:</strong>  &nbsp' + row.initialFloatation + '' +
        '<strong> Initial Subscription:</strong>  &nbsp' + row.initialSubscription + '' +
        '<strong> Accounting Standard:</strong>  &nbsp' + row.accountingStandard + '' +
        '<strong> End of Year Profit or Loss Ledger:</strong>  &nbsp' + row.eoyprofitAndLossGl + ''
        ;

    htmlData = htmlData + html;
    htmlData = htmlData + footer;
    $detail.html(htmlData);
});