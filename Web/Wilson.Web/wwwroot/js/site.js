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
