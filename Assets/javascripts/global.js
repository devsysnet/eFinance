function loadCombo() {
    $("select").select2();
}

function loadTitle(title, id) {
    $(document).attr("title", title + $("#" + id).html());
}

function loadNumeric() {
    $(".numeric").numeric({ decimal: ".", negative: false });

    $(".numeric").blur(function () {
        var num = $(this).val();
        if (num == "") {
            $(this).val('0');
        }
    });
}

function loadTime() {
    var options2 = {
        minuteStep: 1,
        showSeconds: true,
        showMeridian: false,
        showInputs: false,
        orientation: $('body').hasClass('right-to-left') ? { x: 'right', y: 'auto' } : { x: 'auto', y: 'auto' }
    }
    $('.time').timepicker(options2);

}

function loadNumericAll() {
    $(".negative").numeric({ decimal: ".", negative: true });
}

function loadMoney() {
    //$(".money").css("text-align", "right");
    //$(".money").addClass("right");
    $(".money").numeric({ decimal: ".", negative: true });
    $(".money").focus(function () {
        if ($(this).val() != "") {
            $(this).val(removeFormatMoney($(this).val()));
        }
    });
    $(".money").blur(function () {
        var num = $(this).val();
        if (num != "") {
            //thousandFormat(num);
            num = removeFormatMoney(num);
            var string = String(num);
            var string = string.replace('.', ' ');
            
            var array2 = string.toString().split(' ');
            num = parseInt(num).toFixed(0);

            var array = num.toString().split('');
            var index = -3;
            while (array.length + index > 0) {
                array.splice(index, 0, ',');
                index -= 4;
            }
            if (array2.length == 1) {
                $(this).val(array.join('') + '.00');
            } else {
                if (array2[1].length == 1) {
                    $(this).val(array.join('') + '.' + array2[1].substring(0, 2) + '0');
                } else {
                    $(this).val(array.join('') + '.' + array2[1].substring(0, 2));
                }
            }
        } else {
            $(this).val('0.00');
        }
    });
}

function thousandFormat(input) {
    var output = input;
    if (parseFloat(input)) {
        input = new String(input);
        var parts = input.split(".");
        parts[0] = parts[0].split("").reverse().join("").replace(/(\d{3})(?!$)/g, "$1,").split("").reverse().join("");
        output = parts.join(".");
    }

    return (output).replace(".0000", "");
}

function viewMoney(num) {
    if (num != "" && num != "0.0000") {
        num = removeFormatMoney(num);
        var array = num.toString().split('');
        var index = -3;

        while (array.length + index > 0) {
            array.splice(index, 0, ',');
            index -= 4;
        }

        return array.join('') + '.00';
    }
}

function removeFormatMoney(money) {
    var minus = money.substring(0, 1);
    if (minus == "-") {
        var number = '-' + Number(money.replace(/[^0-9\.]+/g, ""));
    } else {
        var number = Number(money.replace(/[^0-9\.]+/g, ""));
    }
    return number;
}

function loadPicker() {
    $(".date").removeClass("center");
    $(".date").Zebra_DatePicker({ format: 'd-M-Y' });
}

function removePicker(id, value) {
    $("#" + id).addClass("center").val(value);
    $('#' + id).Zebra_DatePicker().data('Zebra_DatePicker').destroy();
}

function loadPhone() {
    //$(".phone").attr("data-inputmask", "'mask': ['999-999-9999 [-99999]', '+099 99 99 9999[9]-9999']");
    //$(".phone").inputmask();
    //$(".phone").attr("class", "form-control phone");
    $(".phone").mask("?(9999) 9999-9999");
}


function jqInitNumeric(elem) {
    $(elem).css("text-align", "left");
    $(elem).numeric({ decimal: ".", negative: false });
}

function jqInitMoney(elem) {
    $(elem).css("text-align", "right");
    $(elem).attr("class", "money");
    $(elem).numeric({ decimal: ".", negative: false });
    $(elem).focus(function () {
        if ($(this).val() != "") {
            $(this).val(removeFormatMoney($(this).val()));
        }
    });
    $(elem).blur(function () {
        var num = $(this).val();
        if (num != "") {
            num = removeFormatMoney(num);
            var array = num.toString().split('');
            var index = -3;
            while (array.length + index > 0) {
                array.splice(index, 0, ',');
                index -= 4;
            }

            $(this).val(array.join('') + '.00');
        }
    });
}