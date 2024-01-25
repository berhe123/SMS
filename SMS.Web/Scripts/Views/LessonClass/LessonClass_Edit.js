/// <reference path="../../jquery-1.7.1-vsdoc.js" />
/// <reference path="../../FormHelper.js" />
/// <reference path="../../dateExt.js" />

var handler = new DefaultFormHandler("../api/LessonClass/GetModel/" + $("#txtId").val(), "Update", "#Form", false, "LessonClass");

$().ready(function () {
    handler.Init(function () {
        ko.applyBindings(handler.model);        
        AdjustRequiredDataForSelect("#Form");
       

        $("#cmbTeacherGuid").change(function () {
            //$('#cmbSubjectClassRoom').trigger('change').val(null);
            if ($("#cmbTeacherGuid").val() != null && $("#cmbTeacherGuid").val() != "") {
                $('#cmbSubjectClassRoom').trigger('change').val(null);
                FillClassRoomSubject($("#cmbTeacherGuid").val());
            }
        });

    });
});

function FillClassRoomSubject(id) {
    $.getJSON("../api/SubjectTeacherClassRoom/GetClassRoomSubjectByTeacherId/" + id, function (response) {    
        var listItems = "";
        listItems += "<option value=" + response.Value + ">" + response.Text + "</option>";
        for (var i = 0; i < response.length; i++) {
            listItems += "<option value=" + response[i].Value + ">" + response[i].Text + "</option>";
        }
        $("#cmbSubjectClassRoom").html(listItems);
        $("#cmbSubjectClassRoom").attr("disabled", false);
    });
}

