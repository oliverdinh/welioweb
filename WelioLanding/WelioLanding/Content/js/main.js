$('a.scroll').click(function() {
    if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
        var target = $(this.hash);
        target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
        if (target.length) {
            $('html, body').animate({
                scrollTop: target.offset().top
            }, 1000);
            return false;
        }
    }
});

$("#form-register").validate({
    rules: {
        "FirstName": {
            required: true
        },
        "LastName": {
            required: true
        },
        "Email":{
            required: true,
            email: true
        },
        "Street":{
            required: true
        },
        "City":{
            required: true
        },
        "PostCode":{
            required: true
        }
    },
    messages: {
        "FirstName": {
            required: "The First name is required."
        },
        "LastName": {
            required: "The Last name is required."
        },
        "Email": {
            required: "The Email is required."
        },
        "Street": {
            required: "The Street Name is required."
        },
        "City": {
            required: "The City Name is required."
        },
        "PostCode": {
            required: "The Post Code is required."
        }
    }
});