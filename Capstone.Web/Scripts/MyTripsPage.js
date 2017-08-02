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

        $(".ui-accordion-header-active").accordion({
            activate: function (event, ui) {}
        });

        $(".ui-accordion-header-active").on("accordionactivate", function (event, ui) {
            alert("Hey!");
        });
    }
});