/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblExamSchedule;
var idColName = "ExamScheduleGuid";
$(document).ready(function () {
    $('#tblExamSchedule tbody tr').live('click', function () {
        $('#tblExamSchedule tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblExamSchedule.fnGetData(this._DT_RowIndex);
        OnExamScheduleSelected(row[idColName]);
    });


    tblExamSchedule = $('#tblExamSchedule').dataTable({
        "sAjaxSource": "../api/ExamSchedule/GetList",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);           
            oSettings.oLanguage.sSearch = "ExamSchedule";
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
            EditLink(idColName),
             { "mData": "ExamSchedule" },
             {
                 "mData": "StartTime",
                 "sType": "time",
                 "fnRender": function (obj) {
                     return FormatTime(obj)
                 }
             },
             {
                 "mData": "EndTime",
                 "sType": "time",
                 "fnRender": function (obj) {
                     return FormatTime(obj)
                 }
             },
             { "mData": "OutOf" },
             { "mData": "TeacherFullName" },
             { "mData": "ExamInformation" },
             {
                 "mData": "GivenDate",
                 "sType": "date",
                 "fnRender": function (obj) {
                     return OnlyFormatDate(obj)
                 }
             },
            DeleteLink(idColName, "ExamSchedule", EntityName.ExamSchedule, EntityCaption.ExamSchedule, "tblExamSchedule")],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
    
   
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.ExamSchedule, tblExamSchedule);
}

function OnExamScheduleSelected(id){
    //alert("Selected id is: " + id);
}
