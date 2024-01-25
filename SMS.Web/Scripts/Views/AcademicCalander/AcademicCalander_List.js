/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblAcademicCalander;
var idColName = "TableRowGuid";
$(document).ready(function () {
    $('#tblAcademicCalander tbody tr').live('click', function () {
        $('#tblAcademicCalander tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblAcademicCalander.fnGetData(this._DT_RowIndex);
        OnAcademicCalanderSelected(row[idColName]);
    });

    tblAcademicCalander = $('#tblAcademicCalander').dataTable({
        "sAjaxSource": "../api/AcademicCalander/Get",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);           
            oSettings.oLanguage.sSearch = "AcademicName";
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
            { "mData": "AcademicName" },
             {
                 "mData": "FromDate",
                 "sType": "date",
                 "fnRender": function (obj) {
                     return FormatDate(obj)
                 }
             },
             {
                 "mData": "ToDate",
                 "sType": "date",
                 "fnRender": function (obj) {
                     return FormatDate(obj)
                 }
             },
            DeleteLink(idColName, "AcademicName", EntityName.AcademicCalander, EntityCaption.AcademicCalander, "tblAcademicCalander")],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
    
   
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.AcademicCalander, tblAcademicCalander);
}

function OnAcademicCalanderSelected(id){
    //alert("Selected id is: " + id);
}
