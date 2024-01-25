/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblStudentAttendance;
var idColName = "TableRowGuid";
$(document).ready(function () {

    //FillClassRoomSubject(id)

    $("#cmbClassRoomSubject").change(function () {
        if ($("#cmbClassRoomSubject").val() != null && $("#cmbClassRoomSubject").val() != "") {

            $('#tblStudentAttendance tbody tr').live('click', function () {
                $('#tblStudentAttendance tbody tr').removeClass('row_selected');
                $(this).addClass('row_selected');
                var row = tblStudentAttendance.fnGetData(this._DT_RowIndex);
                OnStudentAttendanceSelected(row[idColName]);
            });

            tblStudentAttendance = $('#tblStudentAttendance').dataTable({
                "sAjaxSource": "../api/StudentAttendance/GetList",
                fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
                    var pageDetail = GetPageDetail(oSettings);
                    oSettings.oLanguage.sSearch = "StudentAttendance";
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
                    { "mData": "StudentAttendance" },
                    {
                        "mData": "Date",
                        "sType": "date",
                        "fnRender": function (obj) {
                            return FormatDate(obj)
                        }
                    },
                    { "mData": "Comment" },
                    DeleteLink(idColName, "StudentAttendance", EntityName.StudentAttendance, EntityCaption.StudentAttendance, "tblStudentAttendance")],
                "aaSorting": [[1, "asc"]],
                "width": "100%"
            });            
        }
    });          
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.StudentAttendance, tblStudentAttendance);
}

function OnStudentAttendanceSelected(id){
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

