/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblGroupWorkResult;
var idColName = "TableRowGuid";
$(document).ready(function () {    

    //FillClassRoomSubject(id)

    $("#cmbClassRoomSubject").change(function () {
        if ($("#cmbClassRoomSubject").val() != null && $("#cmbClassRoomSubject").val() != "") {

        $('#tblGroupWorkResult tbody tr').live('click', function () {
        $('#tblGroupWorkResult tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblGroupWorkResult.fnGetData(this._DT_RowIndex);
        OnGroupWorkResultSelected(row[idColName]);
        });

            tblGroupWorkResult = $('#tblGroupWorkResult').dataTable({
        "sAjaxSource": "../api/GroupWorkResult/GetList",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);           
            oSettings.oLanguage.sSearch = "GroupMember";
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
            { "mData": "GroupMember" },
            { "mData": "SubjectClassRoom" },
             { "mData": "Result" },
             { "mData": "OutOf" },
             { "mData": "Comment" },
            DeleteLink(idColName, "GroupMember", EntityName.GroupWorkResult, EntityCaption.GroupWorkResult, "tblGroupWorkResult")],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
   }
 });          
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.GroupWorkResult, tblGroupWorkResult);
}

function OnGroupWorkResultSelected(id){
    //alert("Selected id is: " + id);
}

function FillClassRoomSubject(id) {
    $.getJSON("../api/LessonClass/GetClassRoomSubjectsByTeacherUserId/" + id, function (response) {
        var listItems = "";
        listItems += "<option value='null'>Choose...</option>";
        for (var i = 0; i < response.length; i++) {
            listItems += "<option value=" + response[i].Value + ">" + response[i].Text + "</option>";
        }
        $("#cmbClassRoomSubject").html(listItems);
        //$("#cmbSubjectClassRoom").val('null');
    });
}