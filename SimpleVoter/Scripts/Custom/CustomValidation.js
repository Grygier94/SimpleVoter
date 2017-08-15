$.validator.addMethod("currentdate", function (value, element, params) {
    debugger; 
    var currentDate = new Date();
    return Date.parse(element.value) > Date.parse(currentDate);
});

$.validator.unobtrusive.adapters.add("currentdate", [], function (options) {
    debugger; 
    options.messages["currentdate"] = options.message;
});

$.validator.addMethod("ensureminimumelements", function (value, element, params) {
    debugger; 
    var numberOfValues = 0;
    $.each(element.parentElement.children, function (index, element) {
        if ($.trim(element.value).length > 0)
            numberOfValues++;
    });

    return numberOfValues >= params;
});

$.validator.unobtrusive.adapters.add("ensureminimumelements", ["minelements"], function (options) {
    debugger; 
    options.rules["ensureminimumelements"] = options.params.minelements;
    options.messages["ensureminimumelements"] = options.message;
});