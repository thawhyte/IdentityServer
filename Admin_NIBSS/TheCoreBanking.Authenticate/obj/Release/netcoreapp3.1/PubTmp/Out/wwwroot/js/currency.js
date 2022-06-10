
function currencyFormatter(value, row, index) {
    return [
        '<a style="color:white"  class="edit btn btn-sm btn-danger"  title="Edit Currency">'
        + '<i class="now-ui-icons ui-2_settings-90"></i>' +
        '<a style="color:white"  title="Remove currency" class="remove btn btn-sm btn-danger">'
        +'<i class="now-ui-icons ui-1_simple-remove"></i></a>'+
        '</a> '
    ].join('');
}

window.currencyEvents = {
    'click .edit': function (e, value, row, index) {
       
        if (row.state = true) {
          
            var data = JSON.stringify(row);

            $('#Id').val(row.id);
            $('#CurrCode').val(row.currCode);
            $('#CurrName').val(row.currName);
            $('#CurrSymbol').val(row.currSymbol);
            $('#ExchangeRate').val(row.exchangeRate);
            $('#CountryCode').val(row.countryCode);
            $('#AddNewCurrency').modal('show'); 

            $('#btnCurrencyUpdate').html('  <i class="now-ui-icons ui-1_check"></i> Update Record');
        }
    },
    'click .remove': function (e, value, row, index) {
        info = JSON.stringify(row);
        console.log(info);
        var reg = {
           Id: $('#id').val(),
            Code:$('#CurrCode').val(),
            Name:$('#CurrName').val(),
            Symbol:$('#CurrSymbol').val(),
            Rate:$('#ExchangeRate').val(),
           Country: $('#CountryCode').val()
        }
        debugger
        $('#ID').val(row.id);
        $.ajax({
            url: 'CompanySetup/RemoveCurrency',
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
                        $('#currencyTable').
                            bootstrapTable(
                                'refresh', { url: 'CompanySetup/listcurrency' });

                        //return false;
                    }
                    else {
                        swal("Currency", "You cancelled add bank.", "error");
                    }
                     $('#currencyTable').
                            bootstrapTable(
                                'refresh', { url: 'CompanySetup/listcurrency' });
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

function updateCurrency() {
    debugger
   
    var json_data = {};
    json_data.Id = $('#Id').val();
    json_data.CurrCode = $('#CurrCode').val();
    json_data.CurrName = $('#CurrName').val();
    json_data.CurrSymbol = $('#CurrSymbol').val();
    json_data.ExchangeRate = $('#ExchangeRate').val();
    json_data.CountryCode = $('#CountryCode').val();

    $("input[type=submit]").attr("disabled", "disabled");  

    $('#frmCurrency').validate({

        messages: {
            CurrCode: { required: "Currency Code is required" },
            CurrName: { required: "Currency Name is required" },
            CurrSymbol: { required: "Currency Symbol is required" },
            ExchangeRate: { required: "Exchange Rate is required" },
            CountryCode: { required: "Country Code is required" }
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
                text: "Currency will be updated!",
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
                    $("#btnCurrencyUpdate").attr("disabled", "disabled");
                    debugger
                    $.ajax({
                        url: 'CompanySetup/EditCurrency',
                        type: 'POST',
                        data: json_data,
                        dataType: "json",
                        //headers: {
                        //    'VerificationToken': forgeryId
                        //},
                        success: function (result) {
                            
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Update Currency', text: 'Currency updated successfully!', type: 'success' }).then(function () { clear(); });

                                $('#currencyTable').
                                    bootstrapTable(
                                        'refresh', { url: 'CompanySetup/listcurrency' });

                                $("#btnCurrencyUpdate").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Update Currency', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnCurrencyUpdate").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Update Currency', text: 'Currency update encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnCurrencyUpdate").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Update Currency', 'You cancelled currency update.', 'error');
            $("#btnCurrencyUpdate").removeAttr("disabled");
        });

}



$(document).ready(function ($) {

    $('#btnCurrency').on('click', function () {
        debugger      
       
            addCurrency();   
 
    });

});
function addCurrency() {
    debugger
    $('#btnCurrencyUpdate').hide();

  

    $("input[type=submit]").attr("disabled", "disabled");
    $('#frmCurrency').validate({
        messages: {
            CurrCode: { required: "Currency Code is required" },
            CurrName: { required: "Currency Name is required" },
            CurrSymbol: { required: "Currency Symbol is required" },
            ExchangeRate: { required: "Exchange Rate is required" },
            CountryCode: { required: "Country Code is required" }
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
                text: "Currency will be Added!",
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
                    $("#btnCurrency").attr("disabled", "disabled");

                    debugger
                    var json_data = {};
                    json_data.Id = $('#Id').val();
                    json_data.CurrCode = $('#CurrCode').val();
                    json_data.CurrName = $('#CurrName').val();
                    json_data.CurrSymbol = $('#CurrSymbol').val();
                    json_data.ExchangeRate = $('#ExchangeRate').val();
                    json_data.CountryCode = $('#CountryCode').val();
                    $.ajax({
                        url: 'CompanySetup/AddCurrency',
                        type: 'POST',
                        data: json_data,
                        dataType: "json",
                        //headers: {
                        //    'VerificationToken': forgeryId
                        //},
                        success: function (result) {
                            
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Add Currency', text: 'Currency added successfully!', type: 'success' }).then(function () { clear(); });

                                $('#currencyTable').
                                    bootstrapTable(
                                        'refresh', { url: 'CompanySetup/listcurrency' });

                                $("#btnCurrency").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Add Currency', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnCurrency").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Add Currency', text: 'Adding currency encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnCurrency").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Add Currency', 'You cancelled add currency.', 'error');
            $("#btnCurrency").removeAttr("disabled");
        });

}

$(document).ready(function ($) {

    $('#btnCurrencyUpdate').on('click', function () {
        debugger
        updateCurrency();
    });

});
function reloadpage() {
    location.reload();
}

function clear() {
    $('#CurrCode').val('');
    $('#CurrName').val('');
    $('#CurrSymbol').val('');
    $('#ExchangeRate').val('');
    $('#CountryCode').val('');
}

$('#currencyTable').on('expand-row.bs.table', function (e, index, row, $detail) {
    $detail.html('Loading request...');

    var htmlData = '';
    var header = '<div>';
    var footer = '</div>';
    htmlData = htmlData + header;

    debugger

    var html =
        '<h8>' +
        '<p style="text-align:left">' +
   
        '<strong> Currency Code:</strong> &nbsp' + row.currCode + '' + '<p>' +
        '<strong> Currency Name:</strong> &nbsp' + row.currName + '' + '<p>' +
        '<strong> Currency Symbol:</strong> &nbsp' + row.currSymbol + '' + '<p>' +
        '<strong> Exchange Rate:</strong> &nbsp' + row.exchangeRate + '' + '<p>' +
        '<strong> Country Code:</strong> &nbsp' + row.countryCode + '';
      
    htmlData = htmlData + html;
    htmlData = htmlData + footer;
    $detail.html(htmlData);
});



