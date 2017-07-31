$(document).ready(function () {

    $(".checkbox_rating").on("click", function () {
        var id = $(this).attr('id');
        var checked = $(this).is(':checked');

        if(checked) {
            //$('[name = "avgRating-4"]').val(true); 
            $(this).attr('checked', false);
            $("." + id).show();
        }

        if (!checked) {
            //$('[name = "avgRating-4"]').val(false);
            $(this).attr('checked', true);
            $("."+ id).hide();
        }
       
         
    });
    });