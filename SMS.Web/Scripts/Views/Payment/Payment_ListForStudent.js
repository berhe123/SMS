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
    $('#tblAssignmentSchedule tbody tr').live('click', function () {
        $('#tblAssignmentSchedule tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblAssignmentSchedule.fnGetData(this._DT_RowIndex);
        //OnLessonClassSelected(row[idColName]);
    });
    $('#tblAssignmentResult tbody tr').live('click', function () {
        $('#tblAssignmentResult tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblAssignmentResult.fnGetData(this._DT_RowIndex);
        //OnLessonClassSelected(row[idColName]);
    });
    $('#tblPayment tbody tr').live('click', function () {
        $('#tblPayment tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblPayment.fnGetData(this._DT_RowIndex);
        //OnLessonClassSelected(row[idColName]);
    });

    FillLessonClass();
    FillAssignmentSchedule();
    FillAssignmentResult();
    FillPayment();
    
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
function FillAssignmentSchedule() {
    tblAssignmentSchedule = $('#tblAssignmentSchedule').dataTable({
        "sAjaxSource": "../api/Student/GetStudentsForAssignmentScheduleDataView",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            oSettings.oLanguage.sSearch = "assignmentSchedule";
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
            { "mData": "assignmentSchedule" },
            { "mData": "OutOf" },
            {
                "mData": "AssignmentGivenDate",
                "sType": "date",
                "fnRender": function (obj) {
                    return FormatDate(obj)
                }
            },
            {
                "mData": "AssignmentSubmissionDate",
                "sType": "date",
                "fnRender": function (obj) {
                    return FormatDate(obj)
                }
            }, ],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
}
function FillAssignmentResult() {
    tblAssignmentResult = $('#tblAssignmentResult').dataTable({
        "sAjaxSource": "../api/Student/GetStudentsForAssignmentResultDataView",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            oSettings.oLanguage.sSearch = "assignment";
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
            { "mData": "assignment" },
            { "mData": "OutOf" },
            { "mData": "Result" },
            { "mData": "Comment" }, ],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
}
function FillPayment() {
    tblPayment = $('#tblPayment').dataTable({
        "sAjaxSource": "../api/Student/GetStudentsForPaymentDataView",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            oSettings.oLanguage.sSearch = "payment";
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
            { "mData": "payment" },
            { "mData": "Amount" },
            {
                "mData": "PaymentDate",
                "sType": "date",
                "fnRender": function (obj) {
                    return FormatDate(obj)
                }
            },],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
}