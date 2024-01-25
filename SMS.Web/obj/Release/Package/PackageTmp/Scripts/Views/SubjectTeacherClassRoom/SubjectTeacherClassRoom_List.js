/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblSubjectTeacherClassRoom;
var idColName = "TableRowGuid";
$(document).ready(function () {
    $('#tblSubjectTeacherClassRoom tbody tr').live('click', function () {
        $('#tblSubjectTeacherClassRoom tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblSubjectTeacherClassRoom.fnGetData(this._DT_RowIndex);
        OnSubjectTeacherClassRoomSelected(row[idColName]);
    });

    tblSubjectTeacherClassRoom = $('#tblSubjectTeacherClassRoom').dataTable({
        "sAjaxSource": "../api/SubjectTeacherClassRoom/GetList",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);           
            oSettings.oLanguage.sSearch = "SubjectTeacherClassRoom";
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
            { "mData": "SubjectTeacherClassRoom" },
             { "mData": "HomeClassRoom" },
            DeleteLink(idColName, "SubjectTeacherClassRoom", EntityName.SubjectTeacherClassRoom, EntityCaption.SubjectTeacherClassRoom, "tblSubjectTeacherClassRoom")],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
       
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.SubjectTeacherClassRoom, tblSubjectTeacherClassRoom);
}

function OnSubjectTeacherClassRoomSelected(id){
    //alert("Selected id is: " + id);
}
