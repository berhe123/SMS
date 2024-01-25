/// <reference path="../../jquery-1.7.1-vsdoc.js" />
/// <reference path="../../FormHelper.js" />
/// <reference path="../../dateExt.js" />

var handler = new DefaultFormHandler("../api/PaymentCalander/GetModel/" + $("#txtId").val(), "Update", "#Form", false, "PaymentCalander");

$().ready(function () {
    handler.Init(function () {
        ko.applyBindings(handler.model);
        AdjustRequiredDataForSelect("#Form");
    });
});