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


    /************************************************
    * CATEGORY TOGGLE
    *************************************************/

    $(".categoryIcons").on("click", function () {
       
        $(".location-row").empty();

        var categoryName = $(this).attr("value");
        $.ajax({
            url: "/ThingsToDo/CategoryJson/",
            type: "GET",
            data: { "category": categoryName },
            dataType: "json"

        }).done(function (data) {

            $("#currentCategoryTitle").html(categoryName);

            for (var i = 0; i < data.Landmarks.length; i++) {

                var name = data.Landmarks[i].Name;
                var address = data.Landmarks[i].Address;
                var phoneNumber = data.Landmarks[i].PhoneNumber;
                var description = data.Landmarks[i].Description;
                var websiteUrl = data.Landmarks[i].WebsiteUrl;
                var avgRating = data.Landmarks[i].AvgRating;
                var relativeCost = data.Landmarks[i].RelativeCost;

                $(".location-row").append("<div class='" + avgRating + "-rating " + relativeCost + "-cost col-md-4 location-detail'>" +
                    "<div class='detail-container'><h2 style='margin-top: 10px; font-size: 25px;'>" + name + "</h2>" +
                    "<img style='height: 275px; max-width: 100%;' class='img-fluid' src='../Content/Images/" +
                    categoryName.replace(/\s/g, '') + "/" + name.replace(/\s/g, '_').replace(":", "").replace("'", "") + ".jpg'/></div>" +
                    "<div class='overlay' style='color: #FDBB30; margin: auto;'>" +
                    "<h2 style='margin-top: 10px; font-size: 25px;'>" + name + "</h2>" +
                    "<p>" + address + "</p>" +
                    "<p><span class='glyphicon glyphicon-phone-alt'></span> " + phoneNumber + "</p>" +
                    "<p> " + description + "</p>" +
                    "<p><span class='glyphicon glyphicon-globe'></span> " + websiteUrl + "</p></div>" +
                    "<button type='submit'>Add To Trip</button>");
            }
        }            
    )});
});