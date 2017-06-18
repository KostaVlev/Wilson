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

//var addPaycheckPayment = function (form) {
//    var formData = $(form).serializeArray();
//    //var token = $("input[name=__RequestVerificationToken]").val();
//    var fdata = new FormData();
//    formData.forEach(obj => fdata.append(obj.name, obj.value));
//    //fdata.append("model", model);
//    //fdata.append("__RequestVerificationToken", token);

//    $.ajax({
//        type: "POST",
//        url: "/Accounting/Payroll/AddPaycheckPayment",
//        contentType: false,
//        processData: false,
//        data: fdata,
//        success: function (data) {
//            displayMessage("Payment was added.", "message", true);
//        },
//        error: function () {
//            displayMessage("Something went wrong! Please try again.", "message", false);
//        }
//    });
//};
