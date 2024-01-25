/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblIndividualWorkSchedule;
var idColName = "IndividualWorkScheduleGuid";

$(document).ready(function () {
    $('#tblIndividualWorkSchedule tbody tr').live('click', function () {
        $('#tblIndividualWorkSchedule tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblIndividualWorkSchedule.fnGetData(this._DT_RowIndex);
        OnIndividualWorkScheduleSelected(row[idColName]);
    });

    tblIndividualWorkSchedule = $('#tblIndividualWorkSchedule').dataTable({
        "sAjaxSource": "../api/IndividualWorkSchedule/GetList",
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
            EditLink(idColName),
            { "mData": "IndividualWork" },
            { "mData": "IndividualWorkGivenDate" },
            { "mData": "IndividualWorkSubmissionDate" },
            { "mData": "OutOf" },
            { "mData": "IndividualWorkInformation" },
            DeleteLink(idColName, "IndividualWork", EntityName.IndividualWorkSchedule, EntityCaption.IndividualWorkSchedule, "tblIndividualWorkSchedule")],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
    
   
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.IndividualWorkSchedule, tblIndividualWorkSchedule);
}

function OnIndividualWorkScheduleSelected(id){
    //alert("Selected id is: " + id);
}