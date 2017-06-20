var setScheduleRarioButtonAsChecked = function (selector, radioButtonsToResetName) {
    var elementsToReset = $('[name=' + radioButtonsToResetName + ']');
    for (var i = 1; i < elementsToReset.length; i++) {
        elementsToReset[i].removeAttribute("checked");
        elementsToReset[i].checked = false;
    }

    var elements = $(selector);
    elements[0].setAttribute("checked", "checked");
    elements[0].checked = true;
};

var displayMessage = function (msg, elementId, isSuccesMsg) {
    var element = $("#" + elementId);
    element.html(msg);
    if (isSuccesMsg) {
        element.attr("class", "alert alert-success");
    } else {
        element.attr("class", "alert alert-danger");
    }

    $("#cover").show().delay(4000).fadeOut();
    element.show().delay(4000).fadeOut();
};

var addPaychecksPayments = function () {
    var forms = $('form[name=paymentForm]');
    var length = forms.length;

    // Create IEnumerable<AddPaymentViewModel> object to Post.
    var result = [];
    for (var i = 0; i < length; i++) {

        var form = forms.first().serializeArray();
        forms.splice(0, 1);

        var obj = {};
        for (var j = 0; j < form.length; j++) {
            var index = form[j].name.indexOf(".");
            if (index !== -1) {

                // Create nested PaymentViewModel object.
                var innerObj = {};
                innerObj[form[j].name.substring(index + 1, form[j].name.length)] = form[j].value;
                obj[form[j].name.substring(0, index)] = innerObj;
            }
            else {
                obj[form[j].name] = form[j].value;
            }
        }

        result.push(obj);
    }

    var token = $("input[name=__RequestVerificationToken]").val();

    $.ajax({
        type: "POST",
        url: "/Accounting/Payroll/AddPaycheckPayments",
        data: { model: result, __RequestVerificationToken: token },
        success: function (response) {
            if (response) {
                $('body').html(response);
            }
        },
        error: function () {
            displayMessage("Something went wrong! Please try again.", "message", false);
        }
    });
};
