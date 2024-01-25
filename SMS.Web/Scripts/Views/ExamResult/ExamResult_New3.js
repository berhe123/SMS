/// <reference path="../../jquery-1.7.1-vsdoc.js" />
/// <reference path="../../FormHelper.js" />
/// <reference path="../../dateExt.js" />

var handler = new DefaultFormHandler("../api/ExamResult/GetModel", "Add", "#Form", true, "ExamResult");
var studentIdColName = "StudentGuid";

var studentResults = [];
var tblStudents;
var examscheduleGuid;

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
            var ExamScheduleGuid = examscheduleGuid;

            var obj = { TableRowGuid: null, StudentGuid: studentguid, ExamScheduleGuid: examscheduleGuid, Result: result, Comment: resultComment };
            studentResults[i] = obj;
            i++;
        });
        PostJson("../api/ExamResult/SaveStudentResultByBatch",
            JSON.stringify(studentResults),
            function (response) {
                if (response.Success) {
                    AlertMsg("Success", response.Message);
                }
                else AlertMsg("Error", response.Message, "alert-error");
            }
        );

    });

});
function FillStudents(id) {    
    tblStudent = $('#tblStudent').dataTable({
        "sAjaxSource": "../api/Student/GetStudentsForExamResultDataEntry",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            pageDetail.ExamScheduleGuid = id;
            oSettings.oLanguage.sSearch = "FullName";
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
            { "mData": "FullName" },
             EntryTextBox(studentIdColName),
             EntryTextBoxComment(studentIdColName), ],
        "aaSorting": [[1, "asc"]],
        "bPaginate": false,
        "bFilter": false,
        "bSort": false,
        "bInfo": false,
        "width": "100%"
    });
}

function FillSubject(id) {
    $.getJSON("../api/ExamSchedule/GetSubjectsByClassRoomId/" + id, function (response) {

        handler.model.Subjects(response);
        ko.applyBindings(handler.model, $("#cmbSubject")[0]);

        $("#cmbSubject").attr("disabled", false);
    });
}

$("#btnShowStudents").click(function () {
    examscheduleGuid = $("#cmbSubject").val();
    if (examscheduleGuid != null && examscheduleGuid != "")

        FillStudents(examscheduleGuid);

});

function OnStudentSelected(id) {
    SelectedStudentId = id;

}