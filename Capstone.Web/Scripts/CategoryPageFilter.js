$(document).ready(function () {

    $(".checkbox_rating").on("click", function () {
        var id = $(this).attr('id');
        var checked = $(this).is(':checked');

        if(checked) {           
            //$(this).attr('checked', false);
            $("." + id).show();
            $(this).checked = false;
           
        }

        if (!checked) {             
            //$(this).attr('checked', true);
            $("." + id).hide();
            $(this).checked = true;
          
        }
       
         
    });
    });