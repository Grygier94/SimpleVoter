$.validator.addMethod("ensureminimumelements", function (value, element, params) {


    var numberOfValues = 0;
    $.each(element.parentElement.children, function(index, element) {
        if ($.trim(element.value).length > 0)
            numberOfValues++;
    });

    return numberOfValues >= params;
});

$.validator.unobtrusive.adapters.add("ensureminimumelements", ["minelements"], function (options) {
    options.rules["ensureminimumelements"] = options.params.minelements;
    options.messages["ensureminimumelements"] = options.message;
});