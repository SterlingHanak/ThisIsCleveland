﻿$(document).ready(function () {

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

         /*****************************************************
         * ADD AND REMOVE MARKERS FROM MAP - CATEGORY CHECKBOX
         *****************************************************/
        var gMarkers = {};
        var realMarkers = {};

        GMaps.prototype.addMarkersOfType = function (markerType, categoryName = "") {
            // Clear markers of this type
            realMarkers[markerType] = [];

            // Add each Gmaps marker to map
            for (var i = 0; i < gMarkers[markerType].length; i++) {

                // Retrieve category name of marker if not provided
                if (categoryName == "") {
                    categoryName = gMarkers[markerType][i].Categories[0];
                }

                var marker = map.addMarker({

                    // Mapping properties
                    lat: gMarkers[markerType][i].Latitude,
                    lng: gMarkers[markerType][i].Longitude,
                    title: gMarkers[markerType][i].Name,
                    icon: "/Content/Markers/" + categoryName.replace(/\s/g, '') + "_marker.png",

                    // Other Landmark properties
                    landmarkId: gMarkers[markerType][i].Id,
                    landmarkName: gMarkers[markerType][i].Name,
                    relativeCost: gMarkers[markerType][i].RelativeCost,
                    customAddress: gMarkers[markerType][i].Address,
                    phoneNumber: gMarkers[markerType][i].PhoneNumber,
                    averageRating: gMarkers[markerType][i].AvgRating,
                    websiteUrl: gMarkers[markerType][i].WebsiteUrl,
                    description: gMarkers[markerType][i].Description,
                    schedule: gMarkers[markerType][i].Schedule,
                    click: function (event) {
                        var addressLine1 = event.customAddress.substring(0, event.customAddress.indexOf(", "));
                        var addressLine2 = event.customAddress.substring(event.customAddress.indexOf(", ") + 2);
                        var landmarkPicName = categoryName.replace(/\s/g, '') + "/" + event.landmarkName.replace(/\s/g, '_');

                        $("#landmark_pic").attr("src", "../Content/Images/" + landmarkPicName + ".jpg");
                        $("#landmark_name").html(event.landmarkName);
                        $("#landmark_address_line1").html(addressLine1);
                        $("#landmark_address_line2").html(addressLine2);
                        $("#landmark_phone_number").html(event.phoneNumber);
                        $("#landmark_average_rating").attr("src", "../Content/Images/" + event.averageRating + "-star.png");
                        $("#landmark_relative_cost").attr("src", "../Content/Images/" + event.relativeCost + "-cost.png");
                        $("#landmark_website_url").attr("href", "https://www." + event.websiteUrl);
                        $("#landmark_description").html(event.description);
                        $("#landmark_id").val(event.landmarkId);

                        // Hours of Operation
                        var daysOfWeek = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"];
                        for (var i = 0; i < daysOfWeek.length; i++) {
                            if (event.schedule[daysOfWeek[i]] != "undefined") {
                                var containerId = "#" + daysOfWeek[i] + "_hours";
                                var message = event.schedule[daysOfWeek[i]].TimeOpen + " - " + event.schedule[daysOfWeek[i]].TimeClosed;
                                $(containerId).html(message);
                            }
                        }
                    }
                });
                // Save it as real (Google Maps) marker
                realMarkers[markerType].push(marker);
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
                    GMaps.prototype.addMarkersOfType(categoryName, categoryName);
                });
            }
            else {
                GMaps.prototype.removeMarkersOfType(categoryName);
            }
        });

         /*****************************************************
         * ADD AND REMOVE MARKERS FROM MAP - SEARCH BOX
         *****************************************************/
        $("#searchBar").blur(function (event) {
            // Define marker type
            var markerType = "searchQuery";

            // Retrieve user search query
            var keywords = $("#searchBar").val();

            // If user has typed search query, retrieve landmarks that are close matches
            if (keywords !== "" && keywords !== "undefined") {
                $.ajax({
                    url: "/PlanTrip/LandmarksFromKeywordsJSON/",
                    type: "GET",
                    data: { "keywords": keywords },
                    dataType: "json"

                    // If json data retrieval successful, display all markers on map corresponding to search query
                }).done(function (data) {
                    gMarkers[markerType] = data;
                    GMaps.prototype.addMarkersOfType(markerType);
                });
            }
            else {
                // Otherwise, if search field is blank, remove all markers of type
                if (markerType in realMarkers) {
                    GMaps.prototype.removeMarkersOfType(markerType);
                }
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