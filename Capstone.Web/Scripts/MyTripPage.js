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
            var iconName = categoryName.replace(/\s/g, '') + "_marker.png";

            for (var i = 0; i < gMarkers[categoryName].length; i++) {
                var marker = map.addMarker({
                    lat: gMarkers[categoryName][i].Latitude,
                    lng: gMarkers[categoryName][i].Longitude,
                    title: gMarkers[categoryName][i].Name,
                    icon: "/Content/Markers/" + iconName,
                    landmarkId: gMarkers[categoryName][i].Id,
                    landmarkName: gMarkers[categoryName][i].Name,
                    relativeCost: gMarkers[categoryName][i].RelativeCost,
                    customAddress: gMarkers[categoryName][i].Address,
                    phoneNumber: gMarkers[categoryName][i].PhoneNumber,
                    averageRating: gMarkers[categoryName][i].AvgRating,
                    websiteUrl: gMarkers[categoryName][i].WebsiteUrl,
                    description: gMarkers[categoryName][i].Description,
                    click: function (event) {
                        var addressLine1 = event.customAddress.substring(0, event.customAddress.indexOf(", "));
                        var addressLine2 = event.customAddress.substring(event.customAddress.indexOf(", ") + 2);

                        $("#landmark_name").html(event.landmarkName);
                        $("#landmark_relative_cost").html("Relative Cost ($): " + event.relativeCost + " out of 5");
                        $("#landmark_address_line1").html(addressLine1);
                        $("#landmark_address_line2").html(addressLine2);
                        $("#landmark_phone_number").html(event.phoneNumber);
                        $("#landmark_average_rating").html("Average Rating: " + event.averageRating);
                        $("#landmark_website_url").attr("href", "'https://www'" + event.websiteUrl);
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

        // Enable sorting of #stops div with jQuery UI script
        $("#stops").sortable();
        $("#stops").disableSelection();

        $("#addLandmarkToTripBtn").on("click", function () {
            // Add row for new stop order (#1, #2, #3, etc.)
            var newStopOrderRow = "<div class='row'>" + (landmarkIds.length + 1) + "</div>";

            // Add row for new landmark stop
            var newStopRow = "<div id='landmark" + $("#landmark_id").val() + "' style='background-color: yellow; border: 2px solid orange;'>" +
                "<span class='ui-icon ui-icon-caret-2-n-s' ></span>" + $("#landmark_name").html() + "</div>";

            // Add each new rows to appropriate container
            $(newStopOrderRow).appendTo("#stopOrder");
            $(newStopRow).appendTo("#stops");

            // Update array of stored landmark ids
            landmarkIds.push($("#landmark_id").val());

            // Create a new hidden input field that will bind to model after POST
            var landmarkIdHiddenField = "<input type='hidden' name='MyTripViewModel.SelectedLandmarkIds[" +
                (landmarkIds.length - 1) + "]' value='" + landmarkIds[(landmarkIds.length - 1)]
                + "' />";

            // Append input field to new landmark stop row
            var newStopDivId = "#landmark" + $("#landmark_id").val();
            $(landmarkIdHiddenField).appendTo(newStopDivId);
        });
    }
});