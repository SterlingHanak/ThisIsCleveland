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

        $(".tripDiv").on("click", function(event) {
            // Get trip id
            var tripId = $(this).attr("id");

            // Retrieve landmark details based on trip id
            $.ajax({
                url: "/PlanTrip/LandmarksInTripJSON/",
                type: "GET",
                data: { "tripId": tripId },
                dataType: "json"

            // If json data retrieval successful, display route on map
            }).done(function (data) {

                // Center map on coordinates of first landmark
                routeMap.setCenter(data[0].Latitude, data[0].Longitude);

                for (var i = 0; i < data.length - 1; i++) {
                    // Current landmark coordinates
                    var originLatitude = data[i].Latitude;
                    var originLongitude = data[i].Longitude;

                    // Retrieve category name of current landmark
                    var categoryName = data[i].Categories[i];

                    // Add marker to current landmark
                    var marker = routeMap.addMarker({
                        lat: originLatitude,
                        lng: originLongitude,
                        title: "#" + (i + 1) + ": " + data[0].Name,
                        icon: "/Content/Markers/" + categoryName.replace(/\s/g, '') + "_marker.png",
                    });

                    // Next landmark coordinates
                    var destinationLatitude = data[i + 1].Latitude;
                    var destinationLongitude = data[i + 1].Longitude;

                    // Draw route between current and next landmark
                    routeMap.drawRoute({
                        origin: [originLatitude, originLongitude],
                        destination: [destinationLatitude, destinationLongitude],
                        travelMode: 'driving',
                        strokeColor: '#131540',
                        strokeOpacity: 0.6,
                        strokeWeight: 6
                    });

                    if (i == data.length - 2) {
                        // Plot marker on last landmark
                        var lastMarker = routeMap.addMarker({
                            lat: destinationLatitude,
                            lng: destinationLongitude,
                            title: "#" + (i + 1) + ": " + data[i].Name,
                            icon: "/Content/Markers/" + categoryName.replace(/\s/g, '') + "_marker.png",
                        });
                    }
                }
            });
        });
    }
});