var current_template = 0;

console.log('Starting Process');

function refresh() {
    // check for a new template

    var event_id = $("#hdn_event_id").val();

    $.ajax({
        type: "POST",
        url: "/services/media.asmx/find",
        data: "{'event_id': " + event_id + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            switch(data.d.view_id) {
                case 1002:
                    if (current_template !== 1002) {
                        $("#content").load("http://fnpix.fntech.com/displays/magicwall/" + event_id + "/" + data.d.slide_duration);
                        current_template = 1002;
                    }
                    break;
                case 1006:
                    if (current_template !== 1006) {
                        $("#content").load("http://fnpix.fntech.com/displays/instagram-twitter/" + event_id + "/" + data.d.slide_duration);
                        current_template = 1006;
                    }
                    break;
            }
        }
    });
}


setInterval(refresh, 10000);