/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblSalary;
var idColName = "TableRowGuid";
$(document).ready(function () {
    $('#tblSalary tbody tr').live('click', function () {
        $('#tblSalary tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblSalary.fnGetData(this._DT_RowIndex);
        OnSalarySelected(row[idColName]);
    });

    tblSalary = $('#tblSalary').dataTable({
        "sAjaxSource": "../api/Salary/GetList",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);           
            oSettings.oLanguage.sSearch = "Salary";
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
            { "mData": "Salary" },
             {
                 "mData": "RecieveDate",
                 "sType": "date",
                 "fnRender": function (obj) {
                     return FormatDate(obj)
                 }
             },
            DeleteLink(idColName, "Salary", EntityName.Salary, EntityCaption.Salary, "tblSalary")],
        "aaSorting": [[1, "asc"]],
        "width": "100%"
    });
    
   
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.Salary, tblSalary);
}

function OnSalarySelected(id){
    //alert("Selected id is: " + id);
}
