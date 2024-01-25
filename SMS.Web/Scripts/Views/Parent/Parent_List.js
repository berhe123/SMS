/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblParent;
var idColName = "TableRowGuid";
$(document).ready(function () {
    $('#tblParent tbody tr').live('click', function () {
        $('#tblParent tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblParent.fnGetData(this._DT_RowIndex);
        OnParentSelected(row[idColName]);
    });

    tblParent = $('#tblParent').dataTable({
        "sAjaxSource": "../api/Parent/Get",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);           
            oSettings.oLanguage.sSearch = "FullName";
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
            { "mData": "FullName" },
             { "mData": "Address" },
             { "mData": "MobilePhoneNumber" },
             { "mData": "HomePhoneNumber" },
            DeleteLink(idColName, "FullName", EntityName.Parent, EntityCaption.Parent, "tblParent")],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
    
   
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.Parent, tblParent);
}

function OnParentSelected(id){
    //alert("Selected id is: " + id);
}
