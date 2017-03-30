(function ($) {

    var form = $("#filterForm").serialize();
    $("#filter")
        .click(function (event) {
            var form2 = $("#filterForm").serialize();
            if (form === form2) {
                event.preventDefault();
            }
        });

})(jQuery);