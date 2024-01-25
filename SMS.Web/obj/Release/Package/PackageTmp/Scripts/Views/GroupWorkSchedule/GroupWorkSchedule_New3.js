/// <reference path="../../jquery-1.7.1-vsdoc.js" />
/// <reference path="../../FormHelper.js" />
/// <reference path="../../dateExt.js" />

var handler = new DefaultFormHandler("../api/GroupWorkSchedule/GetModel", "Add", "#Form", true, "GroupWorkSchedule");

$().ready(function () {
    handler.Init(function () {
        ko.applyBindings(handler.model);
        AdjustRequiredDataForSelect("#Form");
    });

    var listItems = "";
    listItems += "<option value='null'>Choose...</option>";
    $("#cmbGroupName").html(listItems);

    $("#cmbClassRoomSubject").change(function () {
        if ($("#cmbClassRoomSubject").val() != null && $("#cmbClassRoomSubject").val() != "") {
            $('#cmbGroupName').trigger('change').val(null);

            FillGroupName($("#cmbClassRoomSubject").val());

        }
    });

});

function FillGroupName(id) {
    $.getJSON("../api/GroupName/GetGroupNamesBySubjectTeaacherClassRoomGuid/" + id, function (response) {

        handler.model.GroupNames(response);
        ko.applyBindings(handler.model, $("#cmbGroupName")[0]);

        $("#cmbGroupName").attr("disabled", false);
        //$("#cmbSubjectClassRoom").val('null');
    });
}

