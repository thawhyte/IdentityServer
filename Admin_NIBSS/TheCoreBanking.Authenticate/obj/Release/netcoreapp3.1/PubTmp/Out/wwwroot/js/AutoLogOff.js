var url_path = window.location.pathname;
if (url_path.charAt(url_path.length - 1) == '/') {
    url_path = url_path.slice(0, url_path.length - 1);
}
function dateFormatter(value) {
    return moment(value).format("DD MMMM, YYYY");
}
function LogOffFormatter(value, row, index) {
    return [
        '<div class="btn-group">' +
     
        '<a style="color:white" title="Remove" class="remove btn btn-sm btn-danger">'
        + '<i class="fas fa-trash"></i></a>' +
        '</a> '
    ].join('');
}

window.LogOffEvents = {
    'click .edit': function (e, value, row, index) {

        if (row.state = true) {

            var data = JSON.stringify(row);

            $('#Id').val(row.id);
            $('#Time').val(row.logoutTime);
      
            $('#AddNewDirector').modal('show');
            $('#btnDirectorUpdate').html('  <i class="now-ui-icons ui-1_check"></i> Update Record');
            $('#btnDirectorUpdate').show();
            $('#btnDirector').hide();
        }
    },
    'click .remove': function (e, value, row, index) {
        info = JSON.stringify(row);
        console.log(info);

        debugger
        $('#ID').val(row.iD);
        $.ajax({
            url:  '/Administration/RemoveDuration',
            type: 'POST',
            data: { id: row.id },
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
                    allowOutsideClick: false,
                    allowEscapeKey: false,
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
                        $('#durationTable').
                            bootstrapTable(
                                'refresh', { url:  '../Adiministration/listAutoLogOut' });

                        //return false;
                    }
                    else {
                        swal("Duration", "You cancelled add duration.", "error");
                    }
                    $('#durationTable').
                        bootstrapTable(
                            'refresh', { url:'/Adiministration/listAutoLogOut' });
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

function updateDirector() {
    debugger
    var rBvn = $('#Bvn').val();
    var json_data = {};
    json_data.Id = $('#Id').val();
    json_data.Bvn = $('#Bvn').val();
    json_data.CompanyId = $('#ddlDirCompany').val();
    json_data.PercentageShare = $('#PercentageShare').val();
    json_data.Position = $('#Position').val();
    json_data.FullName = $('#FullName').val();


    $("input[type=submit]").attr("disabled", "disabled");

    $('#frmDirector').validate({

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
                text: "Director will be updated!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#ff9800",
                confirmButtonText: "Yes, continue",
                cancelButtonText: "No, stop!",
                showLoaderOnConfirm: true,
                allowOutsideClick: false,
                allowEscapeKey: false,
                preConfirm: function () {
                    return new Promise(function (resolve) {
                        setTimeout(function () {
                            resolve();
                        }, 4000);
                    });
                }
            }).then(function (isConfirm) {
                if (isConfirm) {
                    $("#btnDirectorUpdate").attr("disabled", "disabled");
                    debugger
                    if (rBvn.length == 11) {
                        $.ajax({
                            url: url_path + '/Companysetup/UpdateDirector',
                            type: 'POST',
                            data: json_data,
                            dataType: "json",
                            //headers: {
                            //    'VerificationToken': forgeryId
                            //},
                            success: function (result) {

                                if (result.toString != '' && result != null) {
                                    swal({ title: 'Update Director', text: 'Director updated successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                    $('#AddNewDirector').modal('hide')
                                    $('#DirectorTable').
                                        bootstrapTable(
                                            'refresh', { url: url_path + '/Companysetup/listDirector' });

                                    $("#btnDirectorUpdate").removeAttr("disabled");
                                }
                                else {
                                    swal({ title: 'Update Director', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                    $("#btnDirectorUpdate").removeAttr("disabled");
                                }
                            },
                            error: function (e) {
                                swal({ title: 'Update Director', text: 'Director update encountered an error', type: 'error' }).then(function () { clear(); });
                                $("#btnDirectorUpdate").removeAttr("disabled");
                            }
                        });
                    }
                    else {
                        swal("BVN must be 11 digits")
                    }
                }
            });
        }

    },
        function (dismiss) {
            swal('Update Director', 'You cancelled Director update.', 'error');
            $("#btnDirectorUpdate").removeAttr("disabled");
        });

}



$(document).ready(function ($) {
    $('#btnAddLogUpdate').hide();
    $('#btnAddLog').on('click', function () {
        debugger

        addAutoLogOut();

    });

});
function addAutoLogOut() {
    debugger

    $("input[type=submit]").attr("disabled", "disabled");
    $('#frmLogOut').validate({
        messages: {

            Time: { required: "Duration is required" },

           
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
                text: "Duration will be added!",
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
                    $("#btnAddLog").attr("disabled", "disabled");

                    debugger
                    var json_data = {};

                    json_data.Id = $('#Id').val();
                    
                    json_data.logoutTime = $('#Time').val();
                    
                    $.ajax({
                        url: '/Administration/AutoLogOut',
                        type: 'POST',
                        data: json_data,
                        dataType: "json",
                        
                        success: function (result) {

                            if (result == "") {
                                swal({ title: 'Add Duration', text: 'Duration added successfully!', type: 'success' }).then(function () {  });

                                $('#durationTable').
                                    bootstrapTable(
                                        'refresh', { url: 'Administration/listAutoLogOut' });

                                $("#btnAddLog").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Add Duration', text: 'Something went wrong: ' + result, type: 'error' }).then(function () {  });
                                
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Add Duration', text: 'Adding duration encountered an error', type: 'error' }).then(function () { });
                           
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Add Duration', 'You cancelled add duration.', 'error');
           
        });

}



$(document).ready(function ($) {
   
    $('#btnDirector').show();
    $('#btnAddLogUpdate').on('click', function () {
        debugger
        updateDirector();
    });

});
function reloadpage() {
    location.reload();
}



$(document).ready(function ($) {
    debugger

    $('#Bvn').mouseleave(function (event) {
        $.ajax({
            url: url_path + '/Administration/DirectorBVN',
            data: { Bvn: $('#Bvn').val() }
        }).then(function (result) {
            debugger
            if (result != "" && result != null) {
                $.notify({ icon: "add_alert", message: result.toString() }, { type: 'danger', timer: 1000 });
                $(this).css('border-color', 'red');
                $('#Bvn').focus();
                return false;

            }
        })
    });
});

