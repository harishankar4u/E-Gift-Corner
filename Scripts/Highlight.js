$(document).ready(function () {
    $(".row").hover(
function () {
    $(this).addClass("highlight");
},
function () {
    $(this).removeClass("highlight");
}
);
});