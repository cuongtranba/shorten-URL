$(function () {
    $("#link-url").keypress(function (e) {
        e.preventDefault();
        $(this).tooltip('toggle');
    });
    $("#link-url").bind("paste", function (e) {
        var seft = $(this);
        $(this).tooltip('hide');
        var data = e.originalEvent.clipboardData.getData('Text');
        if (data) {
            seft.addClass("loadinggif");
            seft.prop("disabled", true);
            $.get("/home/GetShortUrl", { url: data }, function (data) {
                seft.prop("disabled", false);
                seft.val(window.location.href + data);
                seft.select();
                seft.removeClass("loadinggif");
            });
        }
    });
})