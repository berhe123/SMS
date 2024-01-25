/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblPaymentCalander;
var idColName = "PaymentCalanderGuid";
$(document).ready(function () {
    $('#tblPaymentCalander tbody tr').live('click', function () {
        $('#tblPaymentCalander tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblPaymentCalander.fnGetData(this._DT_RowIndex);
        OnPaymentCalanderSelected(row[idColName]);
    });

    tblPaymentCalander = $('#tblPaymentCalander').dataTable({
        "sAjaxSource": "../api/PaymentCalander/GetList",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);           
            oSettings.oLanguage.sSearch = "MonthName";
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
            { "mData": "MonthName" },            
             {
                 "mData": "RegularPaymentDateFrom",
                 "sType": "date",
                 "fnRender": function (obj) {
                     return FormatDate(obj)
                 }
             },
              {
                  "mData": "RegularPaymentDateTo",
                  "sType": "date",
                  "fnRender": function (obj) {
                      return FormatDate(obj)
                  }
              },             
            DeleteLink(idColName, "MonthName", EntityName.PaymentCalander, EntityCaption.PaymentCalander, "tblPaymentCalander")],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
    
   
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.PaymentCalander, tblPaymentCalander);
}

function OnPaymentCalanderSelected(id){
    //alert("Selected id is: " + id);
}
