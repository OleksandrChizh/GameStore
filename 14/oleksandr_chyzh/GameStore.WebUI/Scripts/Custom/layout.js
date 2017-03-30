(function ($) {
    var currentCulture = $("div").find("[data-is-current-culture]").val().substring(0, 2);

    $("#basket")
        .click(function () {
            document.location = "/" + currentCulture + "/basket";
        });

    var purchases = localStorage.getItem("purchases") || "0";
    var totalPrice = localStorage.getItem("totalPrice") || "0";
    $("#basket > span").text("Purchases: " + purchases + ", Total price: " + totalPrice);

    $("select[name='lang'] > option")
        .each(function() {
            var target = $(this);
            if (target.val() == currentCulture) {
                target.attr("selected", "true");
            } else {
                target.removeAttr("selected");
            }
        });

})(jQuery);