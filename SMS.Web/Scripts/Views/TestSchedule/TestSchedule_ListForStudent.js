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
    $('#tblTestSchedule tbody tr').live('click', function () {
        $('#tblTestSchedule tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblTestSchedule.fnGetData(this._DT_RowIndex);
        //OnLessonClassSelected(row[idColName]);
    });
    $('#tblTestResult tbody tr').live('click', function () {
        $('#tblTestResult tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblTestResult.fnGetData(this._DT_RowIndex);
        //OnLessonClassSelected(row[idColName]);
    });

    FillLessonClass();
    FillTestSchedule();
    FillTestResult();
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
function FillTestSchedule() {
    tblTestSchedule = $('#tblTestSchedule').dataTable({
        "sAjaxSource": "../api/Student/GetStudentsForTestScheduleDataView",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            oSettings.oLanguage.sSearch = "TestSchdule";
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
            { "mData": "TestSchdule" },
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
function FillTestResult() {
    tblTestResult = $('#tblTestResult').dataTable({
        "sAjaxSource": "../api/Student/GetStudentsForTestResultDataView",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            oSettings.oLanguage.sSearch = "Test";
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
            { "mData": "Test" },
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