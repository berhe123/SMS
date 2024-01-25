/// <reference path="../../jquery-1.7.1-vsdoc.js" />
/// <reference path="../../FormHelper.js" />
/// <reference path="../../dateExt.js" />

var handler = new DefaultFormHandler("../api/GroupWorkResult/GetModel", "Add", "#Form", true, "GroupWorkResult");
var tblGroupName;
var groupName;
var SelectedgroupId;
var groupIdColName = "TableRowGuid";

$().ready(function () {
    handler.Init(function () {
        ko.applyBindings(handler.model);
        AdjustRequiredDataForSelect("#Form");
    });

    $("#cmbClassRoomSubject").change(function () {
        if ($("#cmbClassRoomSubject").val() != null && $("#cmbClassRoomSubject").val() != "") {
            FillGroupNames($("#cmbClassRoomSubject").val());
        }
    });
});

function FillGroupNames(id) {
    $.getJSON("../api/GroupMember/GetGroupNamesBySubjectId/" + id, function (response) {

        handler.model.GroupNames(response);
        ko.applyBindings(handler.model, $("#cmbGroupName")[0]);

        $("#cmbGroupName").attr("disabled", false);

    });    
}

function OnGroupSelected(id) {
    SelectedgroupId = id;

}