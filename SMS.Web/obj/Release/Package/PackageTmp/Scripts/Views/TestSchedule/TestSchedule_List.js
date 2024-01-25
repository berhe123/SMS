/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblTestSchedule;
var idColName = "TestScheduleGuid";

$(document).ready(function () {
    $('#tblTestSchedule tbody tr').live('click', function () {
        $('#tblTestSchedule tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblTestSchedule.fnGetData(this._DT_RowIndex);
        OnTestScheduleSelected(row[idColName]);
    });

    tblTestSchedule = $('#tblTestSchedule').dataTable({
        "sAjaxSource": "../api/TestSchedule/GetList/",
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
            EditLink(idColName),
            { "mData": "Test" },
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
             { "mData": "TestInformation" },
            DeleteLink(idColName, "Test", EntityName.TestSchedule, EntityCaption.TestSchedule, "tblTestSchedule")],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
    
   
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.TestSchedule, tblTestSchedule);
}

function OnTestScheduleSelected(id){
    //alert("Selected id is: " + id);
}
