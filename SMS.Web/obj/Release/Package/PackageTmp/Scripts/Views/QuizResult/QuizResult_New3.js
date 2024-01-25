/// <reference path="../../jquery-1.7.1-vsdoc.js" />
/// <reference path="../../FormHelper.js" />
/// <reference path="../../dateExt.js" />

var handler = new DefaultFormHandler("../api/QuizResult/GetModel", "Add", "#Form", true, "QuizResult");
var studentIdColName = "StudentGuid";

var studentResults = [];
var tblStudents;
var quizGuid;

$().ready(function () {
    handler.Init(function () {
        ko.applyBindings(handler.model);
        AdjustRequiredDataForSelect("#Form");
    });

    var listItems = "";
    listItems += "<option value='null'>Choose...</option>";
    $("#cmbQuiz").html(listItems);

    $('#tblStudent tbody tr').live('click', function () {
        $('#tblStudent tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblStudent.fnGetData(this._DT_RowIndex);
        studentName = row["StudentName"];
        //OnStudentSelected(row[studentIdColName]);
    });

    $("#cmbClassRoom").change(function () {
        if ($("#cmbClassRoom").val() != null && $("#cmbClassRoom").val() != "") {

            $('#cmbQuiz').trigger('change').val(null);

            FillQuiz($("#cmbClassRoom").val());

        }
    });

   

    $("#btnSaveResult").click(function () {
        var i = 0;
        $('#tblStudent tbody tr').each(function () {
            var row = tblStudent.fnGetData(this._DT_RowIndex);
            var result = ($("#txt" + row[studentIdColName]).val());
            var resultComment = ($("#txtComment" + row[studentIdColName]).val());
            var studentguid = row['StudentGuid'];
            var QuizScheduleGuid = quizGuid;


            var obj = { TableRowGuid: null, StudentGuid: studentguid, QuizScheduleGuid: quizGuid, Result: result, Comment: resultComment };
            studentResults[i] = obj;
            i++;
        });
        PostJson("../api/QuizResult/SaveStudentResultByBatch",
            JSON.stringify(studentResults),
            function (response) {
                if (response.Success) {
                    AlertMsg("Success", response.Message);
                }
                else AlertMsg("Error", response.Message, "alert-error");
            }
        );

    })

});

$("#btnShowStudents").click(function () {
        quizGuid = $("#cmbQuiz").val();
        if (quizGuid != null && quizGuid != "")

            FillStudents(quizGuid);

    });

function FillStudents(id) {    
    tblStudent = $('#tblStudent').dataTable({
        "sAjaxSource": "../api/Student/GetStudentsForQuizResultDataEntry",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            pageDetail.QuizScheduleGuid = id;
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

function FillQuiz(id) {
    $.getJSON("../api/QuizSchedule/GetQuizsByClassRoomId/" + id, function (response) {

        handler.model.Quizs(response);
        ko.applyBindings(handler.model, $("#cmbQuiz")[0]);
        
        $("#cmbQuiz").attr("disabled", false);
        //$("#cmbSubjectClassRoom").val('null');
    });
}

function OnStudentSelected(id) {
    SelectedStudentId = id;

}