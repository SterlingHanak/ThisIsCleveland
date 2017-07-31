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

        $("checkbox_relative_cost input").on("change", function () {
            var id = $(this).parent().attr('id');
            var checked = this.checked;

            if (checked) {
                $("." + id).show();

            }

            if (!checked) {
                $("." + id).hide();
            }

    });





});