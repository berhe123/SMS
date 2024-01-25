/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblClassRoom;
var idColName = "TableRowGuid";
$(document).ready(function () {
    $('#tblClassRoom tbody tr').live('click', function () {
        $('#tblClassRoom tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblClassRoom.fnGetData(this._DT_RowIndex);
        OnClassRoomSelected(row[idColName]);
    });

    tblClassRoom = $('#tblClassRoom').dataTable({
        "sAjaxSource": "../api/ClassRoom/Get",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);           
            oSettings.oLanguage.sSearch = "ClassRoomName";
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
            DeleteLink(idColName, "ClassRoomName", EntityName.ClassRoom, EntityCaption.ClassRoom, "tblClassRoom")],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
    
   
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.ClassRoom, tblClassRoom);
}

function OnClassRoomSelected(id){
    //alert("Selected id is: " + id);
}
