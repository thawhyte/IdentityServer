
function departmentFormatter(value, row, index) {
    return [
        '<a style="color:white"  class="edit btn btn-sm btn-danger"  title="Edit Department">'
        + '<i class="now-ui-icons ui-2_settings-90"></i>' +
        '<a style="color:white"  title="Remove Department" class="remove btn btn-sm btn-danger">'
        +'<i class="now-ui-icons ui-1_simple-remove"></i></a>'+
        '</a> '
    ].join('');
}

window.departmentEvents = {
    'click .edit': function (e, value, row, index) {
       
        if (row.state = true) {
          
            var data = JSON.stringify(row);

            $('#Id').val(row.id);        
            $('#DeptCode').val(row.deptCode);
            $('#Remark').val(row.remark);
            $('#Department').val(row.department);
            $('#AddNewDepartment').modal('show'); 
            $('#btnDepartmentUpdate').html('  <i class="now-ui-icons ui-1_check"></i> Update Record');
        }
    },
    'click .remove': function (e, value, row, index) {
        info = JSON.stringify(row);
        console.log(info);
        var reg = {
            Id: $('#Id').val(),
            CoyId: $('#CoyId').val(),          
            Department:$('#Department').val(),
            DeptCode: $('#DeptCode').val(),
            Remark: $('#Remark').val()
           
        }
        debugger
        $('#ID').val(row.id);
        $.ajax({
            url: 'CustomerSetup/RemoveDepartment',
            type: 'POST',
            data: { ID: row.id},
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
                        $('#departmentTable').
                            bootstrapTable(
                                'refresh', { url: 'CustomerSetup/listdepartment' });

                        //return false;
                    }
                    else {
                        swal("Department", "You cancelled add department.", "error");
                    }
                    $('#departmentTable').
                            bootstrapTable(
                        'refresh', { url: 'CustomerSetup/listdepartment' });
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

    $('#btnDepartmentUpdate').on('click', function () {
        debugger
        updateDepartment();
    });

});
function updateDepartment() {
    debugger

    var json_data = {};
    json_data.Id = $('#Id').val();
    json_data.Department = $('#Department').val();
    json_data.DeptCode = $('#DeptCode').val();
    json_data.Remark = $('#Remark').val();

    $("input[type=submit]").attr("disabled", "disabled");  

    $('#frmdepartment').validate({

        messages: {
            
            Department: { required: "Department Name is required" },
            DeptCode: { required: "Department Code is required" }
           
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
                text: "Department will be updated!",
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
                    $("#btnDepartmentUpdate").attr("disabled", "disabled");
                    debugger
                    $.ajax({
                        url: 'CustomerSetup/UpdateDepartment',
                        type: 'POST',
                        data: json_data,
                        dataType: 'json',
                        cache: false,
                        //headers: {
                        //    'VerificationToken': forgeryId
                        //},
                        success: function (result) {
                            
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Update Department', text: 'Department updated successfully!', type: 'success' }).then(function () { clear(); });

                                $('#departmentTable').
                                    bootstrapTable(
                                        'refresh', { url: 'CustomerSetup/listdepartment' });

                                $("#btnDepartmentUpdate").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Update Department', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnDepartmentUpdate").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Update Department', text: 'Department update encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnDepartmentUpdate").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Update Department', 'You cancelled department update.', 'error');
            $("#btnDepartmentUpdate").removeAttr("disabled");
        });

}



$(document).ready(function ($) {

    $('#btnDepartment').on('click', function () {
        debugger      
       
            addDepartment();   
 
    });

});
function addDepartment() {
    debugger
    $('#btnDepartmentUpdate').hide();
    $("input[type=submit]").attr("disabled", "disabled");
    $('#frmdepartment').validate({
        messages: {

            Department: { required: "Department Name is required" },
            DeptCode: { required: "Department Code is required" }
       
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
                text: "Department will be Added!",
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
                    $("#btnDepartment").attr("disabled", "disabled");

                    debugger
                    var department_data = {
                        id: $('#Id').val(),
                        department: $('#Department').val(),
                        deptCode: $('#DeptCode').val(),
                        remark: $('#Remark').val()

                    }
                           

                    $.ajax({
                        url: 'CustomerSetup/AddDepartment',
                        type: 'POST',
                        data: department_data,
                        dataType: "json",                      
                       
                        success: function (result) {
                         
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Add Department', text: 'Department added successfully!', type: 'success' }).then(function () { clear(); });

                                $('#departmentTable').
                                    bootstrapTable(
                                        'refresh', { url: 'CustomerSetup/listdepartment' });

                                $("#btnDepartment").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Add Department', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnDepartment").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Add Department', text: 'Adding department encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnDepartment").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Add Department', 'You cancelled add department.', 'error');
            $("#btnDepartment").removeAttr("disabled");
        });

}


function reloadpage() {
    location.reload();
}

function clear() {
    $('#Id').val('');
    $('#CoyId').val('');
 
    $('#Department').val('');
    $('#DeptCode').val('');
    $('#Remark').val('');

}

$('#departmentTable').on('expand-row.bs.table', function (e, index, row, $detail) {
    $detail.html('Loading request...');

    var htmlData = '';
    var header = '<div>';
    var footer = '</div>';
    htmlData = htmlData + header;

    debugger

    var html =
        '<h8>' +
        '<p style="text-align:left">' +
        '<strong>Company Id:</strong>&nbsp' + row.coyId + '' + '<p>' +
        ' <strong>Department: </strong>&nbsp' + row.department + '' + '<p>' +
        ' <strong>Remark: </strong>&nbsp' + row.remark + '' + '<p>' +
        ' <strong>Dept Code: </strong>&nbsp' + row.deptCode + '</div>';
       

    htmlData = htmlData + html;
    htmlData = htmlData + footer;
    $detail.html(htmlData);
});


