/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblStudentComment;
var idColName = "TableRowGuid";
$(document).ready(function () {
    $('#tblStudentComment tbody tr').live('click', function () {
        $('#tblStudentComment tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblStudentComment.fnGetData(this._DT_RowIndex);
        OnStudentCommentSelected(row[idColName]);
    });

    tblStudentComment = $('#tblStudentComment').dataTable({
        "sAjaxSource": "../api/StudentComment/GetModel",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);           
            oSettings.oLanguage.sSearch = "StudentGuid";
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
            { "mData": "StudentGuid" },
            { "mData": "Comment" },

            DeleteLink(idColName, "StudentGuid", EntityName.StudentComment, EntityCaption.StudentComment, "tblStudentComment")],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
    
   
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.StudentComment, tblStudentComment);
}

function OnStudentCommentSelected(id){
    //alert("Selected id is: " + id);
}
