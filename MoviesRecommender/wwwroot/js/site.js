// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function searchMovie() {
    var searchValue = $("#search").val();
    if (searchValue.length < 3)
        return;
    $.ajax({
        url: "Home/SearchMovie",
        method: "GET",
        async: false,
        contentType: "application/json",
        data: { name: searchValue },
        success: function (response) {
            $("#pageWrapper").html(response); 
        }
    });
}