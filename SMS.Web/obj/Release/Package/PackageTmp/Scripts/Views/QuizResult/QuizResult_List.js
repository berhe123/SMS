/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblQuizResult;
var idColName = "TableRowGuid";
$(document).ready(function () {
    
    //FillQuiz(id)

    $("#cmbQuiz").change(function () {
        if ($("#cmbQuiz").val() != null && $("#cmbQuiz").val() != "") {

            $('#tblQuizResult tbody tr').live('click', function () {
                $('#tblQuizResult tbody tr').removeClass('row_selected');
                $(this).addClass('row_selected');
                var row = tblQuizResult.fnGetData(this._DT_RowIndex);
                OnQuizResultSelected(row[idColName]);
            });

            tblQuizResult = $('#tblQuizResult').dataTable({
                "sAjaxSource": "../api/QuizResult/GetList",
                fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
                    var pageDetail = GetPageDetail(oSettings);
                    oSettings.oLanguage.sSearch = "QuizResult";
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
                    { "mData": "QuizResult" },
                     { "mData": "Result" },
                     { "mData": "OutOf" },
                     { "mData": "Comment" },
                    DeleteLink(idColName, "QuizResult", EntityName.QuizResult, EntityCaption.QuizResult, "tblQuizResult")],
                "aaSorting": [[1, "asc"]],
                "width": "100%"
            });
        }
    });
})

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.QuizResult, tblQuizResult);
}

function OnQuizResultSelected(id){
    //alert("Selected id is: " + id);
}

function FillQuiz(id) {
    $.getJSON("../api/QuizSchedule/GetQuizsByTeacherUserId/" + id, function (response) {
        var listItems = "";
        listItems += "<option value='null'>Choose...</option>";
        for (var i = 0; i < response.length; i++) {
            listItems += "<option value=" + response[i].Value + ">" + response[i].Text + "</option>";
        }
        $("#cmbQuiz").html(listItems);
        //$("#cmbSubjectClassRoom").val('null');
    });
}