/// <reference path="../../jquery-1.7.1-vsdoc.js" />
/// <reference path="../../FormHelper.js" />
/// <reference path="../../dateExt.js" />

var handler = new DefaultFormHandler("../api/TestSchedule/GetModel", "Add", "#Form", true, "TestSchedule");

$().ready(function () {
    handler.Init(function () {
        ko.applyBindings(handler.model);
        AdjustRequiredDataForSelect("#Form");
    });

    var listItems = "";
    listItems += "<option value='null'>Choose...</option>";
    $("#cmbPeriod").html(listItems);

    $("#cmbClassRoomSubject").change(function () {
        if ($("#cmbClassRoomSubject").val() != null && $("#cmbClassRoomSubject").val() != "") {

            $('#cmbPeriod').trigger('change').val(null);

            FillPeriod($("#cmbClassRoomSubject").val());

        }
    });

});

function FillPeriod(id) {
    $.getJSON("../api/LessonClass/GetPeriodsBySubjectGuid/" + id, function (response) {

        handler.model.Periods(response);
        ko.applyBindings(handler.model, $("#cmbPeriod")[0]);

        $("#cmbPeriod").attr("disabled", false);
        //$("#cmbSubjectClassRoom").val('null');
    });
}