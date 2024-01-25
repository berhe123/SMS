/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblLessonClass;
var idColName = "TableRowGuid";
$(document).ready(function () {
    $('#tblLessonClass tbody tr').live('click', function () {
        $('#tblLessonClass tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblLessonClass.fnGetData(this._DT_RowIndex);
        //OnLessonClassSelected(row[idColName]);
    });
    $('#tblQuizSchedule tbody tr').live('click', function () {
        $('#tblQuizSchedule tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblQuizSchedule.fnGetData(this._DT_RowIndex);
        //OnLessonClassSelected(row[idColName]);
    });
    $('#tblQuizResult tbody tr').live('click', function () {
        $('#tblQuizResult tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblQuizResult.fnGetData(this._DT_RowIndex);
        //OnLessonClassSelected(row[idColName]);
    });

    FillLessonClass();
    FillQuizSchedule();
    FillQuizResult();
    FillStudentInformation();
    
});


function FillLessonClass() {
    tblLessonClass = $('#tblLessonClass').dataTable({
        "sAjaxSource": "../api/Student/GetStudentsForLessonClassDataView",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            oSettings.oLanguage.sSearch = "lessonclass";
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
            { "mData": "lessonclass" },
            {
                "mData": "TimeFrom",
                "sType": "date",
                "fnRender": function (obj) {
                    return FormatDate(obj)
                }
            },
            {
                "mData": "TimeTo",
                "sType": "date",
                "fnRender": function (obj) {
                    return FormatDate(obj)
                }
            }, ],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
}
function FillQuizSchedule() {
    tblAssignmentSchedule = $('#tblQuizSchedule').dataTable({
        "sAjaxSource": "../api/Student/GetStudentsForQuizScheduleDataView",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            oSettings.oLanguage.sSearch = "QuizSchdule";
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
            { "mData": "QuizSchdule" },
            { "mData": "OutOf" },
            {
                "mData": "StartTime",
                "sType": "date",
                "fnRender": function (obj) {
                    return FormatDate(obj)
                }
            },
            {
                "mData": "EndTime",
                "sType": "date",
                "fnRender": function (obj) {
                    return FormatDate(obj)
                }
            },
        {
            "mData": "GivenDate",
            "sType": "date",
            "fnRender": function (obj) {
                return FormatDate(obj)
            }
        }, ],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
}
function FillQuizResult() {
    tblQuizResult = $('#tblQuizResult').dataTable({
        "sAjaxSource": "../api/Student/GetStudentsForQuizResultDataView",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            oSettings.oLanguage.sSearch = "Quiz";
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
            { "mData": "Quiz" },
            { "mData": "OutOf" },
            { "mData": "Result" },
            { "mData": "Comment" }, ],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
}
function FillStudentInformation() {
    tblStudentInformation = $('#tblStudentInformation').dataTable({
        "sAjaxSource": "../api/Student/GetStudentsForInfromationDataView",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            oSettings.oLanguage.sSearch = "Information";
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
            { "mData": "Information" },
            { "mData": "GenderName" },
            {
                "mData": "BirthDate",
                "sType": "date",
                "fnRender": function (obj) {
                    return FormatDate(obj)
                }
            },
            {
                "mData": "RegistrationDate",
                "sType": "date",
                "fnRender": function (obj) {
                    return FormatDate(obj)
                }
            }, ],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
}