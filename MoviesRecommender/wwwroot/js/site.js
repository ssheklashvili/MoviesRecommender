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

$(':radio').change(function () {
    var form = $(this).closest('form');
    var movieId = form.find('*').filter(':input:hidden:first').val();
    var rate = $(this).val();
    $.ajax({
        url: "/Home/RateMovie",
        method: "POST",
        global: false,
        contentType: 'application/x-www-form-urlencoded',
        data: { movieId: movieId, rate: rate },
        success: function (response) {
            var ratingBody = form.closest('.card-rating');
            var ratingSpan = ratingBody.find('.rate').text(rate);
        }
    });
});