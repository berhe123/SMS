/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblGroupWorkSchedule;
var idColName = "GroupWorkScheduleGuid";
$(document).ready(function () {

    FillSubjectClassRoom();

    $("#cmbClassRoomSubject").change(function () {
        if ($("#cmbClassRoomSubject").val() != null && $("#cmbClassRoomSubject").val() != "") {

            $('#tblGroupWorkSchedule tbody tr').live('click', function () {
                $('#tblGroupWorkSchedule tbody tr').removeClass('row_selected');
                $(this).addClass('row_selected');
                var row = tblGroupWorkSchedule.fnGetData(this._DT_RowIndex);
                OnGroupWorkScheduleSelected(row[idColName]);
            });

            tblGroupWorkSchedule = $('#tblGroupWorkSchedule').dataTable({
                "sAjaxSource": "../api/GroupWorkSchedule/GetList",
                fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
                    var pageDetail = GetPageDetail(oSettings);
                    pageDetail.SubjectTeacherClassRoomGuid = $("#cmbClassRoomSubject").val();
                    oSettings.oLanguage.sSearch = "GroupWork";
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
                    { "mData": "GroupWork" },
                    { "mData": "Title" },
                     {
                         "mData": "GroupWorkGivenDate",
                         "sType": "date",
                         "fnRender": function (obj) {
                             return FormatDate(obj)
                         }
                     },
                               {
                                   "mData": "GroupWorkSubmissionDate",
                                   "sType": "date",
                                   "fnRender": function (obj) {
                                       return FormatDate(obj)
                                   }
                               },
                     { "mData": "OutOf" },
                      { "mData": "Information" },
                    DeleteLink(idColName, "GroupWork", EntityName.GroupWorkSchedule, EntityCaption.GroupWorkSchedule, "tblGroupWorkSchedule")],
                "aaSorting": [[1, "asc"]],
                "width": "100%"
            });       
        }
    });          
});

function FillSubjectClassRoom() {
    $.getJSON("../api/SubjectTeacherClassRoom/GetClassRoomSubjectsByTeacherUserId", function (response) {
        var listItems = "";
        listItems += "<option value='null'>Choose...</option>";
        for (var i = 0; i < response.length; i++) {
            listItems += "<option value=" + response[i].Value + ">" + response[i].Text + "</option>";
        }
        $("#cmbClassRoomSubject").html(listItems);
        //$("#cmbSubjectClassRoom").val('null');
    });
}

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.GroupWorkSchedule, tblGroupWorkSchedule);
}

function OnGroupWorkScheduleSelected(id){
    //alert("Selected id is: " + id);
}
