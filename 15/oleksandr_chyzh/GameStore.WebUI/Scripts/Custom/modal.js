(function ($) {

    $(function() {
        $.ajaxSetup({ cache: false });
        $(".modal-link")
            .click(function(e) {

                e.preventDefault();
                $.get(this.href,
                    function(data) {
                        $("#dialogContent").html(data);
                        $("#modDialog").modal("show");
                    });
            });
    });

})(jQuery);