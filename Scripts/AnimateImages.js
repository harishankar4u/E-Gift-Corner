$(document).ready(function () {
    $(".product").mouseover(function () {
        $(this).addClass("zoomIn", 500);

    });

    $(".product").mouseout(function () {
        $(this).removeClass("zoomIn", 500);

    });

});