$(document).ready(function (event) {
    // If on MyTrips page, run following commands:

    if ($("#MyTripsPgLocator").length) {
        $("#accordion").accordion({
            heightStyle: "content"
        });

        var routeMap = new GMaps({
            div: '#routeMap',
            lat: 41.4993,
            lng: -81.6944
        });

        $(".tripDiv").on("hover", function (event) {
            $(this).css("background-color", "lightblue");
        });

        $(".tripDiv").on("click", function(event) {
            // Get trip id
            var tripId = $(this).attr("id");
        });
    }
});