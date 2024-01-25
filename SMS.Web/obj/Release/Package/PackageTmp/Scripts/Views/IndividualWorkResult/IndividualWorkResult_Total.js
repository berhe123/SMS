/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

//var studentIdColName = "StudentGuid";

//var studentResults = [];
var studentName;
var tblIndividualWorkresults;
var SubjectClassRoomTeacherGuid;
var classroomSubjectGuid;
var IndividualWorkScheduleGuid = "IndividualWorkGuid";
var IndividualWorksGuid = [];

$(document).ready(function () {   
    $('#tblIndividualWorkresult tbody tr').live('click', function () {
        $('#tblIndividualWorkresult tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblIndividualWorkresult.fnGetData(this._DT_RowIndex);
        studentName = row["StudentName"];
        //OnStudentSelected(row[studentIdColName]);
    });

        //var listItems = "";
        //listItems += "<option value='null'>Choose...</option>";
        //$("#cmbSubjectClassRoomTeacher").html(listItems);

    FillClassRoomSubject();

    $("#cmbClassRoomSubject").change(function () {
        if ($("#cmbClassRoomSubject").val() != null && $("#cmbClassRoomSubject").val() != "") {
            classroomSubjectGuid = $("#cmbClassRoomSubject").val();

            FillIndividualWorks(classroomSubjectGuid);
        }
    });

    $("#btnShowStudents").click(function () {
        if (classroomSubjectGuid != null && classroomSubjectGuid != "")
            FillStudents(classroomSubjectGuid);
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

    $("#btnSave").click(function () {

    });

});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.IndividualWorkResult, tblIndividualWorkResult);
}

function OnIndividualWorkResultSelected(id) {
    //alert("Selected id is: " + id);
}

function FillClassRoomSubject() {
    $.getJSON("../api/SubjectTeacherClassRoom/GetClassRoomSubjectByTeacherGuid/", function (response) {
        var listItems = "";
        listItems += "<option value='null'>Choose...</option>";
        for (var i = 0; i < response.length; i++) {
            listItems += "<option value=" + response[i].Value + ">" + response[i].Text + "</option>";
        }
        $("#cmbClassRoomSubject").html(listItems);
        //listItems += "<option value='null'>Choose...</option>";
    });
}

function FillStudents(id) {
    tblIndividualWorkresult = $('#tblIndividualWorkresult').dataTable({
        "sAjaxSource": "../api/IndividualWorkResult/GetAllIndividualWorksForStudentDataView",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            pageDetail.SubjectTeacherClassRoomGuid = id;
            oSettings.oLanguage.sSearch = "StudentName";
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
            { "mData": "StudentName" },
            { "mData": "IndividualWorkName" },
            { "mData": "Result" },
            { "mData": "OutOf" },],
        "aaSorting": [[1, "asc"]],
        "bPaginate": false,
        "bFilter": false,
        "bSort": false,
        "bInfo": false,
        "width": "100%"
    });
}

function FillIndividualWorks(id) {
    tblIndividualWork = $('#tblIndividualWork').dataTable({
        "sAjaxSource": "../api/IndividualWorkSchedule/GetAllIndividualWorksForDataView/",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            pageDetail.SubjectTeacherClassRoomGuid = id;
            oSettings.oLanguage.sSearch = "IndividualWorkName";
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
            CheckBoxColumn(IndividualWorkScheduleGuid),
            { "mData": "IndividualWorkName" },
            { "mData": "OutOf" }, ],
        "aaSorting": [[1, "asc"]],
        "bPaginate": false,
        "bFilter": false,
        "bSort": false,
        "bInfo": false,
        "width": "100%"
    });
}
