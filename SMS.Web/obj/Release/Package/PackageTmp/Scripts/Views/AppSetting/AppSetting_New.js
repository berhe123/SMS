/// <reference path="../../jquery-1.7.1-vsdoc.js" />
/// <reference path="../../FormHelper.js" />
/// <reference path="../../dateExt.js" />

var handler = new DefaultFormHandler("../api/Location/GetModel", "Add", "#Form", true, "Location");

$().ready(function () {

    handler.Init(function () {
        ko.applyBindings(handler.model);       
    });   
});

