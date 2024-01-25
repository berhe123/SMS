/// <reference path="../jquery-1.7.1-vsdoc.js" />
/// <reference path="../Site.js" />

$().ready(function () {
    $("#langEn, #langAm, #langOr, #langTg").click(function () {
        var lang = "lang=" + $(this).attr("id").substring(4) + "; expires=Fri, 31 Jan 2015 00:00:00 UTC; path=/";
        document.cookie = lang;
        document.location = document.location;
    });

    SetLanguage($("#lang" + GetCookie("lang")));

    $('.accordion-toggle').on('click', function (e) {
        if ($(this).parents('.accordion-group').children('.accordion-body').hasClass('in')) {
            e.preventDefault();
            e.stopPropagation();
        }
    });
});

function SetLanguage(langControl) {
    $("#langs li").removeClass("active");
    langControl.addClass("active");

    $("#SelectedLang").html($("a", langControl).html());
}