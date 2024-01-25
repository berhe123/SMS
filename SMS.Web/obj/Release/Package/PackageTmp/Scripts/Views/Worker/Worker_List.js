/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblWorker;
var idColName = "WorkerGuid";
$(document).ready(function () {
    $('#tblWorker tbody tr').live('click', function () {
        $('#tblWorker tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblWorker.fnGetData(this._DT_RowIndex);
        OnWorkerSelected(row[idColName]);
    });

    tblWorker = $('#tblWorker').dataTable({
        "sAjaxSource": "../api/Worker/GetList",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);           
            oSettings.oLanguage.sSearch = "WorkerFullName";
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
            { "mData": "WorkerFullName" },
            {
                "mData": "BirthDate",
                "sType": "date",
                "fnRender": function (obj) {
                    return FormatDate(obj)
                }
            },
            { "mData": "GenderName" },
            { "mData": "Address" },
            { "mData": "MobilePhoneNumber" },
            { "mData": "ReservedPhoneNumber" },
            { "mData": "Experience" },
            {
                "mData": "EngagementDate",
                "sType": "date",
                "fnRender": function (obj) {
        return FormatDate(obj)
    }
            },
            { "mData": "Salary" },
            DeleteLink(idColName, "WorkerFullName", EntityName.Worker, EntityCaption.Worker, "tblWorker")],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
    
   
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.Worker, tblWorker);
}

function OnWorkerSelected(id){
    //alert("Selected id is: " + id);
}
