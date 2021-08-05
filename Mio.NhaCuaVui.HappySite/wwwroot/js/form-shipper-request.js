// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {

    var current_fs, next_fs, previous_fs; //fieldsets
    var opacity;

    $(".next").click(function () {

        current_fs = $(this).parent();
        next_fs = $(this).parent().next();

        var fs_current = $(this).parent();
        var inputs = fs_current.find("input[required]");
        console.log(inputs);
        if (inputs.length > 0) {
            for (var index = 0; index < inputs.length; index++) {
                var inputValue = inputs[index].value;
                if (inputValue == "") {
                    alert("Bạn quên điền thông tin này rồi");
                    inputs[index].focus();
                    return;
                }
            }
        }


        //Add Class Active
        $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

        //show the next fieldset
        next_fs.show();
        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now) {
                // for making fielset appear animation
                opacity = 1 - now;

                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                next_fs.css({ 'opacity': opacity });
            },
            duration: 600
        });
    });

    $(".previous").click(function () {

        current_fs = $(this).parent();
        previous_fs = $(this).parent().prev();

        //Remove class active
        $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");

        //show the previous fieldset
        previous_fs.show();

        //hide the current fieldset with style
        current_fs.animate({ opacity: 0 }, {
            step: function (now) {
                // for making fielset appear animation
                opacity = 1 - now;

                current_fs.css({
                    'display': 'none',
                    'position': 'relative'
                });
                previous_fs.css({ 'opacity': opacity });
            },
            duration: 600
        });
    });

    $(".submit").click(function (e) {
        if ($('#district').val() == null || $('#district').val() == '') {
            e.preventDefault();
            alert("Bạn quên điền thông tin Quận/Huyện rồi");
            return false;
        } else {
            $("#form-donate").submit();
        }
        return false;
    })

});