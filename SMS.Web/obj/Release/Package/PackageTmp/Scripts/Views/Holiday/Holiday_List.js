/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblHoliday;
var idColName = "TableRowGuid";
$(document).ready(function () {
    $('#tblHoliday tbody tr').live('click', function () {
        $('#tblHoliday tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblHoliday.fnGetData(this._DT_RowIndex);
        OnHolidaySelected(row[idColName]);
    });

    tblHoliday = $('#tblHoliday').dataTable({
        "sAjaxSource": "../api/Holiday/Get",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);           
            oSettings.oLanguage.sSearch = "HolidayName";
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
            { "mData": "HolidayName" },
             {
                 "mData": "HolidayDate",
                 "sType": "date",
                 "fnRender": function (obj) {
                     return FormatDate(obj)
                 }
             },           
            DeleteLink(idColName, "HolidayName", EntityName.Holiday, EntityCaption.Holiday, "tblHoliday")],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
    
   
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.Holiday, tblHoliday);
}

function OnHolidaySelected(id){
    //alert("Selected id is: " + id);
}
