/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblExamResult;
var idColName = "TableRowGuid";
$(document).ready(function () {

    //FillExam(id)

    $("#cmbExamSchedule").change(function () {
        if ($("#cmbExamSchedule").val() != null && $("#cmbExamSchedule").val() != "") {

            $('#tblExamResult tbody tr').live('click', function () {
                $('#tblExamResult tbody tr').removeClass('row_selected');
                $(this).addClass('row_selected');
                var row = tblExamResult.fnGetData(this._DT_RowIndex);
                OnExamResultSelected(row[idColName]);
            });

            tblExamResult = $('#tblExamResult').dataTable({
                "sAjaxSource": "../api/ExamResult/GetList",
                fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
                    var pageDetail = GetPageDetail(oSettings);
                    oSettings.oLanguage.sSearch = "ExamResult";
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
                    { "mData": "ExamResult" },
                     { "mData": "Result" },
                     { "mData": "OutOf" },
                     { "mData": "Comment" },
                    DeleteLink(idColName, "ExamResult", EntityName.ExamResult, EntityCaption.ExamResult, "tblExamResult")],
                "aaSorting": [[1, "asc"]],
                "width": "100%"
            });

        }
    });          
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.ExamResult, tblExamResult);
}

function OnExamResultSelected(id){
    //alert("Selected id is: " + id);
}

function FillExam(id) {
    $.getJSON("../api/ExamSchedule/GetExamsByTeacherUserId/" + id, function (response) {
        var listItems = "";
        listItems += "<option value='null'>Choose...</option>";
        for (var i = 0; i < response.length; i++) {
            listItems += "<option value=" + response[i].Value + ">" + response[i].Text + "</option>";
        }
        $("#cmbExamSchedule").html(listItems);
        //$("#cmbSubjectClassRoom").val('null');
    });
}
