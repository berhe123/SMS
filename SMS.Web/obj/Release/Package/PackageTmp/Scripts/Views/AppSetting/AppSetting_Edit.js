// 
// NOTE: For the ko mapping to work properly, make sure the #containerDiv names match the setting key names
// otherwise... 
// 
var AppSettingKeys, AppSettingBoolValuedKeys;
var models = new Array();

$().ready(function () {

    GetAppSettingKeys();

    SetModels();

    $("#btnUpdateAppSettings").click(function () {
        if (!IsFormValid("#VatPercentage") || !IsFormValid("#WithHoldingPercentage")) {
            AlertMsg("Error", "Vat and Witholding Percentage are required", "alert-notify");
        }
        else {
            UpdateAppSettings();
        }
    });

});

function GetAppSettingKeys() {
    $.getJSON("../api/AppSetting/GetAppSettingKeys", function (response) {
        AppSettingKeys = $.parseJSON(response);
    });
    $.getJSON("../api/AppSetting/GetAppSettingBoolValuedKeys", function (response) {
        AppSettingBoolValuedKeys = $.parseJSON(response);
    });
}

function SetModels() {
    $.getJSON("../api/AppSetting/GetList", function (response) {
        for (var settingKey in AppSettingKeys) {
            if (AppSettingKeys.hasOwnProperty(settingKey)) {
                SetModel(AppSettingKeys[settingKey], response[AppSettingKeys[settingKey] - 1], settingKey);
            }
        }
    });
}

function SetModel(settingKey, settingData, settingKeyName) {
    models[settingKey - 1] = ko.mapping.fromJS(settingData);
    var isBoolean = false;
    for (var boolValuedSettingKey in AppSettingBoolValuedKeys) {
        if (AppSettingBoolValuedKeys[boolValuedSettingKey] == AppSettingKeys[settingKeyName]) {
            isBoolean = true;
            break;
        }
    }
    if (isBoolean) models[settingKey - 1].SettingValueAsBoolean = GetActualValueAsBoolean(models[settingKey - 1]);
    ko.applyBindings(models[settingKey - 1], $("#" + settingKeyName)[0]);
}

function GetActualValueAsBoolean(model) {
    return ko.computed({
        read: function () { return this.SettingValue() === "true"; },
        write: function (newValue) { this.SettingValue(newValue ? "true" : "false"); },
        owner: model
    });
}

function UpdateAppSettings() {
    PostJson("../api/AppSetting/Update1",
        ko.mapping.toJSON(models),
        function (response) {
            if (response.Success) { AlertMsg("Success", response.Message); SetModels(); }
            else AlertMsg("Error", response.Message, "alert-error");
        }
    );
}
