/// <reference path="jquery-1.7.1-vsdoc.js" />
$().ready(function() {
    $(".live-tile, .flip-list").not(".exclude").liveTile();

    $("#divShowItemsBelowReorderLevel").click(function () {
       
        window.open("../Reports/ListOfItemsWithQuantityOnHandBelowReorderLevelReport?dateFrom=" + '' + "&storeGuid=" + null);

    });
    $("#divShowSMSOrdersNotYetReceived").click(function () {

        window.open("../Reports/ListOfSMSOrdersNotYetReceivedReport");

    });

    $("#divShowPurchaseOrdersNotYetReceived").click(function () {

        window.open("../Reports/ListOfPurchaseOrdersNotYetReceivedReport");
    });
    $("#divShowSalesOrdersNotYetInvoiced").click(function () {

        window.open("../Reports/ListOfSalesOrdersNotYetInvoicedReport");
    });
    $("#divShowSalesInvoicesNotYetIssued").click(function () {

        window.open("../Reports/ListOfSalesInvoicesNotYetIssuedReport");
    });
    $("#divShowCreditInvoicesNotYetSettled").click(function () {

        window.open("../Reports/ListOfCreditInvoicesNotYetSettledReport");
    });
    $("#divShowNumberOfDamagedGood").click(function () {

        window.open("../Reports/ListOfNumberOfDamagedGoodReport");
    });
    $("#divShowNumberOfLostGood").click(function () {

        window.open("../Reports/ListOfNumberOfLostGoodReport");
    });
    
});

