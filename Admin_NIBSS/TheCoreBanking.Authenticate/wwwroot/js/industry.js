var url_path = window.location.pathname;
if (url_path.charAt(url_path.length - 1) == '/') {
    url_path = url_path.slice(0, url_path.length - 1);
}

$(document).ready(function ($) {
    initIndustryValidation();
    initIndustrySelectTwoConfig();
});

function initIndustryValidation() {
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
    $("#frmIndustrySec").validate({
        messages: {
            name: {
                required: "Industry is required"
            }
        }
    });
}

function initIndustrySelectTwoConfig() {
    $.fn.select2.defaults.set("theme", "bootstrap4");
    $.fn.select2.defaults.set("dropdownParent", $(".modal").first());
    $.fn.select2.defaults.set("width", "100%");
    $.fn.select2.defaults.set("allowClear", true);

    var form = $("#frmIndustrySec");

    $.ajax(url_path + "/LoadSectors")
        .then(function (response) {
            form.find("#sectorid").select2({
                placeholder: "Select sector",
                data: response,
                dropdownParent: $("#AddNewIndustry.modal"),
            });
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

window.industryEvents = {
    'click .edit': function (e, value, row, index) {        
        var form = $("#frmIndustrySec");
        form.trigger("reset");
        if (row.state = true) {
            form.find("[name=industryid]").val(row.industryid);
            form.find("[name=name]").val(row.name);
            form.find("[name=sectorid]").val(row.sectorid);       
            //form.find("[name=isdeleted]").prop("checked", row.isdeleted);
            $("#industrSecTitle").text("Update");
            $('#btnAddIndustry').hide();
            $("#btnIndustryUpdate").show();
            $('#AddNewIndustry').modal('show');
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
                    url: url_path + '/DeleteIndustrySetup',
                    method: "POST",
                    data: JSON.stringify({ industryid: row.industryid }),
                    contentType: "application/json",
                    success: function (data) {
                        swal("Deleted succesfully");
                        $('#industrySeTable').bootstrapTable('refresh');
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

function updateIndustrySec() {
    swal({
        title: "Are you sure?",
        text: "Industry will be updated!",
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
            var form = $("#frmIndustrySec");
            var data = {
                industryid: form.find("#industryid").val(),
                name: form.find("#name").val(),
                sectorid: form.find("#sectorid").val(),               
                isdeleted: form.find('#isdeleted').prop("checked"),
            };
            form.find("#btnIndustryUpdate").attr("disabled", "true");
            $.ajax({
                url: url_path + '/updateIndustrySec',
                method: 'POST',
                data: JSON.stringify(data),
                contentType: "application/json",
                success: function (result) {
                    swal({
                        title: 'Industry Setup',
                        text: 'Industry updated successfully!',
                        type: 'success'
                    }).then(function () {
                        $('#industrySeTable').
                            bootstrapTable('refresh');
                        $("#btnIndustryUpdate").removeAttr("disabled");
                        $('#AddNewIndustry').modal('hide');
                    });
                },
                error: function (e) {
                    swal({
                        title: 'Industry Setup',
                        text: 'Industry setup encountered an error during update',
                        type: 'error'
                    }).then(function () {
                        $("#btnIndustryUpdate").removeAttr("disabled");
                    });
                }
            });
        }
    }, function (isRejected) {
        return;
    });
}

function openIndustrySecModal() {
    var form = $("#frmIndustrySec");
    form.trigger("reset");
    $("#industrSecTitle").text("Add");
    form.find('#btnAddIndustry').show();
    form.find("#btnIndustryUpdate").hide();
    $('#AddNewIndustry').modal('show');
}

function AddIndustrySec() {
    swal({
        title: "Are you sure?",
        text: "Industry will be saved!",
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
                var form = $("#frmIndustrySec");
                form.find("#btnAddIndustry").attr("disabled", "true");
                var data = {
                    name: form.find('#name').val(),
                    sectorid: form.find('#sectorid').val(),
                }
                $.ajax({
                    url: url_path + '/AddIndustrySec',
                    method: "POST",
                    data: JSON.stringify(data),
                    contentType: "application/json",
                    success: function (result) {
                        swal({
                            title: 'Industry Setup',
                            text: 'Industry saved successfully!',
                            type: 'success'
                        }).then(function () {
                            $('#industrySeTable').
                                bootstrapTable('refresh');
                            $("#btnAddIndustry").removeAttr("disabled");
                            $('#AddNewIndustry').modal('hide');
                        });
                    },
                    error: function (e) {
                        swal({
                            title: 'Industry Setup',
                            text: 'Industry encountered an error',
                            type: 'error'
                        }).then(function () {
                            $("#btnAddIndustry").removeAttr("disabled");
                        });
                    }
                });
            }
        }, function (isRejected) {
            return;
        });
}