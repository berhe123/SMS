/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblPaymentCalanderToClassRoom;
var idColName = "PaymentCalanderToClassRoomGuid";
$(document).ready(function () {
    $('#tblPaymentCalanderToClassRoom tbody tr').live('click', function () {
        $('#tblPaymentCalanderToClassRoom tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblPaymentCalanderToClassRoom.fnGetData(this._DT_RowIndex);
        OnPaymentCalanderSelected(row[idColName]);
    });

    tblPaymentCalanderToClassRoom = $('#tblPaymentCalanderToClassRoom').dataTable({
        "sAjaxSource": "../api/PaymentCalandersToClassRoom/GetList",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);           
            oSettings.oLanguage.sSearch = "MonthName" + "ClassRoomName";
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
            { "mData": "ClassRoomName" },
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
            DeleteLink(idColName, "ClassRoomName", EntityName.PaymentCalanderToClassRoom, EntityCaption.PaymentCalanderToClassRoom, "tblPaymentCalanderToClassRoom")],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
    
   
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.PaymentCalanderToClassRoom, tblPaymentCalanderToClassRoom);
}

function OnPaymentCalanderSelected(id){
    //alert("Selected id is: " + id);
}
