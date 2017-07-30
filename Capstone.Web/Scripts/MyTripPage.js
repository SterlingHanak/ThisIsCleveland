$(document).ready(function () {

    // If on NewTrip page, run following commands:

    if ($("#map").length) {
        /**********************
         * CREATE MAP 
         **********************/
        var map = new GMaps({
            div: '#map',
            lat: 41.4993,
            lng: -81.6944
        });

         /***********************************
         * ADD AND REMOVE MARKERS FROM MAP
         ************************************/

        var gMarkers = {};
        var realMarkers = {};

        GMaps.prototype.addMarkersOfType = function (categoryName) {
            // Clear markers of this type
            realMarkers[categoryName] = [];

            // Add each Gmaps marker to map
            for (var i = 0; i < gMarkers[categoryName].length; i++) {
                var marker = map.addMarker({
                    lat: gMarkers[categoryName][i].Latitude,
                    lng: gMarkers[categoryName][i].Longitude,
                    title: gMarkers[categoryName][i].Name,
                    //icon: "/Content/Markers/" + categoryName + "_marker.png",
                    landmarkId: gMarkers[categoryName][i].Id,
                    landmarkName: gMarkers[categoryName][i].Name,
                    relativeCost: gMarkers[categoryName][i].RelativeCost,
                    customAddress: gMarkers[categoryName][i].Address,
                    phoneNumber: gMarkers[categoryName][i].PhoneNumber,
                    averageRating: gMarkers[categoryName][i].AvgRating,
                    websiteUrl: gMarkers[categoryName][i].WebsiteUrl,
                    description: gMarkers[categoryName][i].Description,
                    click: function (event) {
                        $("#landmark_name").html(event.landmarkName);
                        $("#landmark_relative_cost").html("Relative Cost ($): " + event.relativeCost + " out of 5");
                        $("#landmark_address").html(event.customAddress);
                        $("#landmark_phone_number").html(event.phoneNumber);
                        $("#landmark_average_rating").html("Average Rating: " + event.averageRating);
                        $("#landmark_website_url").html(event.websiteUrl);
                        $("#landmark_description").html(event.description);
                        $("#landmark_id").val(event.landmarkId);
                    }
                });
                // Save it as real (Google Maps) marker
                realMarkers[categoryName].push(marker);
            }
        }

        GMaps.prototype.removeMarkersOfType = function (categoryName) {
            // Remove each real marker of this type
            for (var i = 0; i < gMarkers[categoryName].length; i++) {
                realMarkers[categoryName][i].setMap(null);
            }
            // Clear markers of this type
            realMarkers[categoryName] = [];
        }

        $(".category").on("click", function () {
            // Retrieve user-selected category from checkbox
            var categoryName = event.target.name;

            // If box is now checked, get ajax data for landmarks with passed-in category
            if ($(this).is(':checked')) {
                $.ajax({
                    url: "/PlanTrip/LandmarksInCategoryJSON/",
                    type: "GET",
                    data: { "category": categoryName },
                    dataType: "json"

                // If json data retrieval successful, display all markers on map corresponding to category
                }).done(function (data) {
                    gMarkers[categoryName] = data;
                    GMaps.prototype.addMarkersOfType(categoryName);
                });
            }
            else {
                GMaps.prototype.removeMarkersOfType(categoryName);
            }
        });

        /**********************************************
        * ADD LANDMARK STOP TO THE "CURRENT TRIP" PANEL
        ***********************************************/
        var landmarkIds = [];
        var dropSettings = landmarkIds.length > 1 ? "ondrop='drop(event)' ondragover='allowDrop(event)'" : "";

        var drag = function(event) {
            event.dataTransfer.setData("text", event.target.id);
        }

        $("#addLandmarkToTripBtn").on("click", function () {
            var newLandmarkDiv = "<div id='" + $("#landmark_id").val() + "' draggable='true' " +
                "ondragstart='drag' " + dropSettings + " style='background-color: pink;'>" +
                $("#landmark_name").html() + "</div>";
            landmarkIds.push($("#landmark_id").val());
            $(newLandmarkDiv).appendTo("#stops");

            // Create a new hidden input field that will bind to model after POST
            var landmarkIdHiddenField = "<input type='hidden' name='MyTripViewModel.SelectedLandmarkIds[" +
                (landmarkIds.length - 1) + "]' value='" + landmarkIds[(landmarkIds.length - 1)]
                + "' />";

            // Append input field to div
            $(landmarkIdHiddenField).appendTo("#stops");
        });

        function drop(ev) {
            ev.preventDefault();
            var data = ev.dataTransfer.getData("text");
            ev.target.appendChild(document.getElementById(data));
        }

        function allowDrop(ev) {
            ev.preventDefault();
        }
    }
});