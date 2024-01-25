/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblPeriod;
var idColName = "TableRowGuid";
$(document).ready(function () {
    $('#tblPeriod tbody tr').live('click', function () {
        $('#tblPeriod tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblPeriod.fnGetData(this._DT_RowIndex);
        OnPeriodSelected(row[idColName]);
    });

    tblPeriod = $('#tblPeriod').dataTable({
        "sAjaxSource": "../api/Period/GetList",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);           
            oSettings.oLanguage.sSearch = "periodView";
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
            { "mData": "periodView" },
            {
                "mData": "TimeFrom",
                "sType": "time",
                "fnRender": function (obj) {
                    return FormatTime(obj)
                }
            },
            {
                "mData": "TimeTo",
                "sType": "time",
                "fnRender": function (obj) {
                    return FormatTime(obj)
                }
            },
            DeleteLink(idColName, "periodView", EntityName.Period, EntityCaption.Period, "tblPeriod")],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
    
   
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.Period, tblPeriod);
}

function OnPeriodSelected(id){
    //alert("Selected id is: " + id);
}
