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
                    icon: "/Content/Markers/" + categoryName + "_marker.png",
                    click: function () {
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
    }
});