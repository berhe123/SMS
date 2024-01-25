/// <reference path="../../jquery-1.7.1-vsdoc.js" />
/// <reference path="../../FormHelper.js" />
/// <reference path="../../dateExt.js" />

var handler = new DefaultFormHandler("../api/ExamSchedule/GetModel", "Add", "#Form", true, "ExamSchedule");
var teacherGuid = "teacherGuid";

$().ready(function () {
    handler.Init(function () {
        ko.applyBindings(handler.model);
        AdjustRequiredDataForSelect("#Form");
    });

    var listItemsofTeachers = "";
    listItemsofTeachers += "<option value='null'>Choose...</option>";
    $("#cmbTeacher").html(listItemsofTeachers);

    //var listItemsofExaminers = "";
    //listItemsofExaminers += "<option value='null'>Choose...</option>";
    //$("#cmbExaminer").html(listItemsofExaminers);

    $("#cmbClassRoom").change(function () {
        if ($("#cmbClassRoom").val() != null && $("#cmbClassRoom").val() != "") {

            $('#cmbTeacher').trigger('change').val(null);

            FillTeacher($("#cmbClassRoom").val());

        }
    });

    //$("#cmbTeacher").change(function () {
    //    if ($("#cmbTeacher").val() != null && $("#cmbTeacher").val() != "") {

    //        $('#cmbExaminer').trigger('change').val(null);

    //        FillExaminer($("#cmbTeacher").val());

    //    }
    //});
});

function FillTeacher(id) {
    $.getJSON("../api/SubjectTeacherClassRoom/GetTeacherByClassRoomGuid/" + id, function (response) {

        handler.model.Teachers(response);
        ko.applyBindings(handler.model, $("#cmbTeacher")[0]);

        $("#cmbTeacher").attr("disabled", false);

    });
}
//function FillExaminer(id) {
//    $.getJSON("../api/Teacher/GetExaminer/" + id, function (response) {       

//        handler.model.Examiners(response);
//        ko.applyBindings(handler.model, $("#cmbExaminer")[0]);

//        $("#cmbExaminer").attr("disabled", false);

//    });
//}