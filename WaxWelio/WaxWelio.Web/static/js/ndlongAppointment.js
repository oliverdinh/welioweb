$('#btn-appointment').on('click', function () {
    $('#apointment-detail-patient').show();
    $('#message-patient').hide();
    $('#btn-appointment').addClass("btn-selected").removeClass("btn-unselected");
    $('#btn-message').addClass("btn-unselected").removeClass("btn-selected");
});

$('#close-panel').click(function () {
    $('#add-appointment').show();
    $('#leftSidebar').hide();
    $('#col-table').removeClass('col-820-1279-9').addClass('col-820-1279-12');
    $('#col-sidebar').addClass('hide-820-1279');
    $('#div-fill-appointment').removeClass('col-820-1279-60');
    $('#div-fill-date-appointment').removeClass('col-820-1279-5');
    $('#div-fill-doctor-appointment').removeClass('col-820-1279-5');
    $('#div-fill-search-appointment').removeClass('col-820-1279-2');
    $('#tblAppointment_paginate').removeClass('paginate-820-1279');
    $('#tblAppointment_paginate').parent().removeClass('parent-paginate-820-1279');
});

