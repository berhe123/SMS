/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblStudentClassRoom;
var idColName = "TableRowGuid";
$(document).ready(function () {
    $('#tblStudentClassRoom tbody tr').live('click', function () {
        $('#tblStudentClassRoom tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblStudentClassRoom.fnGetData(this._DT_RowIndex);
        OnStudentClassRoomSelected(row[idColName]);
    });

    tblStudentClassRoom = $('#tblStudentClassRoom').dataTable({
        "sAjaxSource": "../api/StudentClassRoom/GetList",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);           
            oSettings.oLanguage.sSearch = "StudentClassRoom";
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
            { "mData": "StudentClassRoom" },
            DeleteLink(idColName, "StudentClassRoom", EntityName.StudentClassRoom, EntityCaption.StudentClassRoom, "tblStudentClassRoom")],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
    
   
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.StudentClassRoom, tblStudentClassRoom);
}

function OnStudentClassRoomSelected(id){
    //alert("Selected id is: " + id);
}
