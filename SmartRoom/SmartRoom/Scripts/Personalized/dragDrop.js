$(function () {
    $("#sortable").sortable();
    $("#sortable").disableSelection();
    $("#sortable").sortable("disable");
});

function EnableSortable() {
    $("#sortable").sortable("enable");
    $(".toggle").toggle();
}
function DisableSortable() {
    $("#sortable").sortable("disable");
    $(".toggle").toggle();
}