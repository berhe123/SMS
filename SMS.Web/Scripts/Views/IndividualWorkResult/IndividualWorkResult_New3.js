/// <reference path="../../jquery-1.7.1-vsdoc.js" />
/// <reference path="../../FormHelper.js" />
/// <reference path="../../dateExt.js" />

var handler = new DefaultFormHandler("../api/IndividualWorkResult/GetModel", "Add", "#Form", true, "IndividualWorkResult");
var studentIdColName = "StudentGuid";

var StudentResults = [];
var tblStudents;
var individualworkscheduleGuid;


$(document).ready(function () {
    handler.Init(function () {
        ko.applyBindings(handler.model);
        AdjustRequiredDataForSelect("#Form");
    });

    var listItems = "";
    listItems += "<option value='null'>Choose...</option>";
    $("#cmbIndividualWorkSchedule").html(listItems);

    $('#tblStudent tbody tr').live('click', function () {
        $('#tblStudent tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblStudent.fnGetData(this._DT_RowIndex);
        studentName = row["StudentName"];
        //OnStudentSelected(row[studentIdColName]);
    });    

    $("#cmbClassRoom").change(function () {
        if ($("#cmbClassRoom").val() != null && $("#cmbClassRoom").val() != "") {                        
            FillIndividualWorkSchedule($("#cmbClassRoom").val());
        }                  
    });

    $("#btnShowStudents").click(function () {
        individualworkscheduleGuid = $("#cmbIndividualWorkSchedule").val();
        if (individualworkscheduleGuid != null && individualworkscheduleGuid != "")
            FillStudents();
    });

    $("#btnSaveResult").click(function () {

        var i = 0;
        $('#tblStudent tbody tr').each(function () {
            var row = tblStudent.fnGetData(this._DT_RowIndex);
            var result = ($("#txt" + row[studentIdColName]).val());
            var resultComment = ($("#txtComment" + row[studentIdColName]).val());
            var studentguid = row['StudentGuid'];
            var IndividualWorkScheduleGuid = individualworkscheduleGuid;

           
            var obj = { TableRowGuid: null, StudentGuid: studentguid, IndividualWorkScheduleGuid: individualworkscheduleGuid, Result: result, Comment: resultComment };
            StudentResults[i] = obj;
            i++;
        });
        PostJson("../api/IndividualWorkResult/SaveStudentResultByBatch",
            JSON.stringify(StudentResults),
            function (response) {
                if (response.Success) {
                    AlertMsg("Success", response.Message);
                }
                else AlertMsg("Error", response.Message, "alert-error");
            }
        );

    })
    
});
    
function FillStudents() {
    tblStudent = $('#tblStudent').dataTable({
        "sAjaxSource": "../api/Student/GetStudentsForIndividualWorkResultDataEntry",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            pageDetail.IndividualWorkScheduleGuid = individualworkscheduleGuid;
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
            { "mData": "StudentFullName" },
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

function FillIndividualWorkSchedule(id) {
    $.getJSON("../api/IndividualWorkSchedule/GetIndividualWorkSchedulesBySubjectId/" + id, function (response) {

        handler.model.IndividualWorkSchedules(response);
        ko.applyBindings(handler.model, $("#cmbIndividualWorkSchedule")[0]);

        $("#cmbIndividualWorkSchedule").attr("disabled", false);
    });
}

function OnStudentSelected(id) {
    SelectedStudentId = id;

}