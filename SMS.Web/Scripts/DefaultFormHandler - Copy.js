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
            ko.mapping.fromJS(modelJson.Model, thisObj.model);
            if (thisObj.LoadCallback) thisObj.LoadCallback(thisObj.model);
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
