

function PushExpense() {

}


var world = $.connection.world;
$(document).ready(function () {
    world.updateExplorer = function (explorer) {
        var explorer = jQuery.parseJSON(explorer);
        alert(explorer.Name + " has moved");
    };
    $("#tryIt").click(function () {
        var explorer;
        explorer = "{'name':'Panda','statusmessage':'I am Panda!','location':{'x':'10','y':'10'}}";
        world.explore(explorer, "");
    });
    $("#joinGroup").click(function () {
        world.join("kuma");
    });
    $("#groupTry").click(function () {
        var explorer;
        explorer = "{'name':'Kuma','statusmessage':'I am Kuma!','location':{'x':'10','y':'10'}}";
        world.explore(explorer, "kuma");
    });
    $.connection.hub.start();
});