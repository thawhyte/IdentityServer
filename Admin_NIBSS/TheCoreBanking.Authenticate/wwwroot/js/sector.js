var url_path = window.location.pathname;
if (url_path.charAt(url_path.length - 1) == '/') {
    url_path = url_path.slice(0, url_path.length - 1);
}

$(document).ready(function ($) {
    initSectorValidation();
});

function initSectorValidation() {
    // defaults
    jQuery.validator.setDefaults({
        onfocusout: false,
        onkeyup: false,
        onclick: false,
        normalizer: function (value) {
            // Trim the value of every element
            // before validation
            return $.trim(value);
        },
        errorPlacement: function (error, element) {
            $.notify({
                icon: "now-ui-icons travel_info",
                message: error.text()
            }, {
                    type: "danger",
                    placement: {
                        from: "top",
                        align: "right"
                    }
                });
        }
    });
    $("#frmSector").validate({
        messages: {
            name: {
                required: "Sector Name is required"
            }
        }
    });
}

function editFormatter(value, row, index) {
    return [
        '<button type="button" class="edit btn btn-sm btn-warning" title="Edit">',
        '<i class="now-ui-icons ui-2_settings-90"></i>',
        '</button>'
    ].join('');
}

function deleteFormatter(value, row, index) {
    return [
        '<button type="button" class="remove btn btn-sm btn-danger" title="Delete">',
        '<i class="now-ui-icons ui-1_simple-remove"></i> ',
        '</button>'
    ].join('');
}

window.sectorEvents = {
    'click .edit': function (e, value, row, index) {
        var form = $("#frmSector");
        form.trigger("reset");
        if (row.state = true) {
            form.find("[name=sectorid]").val(row.sectorid);
            form.find("[name=name]").val(row.name);
            form.find("[name=code]").val(row.code);
            // form.find("[name=active]").prop("checked", row.active);
            $("#sectorTitle").text("Update");
            $('#btnAddSector').hide();
            $("#btnSectorUpdate").show();
            $('#AddNewSector').modal('show');
        }
    },
    'click .remove': function (e, value, row, index) {
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
                    }, 1000);
                });
            }
        }).then(function (isConfirm) {
            if (isConfirm) {
                $.ajax({
                    url: url_path + '/DeleteSectorSetup',
                    method: "POST",
                    data: JSON.stringify({ sectorid: row.sectorid }),
                    contentType: "application/json",
                    success: function (data) {
                        swal("Deleted succesfully");
                        $('#sectorTable').bootstrapTable('refresh');
                    },
                    error: function (e) {
                        swal("An exception occured!");
                    }
                });
            }
        }, function (isRejected) {
            return;
        });
    }
};

function updateSectorSetup() {
    swal({
        title: "Are you sure?",
        text: "Sector Setup will be updated!",
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
                }, 1000);
            });
        }
    }).then(function (isConfirm) {
        if (isConfirm) {
            var form = $("#frmSector");
            var data = {
                sectorid: form.find("#sectorid").val(),
                name: form.find("#name").val(),
                code: form.find("#code").val()
            };
            form.find("#btnSectorUpdate").attr("disabled", "true");
            $.ajax({
                url: url_path + '/updateSectorSetup',
                method: "POST",
                data: JSON.stringify(data),
                contentType: "application/json",
                success: function (result) {
                   
                    if (result == false) {
                        console.log(result);
                        swal({
                            title: 'Sector Setup',
                            text: 'Sector Name and/or Code already exist!',
                            type: 'error'
                        }).then(function () {
                            $('#sectorTable').
                                bootstrapTable('refresh');
                            $("#btnAddSector").removeAttr("disabled");
                            $('#AddNewSector').modal('hide');
                        });
                    } else {
                        swal({
                            title: 'Sector Setup',
                            text: 'Sector Name/Code updated successfully!',
                            type: 'success'
                        }).then(function () {
                            $('#sectorTable').
                                bootstrapTable('refresh');
                            $("#btnSectorUpdate").removeAttr("disabled");
                            $('#AddNewSector').modal('hide');
                        });
                    }
                },
                error: function (e) {
                    swal({
                        title: 'Sector Setup',
                        text: 'Sector Setup encountered an error during update',
                        type: 'error'
                    }).then(function () {
                        $("#btnSectorUpdate").removeAttr("disabled");
                    });
                }
            });
        }
    }, function (isRejected) {
        return;
    });
}

function openSectorModal() {
    var form = $("#frmSector");
    form.trigger("reset");
    $("#sectorTitle").text("Add");
    form.find('#btnAddSector').show();
    form.find("#btnSectorUpdate").hide();
    $('#AddNewSector').modal('show');
}

function AddSectorSetup() {
    swal({
        title: "Are you sure?",
        text: "Sector Setup will be saved!",
        type: "question",
        showCancelButton: true,
        confirmButtonColor: "#ff9800",
        confirmButtonText: "Yes, continue",
        cancelButtonText: "No, stop!",
        showLoaderOnConfirm: true,
        preConfirm: function () {
            return new Promise(function (resolve) {
                setTimeout(function () {
                    resolve();
                }, 1000);
            });
        }
    }).then(
        function (isConfirm) {
            if (isConfirm) {
                var form = $("#frmSector");
                form.find("#btnAddSector").attr("disabled", "true");
                var data = {
                    name: form.find('#name').val(),
                    code: form.find('#code').val(),
                }
                $.ajax({
                    url: url_path + '/AddSectorSetup',
                    method: "POST",
                    data: JSON.stringify(data),
                    contentType: "application/json",
                    success: function (result) {
                        
                        
                        if (result == false) {
                            console.log(result);
                            swal({
                                title: 'Sector Setup',
                                text: 'Sector Name and/or Code already exist!',
                                type: 'error'
                            }).then(function () {
                                $('#sectorTable').
                                    bootstrapTable('refresh');
                                $("#btnAddSector").removeAttr("disabled");
                                $('#AddNewSector').modal('hide');
                            });
                        } else {
                        
                        swal({
                            title: 'Sector Setup',
                            text: 'Sector Setup saved successfully!',
                            type: 'success'
                        }).then(function () {
                            $('#sectorTable').
                                bootstrapTable('refresh');
                            $("#btnAddSector").removeAttr("disabled");
                            $('#AddNewSector').modal('hide');
                        });
                        } },
                    error: function (e) {
                        swal({
                            title: 'Sector Setup',
                            text: 'Sector Setup encountered an error',
                            type: 'error'
                        }).then(function () {
                            $("#btnAddSector").removeAttr("disabled");
                        });
                    }
                });
            }
        }, function (isRejected) {
            return;
        });
}