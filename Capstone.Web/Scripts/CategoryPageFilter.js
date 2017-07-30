$(document).ready(function () {

    $(".checkbox_rating").on("click", function () {
        var name = event.target.name;
        //var address = event.target.address;
        //var phoneNumber = event.target.phoneNumber;
        //var rating = event.target.avgRating;
        //var description = event.target.description;
        //var websiteUrl = event.target.websiteUrl;
        if ($(this).not(':checked')) {
            $.ajax({
                url: "/ThingsToDo/Category/",
                type: "GET",
                data: {
                    "name": name,
                    
                },
                dataType: "json"
            }).done(function (data) {
                $("."+ name).removeData();
            });
        }


        //    var inputValue = $(this).val();
        //    $("."+ inputValue + "-rating").show();
        //}
        //else {
        //    var inputValue = $(this).val();
        //    $("." + inputValue + "-rating").hide();
        //}
        //    $("#category_info").show();      
        //}
        //else {
        //    $("#category_info").hide();      
        //}
    });
    });