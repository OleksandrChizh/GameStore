(function($) {
    var currentCulture = $("div").find("[data-is-current-culture]").val().substring(0, 2);

    updateBasketBox();

    $("div").find("[data-is-change-quantity-control]")
        .change(function() {
            var id = $(this).attr("data-game-id");
            var value = $(this).val();

            if (value == null) {
                return;
            }

            if (value == 0) {
                $(this).parent().remove();
            }

            var form = {};
            form["Id"] = id;
            form["Quantity"] = value;
            var url = "/" + currentCulture + "/Basket/ChangeGameQuantity";
            $.post(url, form).then(function() { updateBasketBox(); });
        });

    function updateBasketBox() {
        var AuthorizationCookieKey = ".ASPXAUTH";
        var authorizationTicket = getCookie(AuthorizationCookieKey);
        $.ajax({
            type: "PUT",
            url: "/api/" + currentCulture + "/Orders",
            dataType: "json",
            headers: { "Authorization": authorizationTicket }
        })
        .then(function (orderId) {
            return $.ajax({
                type: "GET",
                url: "/api/" + currentCulture + "/Orders/" + orderId,
                dataType: "json",
                headers: { "Authorization": authorizationTicket }
            });
        })
        .then(function (order) {
            var amountOfPurchases = order.OrderDetails.length;
            localStorage.setItem("purchases", amountOfPurchases);

            var totalPrice = 0;
            $(order.OrderDetails)
                    .each(function () {
                        totalPrice += this.Price * this.Quantity;
                    });

            localStorage.setItem("totalPrice", totalPrice);
            $("#total-price").text(totalPrice);
            $("#basket > span").text("Purchases: " + amountOfPurchases + ", Total price: " + totalPrice);
        });
    }

})(jQuery);

function getCookie(name) {
    var matches = document.cookie.match(new RegExp(
      "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, "\\$1") + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}