/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblGroupName;
var idColName = "GroupNameGuid";
$(document).ready(function () {

    FillClassRoom();

    $("#cmbClassRoom").change(function () {
        if ($("#cmbClassRoom").val() != null && $("#cmbClassRoom").val() != "") {

            $('#tblGroupName tbody tr').live('click', function () {
                $('#tblGroupName tbody tr').removeClass('row_selected');
                $(this).addClass('row_selected');
                var row = tblGroupName.fnGetData(this._DT_RowIndex);
                OnGroupMemberSelected(row[idColName]);
            });

            tblGroupName = $('#tblGroupName').dataTable({
                "sAjaxSource": "../api/GroupName/GetList/",
                fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
                    var pageDetail = GetPageDetail(oSettings);
                    pageDetail.SubjectTeacherClassRoomGuid = $("#cmbClassRoom").val();
                    oSettings.oLanguage.sSearch = "GroupName";
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
                    { "mData": "GroupName" },
                    DeleteLink(idColName, "GroupName", EntityName.GroupName, EntityCaption.GroupName, "tblGroupName")],
                "aaSorting": [[1, "asc"]],
                "width": "100%"
            });
        }
    });

});

    function OnDelete(id, name) {
        OnDeleteEntity(id, name, EventName.GroupMember, tblGroupName);
    }

    function OnGroupMemberSelected(id) {
        //alert("Selected id is: " + id);
    }

    function FillClassRoom() {
        $.getJSON("../api/SubjectTeacherClassRoom/GetClassRoomSubjectByTeacherGuid/", function (response) {

            var listItems = "";
            listItems += "<option value='null'>Choose...</option>";
            for (var i = 0; i < response.length; i++) {
                listItems += "<option value=" + response[i].Value + ">" + response[i].Text + "</option>";
            }

            $("#cmbClassRoom").html(listItems);

        });
    }