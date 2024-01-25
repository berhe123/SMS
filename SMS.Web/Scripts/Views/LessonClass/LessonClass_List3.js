/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblLessonClass;
var idColName = "TableRowGuid";
$(document).ready(function () {
    $('#tblLessonClass tbody tr').live('click', function () {
        $('#tblLessonClass tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblLessonClass.fnGetData(this._DT_RowIndex);
        OnLessonClassSelected(row[idColName]);
    });

    tblLessonClass = $('#tblLessonClass').dataTable({
        "sAjaxSource": "../api/LessonClass/GetList",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);           
            oSettings.oLanguage.sSearch = "TeacherProgram";
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
            { "mData": "TeacherProgram" },
            {
                "mData": "TimeFrom",
                "sType": "time",
                "fnRender": function (obj)
                {
                    return FormatTime(obj)
                }
            },
            {
                "mData": "TimeTo",
                "sType": "time",
                "fnRender": function (obj) {
                    return FormatTime(obj)
                }
            },
            DeleteLink(idColName, "TeacherProgram", EntityName.LessonClass, EntityCaption.LessonClass, "tblLessonClass")],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
    
   
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.LessonClass, tblLessonClass);
}

function OnLessonClassSelected(id) {
    //alert("Selected id is: " + id);
}
