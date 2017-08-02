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
                for (var i = 0; i < data.length; i++)
                {
                    var name = data[i].Name;
                    var address = data[i].Address;
                    var phoneNumber = data[i].PhoneNumber;
                    var description = data[i].Description;
                    var websiteUrl = data[i].WebsiteUrl;
                    var relativeCost = data[i].RelativeCost;
                    var avgRating = data[i].AvgRating;
                }
               

            }               
        )});

});