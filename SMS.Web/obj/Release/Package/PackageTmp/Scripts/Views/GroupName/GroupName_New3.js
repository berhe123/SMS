/// <reference path="../../jquery-1.7.1-vsdoc.js" />
/// <reference path="../../FormHelper.js" />
/// <reference path="../../dateExt.js" />

var handler = new DefaultFormHandler("../api/GroupName/GetModel", "Add", "#Form", true, "GroupName");
//var studentName;
//var SelectedUserId;
//var studentIdColName = "StudentGuid";
//var SelectStudents = [];
//var StudentsInGroupMember;
//var classroomGuid;
//var obj;

$().ready(function () {
    handler.Init(function () {
        ko.applyBindings(handler.model);
        AdjustRequiredDataForSelect("#Form");        
    });

    var listItems = "";
    listItems += "<option value='null'>Choose...</option>";
    $("#cmbSubject").html(listItems);

    //$('#tblStudent tbody tr').live('click', function () {
    //    $('#tblStudent tbody tr').removeClass('row_selected');
    //    $(this).addClass('row_selected');
    //    var row = tblStudent.fnGetData(this._DT_RowIndex);
    //    studentName = row["FullName"];
    //    OnUserSelected(row[studentIdColName]);
    //});


    $("#cmbClassRoom").change(function () {
        if ($("#cmbClassRoom").val() != null && $("#cmbClassRoom").val() != "") {
            
            $('#cmbSubject').trigger('change').val(null);

            FillSubject($("#cmbClassRoom").val());           
            //FillStudents();
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
    $.getJSON("../api/SubjectTeacherClassRoom/GetSubjectsByClassRoomId/" + id, function (response) {

        handler.model.Subjects(response);
        ko.applyBindings(handler.model, $("#cmbSubject")[0]);
        
        $("#cmbSubject").attr("disabled", false);

    });
}

//function FillStudents() {    
//    tblStudent = $('#tblStudent').dataTable({
//        "sAjaxSource": "../api/Student/GetStudentsByClassRoomGuid/",
//        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
//            var pageDetail = GetPageDetail(oSettings);
//            pageDetail.ClassRoomGuid = classroomGuid;
//            oSettings.oLanguage.sSearch = "FullName";
//            $.ajax({
//                dataType: 'json',
//                type: "POST",
//                contentType: "application/json; charset=utf-8",
//                url: sUrl,
//                data: JSON.stringify(pageDetail),
//                success: fnCallback
//            });
//        },
//        "aoColumns": [
//            CheckBoxColumn(studentIdColName),
//            { "mData": "FullName" }],
//        "aaSorting": [[1, "asc"]],
//        "bPaginate": false,
//        "bFilter": false,
//        "bSort": false,
//        "bInfo": false,
//        "width": "100%"
//    });
//}

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