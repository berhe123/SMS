/// <reference path="../../jquery-1.7.1-vsdoc.js" />
/// <reference path="../../FormHelper.js" />
/// <reference path="../../dateExt.js" />

var handler = new DefaultFormHandler("../api/ClassRoom/GetModel/", "Add", "#Form", true, "ClassRoom");

$().ready(function () {
    handler.Init(function () {
        ko.applyBindings(handler.model);
        AdjustRequiredDataForSelect("#Form");
    });   
});

