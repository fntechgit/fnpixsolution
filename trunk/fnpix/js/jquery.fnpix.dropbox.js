function approve() {

    var images = [];

    $(".mg-files :checked").each(function () {
        images.push($(this).attr("id").replace("file_", ""));
    });


    $.ajax({
        type: "POST",
        url: "/services/manager.asmx/approve_dropbox",
        data: "{'images': '" + images + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function () {
            location.reload();
        }
    });

}

function unapprove() {
    var images = [];

    $(".mg-files :checked").each(function () {
        images.push($(this).attr("id").replace("file_", ""));
    });


    $.ajax({
        type: "POST",
        url: "/services/manager.asmx/unapprove_dropbox",
        data: "{'images': '" + images + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function () {
            location.reload();
        }
    });
}

