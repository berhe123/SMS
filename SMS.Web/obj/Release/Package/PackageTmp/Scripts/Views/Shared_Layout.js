/// <reference path="jquery-1.7.1-vsdoc.js" />
$().ready(function() {
    $(".nav-menu-item").removeClass("active");
    $("." + $("#mnu-active").val()).addClass("active");
    $("." + $("#mnu-active").val()).parent().parent().parent().removeClass("collapse");
});