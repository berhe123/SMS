/// <reference path="../../jquery-1.7.1-vsdoc.js" />
/// <reference path="../../FormHelper.js" />
/// <reference path="../../dateExt.js" />

var tblUser;
var tblLocation;
var tblUserRole;
var SelectedUserId;
var idColName = "TableRowGuid";
var userIdColName = "UserId";
var roleIdColName = "RoleId";
var roleRoleNameColName = "RoleName";
var userName;


var SelectedLocations = [];
var SelectedRoles = [];


$().ready(function () {
    $('#tblUser tbody tr').live('click', function () {
        $('#tblUser tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblUser.fnGetData(this._DT_RowIndex);
        userName = row["UserName"];
        OnUserSelected(row[userIdColName]);
    });

    $("#btnSave").click(function () {
        var i = 0;

        //$('#tblLocation tbody tr').each(function () {
        //    var row = tblLocation.fnGetData(this._DT_RowIndex);
        //    var checkboxvalue = (Boolean)($("#" + row[idColName] + ":checked").val());
        //    if (checkboxvalue) {
        //        SelectedLocations[i] = row[idColName];
        //        i++;
        //    }
        //});

        //var UserInLocations = { UserId: SelectedUserId, SelectedUserLocations: SelectedLocations };

        //SaveLocations(UserInLocations);



        $('#tblRole tbody tr').each(function () {
            var row = tblRole.fnGetData(this._DT_RowIndex);
            var checkboxvalue = (Boolean)($("#" + row[roleIdColName] + ":checked").val());
            if (checkboxvalue) {
                SelectedRoles[i] = row[roleIdColName];
                i++;
            }
        });

        var UserInRoles = { UserId: SelectedUserId, SelectedUserRoles: SelectedRoles };

        SaveRoles(UserInRoles);
    });

    GetUsers();

    //GetLocations();

    GetRoles();

});

//function GetUserLocations(id) {
//    $.getJSON("../api/UsersInLocation/GetUserLocations/" + id, function (response) {
//        var LocationGuids = response;
//        $('#tblLocation tbody tr').each(function () {
//            var row = tblLocation.fnGetData(this._DT_RowIndex);
//            for (var i = 0; LocationGuids[i] != null; i++) {
//                if (row[idColName] == LocationGuids[i]) {
//                    $("#" + row[idColName]).attr("checked", true);
//                }
//            }

//        });
//    });
//}

function GetUserRoles(id) {
    $.getJSON("../api/Role/GetUserRoles/" + id, function (response) {
        var RolesGuids = response;
        $('#tblRole tbody tr').each(function () {
            var row = tblRole.fnGetData(this._DT_RowIndex);
            for (var i = 0; RolesGuids[i] != null; i++) {
                if (row[roleIdColName] == RolesGuids[i]) {
                    $("#" + row[roleIdColName]).attr("checked", true);
                }
            }

        });
    });
}

function GetUsers() {
    tblUser = $('#tblUser').dataTable({
        "sAjaxSource": "../api/UserProfile/Get",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            oSettings.oLanguage.sSearch = "UserName";
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
            ColHidden(userIdColName),
            { "mData": "UserName" }, { "mData": "FullName" }],
        "aaSorting": [[1, "asc"]],
        "aoColumnDefs": [
            { "bSearchable": false, "bVisible": false, "aTargets": [0] }
        ],
        "width": "100%"
    });
}

//function GetLocations() {
//    tblLocation = $('#tblLocation').dataTable({
//        "sAjaxSource": "../api/Location/Get",
//        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
//            var pageDetail = GetPageDetail(oSettings);
//            oSettings.oLanguage.sSearch = "Name";
//            pageDetail.DisplayLength = 100;
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
//            CheckBoxColumn(idColName),
//            { "mData": "Name" }],
//        "aaSorting": [[1, "asc"]],
//        "bPaginate": false,
//        "bFilter": false,
//        "bSort": false,
//        "bInfo": false,
//        "width": "100%"
//    });

//}

function GetRoles() {
    tblRole = $('#tblRole').dataTable({
        "sAjaxSource": "../api/Role/Get/",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            oSettings.oLanguage.sSearch = "RoleName";
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
            CheckBoxColumn(roleIdColName),
            { "mData": "RoleName" }],
        "aaSorting": [[1, "asc"]],
        "bPaginate": false,
        "bFilter": false,
        "bSort": false,
        "bInfo": false,
        "width": "100%"
    });
}

function OnUserSelected(id) {
    SelectedUserId = id;
    //ClearAllUserLocations();
    ClearAllUserRoles();
    //GetUserLocations(id);
    GetUserRoles(id);
}

//function SaveLocations(UserInLocations) {
//    PostJson("../api/UsersInLocation/SaveUserLocations",
//           JSON.stringify(UserInLocations),
//           function (response) {
//               if (response.Success) {
//                   AlertMsg("Success", response.Message);
//               }
//               else AlertMsg("Error", response.Message, "alert-error");
//           }
//       );
//}

function SaveRoles(UserInRoles) {
    PostJson("../api/UserProfile/SaveUserRoles",
           JSON.stringify(UserInRoles),
           function (response) {
               if (response.Success) {
                   AlertMsg("Success", response.Message);
               }
               else AlertMsg("Error", response.Message, "alert-error");
           }
       );
}

//function ClearAllUserLocations() {
//    $('#tblLocation tbody tr').each(function () {
//        var row = tblLocation.fnGetData(this._DT_RowIndex);
//        $("#" + row[idColName]).attr("checked", false);
//    });
//}

function ClearAllUserRoles() {
    $('#tblRole tbody tr').each(function () {
        var row = tblRole.fnGetData(this._DT_RowIndex);
        $("#" + row[roleIdColName]).attr("checked", false);
    });
}