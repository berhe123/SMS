/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblTeacher;
var idColName = "TeacherGuid";
$(document).ready(function () {
    $('#tblTeacher tbody tr').live('click', function () {
        $('#tblTeacher tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblTeacher.fnGetData(this._DT_RowIndex);
        OnTeacherSelected(row[idColName]);
    });

    tblTeacher = $('#tblTeacher').dataTable({
        "sAjaxSource": "../api/Teacher/GetList",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);           
            oSettings.oLanguage.sSearch = "TeacherFullName";
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
            { "mData": "TeacherFullName" },
            {
                "mData": "BirthDate",
                "sType": "date",
                "fnRender": function (obj) {
                    return FormatDate(obj)
                }
            },
            { "mData": "GenderName" },
            { "mData": "Address" },
            { "mData": "MobilePhoneNumber" },
            { "mData": "ReservedPhoneNumber" },
            { "mData": "Experience" },
            {
                "mData": "EngagementDate",
                "sType": "date",
                "fnRender": function (obj) {
        return FormatDate(obj)
    }
            },
            { "mData": "Salary" },
            DeleteLink(idColName, "TeacherFullName", EntityName.Teacher, EntityCaption.Teacher, "tblTeacher")],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
    
   
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.Teacher, tblTeacher);
}

function OnTeacherSelected(id){
    //alert("Selected id is: " + id);
}
