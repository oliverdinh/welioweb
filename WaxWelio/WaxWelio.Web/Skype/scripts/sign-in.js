//sign in sample:
//if user has signed in give prompt, otherwise go to index page
$(function () {
    'use strict';
    window['sign-in_load'] = function () {
        if (window.skypeWebApp && window.skypeWebApp.signInManager.state() === "SignedIn") {
            $('.wrappingdiv .signed-in').show();
            return;
        } else {
            $('.wrappingdiv .signed-in').hide();
            $('.modal').show();
            configAndSignIn({
                "client_id": "99fb8594-241f-44c3-8fd1-0ecbb16444ab",
                "origins": ["https://webdir.online.lync.com/autodiscover/autodiscoverservice.svc/root"],
                "cors": true,
                "version": 'skype for business helio/1.0.0',
                "redirect_uri": '/token.html'
            });
        }
       
    };
});
