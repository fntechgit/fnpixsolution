var last = [];

var max_up = parseInt($("#hdn_max").val());
var max_down = parseInt($("#hdn_min").val());

var interval = $("#hdn_interval").val();

console.log(interval);

var up_down = 1;

var up_cnt = 0;
var down_cnt = 0;

$(document).ready(function() {
    
    // reset the display
    reset();

    max_up = parseInt($("#hdn_max").val());
    max_down = parseInt($("#hdn_min").val());
    interval = $("#hdn_interval").val();

    //console.log('Reset Complete...');

    window.setInterval(move, interval);

    window.setInterval(check_force_reset, 10000);

});

function reset() {
    $(".content").each(function (index, value) {

        var top_value = index * 1012;

        $(this).css("top", top_value + "px");
    });
}

function randomUp() {

    //console.log('Random Up Fired...');

    //console.log('Max Up: ' + max_up);

    var proposed = getRandomInt(1, max_up);

    //console.log('Proposed: ' + proposed);

    allowed(proposed, 1);
}

function randomDown() {
    var proposed = getRandomInt(1, max_down);

    allowed(proposed, -1);
}

function allowed(prop, direction) {

    //console.log('Allowed Fired...');

    var current_position = parseInt($(".content").css('top'), 10);

    //console.log('Current Position: ' + current_position);

    var proposed_position = current_position + ((prop * direction) * 1012);

    if ($.inArray(proposed_position, last) > 0) {
        // the entry is not ok - run again
        if (direction == -1) {
            randomUp();
        } else {
            randomDown();
        }

    } else {
        last.push(proposed_position);

        //console.log('Last Array: ' + last);

        if (last.length >= 5) {
            last.shift();
        }

        // now do the scroll
        if (direction == 1) {
            scrollUp(prop);
        } else {
            scrollDown(prop);
        }
    }
}

function move() {

    //console.log('Move Fired...');

    if (up_or_down() == 1) {
        randomUp();
    } else {
        randomDown();
    }
}

function scrollUp(cnt) {
    $(".content").animate({ "top": "-=" + (1012 * cnt) + "px" }, 750, "linear");

    max_up = max_up - cnt;
    max_down = max_down + cnt;
}

function scrollDown(cnt) {
    $(".content").animate({ "top": "+=" + (1012 * cnt) + "px" }, 750, "linear");

    max_up = max_up + cnt;
    max_down = max_down - cnt;
}

function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1) + min);
}

function up_or_down() {
    if (max_up == 0) {
        // nowhere to go up so make it go down
        up_down = 2;
    } else if (max_down == 0) {
        // nowhere to go down, so make it go up
        up_down = 1;
    } else {
        // decide randomly
        up_down = getRandomInt(1, 2);
    }

    return up_down;
}

function check_force_reset() {
    var event_id = $("#hdn_event_id").val();

    $.ajax({
        type: "POST",
        url: "/services/media.asmx/force_refresh",
        data: "{'event_id': " + event_id + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data.d == true) {
                location.reload();
            } else {
                console.log('We are all good...');
            }
        }
    });
}