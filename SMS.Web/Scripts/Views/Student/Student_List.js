/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblStudent;
var idColName = "StudentGuid";
$(document).ready(function () {
    $('#tblStudent tbody tr').live('click', function () {
        $('#tblStudent tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblStudent.fnGetData(this._DT_RowIndex);
        OnStudentSelected(row[idColName]);
    });

    tblStudent = $('#tblStudent').dataTable({
        "sAjaxSource": "../api/Student/GetList",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);           
            oSettings.oLanguage.sSearch = "StudentFullName";
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
            { "mData": "StudentFullName" },
             {
                 "mData": "BirthDate",
                 "sType": "date",
                 "fnRender": function (obj) {
                     return FormatDate(obj)
                 }
             },
             { "mData": "GenderName" },
             {
                 "mData": "RegistrationDate",
                 "sType": "date",
                 "fnRender": function (obj) {
                     return FormatDate(obj)
                 }
             },
            DeleteLink(idColName, "StudentFullName", EntityName.Student, EntityCaption.Student, "tblStudent")],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
    
   
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.Student, tblStudent);
}

function OnStudentSelected(id){
    //alert("Selected id is: " + id);
}
