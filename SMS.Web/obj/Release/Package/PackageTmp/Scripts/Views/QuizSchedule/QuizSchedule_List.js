/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblQuizSchedule;
var idColName = "QuizScheduleGuid";
$(document).ready(function () {

    $('#tblQuizSchedule tbody tr').live('click', function () {
        $('#tblQuizSchedule tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblQuizSchedule.fnGetData(this._DT_RowIndex);
        OnQuizScheduleSelected(row[idColName]);
    });

    tblQuizSchedule = $('#tblQuizSchedule').dataTable({
        "sAjaxSource": "../api/QuizSchedule/GetList",
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
            EditLink(idColName),
            { "mData": "Quiz" },
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
             { "mData": "OutOf" },
              {
                  "mData": "GivenDate",
                  "sType": "date",
                  "fnRender": function (obj) {
                      return FormatDate(obj)
                  }
              },
             { "mData": "QuizInformation" },
            DeleteLink(idColName, "Quiz", EntityName.QuizSchedule, EntityCaption.QuizSchedule, "tblQuizSchedule")],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
    
   
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.QuizSchedule, tblQuizSchedule);
}

function OnQuizScheduleSelected(id){
    //alert("Selected id is: " + id);
}
