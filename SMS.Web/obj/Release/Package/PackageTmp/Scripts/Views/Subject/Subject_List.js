/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblSubject;
var idColName = "TableRowGuid";
$(document).ready(function () {
    $('#tblSubject tbody tr').live('click', function () {
        $('#tblSubject tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblSubject.fnGetData(this._DT_RowIndex);
        OnSubjectSelected(row[idColName]);
    });

    tblSubject = $('#tblSubject').dataTable({
        "sAjaxSource": "../api/Subject/Get",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);           
            oSettings.oLanguage.sSearch = "SubjectName";
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
            { "mData": "SubjectName" },
            DeleteLink(idColName, "SubjectName", EntityName.Subject, EntityCaption.Subject, "tblSubject")],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
    
   
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.Subject, tblSubject);
}

function OnSubjectSelected(id){
    //alert("Selected id is: " + id);
}
