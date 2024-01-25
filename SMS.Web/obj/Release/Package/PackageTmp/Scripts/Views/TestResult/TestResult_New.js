/// <reference path="../../jquery-1.7.1-vsdoc.js" />
/// <reference path="../../FormHelper.js" />
/// <reference path="../../dateExt.js" />

var handler = new DefaultFormHandler("../api/TestResult/GetModel", "Add", "#Form", true, "TestResult");
var studentIdColName = "StudentGuid";

var studentResults = [];
var tblStudents;
var testGuid;

$().ready(function () {
    handler.Init(function () {
        ko.applyBindings(handler.model);
        AdjustRequiredDataForSelect("#Form");
    });

    //var listItems = "";
    //listItems += "<option value='null'>Choose...</option>";

    $('#tblStudent tbody tr').live('click', function () {
        $('#tblStudent tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblStudent.fnGetData(this._DT_RowIndex);
        studentName = row["FullName"];
        //OnStudentSelected(row[studentIdColName]);
    });

    $("#cmbClassRoom").change(function () {
        if ($("#cmbClassRoom").val() != null && $("#cmbClassRoom").val() != "") {
            FillTest($("#cmbClassRoom").val());
        }
    });

    $("#btnSaveResult").click(function () {
        var i = 0;
        $('#tblStudent tbody tr').each(function () {
            var row = tblStudent.fnGetData(this._DT_RowIndex);
            var result = ($("#txt" + row[studentIdColName]).val());
            var resultComment = ($("#txtComment" + row[studentIdColName]).val());
            var studentguid = row['StudentGuid'];
            var TestScheduleGuid = testGuid;


            var obj = { TableRowGuid: null, StudentGuid: studentguid, TestScheduleGuid: testGuid, Result: result, Comment: resultComment };
            studentResults[i] = obj;
            i++;
        });
        PostJson("../api/TestResult/SaveStudentResultByBatch",
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
        "sAjaxSource": "../api/Student/GetStudentsForTestResultDataEntry",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            pageDetail.TestScheduleGuid = id;
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

function FillTest(id) {
    $.getJSON("../api/TestSchedule/GetTestsByClassRoomId/" + id, function (response) {
        handler.model.Tests(response);
        ko.applyBindings(handler.model, $("#cmbTest")[0]);

        $("#cmbTest").attr("disabled", false);
    });
}

$("#btnShowStudents").click(function () {
    testGuid = $("#cmbTest").val();
    if (testGuid != null && testGuid != "")
        FillStudents(testGuid);
});

function OnStudentSelected(id) {
    SelectedStudentId = id;

}