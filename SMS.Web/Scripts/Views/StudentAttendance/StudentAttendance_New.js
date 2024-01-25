/// <reference path="../../jquery-1.7.1-vsdoc.js" />
/// <reference path="../../FormHelper.js" />
/// <reference path="../../dateExt.js" />

var handler = new DefaultFormHandler("../api/StudentAttendance/GetModel", "Add", "#Form", true, "StudentAttendance");
var tblStudent;
var studentName;
var SelectedStudentId;
var studentIdColName = "TableRowGuid";

$().ready(function () {

    handler.Init(function () {
        ko.applyBindings(handler.model);
        AdjustRequiredDataForSelect("#Form");
    });

    //FillClassRoom();

    var listItems = "";
    listItems += "<option value='null'>Choose...</option>";

    $("#cmbClassRoom").change(function () {
        if ($("#cmbClassRoom").val() != null && $("#cmbClassRoom").val() != "") {

            $('#cmbLessonClass').trigger('change').val(null);
            //$('#cmbStudent').trigger('change').val(null);

            FillLessonClass($("#cmbClassRoom").val());           
        }
    });

    FillStudents();

});

function FillClassRoom(id) {
    $.getJSON("../api/SubjectTeacherClassRoom/GetClassRoomsByTeacherUserId/" + id, function (response) {
        var listItems = "";
        listItems += "<option value='null'>Choose...</option>";
        for (var i = 0; i < response.length; i++) {
            listItems += "<option value=" + response[i].Value + ">" + response[i].Text + "</option>";
        }
        $("#cmbClassRoom").html(listItems);
        //$("#cmbSubjectClassRoom").val('null');
    });
}

function FillLessonClass(id, id) {
    $.getJSON("../api/LessonClass/GetLessonClassesByTeacherUserIdandClassRoomId/" + id, id, function (response) {

        handler.model.LessonClasses(response);
        ko.applyBindings(handler.model, $("#cmbLessonClass")[1]);

        $("#cmbLessonClass").html(listItems);
        $("#cmbLessonClass").attr("disabled", false);
        //$("#cmbSubjectClassRoom").val('null');
    });
}

function FillStudents(id) {

    $('#tblStudent tbody tr').live('click', function () {
        $('#tblStudent tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblStudent.fnGetData(this._DT_RowIndex);
        studentName = row["StudentName"];
        OnStudentSelected(row[studentIdColName]);
    });

    tblStudent = $('#tblStudent').dataTable({
        "sAjaxSource": "../api/StudentClassRoom/GetStudentsByClassRoomId",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
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
            CheckBoxColumn(idColName),
            { "mData": "StudentName" }],
        "aaSorting": [[1, "asc"]],
        "bPaginate": false,
        "bFilter": false,
        "bSort": false,
        "bInfo": false,
        "width": "100%"
    });
}

function OnStudentSelected(id) {
    SelectedStudentId = id;

}