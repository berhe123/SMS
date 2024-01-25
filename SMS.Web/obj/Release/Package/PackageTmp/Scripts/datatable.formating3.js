var oCustomInfoDecimalNumberFormat = {
    decimalPlaces: 4,
    thousandSeparator: ",",
    decimalSeparator: "."
}

var oCustomInfoNonDecimalNumberFormat = {
    decimalPlaces: 0,
    thousandSeparator: ",",
    decimalSeparator: "."
}

function RenderDecimalNumber(oObj, oCustomInfo) {
    if (oObj == null) return null;
    if (oObj.aData[oObj.mDataProp] == null) return null;

    var num = new NumberFormat();
    num.setInputDecimal('.');
    num.setNumber(oObj.aData[oObj.mDataProp]);
    num.setPlaces(oCustomInfo.decimalPlaces, true);
    num.setCurrency(false);
    num.setNegativeFormat(num.LEFT_DASH);
    num.setSeparators(true, oCustomInfo.thousandSeparator, oCustomInfo.decimalSeparator);

    return num.toFormatted();
}

function RenderDecimalNumber(oObj) {
    if (oObj == null) return null;
    if (oObj.aData[oObj.mDataProp] == null) return null;

    var num = new NumberFormat();
    num.setInputDecimal('.');
    num.setNumber(oObj.aData[oObj.mDataProp]);
    num.setPlaces(oCustomInfoDecimalNumberFormat.decimalPlaces, true);
    num.setCurrency(false);
    num.setNegativeFormat(num.LEFT_DASH);
    num.setSeparators(true);

    return num.toFormatted();
}

function RenderNonDecimalNumber(oObj) {
    if (oObj == null) return null;
    if (oObj.aData[oObj.mDataProp] == null) return null;

    var num = new NumberFormat();
    num.setInputDecimal('.');
    num.setNumber(oObj.aData[oObj.mDataProp]);
    num.setPlaces(oCustomInfoNonDecimalNumberFormat.decimalPlaces, false);
    num.setCurrency(false);
    num.setNegativeFormat(num.LEFT_DASH);
    num.setSeparators(true);

    return num.toFormatted();
}

function FormatDecimalNumber(numValue, oCustomInfo) {
    if (numValue == null) return null;

    var num = new NumberFormat();
    num.setInputDecimal('.');
    num.setNumber(numValue);
    num.setPlaces(oCustomInfo.decimalPlaces, true);
    num.setCurrency(false);
    num.setNegativeFormat(num.LEFT_DASH);
    num.setSeparators(true, oCustomInfo.thousandSeparator, oCustomInfo.decimalSeparator);

    return num.toFormatted();
}

function FormatDecimalNumber(numValue) {
    if (numValue == null) return null;

    var num = new NumberFormat();
    num.setInputDecimal('.');
    num.setNumber(numValue);
    num.setPlaces(oCustomInfoDecimalNumberFormat.decimalPlaces, true);
    num.setCurrency(false);
    num.setNegativeFormat(num.LEFT_DASH);
    num.setSeparators(true, oCustomInfoDecimalNumberFormat.thousandSeparator, oCustomInfoDecimalNumberFormat.decimalSeparator);

    return num.toFormatted();
}

function FormatNonDecimalNumber(numValue) {
    if (numValue == null) return null;

    var num = new NumberFormat();
    num.setInputDecimal('.');
    num.setNumber(numValue);
    num.setPlaces(oCustomInfoNonDecimalNumberFormat.decimalPlaces, false);
    num.setCurrency(false);
    num.setNegativeFormat(num.LEFT_DASH);
    num.setSeparators(true, oCustomInfoNonDecimalNumberFormat.thousandSeparator, oCustomInfoNonDecimalNumberFormat.decimalSeparator);

    return num.toFormatted();
}

function RowTotal(nRow, aData, iDisplayIndex) {
    var columnPricePerUnit = "PricePerUnit";
    var columnQuantity = "Quantity";
    var TotalAmount = 0;

    // Remove the formatting to get integer data for summation
    var intVal = function (i) {
        return typeof i === 'string' ?
            i.replace(/[\$,]/g, '') * 1 :
            typeof i === 'number' ?
            i : 0;
    };

    TotalAmount = (intVal(aData[columnPricePerUnit]) * 1) * (intVal(aData[columnQuantity]) * 1);
    /* Modify the footer row to match what we want */
    var nCells = nRow.getElementsByTagName('td');
    nCells[5].innerHTML = FormatDecimalNumber(parseFloat(TotalAmount));
}

function SetRowTotalAmount(nRow, aData, iDisplayIndex, unitPriceDataField, quantityDataField, columnTotalAmountIndex) {
    var TotalAmount = 0;

    // Remove the formatting to get integer data for summation
    var intVal = function (i) {
        return typeof i === 'string' ?
            i.replace(/[\$,]/g, '') * 1 :
            typeof i === 'number' ?
            i : 0;
    };

    TotalAmount = (intVal(aData[unitPriceDataField]) * 1) * (intVal(aData[quantityDataField]) * 1);
    
    var nCells = nRow.getElementsByTagName('td');
    nCells[columnTotalAmountIndex].innerHTML = FormatDecimalNumber(parseFloat(TotalAmount));
}

function SetGrossVatGrand(nRow, aaData, iStart, iEnd, aiDisplay, unitPriceDataField, quantityDataField, VatDataField) {
    var TotalAmount = 0;
    var GrossTotal = 0;
    var Vat = 0;
    var GrandTotal = 0;

    var grossRow = nRow;
    var vatRow = $(grossRow).next()[0];;
    var grandRow = $(vatRow).next()[0];;

    // Remove the formatting to get integer data for summation
    var intVal = function (i) {
        return typeof i === 'string' ?
            i.replace(/[\$,]/g, '') * 1 :
            typeof i === 'number' ?
            i : 0;
    };

    /*
     * Calculate the total Quantity for all data in this table (ie inc. outside
     * the pagination)
     */
    var iTotalQuantity = 0;
    for (var i = 0 ; i < aaData.length ; i++) {
        //iTotalQuantity += aaData[i][columnToAdd] * 1;
        TotalAmount = (intVal(aaData[i][unitPriceDataField]) * 1) * (intVal(aaData[i][quantityDataField]) * 1);
        GrossTotal += TotalAmount;
        Vat += intVal(aaData[i][VatDataField]) * 1;
    }

    GrandTotal = GrossTotal + Vat;

    ///* Calculate the Quantity for this page */
    //var iPageQuantity = 0;
    //for (var i = iStart ; i < iEnd ; i++) {
    //    iPageQuantity += aaData[aiDisplay[i]][columnToAdd] * 1;
    //}

    /* Modify the footer row to match what we want */
    var nCells = grossRow.getElementsByTagName('th');
    nCells[1].innerHTML = FormatDecimalNumber(parseFloat(GrossTotal));

    nCells = vatRow.getElementsByTagName('th');
    nCells[1].innerHTML = FormatDecimalNumber(parseFloat(Vat));

    nCells = grandRow.getElementsByTagName('th');
    nCells[1].innerHTML = FormatDecimalNumber(parseFloat(GrandTotal));
}

function SetGrossVatDiscountGrand(nRow, aaData, iStart, iEnd, aiDisplay, unitPriceDataField, quantityDataField, VatDataField, DiscountDataField) {
    var TotalAmount = 0;
    var GrossTotal = 0;
    var DiscountTotal = 0;
    var GrossAfterDiscountTotal = 0;
    var Vat = 0;
    var GrandTotal = 0;

    var grossRow = nRow;
    var discountRow = $(grossRow).next()[0];
    var grossAfterDiscountRow = $(discountRow).next()[0];
    var vatRow = $(grossAfterDiscountRow).next()[0];
    var grandRow = $(vatRow).next()[0];

    // Remove the formatting to get integer data for summation
    var intVal = function (i) {
        return typeof i === 'string' ?
            i.replace(/[\$,]/g, '') * 1 :
            typeof i === 'number' ?
            i : 0;
    };

    /*
     * Calculate the total Quantity for all data in this table (ie inc. outside
     * the pagination)
     */
    var iTotalQuantity = 0;
    for (var i = 0 ; i < aaData.length ; i++) {
        //iTotalQuantity += aaData[i][columnToAdd] * 1;
        TotalAmount = (intVal(aaData[i][unitPriceDataField]) * 1) * (intVal(aaData[i][quantityDataField]) * 1);
        GrossTotal += TotalAmount;
        DiscountTotal += intVal(aaData[i][DiscountDataField]) * 1;
        Vat += intVal(aaData[i][VatDataField]) * 1;
    }

    GrossAfterDiscountTotal = GrossTotal - DiscountTotal;
    GrandTotal = GrossAfterDiscountTotal + Vat;

    ///* Calculate the Quantity for this page */
    //var iPageQuantity = 0;
    //for (var i = iStart ; i < iEnd ; i++) {
    //    iPageQuantity += aaData[aiDisplay[i]][columnToAdd] * 1;
    //}

    /* Modify the footer row to match what we want */
    var nCells = grossRow.getElementsByTagName('th');
    nCells[1].innerHTML = FormatDecimalNumber(parseFloat(GrossTotal));

    nCells = discountRow.getElementsByTagName('th');
    nCells[1].innerHTML = FormatDecimalNumber(parseFloat(DiscountTotal));

    nCells = grossAfterDiscountRow.getElementsByTagName('th');
    nCells[1].innerHTML = FormatDecimalNumber(parseFloat(GrossAfterDiscountTotal));

    nCells = vatRow.getElementsByTagName('th');
    nCells[1].innerHTML = FormatDecimalNumber(parseFloat(Vat));

    nCells = grandRow.getElementsByTagName('th');
    nCells[1].innerHTML = FormatDecimalNumber(parseFloat(GrandTotal));
}

function SetGrossVatWithholdingGrand(nRow, aaData, iStart, iEnd, aiDisplay, unitPriceDataField, quantityDataField, VatDataField, WithholdingDataField) {
    var TotalAmount = 0;
    var GrossTotal = 0;
    var Vat = 0;
    var GrandTotal = 0;
    var WithholdingTotal = 0;
    var GrandAfterWithholdingTotal = 0;

    var grossRow = nRow;
    var vatRow = $(grossRow).next()[0];
    var grandRow = $(vatRow).next()[0];
    var withholdingRow = $(grandRow).next()[0];
    var grandAfterWithholdingRow = $(withholdingRow).next()[0];

    // Remove the formatting to get integer data for summation
    var intVal = function (i) {
        return typeof i === 'string' ?
            i.replace(/[\$,]/g, '') * 1 :
            typeof i === 'number' ?
            i : 0;
    };

    /*
     * Calculate the total Quantity for all data in this table (ie inc. outside
     * the pagination)
     */
    var iTotalQuantity = 0;
    for (var i = 0 ; i < aaData.length ; i++) {
        //iTotalQuantity += aaData[i][columnToAdd] * 1;
        TotalAmount = (intVal(aaData[i][unitPriceDataField]) * 1) * (intVal(aaData[i][quantityDataField]) * 1);
        GrossTotal += TotalAmount;
        WithholdingTotal += intVal(aaData[i][WithholdingDataField]) * 1;
        Vat += intVal(aaData[i][VatDataField]) * 1;
    }

    GrandTotal = GrossTotal + Vat;
    GrandAfterWithholdingTotal = GrandTotal - WithholdingTotal;

    ///* Calculate the Quantity for this page */
    //var iPageQuantity = 0;
    //for (var i = iStart ; i < iEnd ; i++) {
    //    iPageQuantity += aaData[aiDisplay[i]][columnToAdd] * 1;
    //}

    /* Modify the footer row to match what we want */
    var nCells = grossRow.getElementsByTagName('th');
    nCells[1].innerHTML = FormatDecimalNumber(parseFloat(GrossTotal));

    nCells = vatRow.getElementsByTagName('th');
    nCells[1].innerHTML = FormatDecimalNumber(parseFloat(Vat));

    nCells = grandRow.getElementsByTagName('th');
    nCells[1].innerHTML = FormatDecimalNumber(parseFloat(GrandTotal));
    
    nCells = withholdingRow.getElementsByTagName('th');
    nCells[1].innerHTML = FormatDecimalNumber(parseFloat(WithholdingTotal));

    nCells = grandAfterWithholdingRow.getElementsByTagName('th');
    nCells[1].innerHTML = FormatDecimalNumber(parseFloat(GrandAfterWithholdingTotal));
}

function SetColumnTotal(nRow, aaData, iStart, iEnd, aiDisplay, columnToAdd, footerTotalColumnIndex) {
    var columnTotal = 0;

    // Remove the formatting to get integer data for summation
    var intVal = function (i) {
        return typeof i === 'string' ?
            i.replace(/[\$,]/g, '') * 1 :
            typeof i === 'number' ?
            i : 0;
    };

    for (var i = 0 ; i < aaData.length ; i++) {
        columnTotal += (intVal(aaData[i][columnToAdd]) * 1);
    }

    var nCells = nRow.getElementsByTagName('th');
    nCells[footerTotalColumnIndex].innerHTML = FormatDecimalNumber(parseFloat(columnTotal));
}

function FormatDate(oObj) {
    if (oObj.aData[oObj.mDataProp] == null) return null;

    //var formatedDate = new Date(oObj.aData[oObj.mDataProp]);
    //formatedDate = formatedDate.getDate() + "/" + (formatedDate.getMonth() + 1) + "/" + formatedDate.getFullYear() + " " + formatedDate.getUTCHours() + ":" + formatedDate.getUTCMinutes();
    //return "<div class='date'>" + formatedDate + "</div>";

    var pattern = 'DD/MM/YYYY HH:mm';
    var output = moment(oObj.aData[oObj.mDataProp]).format(pattern);
    return "<div class='date'>" + output + "</div>";
}
function FormatDateFromDate(oObj) {
    if (oObj == null) return null;

    //var formatedDate = new Date(oObj.aData[oObj.mDataProp]);
    //formatedDate = formatedDate.getDate() + "/" + (formatedDate.getMonth() + 1) + "/" + formatedDate.getFullYear() + " " + formatedDate.getUTCHours() + ":" + formatedDate.getUTCMinutes();
    //return "<div class='date'>" + formatedDate + "</div>";

    var pattern = 'DD/MM/YYYY HH:mm';
    var output = moment(oObj).format(pattern);
    return output;
}
function FormatTime(oObj) {
    if (oObj.aData[oObj.mDataProp] == null) return null;

    var pattern = 'HH:mm';
    var output = moment(oObj.aData[oObj.mDataProp]).format(pattern);
    return "<div class='time'>" + output + "</div>";

}
function OnlyFormatDate(oObj) {
    if (oObj.aData[oObj.mDataProp] == null) return null;

    //var formatedDate = new Date(oObj.aData[oObj.mDataProp]);
    //formatedDate = formatedDate.getDate() + "/" + (formatedDate.getMonth() + 1) + "/" + formatedDate.getFullYear() + " " + formatedDate.getUTCHours() + ":" + formatedDate.getUTCMinutes();
    //return "<div class='date'>" + formatedDate + "</div>";

    var pattern = 'DD/MM/YYYY';
    var output = moment(oObj.aData[oObj.mDataProp]).format(pattern);
    return "<div class='date'>" + output + "</div>";
}