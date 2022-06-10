function SystemLogout() {
    $(document).ready(function () {
        swal({
        title: "Are you sure?",
        text: "Are you sure, You want to exit the application. You have 0 pending approvals!",
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
                }, 400000);
            });
        }
    }).then(function (isConfirm) {
        if (isConfirm) {
            debugger
           $.ajax({
            url: "../Account/Logout",
            type: "POST",
               success: function () {
                   window.location.href = "../Account/Logout"
    }
});
        }
    });
    })
    
}

