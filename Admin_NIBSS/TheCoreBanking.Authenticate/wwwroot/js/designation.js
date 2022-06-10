
function designationFormatter(value, row, index) {
    return [
        '<a style="color:white"  class="edit btn btn-sm btn-danger"  title="Edit Designation">'
        + '<i class="now-ui-icons ui-2_settings-90"></i>' +
        '<a style="color:white"  title="Remove designation" class="remove btn btn-sm btn-danger">'
        + '<i class="now-ui-icons ui-1_simple-remove"></i></a>' +
        '</a> '
    ].join('');
}

window.designationEvents = {
    'click .edit': function (e, value, row, index) {
      
        if (row.state = true) {

            var data = JSON.stringify(row);
            $('#Id').val(row.id);
            $('#Designation').val(row.designation);
            $('#AddNewDesignation').modal('show');            
            $('#btnDesignationUpdate').html('  <i class="now-ui-icons ui-1_check"></i> Update Record');
        }
    },
    'click .remove': function (e, value, row, index) {
        info = JSON.stringify(row);
        console.log(info);
        var reg = {
            Id: $('#Id').val(),
            Designation: $('#Designation').val()    
        }
        debugger
        $('#ID').val(row.Id);
        $.ajax({
            url: 'CustomerSetup/RemoveDesignation',
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
                        $('#designationTable').
                            bootstrapTable(
                            'refresh', { url: 'CustomerSetup/listdesignation' });

                        //return false;
                    }
                    else {
                        swal("Designation", "You cancelled delete designation.", "error");
                    }
                    $('#designationTable').
                        bootstrapTable(
                        'refresh', { url: 'CustomerSetup/listdesignation' });
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
 
    $('#btnDesignationUpdate').on('click', function () {
        updateDesignation();
      
    });

});
$(document).ready(function ($) {
   
    $('#btnDesignation').on('click', function () {
        debugger
        addDesignation();
       

    });

});
function addDesignation() {
    debugger

 

    $('#btnDesignationUpdate').hide();
    $("input[type=submit]").attr("disabled", "disabled");
    $('#frmdesignation').validate({
        messages: {
            Designation: { required: "Designation is required" }
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
                text: "Desigantion will be Added!",
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
                    $("#btnDesignation").attr("disabled", "disabled");

                    debugger
                    var json_data = {};
                    json_data.Id = $('#Id').val();
                    json_data.Designation = $('#Designation').val();

                    //var Id = $('#Id').val();
                    //var Designation = $('#Designation').val();

                    $.ajax({
                        url: 'CustomerSetup/AddDesignation',
                        type: 'POST',
                        data: json_data,
                        dataType: "json",                     
                        success: function (result) {
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Add Designation', text: 'Designation added successfully!', type: 'success' }).then(function () { clear(); });

                                $('#designationTable').
                                    bootstrapTable(
                                    'refresh', { url: 'CustomerSetup/listdesignation' });

                                $("#btnDesignation").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Add Designation', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnDesignation").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Add Designation', text: 'Adding designation encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnDesignation").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Add Designation', 'You cancelled add designation.', 'error');
            $("#btnDesignation").removeAttr("disabled");
        });

}
function reloadpage() {
    location.reload();
}

function clear() {
    $('#Id').val('');
    $('#Designation').val('');
}

function updateDesignation() {
    debugger
   
    $("input[type=submit]").attr("disabled", "disabled");
    $('#frmdesignation').validate({
        messages: {
            Designation: { required: "Designation is required" },

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
                text: "Designation will be Updated!",
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
                    $("#btnDesignationUpdate").attr("disabled", "disabled");
                    debugger
                    //var json_data = {};
                    //json_data.Id = $('#Id').val();
                    //json_data.Designation = $('#Designation').val();
                    var id = $('#Id').val();
                    var Designation = $('#Designation').val();
                    $.ajax({
                        url: 'CustomerSetup/UpdateDesignation',
                        type: 'POST',
                        data: { id, Designation},
                        dataType: "json",
                        //headers: {
                        //    'VerificationToken': forgeryId
                        //},
                        success: function (result) {
                           
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Update Designation', text: 'Designation updated successfully!', type: 'success' }).then(function () { clear(); });

                                $('#designationTable').
                                    bootstrapTable(
                                    'refresh', { url: 'CustomerSetup/listdesignation' });

                                $("#btnDesignationUpdate").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Update Designation', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnDesignationUpdate").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Update Designation', text: 'Update Designation encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnDesignationUpdate").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Update Designation', 'You cancelled Designation updatation.', 'error');
            $("#btnDesignationUpdate").removeAttr("disabled");
        });

}
$('#designationTable').on('expand-row.bs.table', function (e, index, row, $detail) {
    $detail.html('Loading request...');

    var htmlData = '';
    var header = '<div>';
    var footer = '</div>';
    htmlData = htmlData + header;

    debugger

    var html =
        '<p style="text-align:left">'+
        '<strong> Id:</strong>&nbsp' + row.id + '' + '<p>' +      
        '<strong>Designation: </strong>&nbsp' + row.designation + ''+'</div>';
    htmlData = htmlData + html;
    htmlData = htmlData + footer;
    $detail.html(htmlData);
});