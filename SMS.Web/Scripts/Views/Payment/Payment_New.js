/// <reference path="../../jquery-1.7.1-vsdoc.js" />
/// <reference path="../../FormHelper.js" />
/// <reference path="../../dateExt.js" />

var handler = new DefaultFormHandler("../api/Payment/GetModel", "Add", "#Form", true, "Payment");

$().ready(function () {
    handler.Init(function () {
        ko.applyBindings(handler.model);
        AdjustRequiredDataForSelect("#Form");
    });    

    var listItems = "";
    listItems += "<option value='null'>Choose...</option>";
    $("#cmbFeeSetting").html(listItems);

    $("#cmbClassRoom").change(function () {
        if ($("#cmbClassRoom").val() != null && $("#cmbClassRoom").val() != "") {
            FillStudent($("#cmbClassRoom").val());
        }
    });

    $("#cmbPaymentCalander").change(function () {
        if ($("#cmbPaymentCalander").val() != null && $("#cmbPaymentCalander").val() != "") {
            $("#cmbFeeSetting").trigger('change').val(null);

            FillFeeSetting($("#cmbPaymentCalander").val());
        }
    });

});

function FillStudent(id) {
    $.getJSON("../api/Student/GetStudentsByClassRoomId/" + id, function (response) {
        handler.model.Students(response);
        ko.applyBindings(handler.model, $("#cmbStudent")[0]);
        
        $("#cmbStudent").attr("disabled", false);
    });
}

function FillFeeSetting(id) {
    $.getJSON("../api/FeeSetting/GetFeeSettingByPaymentCalanderId/" + id, function (response) {
        handler.model.FeeSettings(response);
        ko.applyBindings(handler.model, $("#cmbFeeSetting")[0]);
        
        $("#cmbFeeSetting").attr("disabled", false);
    });
}
