/// <reference path="../../jquery-1.7.1-vsdoc.js" />
/// <reference path="../../FormHelper.js" />
/// <reference path="../../dateExt.js" />

var handler = new DefaultFormHandler("../api/LessonClass/GetModel", "Add", "#Form", true, "LessonClass");

$().ready(function () {
    handler.Init(function () {
        ko.applyBindings(handler.model);
         AdjustRequiredDataForSelect("#Form");             

    });
   
    var listItems = "";
    listItems += "<option value='null'>Choose...</option>";
    $("#cmbClassRoom").html(listItems);

    $("#cmbTeacher").change(function () {
        if ($("#cmbTeacher").val() != null && $("#cmbTeacher").val() != "")
        {
            $('#cmbClassRoom').trigger('change').val(null);

            FillClassRoomSubject($("#cmbTeacher").val());
        }
    });

});



function FillClassRoomSubject(id) {
    $.getJSON("../api/SubjectTeacherClassRoom/GetClassRoomSubjectsByTeacherGuid/" + id, function (response) {

        handler.model.ClassRooms(response);
        ko.applyBindings(handler.model, $("#cmbClassRoom")[0]);
                     
        $("#cmbClassRoom").attr("disabled", false);
    });
}