/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />
/// <reference path="../knockout-2.2.1.js" />

var tblExerciseBookResult;
var idColName = "TableRowGuid";
$(document).ready(function () {

    //FillSubjectClassRoom(id)    

    $("#cmbClassRoomSubject").change(function () {
        if ($("#cmbClassRoomSubject").val() != null && $("#cmbClassRoomSubject").val() != "") {

            $('#tblExerciseBookResult tbody tr').live('click', function () {
                $('#tblExerciseBookResult tbody tr').removeClass('row_selected');
                $(this).addClass('row_selected');
                var row = tblExerciseBookResult.fnGetData(this._DT_RowIndex);
                OnExerciseBookResultSelected(row[idColName]);
            });

            tblExerciseBookResult = $('#tblExerciseBookResult').dataTable({
                "sAjaxSource": "../api/ExerciseBookResult/GetList",
                fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
                    var pageDetail = GetPageDetail(oSettings);
                    oSettings.oLanguage.sSearch = "ExerciseBook" + "TeacherName";
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
                    { "mData": "ExerciseBook" },
                     { "mData": "Result" },
                     { "mData": "OutOf" },
                     { "mData": "Comment" },
                    DeleteLink(idColName, "ExerciseBook", EntityName.ExerciseBookResult, EntityCaption.ExerciseBookResult, "tblExerciseBookResult")],
                "aaSorting": [[1, "asc"]],
                "width": "100%"
            });
        }
    });           
});

function OnDelete(id, name) {
    OnDeleteEntity(id, name, EventName.ExerciseBookResult, tblExerciseBookResult);
}

function OnExerciseBookResultSelected(id){
    //alert("Selected id is: " + id);
}

function FillSubjectClassRoom(id) {
    $.getJSON("../api/LessonClass/GetClassRoomSubjectsByTeacherUserId/" + id, function (response) {

        var listItems = "";
        listItems += "<option value='null'>Choose...</option>";
        for (var i = 0; i < response.length; i++) {
            listItems += "<option value=" + response[i].Value + ">" + response[i].Text + "</option>";
        }
        $("#cmbClassRoomSubject").html(listItems);
        //$("#cmbSubjectClassRoom").val('null');
    });
}
