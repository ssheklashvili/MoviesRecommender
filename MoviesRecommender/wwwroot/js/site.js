
$("#searchForm").on("submit", function (e) {
    e.preventDefault();
    var searchValue = $("#search").val();
    if (searchValue.length < 3)
        return;
    $("#show_more").hide();
    $("#movie_card_view").html("");
    startLoading();
    $("#show_more").data("page", 1);
    $("#show_more").data("name", searchValue);
    $.get("/Home/GetMovies", { page: 1, name: searchValue }, function (responses) {
        $("#movie_card_view").append(responses);
        stopLoading();
        $("#show_more").show();
    });


});
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

$(document).on("change",':radio',function () {
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



//added by oleg
//ლოადერი გავიტანე css ში.
//დიზაინში ცვლილებისთვის ლეიაუთში კონტეინერი რომ იყო ის არ მაწყობდა და იმას მივაყოლე ლოადერიც
function startLoading() {
    $("body").append("<div class='lds-spinner'><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div><div></div></div>");
}
function stopLoading() {
    $("body").find(".lds-spinner").remove();
}
