$(document).ajaxStart(function () {
    $("#loading").show();
    $("#pageWrapper").hide();
});

$(document).ajaxStop(function () {
    $("#loading").hide();
    $("#pageWrapper").show();
});

function searchMovie() {
    var searchValue = $("#search").val();
    if (searchValue.length < 3)
        return;
    $.ajax({
        url: "/Home/SearchMovie",
        method: "GET",
        contentType: "application/json",
        data: { name: searchValue },
        success: function (response) {
            $("#pageWrapper").html(response);
        }
    });
};

function getRecommendation(userId) {
    $.ajax({
        url: "/Home/GetRecommendation",
        method: "GET",
        contentType: "application/json",
        data: { userId: userId },
        success: function (response) {
            $("#pageWrapper").html(response);
        }
    });
};
