var audio = new Audio("/sounds/notif-sound.wav");

const client = new signalR.HubConnectionBuilder()
    .withUrl('/notifications')
    .build();

client.on('NewNotificationReceived', data => {

    if (CheckUser(data[0].receiverid) == 0) {

        audio.play();

        var count = GetCount(data[0].receiverid);
        if (count > 0) {
            $(".countitem").html(count);
        }

        var link = "../../" + data[0].url;
        const dateTimeAgo = moment(data[0].dateCreated).fromNow().fontsize(1);
        var description = data[0].description;

        var subject = data[0].subject;

        var read = '';
        var unread = '';
        var img_path = '';
        
        if (data[0].isRead == 0) {
            read = '&nbsp;&nbsp;<div><i class="fas fa-circle notif-circle"></i><button type="button" class="btn btn-delete' + data[0].id + '" onclick="deleteNotif(' + data[0].id + ');"><i class="fas fa-times" style="color:gray"></i></button></div>';
            unread = 'notif-unread';
        }
        else {
            read = '<div><button type="button" class="btn btn-delete' + data[0].id + '" onclick="deleteNotif(' + data[0].id + ');"><i class="fas fa-times" style="color:gray"></i></button></div>';
        }

        if (data[0].imagePath == "" || data[0].imagePath == null) {
            img_path = '<div style="width:50;height:50px;border-radius:100px;border:1px solid gray;text-align:center;"><i class="fas fa-user-tie fa-3x" style="color:gray;margin-top:5px"></i>';
        }
        else {
            img_path = '<img style="width:60px;height:60px; " src="' + data[0].imagePath + '" class="notif-img">';
        }

        $("#notifList tbody").prepend('<tr class="trbody idselectt">'
            + '<td style="width:65px" class="notif-item ' + unread + '"><a class="notif-update" data-id="' + data[0].id + '"  href="' + link + '">' + img_path + '</a></td>'
            + '<td class="notif-item ' + unread + '"><a class="notif-update" data-id="' + data[0].id + '"  href="' + link + '">'
            + '<span style="font-size:16px!important;text-align:left!important;">' + subject + '</span><br>'
            + '<span class="notif-description">' + description + '</span><br><span class="time">' + dateTimeAgo
            + '</span></a></td>'
            + '<td style="width:20px" class="' + unread + '">' + read + '</td>'
            + '</tr>');

        if (count > 0) {
            $("#notif-foot").empty();
            $("#notif-foot").append('End Of Result...<br><div class="w-100 notif-seeall" style="text-align:center!important"><a href="../../D_Notification/list">See All Notifications...</a></div>');
        }
    }
});
    
function CheckUser(userId) {
    var status = 0;
    $.ajax({
        url: '/D_Notification/CheckUser',
        data: { userId: userId },
        type: "GET",
        datatype: "json",
        async: false
    }).done(function (data) {
        status = data;
    }).fail(function () {
        alert("There was an Error When Loading Data...");
    });
    return status;
}

function GetCount(userId) {
    var status = 0;
    $.ajax({
        url: '/D_Notification/GetCount',
        data: { userId: userId },           
        type: "GET",
        datatype: "json",
        async: false
    }).done(function (data) {
        status = data;
    }).fail(function () {
        alert("There was an Error When Loading Data...");
    });
    return status;
}



function SendNotification() {
    $.ajax({
        url: '/D_Notification/SendNotification',
        type: "GET",
        datatype: "json",
        async: false
    }).done(function (data) {
        console.log('Successfully Notified...');
    }).fail(function () {
        alert("There was an Error When Loading Data...");
    });
}


function notif() {
    $('#notifList tbody').empty();
    $("#notif-foot").empty();

    $.ajax({
        url: '/D_Notification/GetNotificationListReceiver',
        data: {
            flag: 1
        },
        type: "GET",
        datatype: "json",
        async: false
    }).done(function (data) {
        var img_path = "";
        for (var i = 0; i < data.length; i++) {

            var link = "../../" + data[i].url;
            const dateTimeAgo = moment(data[i].dateCreated).fromNow().fontsize(1);
            var description = data[i].description;
            var subject = data[i].subject;
            var read = '';
            var unread = '';
            if (data[i].isRead == 0) {
                read = '<div><i class="fas fa-circle notif-circle"></i><button type="button" class="btn btn-delete' + data[i].id + '" onclick="deleteNotif(' + data[i].id + ');"><i class="fas fa-times" style="color:gray"></i></button></div>';
                unread = 'notif-unread';
            }
            else {
                read = '<div><button type="button" class="btn btn-delete' + data[i].id + '" onclick="deleteNotif(' + data[i].id + ');"><i class="fas fa-times" style="color:gray"></i></button></div>';
            }

            if (data[i].imagePath == "" || data[i].imagePath == null) {
                img_path = '<div style="width:50;height:50px;border-radius:100px;border:1px solid gray;text-align:center;"><i class="fas fa-user-tie fa-3x" style="color:gray;margin-top:5px"></i>';
            }
            else {
                img_path = '<img style="width:60px;height:60px; " src="' + data[i].imagePath + '" class="notif-img">';
            }

            console.log(img_path);

            if (data[i].count > 0) {

                $(".countitem").html(data[i].count);
            }
            $("#notifList tbody").append('<tr class="trbody idselectt">'
                + '<td style="width:65px" class="notif-item ' + unread + '"><a class="notif-update" data-id="' + data[i].id + '" href="' + link + '">' + img_path + '</a></td>'
                + '<td class="notif-item ' + unread + '"><a class="notif-update" data-id="' + data[i].id + '" href="' + link + '">'
                + '<b style="font-size:16px!important;text-align:left!important;">' + subject + '</b><br>'
                + '<span class="notif-description">' + description + '</span><br><span class="time">' + dateTimeAgo
                + '</span></a></td>'
                + '<td style="width:20px" class="' + unread + ' text-center">' + read + '</td>'
                + '</tr>');
        }

        var rowCount = $("#notifList tbody .idselectt").length;
        if (rowCount == 0) {
            $("#notif-foot").append('No New Notification');
        } else {
            $("#notif-foot").append('End Of Result...<br><div class="w-100 notif-seeall" style="text-align:center!important"><a href="../../D_Notification/list">See All Notifications...</a></div>');
        }

    });
}

function deleteNotif(id) {
    $.ajax({
        url: '/D_Notification/Delete',
        data: {
            id: id
        },
        type: "POST",
        datatype: "json"
    }).done(function (data) {
        notif();
        var rowCount = $("#notifList tbody .idselectt").length;
        if (rowCount == 0) {
            $(".countitem").html('');
        }

    }).fail(function () {
        alert("There was an Error When Loading Data...");
    });
}
