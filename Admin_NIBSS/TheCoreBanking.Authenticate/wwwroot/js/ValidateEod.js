var url_path = window.location.pathname;
if (url_path.charAt(url_path.length - 1) == '/') {
    url_path = url_path.slice(0, url_path.length - 1);
}

$(document).ready(function ($) {
    initTillMapValidation();
    changeText();
});

function dateFormatter(value, row, $element) {
    var format = moment(value).format("DD MMMM, YYYY");
    var html = '<div>' + format + '</div>';
    return html;
}
 

function deleteDefinedFormatter(value, row, index) {
    return [
        '<button type="button" class="remove btn btn-sm btn-danger" title="Delete">',
        '<i class="now-ui-icons ui-1_simple-remove"></i> ',
        '</button>'
    ].join('');
}



window.ValidateEodEvents = {

    'click .add': function (e, value, row, index) {
        debugger 

        if (row.state = true) {
            form.find("[name=id]").val(row.id);

            $('#btnAddEodValidation').hide();
            $('#AddNewValidationEod').modal('show');


        }

    },
    'click .remove': function (e, value, row, index) {
        info = JSON.stringify(row);
        console.log(info);
        debugger
        $('#ID').val(row.id);
        $.ajax({
            url: '../Administration/DeleteValidateEod',
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
                        $('#validateTable').
                            bootstrapTable(
                                'refresh', { url: 'Administration/listPendingTransactions' });                       

                    }
                    else {
                        swal("ValidateEOD", "You cancelled ValidateEOD.", "error");
                    }
                    $('#validateTable').
                        bootstrapTable(
                            'refresh', { url: 'Administration/listPendingTransactions' });
                     
                });
                return

            },

            error: function (e) {             
                swal("An exception occured!");
            }
        });
    }

};
 

function openEodValidateModal() {
    var form = $("#frmEODValid");
    form.trigger("reset");   
    form.find('#btnAddEodValidation').show(); 
    $('#AddNewValidationEod').modal('show');
}

function AddEodValidation() {
    swal({
        title: "Are you sure?",
        text: "End Of Day will be Validated!",
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
                var form = $("#frmEODValid");
                form.find("#btnAddEodValidation").attr("disabled", "true");               
                
                $.ajax({
                    type: "POST",
                    url: "../Administration/ValidateEodResult",
                    data: {} ,
                    dataType: "json", 
                    success: function (response) {
                        debugger
                        if (response == "Successful") {
                            swal({ title: 'Validate EOD', text: 'No Pending Transaction, go ahead with EOD !', type: 'success' })    
                             .then(function () {
                                 $('#validateTable').
                                     bootstrapTable('refresh');
                                 $("#btnAddEodValidation").removeAttr("disabled");
                                 $('#AddNewValidationEod').modal('hide');
                                 window.location.reload(true);
                             });


                        } else if (response == "Pending Approval") {
                            swal({ title: ' Validate EOD', text: 'There are Pending Transaction(s) for Approval , hence you cannot run EOD', type: 'error' })
                                .then(function () {
                                    $('#validateTable').
                                        bootstrapTable('refresh');
                                    $("#btnAddEodValidation").removeAttr("disabled");
                                    $('#AddNewValidationEod').modal('hide');
                                    window.location.reload(true);
                                });                       


                        } else if (response == "Failure") {
                            swal({ title: 'Validate EOD', text: 'Validate EOD encountered an error', type: 'error' })
                                .then(function () {
                                    $("#btnAddEodValidation").removeAttr("disabled");
                                    window.location.reload(true);
                                });  
                        }
                    } 

                });
            }
        },
        function (isRejected) {
            return;
        }
    );
}


var define = {

    tillDefineTableFormatter: function (index, row, el) {
        var container = $("<div class='row mx-0'></div>");
        container.append($("<div class='col-xs-12 col-md-6 mb-3 mt-2'><b>customer Name :</b> "
            + row.customerName + "</div>"));    
        container.append($("<div class='col-xs-12 col-md-6 mb-3 mt-2'><b>Created by :</b> "
            + row.officer + "</div>"));
        container.append($("<div class='col-xs-12 col-md-6 mb-2'><b>Date Created :</b> "
            + moment(row.transDate).format("MMMM DD, YYYY") + "</div>"));
        el.append(container);
    }

};
