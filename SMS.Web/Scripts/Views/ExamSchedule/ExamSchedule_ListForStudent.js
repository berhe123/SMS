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
    $('#tblExamSchedule tbody tr').live('click', function () {
        $('#tblExamSchedule tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblExamSchedule.fnGetData(this._DT_RowIndex);
        //OnLessonClassSelected(row[idColName]);
    });
    $('#tblExamResult tbody tr').live('click', function () {
        $('#tblExamResult tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblExamResult.fnGetData(this._DT_RowIndex);
        //OnLessonClassSelected(row[idColName]);
    });

    FillLessonClass();
    FillExamSchedule();
    FillExamResult();
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
function FillExamSchedule() {
    tblExamSchedule = $('#tblExamSchedule').dataTable({
        "sAjaxSource": "../api/Student/GetStudentsForExamScheduleDataView",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            oSettings.oLanguage.sSearch = "examSchdule";
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
            { "mData": "examSchdule" },
            { "mData": "day" },
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
            },
        //{
        //    "mData": "GivenDate",
        //    "sType": "date",
        //    "fnRender": function (obj) {
        //        return FormatDate(obj)
        //    }
        //},
            { "mData": "OutOf" },
            { "mData": "ExamInformation" }, ],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
}
function FillExamResult() {
    tblExamResult = $('#tblExamResult').dataTable({
        "sAjaxSource": "../api/Student/GetStudentsForExamResultDataView",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            oSettings.oLanguage.sSearch = "Exam";
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
            { "mData": "Exam" },
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