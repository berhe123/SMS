/// <rEdiference path="jquery-1.7.1-vsdoc.js" />
/// <reference path="knockout-2.2.1.js" />


function OnDeleteEntity(id, name, entitytType, entitytCaption, table) {
    if (confirm("Are you sure you want to delete " + entitytCaption + " registration of \n\n" + name + " ?")) {
        $.getJSON("../api/" + entitytType + "/Delete/" + id, function (response) {
            if (response.Success) {
                table.fnDraw();
                AlertMsg("Success", response.Message);
            }
            else AlertMsg("Error", response.Message, "alert-error");
        }, "json");
    }
}

function OnDeleteEntityRole(id1, id2, name, entitytType, table) {
    if (confirm("Are you sure you want to delete " + entitytType + " registration of \n\n" + name + " ?")) {
        $.getJSON("../api/" + entitytType + "/RemoveUserInRole/", { selectedUserid: id1, selectedRoleid: id2 }, function (response) {
            if (response.Success) {
                table.fnDraw();
                AlertMsg("Success", response.Message);
            }
            else AlertMsg("Error", response.Message, "alert-error");
        }, "json");
    }
}

var GuidEmpty = "{00000000-0000-0000-0000-000000000000}";

var StoreTransactionTypeValues = {
    PurchaseReceiving: 1,
    SuppliesReceiving: 2,
    SMSReceiving: 3,
    SalesReturnReceiving: 4,
    SalesIssuance: 5,
    SuppliesIssuance: 6,
    RawMaterialIssuance: 7,
    TransferIssuance: 8,
    TransferReceiving: 9,
    Damage: 10,
    Loss: 11,
    StoreIssuance: 12,
    InventoryAdjustment: 12
}

var StoreTransactionTypeGuids = {
    PurchaseReceiving: "f9e57d61-1b89-4323-ac87-4f0e291b90f7",
    SuppliesReceiving: "7f4c804b-2510-44ad-9a66-44cd7f3d6370",
    SMSReceiving: "e8d332cf-7254-4c04-8a92-0aec3d834ea7",
    SalesReturnReceiving: "9b585f05-1453-49aa-b93e-437406449357",
    SalesIssuance: "dc5d2f7d-d96c-420b-ac37-1746319d6a99",
    SuppliesIssuance: "4bed2c8b-7d03-445a-b753-ee0be709f956",
    RawMaterialIssuance: "7c098d24-66e9-49b2-9f62-2efcc519cd62",
    TransferIssuance: "b4db6d55-a8c7-4fa3-b1c6-b9945da42c66",
    TransferReceiving: "9eb07ceb-6790-4443-aab6-dd26b3e93430",
    Damage: "5c9cd2c3-bee6-43ed-84ae-46ab1df532a9",
    Loss: "4bc7044f-69cd-43f7-b32c-453fa6f64ac0",
    StoreIssuance: "75d3de28-1650-4cce-a5da-620a402aaeea",
    InventoryAdjustment: "1e2fc273-d202-4b84-9394-6060038bf031"
}

function GetStoreTransactionFilter(transactionType, TransactionId) {
    return {
        Id: null,
        PurchaseOrderGuid: null,
        SMSOrderGuid: null,
        SuppliesRequisitionGuid: null,
        RawMaterialRequestGuid: null,
        SalesInvoiceGuid: null,
        SalesReturnGuid: null,
        InterStoreTransferGuid: null,
        ItemGuid: null,
        StoreGuid: null,
        TransactionType: transactionType,
        TransactionId: TransactionId
    };
}

var EntityName = {
    ClassRoom: "ClassRoom",
    PaymentCalander: "PaymentCalander",
    PaymentCalanderToClassRoom: "PaymentCalanderToClassRoom",
    Period: "Period",
    Subject: "Subject",
    FeeSetting:"FeeSetting",
    Student: "Student",
    Teacher: "Teacher",
    Worker: "Worker",
    Parent: "Parent",
    LessonClass: "LessonClass",
    SubjectTeacherClassRoom: "SubjectTeacherClassRoom",
    AcademicCalander: "AcademicCalander",
    Holiday: "Holiday",
    //Salary: "Salary",
    ExamSchedule: "ExamSchedule",
    IndividualWorkSchedule: "IndividualWorkSchedule",
    IndividualWorkResult: "IndividualWorkResult",
    QuizSchedule: "QuizSchedule",
    QuizResult: "QuizResult",
    TestSchedule: "TestSchedule",
    TestResult: "TestResult",
    GroupWorkSchedule: "GroupWorkSchedule",
    GroupName: "GroupName",
    GroupWorkResult: "GroupWorkResult",
    StudentAttendance: "StudentAttendance",
    ExerciseBookResult: "ExerciseBookResult",
    ExamResult: "ExamResult",
    StudentClassRoom: "StudentClassRoom"
};

var EntityCaption = {
    ClassRoom: "Class Room",
    PaymentCalander: "Payment Calander",
    PaymentCalanderToClassRoom: "Payment Calander For Specific ClassRoom",
    Period: "Period",
    Subject: "Subject",
    FeeSetting: "Fee Setting",
    Student: "Student",
    Teacher: "Teacher",
    Worker: "Worker",
    Parent: "Parent",
    LessonClass: "Lesson Class",
    SubjectTeacherClassRoom: "Subject Teacher ClassRoom",
    AcademicCalander: "Academic Calander",
    Holiday: "Holiday",
    //Salary: "Salary",
    ExamSchedule: "Exam Schedule",
    IndividualWorkSchedule: "Assignment Schedule",
    IndividualWorkResult: "Assignment Result",
    QuizSchedule: "Quiz Schedule",
    QuizResult: "Quiz Result",
    TestSchedule: "Test Schedule",
    TestResult: "Test Result",
    GroupWorkSchedule: "Group Work Schedule",
    GroupName: "Group Name",
    GroupWorkResult: "Group Work Result",
    StudentAttendance: "Student Attendance",
    ExerciseBookResult: "Exercise Book Result",
    ExamResult: "Exam Result",
    StudentClassRoom: "Student Class Room"
};

$.extend($.fn.dataTable.defaults, {
    "sAjaxSource": "Get",
    "bProcessing": true,
    "bServerSide": true,
    "sServerMethod": "POST",
    "fnServerData": function(sUrl, aoData, fnCallback, oSettings) {
        $.ajax({
            dataType: 'json',
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: sUrl,
            data: JSON.stringify(GetPageDetail(oSettings)),
            success: fnCallback
        });
    }
});

function GetPageDetail(oSettings) {
    if (oSettings != null)
        return {
            DisplayStart: oSettings._iDisplayStart,
            DisplayLength: oSettings._iDisplayLength,
            SortColumn: oSettings.aoColumns[oSettings.aaSorting[0][0]].mData,
            IsAsc: (oSettings.aaSorting[0][2] == 0),
            Search: oSettings.oPreviousSearch.sSearch,
            IncludeInactive: false,
            Id: null,
            ParentId: null,
            DateFrom: null,
            DateTo: null
        };
    else
        return {
            DisplayStart: null,
            DisplayLength: null,
            SortColumn: null,
            IsAsc: null,
            Search: null,
            IncludeInactive: null,
            Id: GuidEmpty,
            ParentId: GuidEmpty,
            DateFrom: null,
            DateTo: null
        };
}

function EditLink(idColName){
    return {
        "mData": idColName,
        "bSortable": false,
        "mRender": function (data, type, row) {
            return "<a href=\"Edit?id=" + row[idColName] + "\"><img class=\"gridbutton\" title=\"Edit\" src=\"../Images/edit.ico\" alt=\"Edit\" /></a>";
        }
    };
};

function EditCustomerLink(idColName) {
    return {
        "mData": idColName,
        "bSortable": false,
        "mRender": function (data, type, row) {
            return "<a href=\"EditCustomer?id=" + row[idColName] + "\"><img class=\"gridbutton\" title=\"Edit\" src=\"../Images/edit.ico\" alt=\"Edit\" /></a>";
        }
    };
};

function PrintLink(controllerfunction,idColName) {
    return {
        "mData": idColName,
        "bSortable": false,
        "mRender": function (data, type, row) {
            return "<a href=\"" + controllerfunction + "?id=" + row[idColName] + "\"><i class=\"icon-print\"></i></a>";
        }
    };
};

function CommandLinkWithControler(controllerfunction,idColName) {
    return {
        "mData": idColName,
        "bSortable": false,
        "mRender": function (data, type, row) {
            return "<a href=\""+ controllerfunction + "?id=" + row[idColName] + "\"><img class=\"gridbutton\" title=\"Edit\" src=\"../Images/edit.ico\" alt=\"Edit\" /></a>";
        }
    };
};

function EditModalLink(idColName, modalId) {
    return {
        "mData": idColName,
        "bSortable": false,
        "mRender": function (data, type, row) {
            return "<a id=\"btnShowModalForEdit\" href=\"#" + modalId + "\" role=\"button\" data-toggle=\"modal\" /><img class=\"gridbutton\" title=\"Edit\" src=\"../Images/edit.ico\" alt=\"Edit\" /></a>";
        }
    };
};

function CheckBoxColumn(idColName) {
    return {
        "mData": idColName,
        "bSortable": false,
        "mRender": function (data, type, row) {
            //            return "<a href=\"Block?id=" + row[idColName] + "\"><img class=\"gridbutton\" title=\"Block\" src=\"/SMS.Web../Images/Calender Delete.png\" alt=\"Block\" /></a>";
            return "<input id=\"" + row[idColName] + "\" type=\"checkbox\" class=\"tablecheckbox\">";
        }
    };
}
function ColHidden(idColName) {
    return {
        "mData": idColName,
        "bSortable": false,
        "bVisible" : false
        };   
};

function DeleteLink(idColName, nameColName, entityName, table) {
    return {
        "mData": idColName,
        "bSortable": false,
        "mRender": function (data, type, row) {
            return "<img class=\"gridbutton\" title=\"Delete\" src=\"../Images/delete.ico\" alt=\"Delete\" onclick=\"OnDeleteEntity('" + row[idColName] + "','" + row[nameColName] + "','" + entityName + "'," + table + ");\" />";
        }
    };
};

function DeleteLink(idColName, nameColName, entityName, entityCaption, table) {
    return {
        "mData": idColName,
        "bSortable": false,
        "mRender": function (data, type, row) {
            return "<img class=\"gridbutton\" title=\"Delete\" src=\"../Images/delete.ico\" alt=\"Delete\" onclick=\"OnDeleteEntity('" + row[idColName] + "','" + row[nameColName] + "','" + entityName + "','" + entityCaption + "'," + table + ");\" />";
        }
    };
};

function EntryTextBox(idColName) {
    return {
        "mData": idColName,
        "mRender": function (data, type, row) {
            return "<input id=\"txt" + row[idColName] + "\" type=\"text\" class=\"tabletext\">";
        }
    };

}
function EntryTextBoxComment(idColName) {
    return {
        "mData": idColName,
        "mRender": function (data, type, row) {
            return "<input id=\"txtComment" + row[idColName] + "\" type=\"text\" class=\"tabletext\">";
        }
    };

}


function AlertMsg(title, msg, css, fadeOutTime) {
    var n = notyfy({
        text: '<h4>' + title + '!</h4> <p>' + msg + '</p>',
        type: title == 'Success' ? 'primary' : 'error',
        layout: 'bottomRight',
        theme: 'boolight',
        closeWith: ['click']
    });
}

function PostJson(url, data, callback) {
    $.ajax({
        type: 'POST',
        url: url,
        data: data,
        success: callback,
        dataType: 'json',
        contentType: 'application/json'
    });
}

function GetCookie(name) {
    var cookies = document.cookie.split(";");
    var lang = "En";
    for (var i = 0; i < cookies.length; i++) {
        if (cookies[i].indexOf(name + "=") == 1) {
            lang = cookies[i].substring(name.length + 2);
        }
    }
    return lang;
}

function GetUserFullName() {
    $.getJSON("../api/UserProfile/GetUserFullName", function (response) {
        return response;
    });
}