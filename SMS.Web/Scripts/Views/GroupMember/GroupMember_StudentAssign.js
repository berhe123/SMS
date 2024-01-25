/// <reference path="../../jquery-1.7.1-vsdoc.js" />
/// <reference path="../../FormHelper.js" />
/// <reference path="../../dateExt.js" />

var handler = new DefaultFormHandler("../api/GroupMember/GetModel", "Add", "#Form", true, "GroupMember");
var studentName;
var groupName;
var SelectedUserId;
var studentIdColName = "StudentGuid";
var groupnameGuid = "GroupNameGuid";
var SelectStudents = [];
var StudentsInGroupMember;
var classroomGuid;
var subjectteacherclassroomGuid;
//var tblStudent;
var tblGroupName;
var obj;

$().ready(function () {
    handler.Init(function () {
        ko.applyBindings(handler.model);
        AdjustRequiredDataForSelect("#Form");        
    });

    var listItems = "";
    listItems += "<option value='null'>Choose...</option>";
    $("#cmbSubject").html(listItems);

    $('#tblStudent tbody tr').live('click', function () {
        $('#tblStudent tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblStudent.fnGetData(this._DT_RowIndex);
        studentName = row["StudentFullName"];
        OnUserSelected(row[studentIdColName]);
    });
    $('#tblGroupName tbody tr').live('click', function () {
        $('#tblGroupName tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblGroupName.fnGetData(this._DT_RowIndex);
        groupName = row["GroupName"];
        OnUserSelected(row[groupNameGuid]);
    });

    $("#cmbClassRoom").change(function () {
        if ($("#cmbClassRoom").val() != null && $("#cmbClassRoom").val() != "") {           
            $('#cmbSubject').trigger('change').val(null);
            classroomGuid = $("#cmbClassRoom").val();
            FillSubject($("#cmbClassRoom").val());
            FillStudents();
        }
    });    

    $("#cmbSubject").change(function () {
        if ($("#cmbSubject").val() != null && $("#cmbSubject").val() != "") {
            subjectteacherclassroomGuid = $("#cmbSubject").val();
            FillGroupNames();
        }
    });

    //$("#btnCreateGroup").click(function () {
    //    var i = 0;

    //    $("#tblStudent tbody tr ").each(function () {
    //        var row = tblStudent.fnGetData(this._DT_RowIndex);
    //        var checkboxvalue = (Boolean)($("#" + row[studentIdColName] + ":checked").val());
    //        if (checkboxvalue) {   
    //            SelectStudents[i] = row[studentIdColName];
    //            i++;
    //        }
    //    });

    //    var subjectteacherclassroomGuid = $("#cmbSubject").val();
    //    var groupname = $("#txtGroupName").val();

    //    var obj = { TableRowGuid: null, SubjectTeacherClassRoomGuid: subjectteacherclassroomGuid, studentsGuid: SelectStudents, GroupName: groupname };
    //    StudentsInGroupMember = obj;

    //    SaveGroupMember(obj);
    //});
});



function FillSubject(id) {
    $.getJSON("../api/GroupName/GetSubjectsByClassRoomId/" + id, function (response) {

        handler.model.Subjects(response);
        ko.applyBindings(handler.model, $("#cmbSubject")[0]);
        
        $("#cmbSubject").attr("disabled", false);

    });
}

function FillStudents() {    
    tblStudent = $('#tblStudent').dataTable({
        "sAjaxSource": "../api/Student/GetStudentsByClassRoomGuid/",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            pageDetail.ClassRoomGuid = classroomGuid;
            oSettings.oLanguage.sSearch = "StudentFullName";
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
            CheckBoxColumn(studentIdColName),
            { "mData": "StudentFullName" }],
        "aaSorting": [[1, "asc"]],
        "bPaginate": false,
        "bFilter": false,
        "bSort": false,
        "bInfo": false,
        "width": "100%"
    });
}

function FillGroupNames() {
    tblGroupName = $('#tblGroupName').dataTable({
        "sAjaxSource": "../api/GroupName/GetList/",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            pageDetail.SubjectTeacherClassRoomGuid = subjectteacherclassroomGuid;
            oSettings.oLanguage.sSearch = "GroupName";
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
            ColHidden(groupnameGuid),
            { "mData": "GroupName" }],
        "aaSorting": [[1, "asc"]],
        "aoColumnDefs": [
            { "bSearchable": false, "bVisible": false, "aTargets": [0] }
        ],
        "width": "100%"
    });
}

function OnUserSelected(id) {
    SelectedUserId = id;

}

//function SaveGroupMember(StudentsInGroupMember) {
//    PostJson("../api/GroupMember/CreateGroupMember",
//           JSON.stringify(StudentsInGroupMember),
//           function (response) {
//               if (response.Success) {
//                   AlertMsg("Success", response.Message);
//               }
//               else AlertMsg("Error", response.Message, "alert-error");
//           }
//       );
//}