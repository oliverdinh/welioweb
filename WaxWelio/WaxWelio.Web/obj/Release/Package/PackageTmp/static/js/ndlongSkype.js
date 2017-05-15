/// <reference path="ndlongSkype.js" />
var chatConver;
var chatContent;
var popupCall;
var vsBrowser = detectIE();
var listeners = [];
var isAccept = false;
var isVideoCall = false;
var callId;
var timeMiss = 160000;
var baseUrl = window.location.origin;

$('#btn-call-patient').on("click", function () {
    //if (window.isLogin) {
    //    if (vsBrowser === 'Firefox' || vsBrowser === 'Safari' || vsBrowser === "Other browser") {
    //        alert('You may be using ' + vsBrowser + '. To use Call feature, please change to use Microsoft Edge, Chrome. Thank you very much!');
    //    } else if (vsBrowser === 'Edge') {
    //        if (isAccept) {
    //            console.log('Edge - video/audio');
    //            startCall(window.conferenceId, isVideoCall);
    //        } else {
    //            navigator.getUserMedia({ video: true, audio: true }, function () {
    //                console.log('Edge - video/audio');
    //                isAccept = true;
    //                isVideoCall = true;
    //                startCall(window.conferenceId, true);
    //            }, function () {
    //                navigator.getUserMedia({ audio: true }, function () {
    //                    console.log('Edge - audio');
    //                    startCall(window.conferenceId, false);
    //                }, function (e) {
    //                    alert('Welio cannot connect to your microphone. Please check your device and try again.');
    //                    console.log(e);
    //                });
    //            });
    //        }

    //    } else {
    //        if (isAccept) {
    //            console.log('Other - audio');
    //            startCall(window.conferenceId, false);
    //        } else {
    //            navigator.getUserMedia({ audio: true }, function () {
    //                console.log('Other - audio');
    //                startCall(window.conferenceId, false);
    //            }, function (e) {
    //                alert('Welio cannot connect to your microphone. Please check your device and try again.');
    //                console.log(e);
    //            });
    //        }
    //    }
    //} else {
    //    //Start link login 365
    //    var isConfirm = confirm('You need to sign in with Microsoft Office 365 to use this feature. Do you want to sign in?');
    //    if (isConfirm) {
           
    //        location.assign("https://login.windows.net/common/oauth2/authorize?response_type=token&client_id=2bd937f8-d693-4e38-8718-2f53deed2dff&redirect_uri=" + baseUrl + "/SkypeForBusiness/Login&resource=https://webdir.online.lync.com");
    //    }
    //}
    $('#overlay-div-detail').show();
    var meetingUri = $(this).attr("data-meetingUri");
    var meetingUrl = $(this).attr("data-meetingUrl");
    var appointmentId = $(this).attr('data-appointmentId');
    var patientName = $('#app-PatientName').text();
    if (meetingUri != "" && meetingUrl != "") {
        var url = $(this).data("url") + "?meetingUrl=" + meetingUrl + "&meetingUri=" + meetingUri + "&appointmentId=" + appointmentId + "&patientName=" + patientName;
        window.open(url, '_blank');
        $.ajax({
            url: window.baseUrl + '/Appointment/UpdateStatus',
            type: "POST",
            data: {
                id: appointmentId,
                status: 2
            },
            success: function (data) {
                $('#app-Status').text("In Progress");
                $('#overlay-div-detail').hide();
                if (data.Key === false) {
                    alert(data.Value);
                    window.location.href = baseurl;
                }
            },
            error: function (xhr, textStatus, error) {
                $('#overlay-div-detail').hide();
                window.location.href = window.baseUrl + '/SignOut/User';
            }
        });
    }
});

$('#btn-message').on('click', function () {
    alert("Coming Soon.")
    //$('#appointment-detail-wrapper').removeClass('scroll-info').addClass('non-scroll-info');
    //$('#btn-appointment').find('.icon-select').html('<img src="' + baseUrl + '/static/images/ic_appoinment_white.png" alt="">');
    //$('#btn-appointment').find('.title-select').css('color', '#fff');
    //$('#btn-message').find('.icon-select').html('<img src="' + baseUrl + '/static/images/ic_messages_green.png" alt="">');
    //$('#btn-message').find('.title-select').css('color', '#5dc7d0');
    //$('#apointment-detail-patient').hide();
    //$('#message-patient').show();
    //if (isLogin) {
    //    $('.isLogin').show();
    //    $('.not-login').hide();
    //} else {
    //    $('.isLogin').hide();
    //    $('.not-login').show();
    //}
    //$('#btn-message').addClass("btn-selected").removeClass("btn-unselected");
    //$('#btn-appointment').addClass("btn-unselected").removeClass("btn-selected");
    //if (conferenceId !== currentId || chatConver === undefined || chatConver === null) {
    //    $('#content-msg').empty();
    //    $('#content-msg-call').empty();
    //    startConversation(conferenceId);
    //}
});

$('#btn-send').on('click', function () {
    if ($('#msg-value').val() === "") {
        alert('Please enter your message');
        return;
    }
    chatConver.chatService.sendMessage($('#msg-value').val());
    $('#msg-value').val('');
});

$('#msg-value').on('keydown', function (e) {
    if (e.which === 13) {
        if ($('#msg-value').val() === "") {
            alert('Please enter your message');
            return;
        }

        chatConver.chatService.sendMessage($('#msg-value').val());
        $('#msg-value').val('');
    }
});

$('#btn-send-call').on('click', function () {
    if ($('#msg-value-call').val() === "") {
        alert('Please enter your message');
        return;
    }

    chatConver.chatService.sendMessage($('#msg-value-call').val());
    $('#msg-value-call').val('');
});

$('#msg-value-call').on('keydown', function (e) {
    if (e.which === 13) {
        if ($('#msg-value-call').val() === "") {
            alert('Please enter your message');
            return;
        }

        chatConver.chatService.sendMessage($('#msg-value-call').val());
        $('#msg-value-call').val('');
    }
});

function startChat(uriConference) {
    currentId = uriConference;
    chatConver = client.conversationsManager.getConversationByUri(uriConference);
    // chatConver.isHistoryEnabled = true;
    $('#content-msg').empty();
    $('#content-msg-call').empty();
    listeners.push(chatConver.chatService.state.changed(function (newState) {
        console.log('Chat: ' + newState);
        if (newState === 'Connected') {
            $('#msg-value').prop('disabled', false);
            $("#btn-send").prop('disabled', false);
            $('#msg-value-call').prop('disabled', false);
            $("#btn-send-call").prop('disabled', false);
            $('#content-msg').empty();
            $('#content-msg-call').empty();
            var olddata = LocalStorage.GetData(appointmentId);
            $.each(olddata, function (i, msg) {
                $('#content-msg').append(msg);
                $('#content-msg-call').append(msg);
            });
            $('#content-msg').animate({ scrollTop: $('#content-msg').prop("scrollHeight") }, 100);
            $('#content-msg-call').animate({ scrollTop: $('#content-msg-call').prop("scrollHeight") }, 100);
        } else {
            $('#msg-value').prop('disabled', true);
            $("#btn-send").prop('disabled', true);
            $('#msg-value-call').prop('disabled', true);
            $("#btn-send-call").prop('disabled', true);
            if (newState === 'Disconnected') {
                $('.can-not-join').remove();
                $('#content-msg').append('<div class="can-not-join">Can not join meeting</div>');
            }
            if (newState === "Connecting") {
                $('.can-not-join').remove();
                $('#content-msg').append('<div class="can-not-join">Connecting...</div>');
            }
        }
    }));
    listeners.push(chatConver.historyService.activityItems.added(function (newMsg) {
        if (newMsg.type() === 'TextMessage') {
            var dt = new Date();
            chatContent = '<div class="col-lg-12 padding-top-1">' +
                '<span class="pull-left c-title">' +
                newMsg.sender.displayName() +
                '</span>' +
                '<span class="pull-right  c-title">' +
                dt.toLocaleTimeString() +
                '</span><br />' +
                '<div style="padding: 8px; background-color: white;">' +
                newMsg.text() +
                '</div></div>';
            LocalStorage.PushHtml(appointmentId, chatContent);
            $('#content-msg').append(chatContent);
            $('#content-msg').animate({ scrollTop: $('#content-msg').prop("scrollHeight") }, 100);
            $('#content-msg-call').append(chatContent);
            $('#content-msg-call').animate({ scrollTop: $('#content-msg-call').prop("scrollHeight") }, 100);
            if (newMsg.direction() === 'Incoming') {
                //
            }
        }
    }));

    chatConver.chatService.start();
}


function startConversation(uriConference) {
    $('#msg-value').prop('disabled', true);
    $("#btn-send").prop('disabled', true);
    $('#msg-value-call').prop('disabled', true);
    $("#btn-send-call").prop('disabled', true);
    $('#content-msg').empty();
    $('#content-msg').append('<div class="can-not-join">Connecting...</div>');
    try {
        for (var i = 0; i < listeners.length; i++) {
            listeners[i].dispose();
        }
        listeners = [];
        if (chatConver) {
            if (chatConver.state() !== 'Disconnected') {
                //                chatConver.leave().then(() => {
                //                        client.conversationsManager.conversations.remove(chatConver);
                //                        startChat(uriConference);
                //                    });
                chatConver.leave();
                client.conversationsManager.conversations.remove(chatConver);
                startChat(uriConference);
            } else {
                client.conversationsManager.conversations.remove(chatConver);
                startChat(uriConference);

            }
        } else {
            startChat(uriConference);
        }
    } catch (e) {
        console.log('End Call error:' + e);
    }

    $('#content-msg').animate({ scrollTop: $('#content-msg').prop("scrollHeight") }, 100);
    $('#content-msg-call').animate({ scrollTop: $('#content-msg-call').prop("scrollHeight") }, 100);


}

var missCall;
function startCall(uriConference, video) {
    window.detailClick();
    $('#content-msg').empty();
    $('#content-msg-call').empty();
    if (chatConver === undefined || chatConver === null) {
        startConversation(uriConference);
    }
    $('#overlay-call').show();
    $('#callModal').modal({
        backdrop: 'static',
        keyboard: false
    });

    $('#list-participant').empty();
    var str =
        '<div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 padding-8" onclick="clickToZoom(this)"><div class="square"><div class="content" id="previewWindow"><div class="single-user-name"><span>' + client.personsAndGroupsManager.mePerson.displayName() + '</span></div></div></div></div>';
    $('#list-participant').append(str);
    var loop = 0;
    if (video && vsBrowser === "Edge") {
        console.log('start videoService');
        listeners.push(chatConver.videoService.state.changed(function (newState, reason, oldState) {
            console.log("Video: -newState: " + newState + " -oldState: " + oldState + " - reason: " + reason);
            if (newState === "Disconnected" && reason != undefined) {
                if (loop < 3) {
                    loop++;
                    console.log("Restart videoService");
                    setTimeout(function () {
                        //startCall(uriConference, video);
                        chatConver.videoService.start();
                    },
                        2000);
                } else {
                    $('#overlay-call').show();
                    $('#overlay-call img').remove();
                    $('#overlay-call').append('<div class="cannot-call">Cannot start call. Please wait Welio reload and try again.</div>');
                    setTimeout(function () {
                        location.reload();
                    },
                        1500);
                    return;
                }
            }
            if (newState === "Connected") {
                $.ajax({
                    url: window.baseUrl + "/Appointment/PushNotification?id=" + appointmentId,
                    type: "GET",
                    success: function (data, textStatus, jqXhr) {
                        if (data.Key === false) {
                            if (data.Value === 'Invalid Session') {
                                alert('Session expired, please try login again.');
                                window.location.href = window.baseUrl + 'User/SignOut';
                            }
                        } else {
                            callId = data.Value;
                        }
                    },
                    error: function (jqXhr, textStatus, errorThrown) {

                    }
                });
                var selfChannel;
                $('#overlay-call').hide();
                setTimeout(function () {
                    selfChannel = chatConver.selfParticipant.video.channels(0);
                    selfChannel.stream.source.sink.container.set(document.getElementById("previewWindow"));
                }, 1000);
                $('#overlay-call').hide();
                missCall = setTimeout(function () {
                    $('#div-noanswer').show();
                },
                    timeMiss);
            }
        }));
        chatConver.videoService.start();
    } else {
        console.log('start audioService');
        chatConver.audioService.state.changed(function (newState, reason, oldState) {
            console.log("Audio: -newState: " + newState + " -oldState: " + oldState + " - reason: " + reason);
            if (newState === "Disconnected" && reason !== undefined) {
                if (loop < 3) {
                    loop++;
                    console.log("Restart audioService");
                    setTimeout(function () {
                        //                        startCall(uriConference, video);
                        chatConver.audioService.start();
                    },
                        2000);
                } else {
                    $('#overlay-call').show();
                    $('#overlay-call img').remove();
                    $('#overlay-call').append('<div class="cannot-call">Cannot start call. Please wait Welio reload and try again.</div>');
                    setTimeout(function () {
                        location.reload();
                    },
                        1500);
                    return;
                }
            }
            if (newState === "Connected") {
                $.ajax({
                    url: window.baseUrl + "/Appointment/PushNotification?id=" + appointmentId,
                    type: "GET",
                    success: function (data, textStatus, jqXhr) {
                        console.log("Sent push to patient");
                        if (data.Key === false) {
                            if (data.Value === 'Invalid Session') {
                                alert('Session expired, please try login again.');
                                window.location.href = window.baseUrl + '/User/SignOut';
                            }
                        } else {
                            callId = data.Value;
                        }
                    },
                    error: function (jqXhr, textStatus, errorThrown) {

                    }
                });

                $('#overlay-call').hide();
                missCall = setTimeout(function () {
                    $('#div-noanswer').show();
                },
                    timeMiss);
            }
        });
        chatConver.audioService.start();
    }
    listeners.push(
        chatConver.participants.added(function (p) {
            window.clearTimeout(missCall);
            var idName = p.person.displayName().replace(/\s/g, "");

            listeners.push(p.video.state.changed(function (newState) {
                console.log(idName + " video state: " + newState);
                if (newState === 'Connected') {
                    if ($('#' + idName).length === 0) {
                        var strPartici = '<div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 padding-8" onclick="clickToZoom(this)">' +
                            '<div class="square"><div class="content" id="' +
                            idName +
                            '">' +
                            '<div class="single-user-name">' +
                            '<span>' +
                            p.person.displayName() +
                            '</span>' +
                            '</div></div></div></div>';
                        $('#list-participant').append(strPartici);
                    }
                    if (vsBrowser === 'Edge' && newState === 'Connected') {
                        console.log(idName + " join video");
                        p.video.channels(0)
                            .stream.source.sink.container.set(document.getElementById(idName))
                            .then(function () {
                                p.video.channels(0).isStarted.set(true);
                            });

                        p.video.channels(0)
                            .isVideoOn.when(true,
                                function () {
                                    console.log(idName + ' show video');
                                    p.video.channels(0)
                                        .stream.source.sink.container.set(document.getElementById(idName))
                                        .then(function () {
                                            setTimeout(function () {
                                                p.video.channels(0).isStarted.set(true);
                                                console.log(idName + ' set started = true');
                                            },
                                                2000);
                                        });
                                }
                            );
                        p.video.channels(0)
                            .isVideoOn.when(false,
                                function () {
                                    console.log(idName + ' hide video');
                                    p.video.channels(0)
                                        .stream.source.sink.container.set(null)
                                        .then(function () {
                                            setTimeout(function () {
                                                p.video.channels(0).isStarted.set(false);
                                                console.log(idName + ' set started = false');
                                            },
                                                2000);
                                        });
                                }
                            );
                    }
                } else {
                    if (newState === "Disconnected") {
                        p.video.channels(0)
                                        .stream.source.sink.container.set(null)
                                        .then(function () {
                                            setTimeout(function () {
                                                p.video.channels(0).isStarted.set(false);
                                                console.log(idName + ' set started = false');
                                            },
                                                2000);
                                        });
                    }
                }

            }));

            listeners.push(p.audio.state.changed(function (newState) {
                console.log(idName + "audio state: " + newState);
                if (newState === 'Connected') {
                    console.log(idName + " join audio");
                    if ($('#' + idName).length === 0) {
                        var strPartici =
                            '<div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 padding-8" onclick="clickToZoom(this)"><div class="square"><div class="content" id="' + idName + '"><div class="single-user-name"><span>' + p.person.displayName() + '</span></div></div></div></div>';
                        $('#list-participant').append(strPartici);
                    }
                } else {
                    if (newState === "Disconnected") {
                        if ($('#' + idName).length > 0)
                            $('#' + idName).parent().parent().remove();
                    }
                }
            }));
        }));
    listeners.push(chatConver.participants.removed(function (p) {
        var idName = p.person.displayName().replace(/\s/g, "");
        if ($('#' + idName).length > 0)
            $('#' + idName).parent().parent().remove();
    }));
}


(function ($) {
    $.fn.clickToggle = function (func1, func2) {
        var funcs = [func1, func2];
        this.data('toggleclicked', 0);
        this.click(function () {
            var data = $(this).data();
            var tc = data.toggleclicked;
            $.proxy(funcs[tc], this)();
            data.toggleclicked = (tc + 1) % 2;
        });
        return this;
    };
}(jQuery));

$('#close-message').clickToggle(function () {
    $('#msg-div').hide("slide", { direction: "right" }, 500);
    $('#call-div').removeClass("col-lg-9 col-md-9 col-sm-9 col-xs-6").addClass("col-lg-12 col-md-12 col-sm-12 col-xs-12");
}, function () {
    $('#call-div').removeClass("col-lg-12 col-md-12 col-sm-12 col-xs-12").addClass("col-lg-9 col-md-9 col-sm-9 col-xs-6");
    $('#msg-div').show("slide", { direction: "right" }, 500);
});
$('#imgEndCall').click(function () {
    window.clearTimeout(missCall);
    var i;
    $.ajax({
        url: window.baseUrl + "/Appointment/FinishCall?id=" + callId,
        type: "GET",
        success: function (data, textStatus, jqXhr) {
            console.log("End call");
            if (data.Key === false) {
                if (data.Value === 'Invalid Session') {
                    alert('Session expired, please try login again.');
                    window.location.href = window.baseUrl + '/User/SignOut';
                }
            }
        },
        error: function (jqXhr, textStatus, errorThrown) {

        }
    });
    try {
        if (chatConver) {
            if (chatConver.state() !== 'Disconnected') {
                //                chatConver.leave().then(() => {
                //                    for (i = 0; i < listeners.length; i++) {
                //                        listeners[i].dispose();
                //                    }
                //                    listeners = [];
                //                    client.conversationsManager.conversations.remove(chatConver);
                //                    table.fnReloadAjax();
                //                    chatConver = null;
                //                    $('#callModal').modal('hide');
                //                });
                chatConver.leave();
                for (i = 0; i < listeners.length; i++) {
                    listeners[i].dispose();
                }
                listeners = [];
                client.conversationsManager.conversations.remove(chatConver);
                table.fnReloadAjax();
                chatConver = null;
                $('#close-panel').trigger("click");
                $('#overlay-div-detail').hide();
                $('#callModal').modal('hide');
            } else {
                client.conversationsManager.conversations.remove(chatConver);
                table.fnReloadAjax();
                chatConver = null;
                for (i = 0; i < listeners.length; i++) {
                    listeners[i].dispose();
                }
                listeners = [];
                $('#close-panel').trigger("click");
                $('#overlay-div-detail').hide();
                $('#callModal').modal('hide');
            }
        }
    } catch (e) {
        for (i = 0; i < listeners.length; i++) {
            listeners[i].dispose();
        }
        listeners = [];
        console.log('End Call error:' + e);
        $('#close-panel').trigger("click");
        $('#overlay-div-detail').hide();
        $('#callModal').modal('hide');
    }

});

$('#imgTryAgain')
    .click(function () {
        $('#div-noanswer').hide();
        $.ajax({
            url: window.baseUrl + "/Appointment/PushNotification?id=" + appointmentId,
            type: "GET",
            success: function (data, textStatus, jqXhr) {
                console.log("Sent push to patient");
                if (data.Key === false) {
                    if (data.Value === 'Invalid Session') {
                        alert('Session expired, please try login again.');
                        window.location.href = window.baseUrl + '/User/SignOut';
                    }
                } else {
                    callId = data.Value;
                }
            },
            error: function (jqXhr, textStatus, errorThrown) {

            }
        });

        missCall = setTimeout(function () {
            $('#div-noanswer').show();
        },
            timeMiss);
    });
$('#imgExit')
    .click(function () {
        $('#div-noanswer').hide();
        $('#imgEndCall').trigger("click");
    });

$('#showVideo').click(function () {
    if (chatConver.videoService.state() === "Disconnected") {
        chatConver.videoService.start();
    }
    if (chatConver.videoService.state() === "Connected") {
        var selfChannel = chatConver.selfParticipant.video.channels(0);
        if (selfChannel.isVideoOn()) {
            selfChannel.stream.source.sink.container.set(document.getElementById("previewWindow"));
            selfChannel.isStarted.set(true);
        }

        for (var i = 0; i < chatConver.participants.size() ; i++) {
            var p = chatConver.participants(i);
            var idName = p.person.displayName().replace(/\s/g, "");
            console.log("get usr: " + idName);
            var channel2 = p.video.channels(0);
            if (channel2.isVideoOn()) {
                channel2.stream.source.sink.container(document.getElementById(idName));
                channel2.isStarted.set(true);
            }
        }
    }
});

function clickToZoom(e) {
    window.divUser = e;
    $('#list-participant > div')
        .each(function (i, v) {
            if ($(window).width() >= 768) {
                if (e === v) {
                    $(v).removeClass('col-lg-3 col-md-3 col-sm-3 col-lg-2 col-md-2 col-sm-2').addClass('col-lg-4 col-md-4 col-sm-4');
                    //$(v).animate({ width: '36%' }, 500);
                } else {
                    $(v).removeClass('col-lg-4 col-md-4 col-sm-4').addClass('col-lg-2 col-md-2 col-sm-2');
                    //$(v).animate({ width: '16%' }, 500);
                }
            }
        });

}
