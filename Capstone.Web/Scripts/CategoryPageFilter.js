$(document).ready(function () {

    $(".checkbox_rating input").on("change", function () {
        var id = $(this).parent().attr('id');
        var checked = this.checked;

        if (checked) {
            $("." + id).show();

        }

        if (!checked) {
            $("." + id).hide();
        }
    });

    $(".checkbox_relative_cost input").on("change", function () {
        var id = $(this).parent().attr('id');
        var checked = this.checked;

        if (checked) {
            $("." + id).show();

        }

        if (!checked) {
            $("." + id).hide();
        }

    });

    $("#openCatBtn").on("click", function (event) {
        document.getElementById("mySidenav").style.width = "250px";
    });

    $("#closeCatBtn").on("click", function (event) {
        document.getElementById("mySidenav").style.width = "0px";
    });

    function openNav() {
        document.getElementById("mySidenav").style.width = "250px";
    }

    function closeNav() {
        document.getElementById("mySidenav").style.width = "0";
    }



    $(".categoryIcons").on("click", function () {
       
        var categoryName = $(this).attr("value");
        $.ajax({
            url: "/ThingsToDo/CategoryJson/",
            type: "GET",
            data: { "category": categoryName },
            dataType: "json"

        }).done(function (data) {


            for (var i = 0; i < data.Landmarks.length; i++) {

                var name = data.Landmarks[i].Name;
                var address = data.Landmarks[i].Address;
                var phoneNumber = data.Landmarks[i].PhoneNumber;
                var description = data.Landmarks[i].Description;
                var websiteUrl = data.Landmarks[i].WebsiteUrl;
                var avgRating = data.Landmarks[i].AvgRating;
                var relativeCost = data.Landmarks[i].RelativeCost;
             
                $(".location-detail").append("<div><p>" + name + "</p>" +
                    "<p>" + address + "</p>" +
                    "<p> " + phoneNumber + "</p>" +
                    "<p> " + description + "</p>" +
                    "<p>" + websiteUrl + "</p>" +
                    "<p>" + avgRating + "</p>" +
                    "<p>" + relativeCost + "</p>" +
                    "</div>")

            }


        }

            
    )});
});