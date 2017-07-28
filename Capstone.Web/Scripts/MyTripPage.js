
$(document).ready(function () {
    if ($("#map").length) {
        var map = new GMaps({
            div: '#map',
            lat: 41.4993,
            lng: -81.6944
        });

        $(".category").on("click", function () {
            var categoryName = event.target.name;

            // Get ajax data for landmarks with passed-in value
            $.ajax({
                url: "/PlanTrip/LandmarksInCategoryJSON/",
                type: "GET",
                data: { "category": categoryName },
                dataType: "json"

             // If success, display all markers on map corresponding to value
            }).done(function (data) {
                alert("Yay!");
                for (var i = 0; i < data.length; i++) {
                    map.addMarker({
                        lat: data[i].Latitude,
                        lng: data[i].Longitude,
                        title: data[i].Name
                    });
                }
            });
        });
    }
});