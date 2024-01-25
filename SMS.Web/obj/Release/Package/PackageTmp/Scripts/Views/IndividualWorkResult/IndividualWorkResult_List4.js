/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var studentIdColName = "StudentGuid";

var studentResults = [];
var tblStudents;
var IndividualWorkGuid;

$(document).ready(function () {   

    $('#tblStudent tbody tr').live('click', function () {
        $('#tblStudent tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblStudent.fnGetData(this._DT_RowIndex);
        studentName = row["FullName"];
        //OnStudentSelected(row[studentIdColName]);
    });

    var listItems = "";
    listItems += "<option value='null'>Choose...</option>";
    $("#cmbIndividualWork").html(listItems);

    FillIndividualWork();

    $("#cmbIndividualWork").change(function () {
        if ($("#cmbIndividualWork").val() != null && $("#cmbIndividualWork").val() != "") {            
            IndividualWorkGuid = $("#cmbIndividualWork").val();
        }
    });

    $("#btnShowStudents").click(function () {
        if (IndividualWorkGuid != null && IndividualWorkGuid != "")
            FillStudents();
    });

    //$("#btnSaveResult").click(function () {

    //    var i = 0;
    //    $('#tblStudent tbody tr').each(function () {
    //        var row = tblStudent.fnGetData(this._DT_RowIndex);
    //        var result = ($("#txt" + row[studentIdColName]).val());
    //        var resultComment = ($("#txtComment" + row[studentIdColName]).val());
    //        var studentguid = row['StudentGuid'];
    //        var IndividualWorkGuid = IndividualWorkGuid;


    //        var obj = { TableRowGuid: null, StudentGuid: studentguid, IndividualWorkGuid: IndividualWorkGuid, Result: result, Comment: resultComment };
    //        studentResults[i] = obj;
    //        i++;
    //    });
    //    PostJson("../api/IndividualWorkResult/SaveStudentResultByBatch",
    //        JSON.stringify(studentResults),
    //        function (response) {
    //            if (response.Success) {
    //                AlertMsg("Success", response.Message);
    //            }
    //            else AlertMsg("Error", response.Message, "alert-error");
    //        }
    //    );

    //})

});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.IndividualWorkResult, tblIndividualWorkResult);
}

function OnIndividualWorkResultSelected(id){
    //alert("Selected id is: " + id);
}

function FillIndividualWork() {
    $.getJSON("../api/IndividualWorkSchedule/GetIndividualWorksByTeacherGuid/", function (response) {
        var listItems = "";
        listItems += "<option value='null'>Choose...</option>";
        for(var i=0; i < response.length; i++)
        {
            listItems += "<option value=" + response[i].Value + ">" + response[i].Text + "</option>";
        }
        $("#cmbIndividualWork").html(listItems);
    });
}

function FillStudents() {
    tblStudent = $('#tblStudent').dataTable({
        "sAjaxSource": "../api/Student/GetStudentsForIndividualWorkResultDataEntry",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            pageDetail.IndividualWorkScheduleGuid = IndividualWorkGuid;
            oSettings.oLanguage.sSearch = "FullName";
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
            ColHidden(studentIdColName),
            { "mData": "FullName" },
             EntryTextBox(studentIdColName),
             EntryTextBoxComment(studentIdColName), ],
        "aaSorting": [[1, "asc"]],
        //"bPaginate": false,
        //"bFilter": false,
        //"bSort": false,
        //"bInfo": false,
        "width": "100%"
    });
}
