(function ($) {

    $("button[data-target='#leftPanel']")
        .click(function () {
            var resource = $("#resource-id").val();
            var resourceJson = JSON.parse(resource);

            $(this).text($("#leftPanel").attr("aria-expanded") == "true" ? resourceJson.Show : resourceJson.Hide);
        });

    $(".dinamicContainer").on("click", "button[data-is-pagination-button='true']", function() {
        var id = $(this).text();
        var pageNumber = $("form").find("[data-is-page-number-control]");
        pageNumber.val(id);
    });

    $("#filterForm")
        .submit(function(event) {
            event.preventDefault();         

            var form = $(this);
            var url = createURL(form);
            var position = url.indexOf("?") - 8;
            var apiUrl = [url.slice(0, position), "api/", url.slice(position)].join("");

            $.get(apiUrl, function (data) {
                setGames(data);
                setPaginationButtons(data);
            });

            setUrl(url);
        });

    function setPaginationButtons(data) {
        var btnGroup = $("#pagination-panel > div:first-child");
        var buttons = "";
        for (var i = 1; i <= data.PageInfo.TotalPages; i++) {
            if (data.PageInfo.PageNumber == i) {
                buttons += "<span class='selected btn-primary btn-orange'>" + i + "</span>";
            } else {
                buttons += "<button class='btn-blue' type='submit' form='filterForm' " +
                           "data-is-pagination-button='true'>" + i + "</button>";
            }
        }

        btnGroup.html(buttons);
    }

    function setGames(data) {
        var isInManagerRole = $("#is-in-manager-role").val();
        var resource = $("#resource-id").val();
        var resourceJson = JSON.parse(resource);
        var result = "";
        $(data.Games)
            .each(function () {
                result += createHtmlGame(this, resourceJson, isInManagerRole);
            });

        $("#game-container").html(result);
    }

    function createHtmlGame(game, resource, isInManagerRole) {
        var result = "<div class='item-box'>";
        result += "<dl>";
        
        var currentCulture = $("div").find("[data-is-current-culture]").val().substring(0, 2);
        var gameName = game.LanguagesNames[currentCulture];
        if (gameName == null) {
            gameName = game.LanguagesNames["ru"];
        }

        result += createDlPair(resource.Name, gameName);
        result += createDlPair(resource.Price, game.Price);
        result += "</dl>";

        result += "<a href='/" + currentCulture + "/game/" + game.Key + "'>" + resource.Details + "</a><br />";

        if (isInManagerRole == "True") {
            result += "<a href='/" + currentCulture + "/game/" + game.Key + "/Update'>" + resource.Edit + "</a><br />";
            result += "<a href='/" + currentCulture + "/game/" + game.Key + "/Remove'>" + resource.Remove + "</a><br />";
        }

        result += "</div>";

        return result;
    }

    function createURL(form) {

        var pageNumber = form.find("[name='PageInfo.PageNumber']").val();
        var pageSize = form.find("[name='PageInfo.PageSize']").val();
        var gameName = form.find("[name='GameName']").val();
        var sortingObject = form.find("[name='SortingObject']").val();
        var publishingDatePeriod = form.find("[name='PublishingDatePeriod']").val();
        var minPrice = form.find("[name='MinPrice']").val();
        var maxPrice = form.find("[name='MaxPrice']").val();

        var genres = form.find("input:checked[name='SelectedGenresIds']");
        var platformTypes = form.find("input:checked[name='SelectedPlatformTypesIds']");
        var publishers = form.find("input:checked[name='SelectedPublishersIds']");

        var url = form[0].action;

        url += "?PageInfo.PageNumber=" + pageNumber;
        url += "&PageInfo.PageSize=" + pageSize;
        
        if (gameName != "") {
            url += "&GameName=" + gameName;
        }

        url += "&SortingObject=" + sortingObject;
        url += "&PublishingDatePeriod=" + publishingDatePeriod;
        url += "&MinPrice=" + minPrice;
        url += "&MaxPrice=" + maxPrice;

        genres.each(function() {
            url += "&SelectedGenresIds=" + $(this).val();
        });

        platformTypes.each(function () {
            url += "&SelectedPlatformTypesIds=" + $(this).val();
        });

        publishers.each(function () {
            url += "&SelectedPublishersIds=" + $(this).val();
        });

        return url;
    }

})(jQuery);

function createDlPair(dt, dd) {
    return  "<dt>" + dt + "</dt>" +
            "<dd>" + dd + "</dd>";
}

function setUrl(url) {
    try {
        history.pushState(null, null, url);
        return;
    } catch (e) { }
    location.hash = "#" + url;
}