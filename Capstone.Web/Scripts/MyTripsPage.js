$(document).ready(function (event) {
    // If on MyTrips page, run following commands:

    if ($("#MyTripsPgLocator").length) {
      
        var travelMode = "driving";

        var routeMap = new GMaps({
            div: '#routeMap',
            lat: 41.4993,
            lng: -81.6944
        });

        var drawRoute = function (tripId) {

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
                    // Print starting location name to Directions div
                    $('#routeDirections').append(`<div data-leg="${i}"><h4>` + data[i].Name + " to " + data[i + 1].Name + "</h4></div>");

                    // Current landmark coordinates
                    var originLatitude = data[i].Latitude;
                    var originLongitude = data[i].Longitude;
                    var destinationLatitude = data[i + 1].Latitude;
                    var destinationLongitude = data[i + 1].Longitude;

                    // Retrieve category name of current landmark
                    var categoryName = data[i].Categories[i];

                    // Add marker to current landmark
                    var marker = routeMap.addMarker({
                        lat: originLatitude,
                        lng: originLongitude,
                        title: "#" + (i + 1) + ": " + data[i].Name,
                        icon: "/Content/Markers/" + categoryName.replace(/\s/g, '') + "_marker.png",
                    });

                    var stepFunction = new stepPrinter(i);

                    // Print direction step
                    routeMap.travelRoute({
                        origin: [originLatitude, originLongitude],
                        destination: [destinationLatitude, destinationLongitude],
                        travelMode: travelMode,
                        step: stepFunction.printStep,
                    });

                    // Draw route between current and next landmark
                    routeMap.drawRoute({
                        origin: [originLatitude, originLongitude],
                        destination: [destinationLatitude, destinationLongitude],
                        travelMode: 'driving',
                        strokeColor: '#131540',
                        strokeOpacity: 0.6,
                        strokeWeight: 6,
                    });

                    // Plot marker on last landmark
                    if (i == data.length - 2) {
                        var lastMarker = routeMap.addMarker({
                            lat: destinationLatitude,
                            lng: destinationLongitude,
                            title: "#" + (i + 1) + ": " + data[i].Name,
                            icon: "/Content/Markers/" + categoryName.replace(/\s/g, '') + "_marker.png",
                        });
                    }
                }
            });

            function stepPrinter(leg) {
                this.printStep = function (e) {
                    console.log(`Step Direction for ${leg} - ${e.instructions}`);
                    $(`div[data-leg=${leg}]`).append('<p>' + e.instructions + '</p>');
                }
            }
        };

        $(".tripDiv").on("click", function (event) {
            var tripId = $(this).attr("id");
            drawRoute(tripId);
        });

        // Deleting trip  
        $(".submit_button").on("click", function () {
            var tripId = $(this).parent().parent().parent().attr('id');
            $("#" + tripId).remove();
            $.ajax({
                type: "POST",
                url: "/MyTrips/DeleteTrip/",
                data: { "tripId": tripId },
                success: function () {
                }
            });
        });

        // Walking and Driving Buttons
        $("#walking_Btn").on("click", function () {
            travelMode = "walking";
            $("#drivingDirectionHeader").html("Walking Direction");
            $("#routeDirections").empty();
        });
    }
});