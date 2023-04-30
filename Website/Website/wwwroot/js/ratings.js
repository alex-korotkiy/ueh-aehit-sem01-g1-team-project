function setRating(itemId, rating)
{
    $.ajax({
        type: "PUT",
        url: "/ratings",
        data: JSON.stringify({ "itemId": parseInt(itemId, 0), "rating": Number(rating) }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log('rating set');
        },
        error: function (errMsg) {
            console.error(errMsg);
        }
    });
}