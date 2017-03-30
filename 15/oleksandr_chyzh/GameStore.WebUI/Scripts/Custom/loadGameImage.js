(function ($) {

    $("#load-image-http-async").click(function(event) {
        event.preventDefault();
        var url = "/games/" + $("#game-key").val() + "/httpAsyncLoad";
        loadImage(url, "game-image-http-picker");
    });

    $("#load-image-async").click(function(event) {
        event.preventDefault();
        loadImage($("#load-image-async-url").val(), "game-image-picker-async");
    });

    $("#load-image-sync").click(function (event) {
        event.preventDefault();
        loadImage($("#load-image-sync-url").val(), "game-image-picker-sync");
    });

    function loadImage(url, imageSelectorId) {
        var selector = document.getElementById(imageSelectorId);
        var files = selector.files;
        if (files.length > 0) {
            var data = new FormData();
            for (var i = 0; i < files.length; i++) {
                data.append(files[i].name, files[i]);
            }

            $.ajax({
                    type: "POST",
                    url: url,
                    contentType: false,
                    processData: false,
                    data: data
                })
                .done(function() {
                    location.reload();
                });
        }
    }

})(jQuery);