/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblPayment;
var idColName = "TableRowGuid";
var handler = new DefaultFormHandler("../api/Payment/GetModel", "Add", "#Form", true, "Payment");

$(document).ready(function () {
    $('#tblPayment tbody tr').live('click', function () {
        $('#tblPayment tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblPayment.fnGetData(this._DT_RowIndex);
        OnPaymentSelected(row[idColName]);        
    });

    $().ready(function () {
        handler.Init(function () {
            ko.applyBindings(handler.model);
            AdjustRequiredDataForSelect("#Form");
        });

        var listItems = "";
        listItems += "<option value='null'>Choose...</option>";

        $("#cmbClassRoom").change(function () {

            if ($('#cmbClassRoom').val() != null && $('#cmbClassRoom').val() != "") {

                $("#cmbStudent").trigger('change').val(null);

                FillStudent($("#cmbClassRoom").val());
            }
        });

    });

    tblPayment = $('#tblPayment').dataTable({
        "sAjaxSource": "../api/Payment/GetList",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);           
            oSettings.oLanguage.sSearch = "Payment";
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
            //EditLink(idColName),
            { "mData": "Payment" },                         
             {
                 "mData": "PaymentDate",
                 "sType": "date",
                 "fnRender": function (obj) {
                     return FormatDate(obj)
                 }
             },
             { "mData": "TinNumber" },
             { "mData": "FSNumber" },
            //DeleteLink(idColName, "Payment", EntityName.Payment, EntityCaption.Payment, "tblPayment")
            ],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });          
});

//function OnDelete(id, name) {
//    OnDeleteEntity(id, name, EventName.Payment, tblPayment);
//}

function OnPaymentSelected(id){
    //alert("Selected id is: " + id);
}

function FillStudent(id) {
    $.getJSON("../api/StudentClassRoom/GetStudentsByClassRoomId/" + id, function (response) {
        var listItems = "";
        listItems += "<option value='null'>Choose...</option>";
        for (var i = 0; i < response.length; i++) {
            listItems += "<option value=" + response[i].Value + ">" + response[i].Text + "</option>";
        }
        $("#cmbStudent").html(listItems);
        $("#cmbStudent").attr("disabled", false);
        //$("#cmbStudent").val('null');
    });
}
