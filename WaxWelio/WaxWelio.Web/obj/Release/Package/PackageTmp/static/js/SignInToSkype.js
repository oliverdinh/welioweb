
function SignIn() {
    if (!location.hash) {
        location.assign('https://login.windows.net/common/oauth2/authorize?response_type=token' + //"&userType=" + usertype + "&displayName=" + displayName + "&meetingId=" + meetingId +
        '&client_id=2bd937f8-d693-4e38-8718-2f53deed2dff' +
        '&redirect_uri=' + window.location.origin + "/HealthCare/Conference" +
        '&resource=https://webdir0f.online.lync.com');
    }
    if (/^#access_token=/.test(location.hash)) {
        InitialiseSkype();
    }   
}
