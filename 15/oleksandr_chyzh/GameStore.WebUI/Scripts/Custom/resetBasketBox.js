(function ($) {

    $("#to-home-page-link")
        .click(function (event) {
            event.preventDefault();
            $("#basket > span").text("Purchases: " + 0 + ", Total price: " + 0);
            localStorage.setItem("totalPrice", "0");
            localStorage.setItem("purchases", "0");

            document.location = this.href;
        });

})(jQuery);