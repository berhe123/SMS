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
    $('#tblIndividualWorkSchedule tbody tr').live('click', function () {
        $('#tblIndividualWorkSchedule tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblIndividualWorkSchedule.fnGetData(this._DT_RowIndex);
        //OnLessonClassSelected(row[idColName]);
    });
    $('#tblIndividualWorkResult tbody tr').live('click', function () {
        $('#tblIndividualWorkResult tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblIndividualWorkResult.fnGetData(this._DT_RowIndex);
        //OnLessonClassSelected(row[idColName]);
    });

    FillLessonClass();
    FillIndividualWorkSchedule();
    FillIndividualWorkResult();
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
function FillIndividualWorkSchedule() {
    tblIndividualWorkSchedule = $('#tblIndividualWorkSchedule').dataTable({
        "sAjaxSource": "../api/Student/GetStudentsForIndividualWorkScheduleDataView",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            oSettings.oLanguage.sSearch = "IndividualWorkSchedule";
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
            { "mData": "IndividualWorkSchedule" },
            { "mData": "OutOf" },
            {
                "mData": "IndividualWorkGivenDate",
                "sType": "date",
                "fnRender": function (obj) {
                    return FormatDate(obj)
                }
            },
            {
                "mData": "IndividualWorkSubmissionDate",
                "sType": "date",
                "fnRender": function (obj) {
                    return FormatDate(obj)
                }
            },],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
}
function FillIndividualWorkResult() {
    tblIndividualWorkResult = $('#tblIndividualWorkResult').dataTable({
        "sAjaxSource": "../api/Student/GetStudentsForIndividualWorkResultDataView",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            oSettings.oLanguage.sSearch = "IndividualWork";
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
            { "mData": "IndividualWork" },
            { "mData": "OutOf" },
            { "mData": "Result" },
            { "mData": "Comment" },],
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
            },],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
}