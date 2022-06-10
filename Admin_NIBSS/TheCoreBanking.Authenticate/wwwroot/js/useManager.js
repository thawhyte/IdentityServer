$(document).ready(function ($) {
    $("#btnAddUnlock").on("click", function () {
        addoperations()
    });
    $("#btnAddlock").on("click", function () {
        addLock()
    });
});
function addoperations() {
    debugger
    var result = $('#txtComment');
    $("input[type=submit]").attr("disabled", "disabled");
    if (result) {
        swal({
            title: "Are you sure?",
            text: "User account will be unlocked!",
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
                $("#btnAddUnlock").attr("disabled", "disabled");
                debugger
                var $approvaltable = $('#userTable');
                var FinanceData = $approvaltable.bootstrapTable('getAllSelections');
                
                $.each(FinanceData, function (index, financeItemData) {
                    $.ajax({
                        url:  '/Administration/UnlockUser',
                        type: 'POST',
                        data: {
                            Id: financeItemData.id
                           
                        },
                        dataType: "json",
                        success: function (result) {
                            debugger
                            if (result.message !== " ") {
                                swal({ title: 'Unlock user', text: 'Something went wrong: ' + result.message.toString(), type: 'error' }).then(function () {  });
                                $("#btnAddUnlock").removeAttr("disabled", true);
                              
                            }
                            else {
                                swal({ title: 'Unlock user', text: 'User account is successfully unlocked!', type: 'success' }).then(
                                    setTimeout(function () { window.location.replace("/administration/usermanagement"); }, 3000

                                    ));
                                $("#btnAddUnlock").removeAttr("disabled",true);
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Unlock user', text: 'User unlock encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnAddUnlock").removeAttr("disabled", true);
                        }
                    });
                })
            }

        }),

            function (dismiss) {
                swal('Unlock user', 'You cancelled approving user unlock.', 'error');
                $("#btnAddUnlock").removeAttr("disabled", true);
            }
    }
}

function addLock() {
    debugger
    var result = $('#txtComment');
    $("input[type=submit]").attr("disabled", "disabled");
    if (result) {
        swal({
            title: "Are you sure?",
            text: "User account will be locked!",
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
                $("#btnAddlock").attr("disabled", "disabled");
                debugger
                var $approvaltable = $('#userTable');
                var FinanceData = $approvaltable.bootstrapTable('getAllSelections');

                $.each(FinanceData, function (index, financeItemData) {
                    $.ajax({
                        url: '/Administration/lockUser',
                        type: 'POST',
                        data: {
                            Id: financeItemData.id
                        },
                        dataType: "json",
                        success: function (result) {
                            debugger
                            if (result.message !== " ") {
                                swal({ title: 'Lock user', text: 'Something went wrong: ' + result.message.toString(), type: 'error' }).then(function () { });
                                $("#btnAddlock").removeAttr("disabled", true);

                            }
                            else {
                                swal({ title: 'Lock user', text: 'User account is successfully locked!', type: 'success' }).then(
                                    setTimeout(function () { window.location.replace("/administration/usermanagement"); }, 3000

                                    ));
                                $("#btnAddlock").removeAttr("disabled", true);
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Lock user', text: 'User lock encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnAddlock").removeAttr("disabled", true);
                        }
                    });
                })
            }

        }),

            function (dismiss) {
                swal('Lock user', 'You cancelled approving user lock.', 'error');
                $("#btnAddlock").removeAttr("disabled", true);
            }
    }
}