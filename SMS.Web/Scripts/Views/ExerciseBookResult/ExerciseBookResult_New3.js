/// <reference path="../../jquery-1.7.1-vsdoc.js" />
/// <reference path="../../FormHelper.js" />
/// <reference path="../../dateExt.js" />

var handler = new DefaultFormHandler("../api/ExerciseBookResult/GetModel", "Add", "#Form", true, "ExerciseBookResult");
var studentIdColName = "StudentGuid";

var studentResults = [];
var tblStudents;
var classroomGuid;


$().ready(function () {
    handler.Init(function () {
        ko.applyBindings(handler.model);
        AdjustRequiredDataForSelect("#Form");
    });

    var listItems = "";
    listItems += "<option value='null'>Choose...</option>";
    $("#cmbSubject").html(listItems);

    $('#tblStudent tbody tr').live('click', function () {
        $('#tblStudent tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblStudent.fnGetData(this._DT_RowIndex);
        studentName = row["FullName"];
        //OnStudentSelected(row[studentIdColName]);
    });

    //var listItems = "";
    //listItems += "<option value='null'>Choose...</option>";

    $("#cmbClassRoom").change(function () {
        if ($("#cmbClassRoom").val() != null && $("#cmbClassRoom").val() != "") {

            FillSubject($("#cmbClassRoom").val());
        }
    });

    $("#btnSaveResult").click(function () {
        var i = 0;
        $('#tblStudent tbody tr').each(function () {
            var row = tblStudent.fnGetData(this._DT_RowIndex);
            var result = ($("#txt" + row[studentIdColName]).val());
            var resultComment = ($("#txtComment" + row[studentIdColName]).val());
            var studentguid = row['StudentGuid'];
            var SubjectTeacherClassRoomGuid = $("#cmbSubject").val();
            var Outof = $("#txtOutOf").val();


            var obj = { TableRowGuid: null, StudentGuid: studentguid, SubjectTeacherClassRoomGuid: SubjectTeacherClassRoomGuid, Result: result, OutOf: Outof, Comment: resultComment };
            studentResults[i] = obj;
            i++;
        });
        PostJson("../api/ExerciseBookResult/SaveStudentResultByBatch",
            JSON.stringify(studentResults),
            function (response) {
                if (response.Success) {
                    AlertMsg("Success", response.Message);
                }
                else AlertMsg("Error", response.Message, "alert-error");
            }
        );

    });

    $("#btnShowStudents").click(function () {
        classroomGuid = $("#cmbClassRoom").val();
        if ($("#cmbClassRoom").val() != null && $("#cmbClassRoom").val() != "")

            FillStudents(classroomGuid);
            
    });
});

function FillSubject(id) {
    $.getJSON("../api/SubjectTeacherClassRoom/GetSubjectsByClassRoomId/" + id, function (response) {

        handler.model.Subjects(response);
        ko.applyBindings(handler.model, $("#cmbSubject")[0]);

        $("#cmbSubject").attr("disabled", false);
    });
}

function FillStudents(id) {    
    tblStudent = $('#tblStudent').dataTable({
        "sAjaxSource": "../api/Student/GetStudentsForExerciseBookResultDataEntry",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            pageDetail.ClassRoomGuid = id;
            oSettings.oLanguage.sSearch = "StudentFullName";
            $.ajax({
                dataType: 'json',
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: sUrl,
                data: JSON.stringify(pageDetail),
                success: fnCallback
            });
        },
        "aoColumns": [
            ColHidden(studentIdColName),
            { "mData": "StudentFullName" },
             EntryTextBox(studentIdColName),
             EntryTextBoxComment(studentIdColName),],
        "aaSorting": [[1, "asc"]],
        "bPaginate": false,
        "bFilter": false,
        "bSort": false,
        "bInfo": false,
        "width": "100%"
    });
}

function OnStudentSelected(id) {
    SelectedStudentId = id;

}