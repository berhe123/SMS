/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblWorkflow;
var idColName = "TableRowGuid";
$(document).ready(function () {
    $('#tblWorkflow tbody tr').live('click', function () {
        $('#tblWorkflow tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblWorkflow.fnGetData(this._DT_RowIndex);
        OnWorkflowSelected(row[idColName]);
    });

    tblWorkflow = $('#tblWorkflow').dataTable({
        "sAjaxSource": "../api/Workflow/Get",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);           
            oSettings.oLanguage.sSearch = "Name";
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
            { "mData": "Name" }, { "mData": "Description" },
            DeleteLink(idColName, "Name", EntityName.Workflow, EntityCaption.Workflow, "tblWorkflow")],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
    
   
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.Workflow, tblWorkflow);
}

function OnWorkflowSelected(id){
    //alert("Selected id is: " + id);
}
