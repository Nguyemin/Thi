var idCategory = 0;
var keyWord = "";
var pageIndex = 1;
$(document).ready(function () {
    $("#category").on("change", function () {
        idCategory = $(this).val();
        onLoad();
    });
});

function loadViewUpdate(idProduct) {
    $("#contentViewUpdate").load("/Product/Update?id=" + idProduct);
}
function onSearch() { 
    keyWord = $("#keyWord").val();
    onLoad();
}

function onEnter(event) {
    if (event.key === "Enter") { // Enter key
        onSearch();
    }
}

function onPaging(pIndex) {
    pageIndex = pIndex;
    onLoad();
}

function onLoad() {
    //cách 1
   /* $("#products").load("/Product/LoadProduct?idCategory=" + idCategory
        + "&keyWord=" + keyWord + "&pageIndex=" + pageIndex);*/

    //Cách 2
    $("#products").load(`/Product/LoadProduct?idCategory=${idCategory}&keyWord=${keyWord}&pageIndex=${pageIndex}`)
}