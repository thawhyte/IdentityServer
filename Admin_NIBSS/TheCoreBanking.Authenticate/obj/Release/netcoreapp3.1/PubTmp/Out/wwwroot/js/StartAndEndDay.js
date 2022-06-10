$(document).ready(function ($) {
    debugger;
    //getCurrentDate(function (result) {
    $('.datetimepicker').datetimepicker({

        format: 'MM/DD/YYYY',
        //format: 'DD MMMM, YYYY',
        showClose: true,
        ignoreReadonly: true,
        daysOfWeekDisabled: [0, 6]
    });
});

$(document).ready(function ($) {
    $('#btnAssignSOD').on('click', function () {
        swal({
            title: "Are you sure?",
            text: "Start of day will be initiated!",
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

                $.ajax({
                    url: '../Administration/ManageSOD',
                    type: 'GET',
                    dataType: "json",
                    //headers: {
                    //    'VerificationToken': forgeryId
                    //},
                    success: function (result) {
                        if (result.toString() == 'Successful') {
                            swal({ title: 'Start of Day', text: 'Start of Day initiated successfully!', type: 'success' }).then(function () { clear(); });

                        }
                        else {
                            swal({ title: 'Start of Day', text: 'End of Day has not been Initiated for the current date' + result.toString(), type: 'error' }).then(function () { clear(); });

                        }
                    },
                    error: function (e) {
                        swal({ title: 'Start of Day', text: 'Start of Day encountered an error', type: 'error' }).then(function () { clear(); });
                        
                    }
                });
            }
        })
    });
    //End of day processes

    $('#btnAssignEOD').on('click', function () {
        swal({
            title: "Are you sure?",
            text: "End of day will be initiated!",
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
                debugger
                $.ajax({
                    url: '../Administration/ManageEOD',
                    type: 'GET',
                    dataType: "json",
                    //headers: {
                    //    'VerificationToken': forgeryId
                    //},
                    success: function (result) {
                        if (result.toString() == 'Successful') {
                            swal({ title: 'End of Day', text: 'End of Day updated successfully!', type: 'success' }).then(function () { clear(); });

                        }
                        else {
                            swal({ title: 'End of Day', text: 'There is a pending Transaction waiting for approval', type: 'error' }).then(function () { clear(); });
                            
                        }
                    },
                    error: function (e) {
                        swal({ title: 'End of Day', text: 'End of Day encountered an error', type: 'error' }).then(function () { clear(); });
                       
                    }
                });
            }
        })
    });

    //End of year processes

    $('#btnAssignEOY').on('click', function () {
        debugger
        var yearEndDate = $("#EndYearDate").val();
        swal({
            title: "Are you sure?",
            text: "End of year will be initiated!",
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

                $.ajax({
                    url: '../Administration/ManageEOY',
                    type: 'GET',
                    data: { YearDate: yearEndDate },
                    dataType: "json",
                    //headers: {
                    //    'VerificationToken': forgeryId
                    //},
                    success: function (result) {
                        if (result.toString() == 'Successful') {
                            swal({ title: 'End of Year', text: 'End of Year updated successfully!', type: 'success' }).then(function () { clear(); });

                        }
                        else {
                            swal({ title: 'End of Year', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });

                        }
                    },
                    error: function (e) {
                        swal({ title: 'End of Year', text: 'End of Year encountered an error', type: 'error' }).then(function () { clear(); });

                    }
                });
            }
        })
    });
});
function clear() {
    window.location.reload(true);
}