/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblLocation;
var idColName = "TableRowGuid";
$(document).ready(function () {
    $('#tblLocation tbody tr').live('click', function () {
        $('#tblLocation tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblLocation.fnGetData(this._DT_RowIndex);
        OnLocationSelected(row[idColName]);
    });

    tblLocation = $('#tblLocation').dataTable({
        "sAjaxSource": "../api/Location/Get",
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
            { "mData": "Name" }, DeleteLink(idColName, "Name", EntityName.Location, EntityCaption.Location, "tblLocation")],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
    
   
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.Location, tblLocation);
}

function OnLocationSelected(id){
    //alert("Selected id is: " + id);
}
