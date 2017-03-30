(function($) {
    var currentCulture = $("div").find("[data-is-current-culture]").val().substring(0, 2);

    $("form").find("[data-is-async-send-button]")
        .click(function(event) {
            event.preventDefault();
            var form = $(this).parent();
            var serializedForm = form.serializeArray();

            $.ajax({
                url: "/" + currentCulture + "/Comment/NewComment",
                type: "POST",
                data: serializedForm,
                datatype: "json",
                success: function(result) {
                    $("body").find("[data-is-create-comment-form]").html(result);
                }
            });
        });

})(jQuery);