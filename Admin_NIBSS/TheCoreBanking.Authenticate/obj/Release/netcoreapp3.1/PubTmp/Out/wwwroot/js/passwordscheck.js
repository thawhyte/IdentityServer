function PasswordNotification(from, align) {

    //$.notify({
    //    icon: "add_alert",
    //    message: '<b>Password must contain both lower and uppercase characters</b>'+
    //    '</br><b>It must have numbers and special characters</b>'

    //},
    //    {
    //        type: 'warning',
    //        timer: 1000,
    //        placement: {
    //            from: from,
    //            align: align
    //        }
    //    });
}
$(document).ready(function () {
    $('#password').keyup(function (event) {
        $('#result').html(checkStrength($('#password').val()))
    })
    function checkStrength(password) {
        var strength = 0
        if (password.length < 6) {
            $('#result').removeClass()
            $('#result').addClass('short')
            debugger
            PasswordNotification();
            return '<b class="text-danger">Too short</b>' 
            
        }
        if (password.length > 7) strength += 1
        // If password contains both lower and uppercase characters, increase strength value.
        if (password.match(/([a-z].*[A-Z])|([A-Z].*[a-z])/)) strength += 1
        // If it has numbers and characters, increase strength value.
        if (password.match(/([a-zA-Z])/) && password.match(/([0-9])/)) strength += 1
        // If it has one special character, increase strength value.
        if (password.match(/([!,%,&,@,#,$,^,*,?,_,~])/)) strength += 1
        // If it has two special characters, increase strength value.
        if (password.match(/(.*[!,%,&,@,#,$,^,*,?,_,~].*[!,%,&,@,#,$,^,*,?,_,~])/)) strength += 1
        // Calculated strength value, we can return messages
        // If value is less than 2
        if (strength < 2) {
            $('#result').removeClass()
            $('#result').addClass('weak')
            return '<b class="text-warning">Weak</b>'
        } else if (strength == 2) {
            $('#result').removeClass()
            $('#result').addClass('good')
            return '<b class="text-blue">Good</b>'
        } else {
            $('#result').removeClass()
            $('#result').addClass('strong')
            return '<b class="text-success">Strong</b>'
        }
    }
});