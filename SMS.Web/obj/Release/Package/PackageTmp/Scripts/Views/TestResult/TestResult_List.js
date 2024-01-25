/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblTestResult;
var idColName = "TableRowGuid";
$(document).ready(function () {

    //FillClassRoom(id)

    $("#cmbTest").change(function () {
        if ($("#cmbTest").val() != null && $("#cmbTest").val() != "") {

            $('#tblTestResult tbody tr').live('click', function () {
                $('#tblTestResult tbody tr').removeClass('row_selected');
                $(this).addClass('row_selected');
                var row = tblTestResult.fnGetData(this._DT_RowIndex);
                OnTestResultSelected(row[idColName]);
            });

            tblTestResult = $('#tblTestResult').dataTable({
                "sAjaxSource": "../api/TestResult/GetList",
                fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
                    var pageDetail = GetPageDetail(oSettings);
                    oSettings.oLanguage.sSearch = "TestResult" + "TeacherName";
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
                    { "mData": "TestResult" },
                     { "mData": "Result" },
                     { "mData": "OutOf" },
                     { "mData": "Comment" },
                    DeleteLink(idColName, "TestResult", EntityName.TestResult, EntityCaption.TestResult, "tblTestResult")],
                "aaSorting": [[1, "asc"]],
                "width": "100%"
            });
        }
    });

});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.TestResult, tblTestResult);
}

function OnTestResultSelected(id){
    //alert("Selected id is: " + id);
}

function FillTest(id) {
    $.getJSON("../api/TestSchedule/GetTestsByTeacherUserId/" + id, function (response) {
        var listItems = "";
        listItems += "<option value='null'>Choose...</option>";
        for (var i = 0; i < response.length; i++) {
            listItems += "<option value=" + response[i].Value + ">" + response[i].Text + "</option>";
        }
        $("#cmbTest").html(listItems);
        //$("#cmbSubjectClassRoom").val('null');
    });
}
