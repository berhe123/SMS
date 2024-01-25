/// <reference path="jquery-1.7.1-vsdoc.js" />
/// <reference path="dateExt.js" />

var DefaultFormHandler = function (getUrl, saveUrl, containerSelector, isNew, entityName) {
    var thisObj = this;
    this.model = null;
    this.lookups = null;
    this.getUrl = getUrl;
    this.saveUrl = saveUrl;
    this.containerSelector = containerSelector;
    this.isNew = isNew;
    this.entityName = entityName;

    this.Init = function (callback) {
        thisObj.LoadFromServer(function () {
            callback();
        });
    };

    this.SaveCallback = null;
    this.Save = function () {
        if (IsFormValid(thisObj.containerSelector)) {
            PostJson("../api/" + thisObj.entityName + "/" + saveUrl, ko.mapping.toJSON(thisObj.model), function (result) {
                if (result.Success) {
                    AlertMsg("Success", result.Message);
                    if (thisObj.SaveCallback) thisObj.SaveCallback(thisObj.model, result.Id);
                }
                else AlertMsg("Error", result.Message, "alert-error");
            });
        }
    };

    this.LoadCallback = null;
    this.LoadFromServer = function (callback) {
        $.getJSON(getUrl, function (modelJson) {
            thisObj.model = ko.mapping.fromJS(modelJson);
            callback();
            if (thisObj.LoadCallback) thisObj.LoadCallback(thisObj.model);
        });
    };

    this.ReloadFromServer = function () {
        $.getJSON(getUrl, function (modelJson) {
            ko.mapping.fromJS(modelJson, thisObj.model);
            if (thisObj.LoadCallback) thisObj.LoadCallback(thisObj.model);
            $(".combo").select2();
        });
    };
};
var DefaultFormHandlerExt = function (getUrl, getObj, saveUrl, containerSelector, isNew, entityName) {
    var thisObj = this;
    this.model = null;
    this.lookups = null;
    this.getUrl = getUrl;
    this.getObj = getObj;
    this.saveUrl = saveUrl;
    this.containerSelector = containerSelector;
    this.isNew = isNew;
    this.entityName = entityName;

    this.Init = function (callback) {
        thisObj.LoadFromServer(function () {
            callback();
        });
    };

    this.SaveCallback = null;
    this.Save = function () {
        if (IsFormValid(thisObj.containerSelector)) {
            PostJson("../api/" + thisObj.entityName + "/" + saveUrl, ko.mapping.toJSON(thisObj.model), function (result) {
                if (result.Success) {
                    AlertMsg("Success", result.Message);
                    if (thisObj.SaveCallback) thisObj.SaveCallback(thisObj.model, result.Id);
                }
                else AlertMsg("Error", result.Message, "alert-error");
            });
        }
    };
    this.LoadCallback = null;
    this.LoadFromServer = function (callback) {
        PostJson(getUrl, JSON.stringify(getObj), function (modelJson) {
            thisObj.model = ko.mapping.fromJS(modelJson);
            callback();
            if (thisObj.LoadCallback) thisObj.LoadCallback(thisObj.model);
        });
    };

    this.ReloadFromServer = function () {
        PostJson(getUrl, JSON.stringify(getObj), function (modelJson) {
            ko.mapping.fromJS(modelJson, thisObj.model);
            if (thisObj.LoadCallback) thisObj.LoadCallback(thisObj.model);
            $(".combo").select2();
        });
    };
};

function InitDatePicker(inputId, field) {
    $(inputId).calendarsPicker({
        onSelect: function (dates) {
            field(dates[0].toJSDate().toISO8601String(3) + "T00:00:00");
        }
    })
    .val(FromISO(field()));
}

function FromISO(isoDate) {
    var date = isoDate.substr(0, isoDate.indexOf("T")).split("-");
    return date[1] + "/" + date[2] + "/" + date[0];
}

function IsFormValid(containerSelector) {
    var form = $(containerSelector + " form");
    var valid = true;
    form.each(function (index, item) {
        $(item).validate({ ignore: '' });
        if (!$(item).valid()) {
            valid = false;
        }
    });
    return valid;
}


ko.bindingHandlers.date = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        // because of the custom binging we have attched below, 
        // it works solidly for binding dates to textboxes. However, there is a major issue with it, 
        // which is that if other knockout computed methods or conditional statements depend on your date value, they will not get fired. 
        // For example, if you add a ko.computed method called isMillennial which checks the dateOfBirth, it will not be fired when the dateOfBirth changes
        // So this function is added into the custom ko binding to fix the issue
        ko.utils.registerEventHandler(element, 'change', function () {
            var value = valueAccessor();

            if (element.value !== null && element.value !== undefined && (element.value.length > 0 || element.value instanceof Date)) {
                value(element.value);
            }
            else {
                value('');
            }
        });
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var value = valueAccessor();
        var allBindings = allBindingsAccessor();
        var valueUnwrapped = ko.utils.unwrapObservable(value);

        // Date formats: http://momentjs.com/docs/#/displaying/format/
        var pattern = allBindings.format || 'DD/MM/YYYY';

        var output = "-";
        if (valueUnwrapped !== null && valueUnwrapped !== undefined && (valueUnwrapped.length > 0 || valueUnwrapped instanceof Date)) {
            // This code only works if valueUnwrapped is a string. When it's a date, (valueUnwrapped.length > 0) returns false, and the result is "-".
            // In order to fix this, the condition is changed to be (valueUnwrapped.length > 0 || valueUnwrapped instanceof Date)
            output = moment(valueUnwrapped).format(pattern);
        }

        if ($(element).is("input") === true) {
            $(element).val(output);
        } else {
            $(element).text(output);
        }
    }
};

ko.bindingHandlers.select2 = {
    init: function (element, valueAccessor) {
        $(element).select2(valueAccessor());

        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).select2('destroy');
        });
    },
    update: function (element) {
        $(element).trigger('change');
    }
};

$.datepicker.regional[""].dateFormat = 'yy-mm-dd'; // yy-mm-ddT00:00:00
$.datepicker.setDefaults($.datepicker.regional['']);

$(".combo").select2({ placeholder: 'Choose...', allowClear: true });
/*
   * When you change the value the select via select2, it triggers
   * a 'change' event, but the jquery validation plugin
   * only re-validates on 'blur'
   */
$(".combo").on('change', function () {
    $(this).trigger('blur');
});

//$(".dateinput").datepicker();
$(".dateinput").datetimepicker();
$(".disabled").attr("disabled", true);
//function AdjustRequiredDataForSelect() {
//    $(".combo").each(function () {
//        var options = $('#' + $(this).attr("id") + ' option').map(function () {
//            return $(this).val();
//        }).get();

//        if ($(this).hasClass("required") && options.length == 2) {
//            $(this).select2("val", options[1]).trigger('change');
//            $(this).addClass("disabled");
//            $(".disabled").attr("disabled", true);
//            $(this).select2("disable");
//        }
//    });
//}
function AdjustRequiredDataForSelect(containerSelector) {
    $(containerSelector + " .combo").each(function () {
        var options = $('#' + $(this).attr("id") + ' option').map(function () {
            return $(this).val();
        }).get();

        if ($(this).hasClass("required") && options.length == 2) {
            $(this).select2("val", options[1]).trigger('change');
            $(this).addClass("disabled");
            $(".disabled").attr("disabled", true);
            $(this).select2("disable");
        }
    });
}


$(".digits").each(function () {
    $(this).bind('keypress', function (e) {
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57))
            return false;
        else
            return true;
    });
});

$(".number").each(function () {
    $(this).bind('keypress', function (e) {
        if (e.which != 8 && e.which != 0 && e.which != 46 && (e.which < 48 || e.which > 57))
            return false;
        else
            return true;
    });
});

function AdjustReferenceNumberElement(settingKey, elementSelector) {
    $.getJSON("../api/AppSetting/GetModel", function (response) {
        var AppSetting = response;
        AppSetting.SettingKey = settingKey;

        if (AppSetting != null && AppSetting != undefined) {
            PostJson("../api/AppSetting/GetSetting",
                ko.mapping.toJSON(AppSetting),
                function (response) {
                    if (response === "true") {
                        $(elementSelector).removeClass("required");
                        $(elementSelector).addClass("disabled");
                        $(elementSelector).attr("placeholder", $(elementSelector).attr("placeholder") + ": Auto Generated");
                        $(".disabled").attr("disabled", true);
                    }
                    else {
                        $(elementSelector).removeClass("disabled");
                        $(elementSelector).addClass("required");
                    }
                }
            );
        }

    });
}