/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblFeeSetting;
var idColName = "FeeSettingGuid";
var classroomGuid;
$(document).ready(function () {
    $('#tblFeeSetting tbody tr').live('click', function () {
        $('#tblFeeSetting tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblFeeSetting.fnGetData(this._DT_RowIndex);
        OnFeeSettingSelected(row[idColName]);
    });

    //var listItems = "";
    //listItems += "<option value='null'>Choose...</option>";
    //$("#cmbClassRoom").html(listItems);

    FillClassRooms();

    $("#cmbClassRoom").change(function () {
        if ($("#cmbClassRoom").val() != null && $("#cmbClassRoom").val() != "") {
            classroomGuid = $("#cmbClassRoom").val();
            FillFeeSettings();
        }
    });
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.FeeSetting, tblFeeSetting);
}

function OnFeeSettingSelected(id){
    //alert("Selected id is: " + id);
}

function FillClassRooms() {
    $.getJSON("../api/ClassRoom/GetComboItems/", function (response) {

        var listItems = "";
        listItems += "<option value='null'>Choose...</option>";
        for (var i = 0; i < response.length; i++) {
            listItems += "<option value=" + response[i].Value + ">" + response[i].Text + "</option>";
        }

        $("#cmbClassRoom").html(listItems);

    });
}

function FillFeeSettings() {
    tblFeeSetting = $('#tblFeeSetting').dataTable({
        "sAjaxSource": "../api/FeeSetting/GetList",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            pageDetail.ClassRoomGuid = classroomGuid;
            oSettings.oLanguage.sSearch = "ClassRoomName" + "MonthName";
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
             { "mData": "Amount" },
            DeleteLink(idColName, "ClassRoomName", EntityName.FeeSetting, EntityCaption.FeeSetting, "tblFeeSetting")],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
}