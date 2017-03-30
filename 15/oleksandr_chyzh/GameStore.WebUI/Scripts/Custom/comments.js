(function ($, undefined) {
    var currentCulture = $("div").find("[data-is-current-culture]").val().substring(0, 2);

    var itIsSecondClick = false;
    $("#comments-link").click(function (event) {
        event.preventDefault();
        var buttonText;

        if (itIsSecondClick) {
            $("#comments").empty();
            buttonText = $("#comments-resource").val();
        } else {
            var apiUrl = createApiUrl(this.href);
            $.get(apiUrl, function (data) {
                setComments(data);
                setHandlers();
            });

            buttonText = $("#hide-resource").val();
        }

        $("#comments-link").html(buttonText);
        itIsSecondClick = !itIsSecondClick;
    });

    function setHandlers() {
        $("#comments").on("submit", ".comment-block .new-comment-form", submitHandler);

        var lastCommentId = -1;
        var lastAnserType;
        $(".answer-link")
            .click(function(event) {
                event.preventDefault();

                $(".new-comment-form").remove();
                $(".comment-block").remove();
                var target = $(this);
                var commentId = target.attr("data-comment-id");
                var answerType = target.attr("data-answer-type");
                var parent;
                var title;

                var isSecondClick = lastCommentId == commentId && answerType == lastAnserType;
                if (isSecondClick) {
                    parent = $(".answer-link").last().parent();
                    answerType = "new";
                    title = $("#new-comment-resource").val();
                    commentId = null;
                } else {
                    parent = target.parent();
                    title = $("#" + answerType + "-resource").val();                   
                }

                var commentForm = createCommentForm(answerType, commentId, title);
                $(commentForm).insertAfter(parent);
                lastCommentId = commentId;
                lastAnserType = answerType;
            });
    }

    function submitHandler(event) {
        event.preventDefault();
        var formData = $(this).serializeObject();
        currentCulture = $("div").find("[data-is-current-culture]").val().substring(0, 2);
        var apiUrl = "/api/" + currentCulture + "/games/" + $("#game-id").val() + "/comments";

        var AuthorizationCookieKey = ".ASPXAUTH";
        var authorizationTicket = getCookie(AuthorizationCookieKey);

        $.ajax({
            type: "POST",
            url: apiUrl,
            contentType: "application/json",
            data: JSON.stringify(formData),
            headers: { "Authorization": authorizationTicket }
        }).done(function () {
            $("#comments").off("submit", ".comment-block .new-comment-form", submitHandler);
            itIsSecondClick = !itIsSecondClick;
            $("#comments-link").click();
        });        
    }

    $.fn.serializeObject = function () {
        var object = {};
        var array = this.serializeArray();
        $.each(array, function () {
            if (object[this.name] !== undefined) {
                if (!object[this.name].push) {
                    object[this.name] = [object[this.name]];
                }
                object[this.name].push(this.value || "");
            } else {
                object[this.name] = this.value || "";
            }
        });
        return object;
    };

    function setComments(data) {
        var container = $("#comments");
        var result = "";

        result += "<ul>";
        $(data).each(function () { result += createComment(this); });
        result += "</ul>";

        if ($("#can-user-comment").val() == "True") {
            var title = $("#new-comment-resource").val();
            result += createCommentForm("new", null, title);
        }

        container.html(result);
    }

    function createCommentForm(type, parentCommentId, title) {
        var result = "<div class='comment-block'>";
        result += "<h4>" + title + "</h4>";
        result += "<form class='new-comment-form'>";
        result += "<input type='text' hidden='hidden' name='IsQuote' value='" + (type == "quote" ? "True" : "False") + "' />";

        if (type == "answer" || type == "quote") {
            result += "<input type='text' hidden='hidden' name='ParentCommentId' value='" + parentCommentId + "' />";
        }

        result += "<input type='text' hidden='hidden' name='GameKey' value='" + $("#game-key").val() + "' />";
        result += "<label for='Body'>" + $("#body-resource").val() + "</label><br />";
        result += "<textarea class='form-control' cols='20' id='Body' name='Body' rows='5'></textarea>";
        result += "<input class='btn-orange comment-submit-button' type='submit' value=" + $("#send-resource").val() + " />";
        result += "</form>";
        result += "</div>";

        return result;
    }

    function createComment(comment) {
        var result = "<li>";

        if (comment.RepliedTo != null && comment.RepliedTo != "") {
            result += "<i><sub>" + $("#replied-to-resource").val() + " " + comment.RepliedTo + "</sub></i><br />";
        }

        if (comment.IsQuote) {
            if (comment.Quote == null || comment.Quote == "") {
                result += "<blockquote>" + $("#comment-was-deleted-resource") + "</blockquote>";
            } else {
                result += "<blockquote>&ldquo;" + comment.Quote + "&rdquo;</blockquote>";
            }
        }

        result += "<b>" + comment.Name + ":</b><span> " + comment.Body + "</span><br />";

        if ($("#can-user-comment").val() == "True") {
            result += "<a class='answer-link' data-answer-type='answer' href='' data-comment-id='" + comment.Id + "' >" + $("#answer-resource").val() + "</a>&ensp;";
            result += "<a class='answer-link' data-answer-type='quote' href='' data-comment-id='" + comment.Id + "' >" + $("#quote-resource").val() + "</a>";
        }

        result += "<br /><br /></li>";

        return result;
    }

    function createApiUrl(url) {
        url = url.replace("game", "games");
        var position = url.indexOf("games") - 3;
        var apiUrl = [url.slice(0, position), "api/", url.slice(position)].join("");

        return apiUrl;
    }

    // For comments page below
    ////////////////////////////////////////////////////////////

    // Delete Comment Click
    var isAnimationExecuting = false;
    $(".dinamicContainer").on("click", "a[data-is-delete-comment-link]", function(event) {
        event.preventDefault();
        var target = $(this);

        if (isAnimationExecuting) {
            target.stop();
        } else {
            runDeletingCommentProcess(target);
        }
    });

    function runDeletingCommentProcess(target) {
        var confirmedDeleting = confirm("Comment will be deleted");
        if (confirmedDeleting) {
            target.animate({ opacity: 0.25 },
                {
                    duration: 5000,
                    start: function () {
                        isAnimationExecuting = true;
                        target.text($("#cancel-resource").val());
                    },
                    done: function () {
                        deleteComment(target);
                    },
                    always: function() {
                        target.text($("#delete-resource").val());
                        target.css("opacity", "1");
                        isAnimationExecuting = false;
                    }
                });
        }
    }

    function deleteComment(target) {
        var id = target.attr("data-comment-id");
        var comment = {};
        comment["commentId"] = id;

        $.ajax({
            type: "POST",
            url: "/" + currentCulture + "/Comment/DeleteComment",
            data: comment
        }).done(function () {
            location.reload();
        });            
    }

    showCreateCommentForm();
    function showCreateCommentForm() {
        var ul = $("#comments-container");
        var gameKey = $("#game-key").val();

        insertCreateCommentForm(ul, gameKey, null, false);
    }

    // Add Comment Click
    var lastParentCommentIdForAnswer;
    var lastValueOfQuote;
    var isAnswerFormOpen;
    $(".dinamicContainer").on("click", "a[data-is-answer-to-comment-link]", function (event) {
        event.preventDefault();
        $("div").find("[data-is-comment-form]").remove();
        $("body").find("[data-is-create-comment-form]").remove();
        var li = $(this).parent();
        var gameKey = $(this).attr("data-game-key");
        var parentCommentId = $(this).attr("data-parent-comment-id");
        var isQuote = $(this).attr("data-is-quote");

        if (lastParentCommentIdForAnswer === parentCommentId && lastValueOfQuote === isQuote && isAnswerFormOpen) {
            isAnswerFormOpen = false;
            var ul = li.parent();
            insertCreateCommentForm(ul, gameKey, null, false);

        } else {
            isAnswerFormOpen = true;
            insertCreateCommentForm(li, gameKey, parentCommentId, isQuote);
        }
        lastParentCommentIdForAnswer = parentCommentId;
        lastValueOfQuote = isQuote;
    });

    function insertCreateCommentForm(after, gameKey, parentCommentId, isQuote) {
        var div = $("<div data-is-create-comment-form='true'></div>");
        div.insertAfter(after);

        var arguments = {};
        arguments["parentCommentId"] = parentCommentId;
        arguments["gameKey"] = gameKey;
        arguments["isQuote"] = isQuote;

        $.ajax({
            url: "/" + currentCulture + "/Comment/NewComment",
            type: "GET",
            data: arguments,
            success: function (result) {
                div.html(result);
            }
        });
    }

})(jQuery);

function getCookie(name) {
    var matches = document.cookie.match(new RegExp(
      "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, "\\$1") + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}