
$(window, document, undefined).ready(function () {

    $('input').blur(function () {
        var $this = $(this);
        if ($this.val())
            $this.addClass('used');
        else
            $this.removeClass('used');
    });

    var $ripples = $('.ripples');

    $ripples.on('click.Ripples', function (e) {

        var $this = $(this);
        var $offset = $this.parent().offset();
        var $circle = $this.find('.ripplesCircle');

        var x = e.pageX - $offset.left;
        var y = e.pageY - $offset.top;

        $circle.css({
            top: y + 'px',
            left: x + 'px'
        });

        $this.addClass('is-active');

    });

    $ripples.on('animationend webkitAnimationEnd mozAnimationEnd oanimationend MSAnimationEnd', function (e) {
        $(this).removeClass('is-active');
    });

});

    console.log("document loaded");
    var input = $('.validate-input .input100');



function loginValidation() {
    console.log('click');
    var input = $('.validate-input .input100');
    var check = true;

    for (var i = 0; i < input.length; i++) {

        if (validate(input[i]) == false) {
            showValidate(input[i]);
            check = false;
        }
    }

    if (check) {
        $.ajax({
            type: "POST",
            url: "/home/login",
            data: {
                "username": $("#uname").val(),
                "password": $("#psd").val()

            },
   
            success: function (res) {

                if (res["type"] != 6) {
                    $(".focus-message").empty().addClass('message-validate').html(res["message"]);

                }
                else { location.href = "/Home/DocRetieval"; }
            }

        });
    }
    return false;
}




$('.input100').each(function () {
    $(this).on('blur', function () {
        if ($(this).val().trim() != "") {
            $(this).addClass('has-val');
        }
        else {
            $(this).removeClass('has-val');
        }
    })
})


$('.validate-form .input100').each(function () {
    $(this).focus(function () {
        hideValidate(this);
    });
});

function validate(input) {
    console.log($(input).val().length);

    if ($(input).val().trim() == '') {
        return false;
    }

}


function showValidate(input) {
    var thisAlert = $(input).parent();

    $(thisAlert).addClass('alert-validate');
}


function hideValidate(input) {
    var thisAlert = $(input).parent();

    $(thisAlert).removeClass('alert-validate');
}
