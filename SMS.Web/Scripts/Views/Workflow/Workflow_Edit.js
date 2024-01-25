/// <reference path="../../jquery-1.7.1-vsdoc.js" />
/// <reference path="../../FormHelper.js" />
/// <reference path="../../dateExt.js" />

var handler = new DefaultFormHandler("../api/Workflow/GetModel/" + $("#txtId").val(), "Update", "#Form", false, "Workflow");
var tblWorkflowTeam;
var idColName = "TableRowGuid";
var newModel, editModel;

$().ready(function () {

    handler.Init(function () {
        ko.applyBindings(handler.model, $("#Form")[0]);
        AdjustRequiredDataForSelect("#Form");
    });

    LoadWorkflowTeams();

    $('#tblWorkflowTeam tbody tr').live('click', function () {
        $('#tblWorkflowTeam tbody tr').removeClass('row_selected');
        $(this).addClass('row_selected');
        var row = tblWorkflowTeam.fnGetData(this._DT_RowIndex);
        OnWorkflowTeamSelected(row[idColName]);
    });

    SetModels();

    $("#btnAdd").click(function () {
        newModel.WorkflowGuid($("#txtId").val());
        PostJson("../api/WorkflowTeam/Add",
            ko.mapping.toJSON(newModel),
            function (response) {
                if (response.Success) {
                    AlertMsg("Success", response.Message);
                    LoadWorkflowTeams();
                    SetModels();
                }
                else AlertMsg("Error", response.Message, "alert-error");
            }
        );
    });

    $("#btnUpdate").click(function () {
        PostJson("../api/WorkflowTeam/Update",
            ko.mapping.toJSON(editModel),
            function (response) {
                if (response.Success) {
                    AlertMsg("Success", response.Message);
                    LoadWorkflowTeams();
                    SetModels();
                }
                else AlertMsg("Error", response.Message, "alert-error");
            }
        );
    });
});

function OnWorkflowTeamSelected(id) {
    $.getJSON("../api/WorkflowTeam/GetModel/" + id, function (response) {
        ko.mapping.fromJS(response, editModel);
        AdjustRequiredDataForSelect();
    });
}

function LoadWorkflowTeams() {
    tblWorkflowTeam = $('#tblWorkflowTeam').dataTable({
        "sAjaxSource": "../api/WorkflowTeam/GetListByParent",
        fnServerData: function (sUrl, aoData, fnCallback, oSettings) {
            var pageDetail = GetPageDetail(oSettings);
            oSettings.oLanguage.sSearch = "FullName";
            pageDetail.ParentId = $("#txtId").val();
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
            EditModalLink(idColName, "editModal"),
                { "mData": "FullName" }, { "mData": "IsTeamLeader" },
            DeleteLink(idColName, "FullName", EntityName.WorkflowTeam, EntityCaption.WorkflowTeam, "tblWorkflowTeam")],
        "aaSorting": [[1, "asc"]],
        "bDestroy": true,
        "bPaginate": false,
        "bFilter": false,
        "bSort": false,
        "bInfo": false,
        "width": "100%"
    });
}

function SetModels() {
    $.getJSON("../api/WorkflowTeam/GetModel", function (response) {
        newModel = ko.mapping.fromJS(response);
        ko.applyBindings(newModel, $("#WorkflowTeamNew")[0]);
        AdjustRequiredDataForSelect("#WorkflowTeamNew");

        editModel = ko.mapping.fromJS(response);
        ko.applyBindings(editModel, $("#WorkflowTeamEdit")[0]);
        AdjustRequiredDataForSelect("#WorkflowTeamEdit");
    });
}