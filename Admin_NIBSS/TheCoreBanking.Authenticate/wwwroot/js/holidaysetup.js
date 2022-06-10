var url_path = window.location.pathname;
if (url_path.charAt(url_path.length - 1) === '/') {
    url_path = url_path.slice(0, url_path.length - 1);
}



$(document).ready(function () {

    //initDataTable();
    //initSelectTwoConfig();
    //initDatePicker(".datepicker");
    initValidations();

    $("#Holiday-SetUp-form").submit(function (e) {
        e.preventDefault();
    });

    //initWizards();
    $(".modal").perfectScrollbar();
    //prepareKYCTables();
});



var groupName = 0;
var groupId;



$(document).ready(function ($) {

    $("#btnmodalx").on('click', function () {
        clearModalContent();
    });

    $("btnmodal").on('click', function () {
        clearModalContent();
    });

});





function clearModalContent() {

    $("#txtdeclaredate").val('');
}






$("#btnclear").on("click", function () {
    clear();
});




function clear() {
    $("#txtdeclaredate").val('');

}






function initFormValidations() {
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
        /* errorPlacement: function (error, element) {
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
         }*/
    });


    $("#Holiday-SetUp-form").validate({
        rules: {
            txtdate: {
                maxlength: 200,
            },
            holtype: {
                maxlength: 200,
            },
            txtdecription: {
                maxlength: 1000,
            },
          
        },
        messages: {
            txtdate: {
                required: "Date is required",
                maxlength: jQuery.validator.format("Date number cannot exceed {0} characters"),
            },
            holtype: {
                required: "Holiday type is required",
                maxlength: jQuery.validator.format("Holiday type number cannot exceed {0} characters"),
            },
            txtdecription: {
                required: "Description is required",
                maxlength: jQuery.validator.format("Description number cannot exceed {0} characters"),
            },
          
        }
    });

}







var utilities = {

    editholsFormatter: function (val, row, index) {

        debugger

        return [
            "<button type='button' class='btn btn-warning btn-icon' title='Edit Holiday' ",
            "onclick='utilities.editholsFrm(" + row.id + ")'>",
            "<i class='fas fa-edit'>",
            "</i></button>"
        ].join("");

    },


    editholsFrm: function (self) {

        debugger

        var groupData = $("#data-table")
            .bootstrapTable('getRowByUniqueId', self);

        console.log(groupData);

        var groupId = groupData.id;

        $("#holId").val(groupId);

        $("#txtdatedit").val(groupData.date);

        $("#txtdecriptionedit").val(groupData.description);

        $("#holtypedit").val(groupData.holidayType);

        $("#editholdaysetup").modal("show");




    },

    dateFormatter: function (date) {
        return moment(date).format("DD MMMM, YYYY");
    }

}


$(document).ready(function () {
    $('#btnedithols').click(function () {
        debugger
        var holy = {};
        holy.Id = $("#holId").val();
        holy.Date = $('#txtdatedit').val();
        holy.Description = $("#txtdecriptionedit").val();
        holy.HolidayType = $("#holtypedit").val();

        $.ajax({
            url: "../HolidaySetUp/EditHoliday",
            data: holy,
            type: "POST",
            dataType: "json",
            data: holy,
            success: function (response) {
                //alert("successful");
                $("#data-table")
                    .bootstrapTable("refresh", {
                        url: "/HolidaySetUp/LoadHoliday"
                    });

                swal({ title: 'Admin Holiday SetUp', text: 'Your Admin Holiday SetUp has been Updated Successfully!', type: 'success' });
                $("#editholdaysetup").modal("hide");
            },
            error: function (err) {
                alert(err);
            }

        })
       
        $("#editholdaysetup").modal("hide");
    });
});




$(document).ready(function () {
    $('#txtsave').click(function (event) {
        debugger

        event.preventDefault();
        var form = $("#Holiday-SetUp-form");

        if (!form.valid()) {
            return false;
        }

        var hol = {};
        hol.Date = moment($('#txtdate').val()).format("MMMM DD, YYYY");
        hol.Description = $('#txtdecription').val();
        hol.HolidayType = $('#holtype').val();


        $.ajax({
            url: "../HolidaySetUp/AddHoliday",
            method: 'POST',
            dataType: "json",
            data: hol,
            success: function (response) {
                //alert("successful");
                $("#data-table")
                    .bootstrapTable("refresh", {
                        url: "/HolidaySetUp/LoadHoliday"
                    });

                swal({ title: 'Holiday SetUp', text: 'Your Holiday SetUp has been Creaated Successfully!', type: 'success' });


            },
            error: function (err) {
                alert(err);
            }
        })

        $('#txtdate').val("");
        $('#txtdecription').val("");
        $('#holtype').val("");

    });
});




$(document).ready(function () {
    $('#btndeclaresave').click(function () {
        debugger

        var declare = {};
        declare.Date = $("#txtdeclaredate").val();

        $.ajax({
            url: "../HolidaySetUp/ReturnWeekends",
            method: 'POST',
            //contentType: 'application/json;charset = utf-8',
            dataType: "json",
            data: declare,
            success: function (response) {
                //alert("successful");
                $("#data-table")
                    .bootstrapTable("refresh", {
                        url: "/HolidaySetUp/LoadHoliday"
                    });

                swal({ title: 'Declare All Weekends', text: 'Your Declare All Weekends has been declared Successfully!', type: 'success' });
                $("#btnDeclareAllWeekend").modal("hide");

            },
            error: function (err) {
                alert(err);
            }
        })
        
        $("#btnDeclareAllWeekend").modal("hide");
        $('#txtdate').val("");       
    });
});






$(document).ready(function () {
    $('#txtundeclare').click(function () {
        debugger


        $.ajax({
            url: "../HolidaySetUp/DeleteWeekends",
            method: 'GET',
            //contentType: 'application/json;charset = utf-8',
            dataType: "json",

            success: function (response) {
                //alert("successful");
                $("#data-table")
                    .bootstrapTable("refresh", {
                        url: "/HolidaySetUp/LoadHoliday"
                    });

                swal({ title: 'UnDeclare All Weekends', text: 'Your UnDeclare All Weekends has been declared Successfully!', type: 'success' });


            },
            error: function (err) {
                alert(err);
            }
        })

        $('#txtdate').val("");
        $('#txtdecription').val("");
        $('#holtype').val("");

    });
});



$(document).ready(function () {
    $('.datetimepicker').datetimepicker({
        format: 'DD MMMM, YYYY',
        icons: {
            time: "fa fa-clock-o",
            date: "fa fa-calendar",
            up: "fa fa-chevron-up",
            down: "fa fa-chevron-down",
            previous: 'fa fa-chevron-left',
            next: 'fa fa-chevron-right',
            today: 'fa fa-screenshot',
            clear: 'fa fa-trash',
            close: 'fa fa-remove',
            inline: true

        }
    });
});
