
function branchFormatter(value, row, index) {
    return [
        '<a style="color:white"  class="edit btn btn-sm btn-danger"  title="Edit Branch">'
        + '<i class="now-ui-icons ui-2_settings-90"></i>' +
        '<a style="color:white" title="Remove branch" class="remove btn btn-sm btn-danger">'
        + '<i class="now-ui-icons ui-1_simple-remove"></i></a>' +
        '</a> '
    ].join('');
}

window.branchEvents = {
    'click .edit': function (e, value, row, index) {

        if (row.state = true) {

            var data = JSON.stringify(row);

            $('#Id').val(row.id);
            $('#BrId').val(row.brId);           
            $('#ddlCompany').val(row.coyID);
            $('#BrAddress').val(row.brAddress);
            $('#BrLocation').val(row.brLocation);
            $('#BrState').val(row.brState);
            $('#BrManager').val(row.brManager);
            $('#BrName').val(row.brName);          
            $('#AddNewBranch').modal('show');

            $('#btnBranchUpdate').html('  <i class="now-ui-icons ui-1_check"></i> Update Record');
        }
    },
    'click .remove': function (e, value, row, index) {
        info = JSON.stringify(row);
        console.log(info);
        var reg = {
            Id:$('#Id').val(),
            BrId:$('#BrId').val(),
            CoyId: $('#ddlCompany').val(),
            BrAddress:$('#BrAddress').val(),
            BrLocation:$('#BrLocation').val(),
            BrState:$('#BrState').val(),
            BrManager:$('#BrManager').val(),
            BrName:$('#BrName').val() 
        }
        debugger
        $('#ID').val(row.id);
        $.ajax({
            url: 'CompanySetup/RemoveBranch',
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
                        $('#branchTable').
                            bootstrapTable(
                                'refresh', { url: 'CompanySetup/listbranch' });

                        //return false;
                    }
                    else {
                        swal("Branch", "You cancelled add branch.", "error");
                    }
                    $('#branchTable').
                        bootstrapTable(
                            'refresh', { url: 'CompanySetup/listbranch' });
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

function updateBranch() {
    debugger

    var json_data = {};

    json_data.Id = $('#Id').val();
    json_data.BrId= $('#BrId').val();
    json_data.CoyId = $('#ddlCompany').val();
    json_data.BrAddress= $('#BrAddress').val();
    json_data.BrLocation= $('#BrLocation').val();
    json_data.BrState= $('#BrState').val();
    json_data.BrManager= $('#BrManager').val();
    json_data.BrName= $('#BrName').val();   

    $("input[type=submit]").attr("disabled", "disabled");

    $('#frmbranch').validate({

        //messages: {
        //    CoyId: { required: "Company Id is required" },     
        //    BrId: { required: "Branch ID is required" },
        //    BrAddress: { required: "Branch Address is required" },
        //    BrLocation: { required: "Branch Location is required" },
        //    BrState: { required: "Branch State is required" },
        //    BrManager: { required: "Branch Manager is required" },
        //    BrName: { required: "Branch Name is required" }
        //},
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
                text: "Branch will be updated!",
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
                    $("#btnBranchUpdate").attr("disabled", "disabled");
                    debugger
                    $.ajax({
                        url: 'CompanySetup/UpdateBranch',
                        type: 'POST',
                        data: json_data,
                        dataType: "json",
                        //headers: {
                        //    'VerificationToken': forgeryId
                        //},
                        success: function (result) {

                            if (result.toString != '' && result != null) {
                                swal({ title: 'Update Branch', text: 'Branch updated successfully!', type: 'success' }).then(function () { clear(); });

                                $('#branchTable').
                                    bootstrapTable(
                                        'refresh', { url: 'CompanySetup/listbranch' });

                                $("#btnBranchUpdate").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Update Branch', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnBranchUpdate").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Update Branch', text: 'Branch update encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnBranchUpdate").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Update Branch', 'You cancelled branch update.', 'error');
            $("#btnBranchUpdate").removeAttr("disabled");
        });

}



$(document).ready(function ($) {

    $('#btnBranch').on('click', function () {
        debugger

        addBranch();

    });

});
function addBranch() {
    debugger
    $('#btnBranchUpdate').hide();
    $("input[type=submit]").attr("disabled", "disabled");
    $('#frmbranch').validate({
        messages: {
          
            BrAddress: { required: "Branch Address is required" },
         
            BrLocation: { required: "Branch Location is required" },
            BrState: { required: "Branch State is required" },
            BrManager: { required: "Branch Manager is required" },
            BrName: { required: "Branch Name is required" }
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
                text: "Branch will be Added!",
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
                    $("#btnBranch").attr("disabled", "disabled");

                    debugger
                    var json_data = {};

                    json_data.Id = $('#Id').val();
                    //json_data.BrId= $('#BrId').val();
                    json_data.CoyId = $('#ddlCompany').val();
                    json_data.BrAddress = $('#BrAddress').val();
                    json_data.BrLocation = $('#BrLocation').val();
                    json_data.BrState = $('#ddlStates').val();
                    json_data.BrManager = $('#BrManager').val();
                    json_data.BrName = $('#BrName').val();   
                    $.ajax({
                        url: 'CompanySetup/AddBranch',
                        type: 'POST',
                        data: json_data,
                        dataType: "json",
                        //headers: {
                        //    'VerificationToken': forgeryId
                        //},
                        success: function (result) {
                            
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Add Branch', text: 'Branch added successfully!', type: 'success' }).then(function () { clear(); });

                                $('#branchTable').
                                    bootstrapTable(
                                        'refresh', { url: 'CompanySetup/listbranch' });

                                $("#btnBranch").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Add Branch', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnBranch").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Add Branch', text: 'Adding branch encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnBranch").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Add Branch', 'You cancelled add branch.', 'error');
            $("#btnBranch").removeAttr("disabled");
        });

}
$(document).ready(function ($) {
    debugger
    //for dropdown list control for Comapny (id=ddlCompany)starts here

    $("#ddlCompany").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",

    });

    $.ajax({
        url: "CompanySetup/loadCompany",
    }).then(function (response) {
        debugger
        $("#ddlCompany").select2({
            theme: "bootstrap4",
            // placeholder: "Select Company...",  
            width: '100%',
            data: response.results
        });
    });
});


$(document).ready(function ($) {

    $('#btnBranchUpdate').on('click', function () {
        debugger
        updateBranch();
    });

});
function reloadpage() {
    location.reload();
}

function clear() {
    $('#Id').val('');
    $('#BrId').val('');
   
    $('#CoyId').val('');
    $('#BrAddress').val('');
    $('#BrLocation').val('');
    $('#BrState').val('');
    $('#BrManager').val('');
    $('#BrName').val('');  
}

$('#branchTable').on('expand-row.bs.table', function (e, index, row, $detail) {
    $detail.html('Loading request...');

    var htmlData = '';
    var header = '<div>';
    var footer = '</div>';
    htmlData = htmlData + header;

    debugger

    var html =
        '<h8>' +
        '<p style="text-align:left">' +
        '<strong>Branch Id:</strong> &nbsp' + row.brId + '' + '<p>'+
        ' <strong>Branch Address: </strong> &nbsp' + row.brAddress + '' + '<p>' +
        ' <strong>Branch Location: </strong>&nbsp' + row.brLocation + '' + '<p>' +
        ' <strong>Branch State: </strong>&nbsp' + row.brState + '' + '<p>' +
        ' <strong>Branch Manager: </strong>&nbsp' + row.brManager + '' + '<p>' +
        ' <strong>Branch Name: </strong>&nbsp' + row.brName + '</p>';


    htmlData = htmlData + html;
    htmlData = htmlData + footer;
    $detail.html(htmlData);
});


