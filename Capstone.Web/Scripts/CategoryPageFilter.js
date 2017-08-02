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
            var categoryName = $(this).val();

            if ($(this).is(':clicked')) {
                $.ajax({
                    url: "/ThingsToDo/CategoryJson/",
                    type: "GET",
                    data: { "category": categoryName },
                    dataType: "json"
                });

        }

});