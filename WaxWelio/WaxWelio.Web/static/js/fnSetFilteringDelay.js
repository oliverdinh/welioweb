/**
 * Enables filtration delay for keeping the browser more responsive while 
 * searching for a longer keyword.
 *
 * This can be particularly useful when working with server-side processing,
 * where you wouldn't typically want an Ajax request to be made with every key
 * press the user makes when searching the table.
 *
 * Please note that this plug-in has been deprecated and the `dt-init
 * searchDelay` option in DataTables 1.10 should now be used. This plug-in will
 * not operate with v1.10+.
 *
 *  @name fnSetFilteringDelay
 *  @summary Add a key debouce delay to the global filtering input of a table
 *  @author [Zygimantas Berziunas](http://www.zygimantas.com/), 
 *    [Allan Jardine](http://www.sprymedia.co.uk/) and _vex_
 *
 *  @example
 *    $(document).ready(function() {
 *        $('.dataTable').dataTable().fnSetFilteringDelay();
 *    } );
 */

jQuery.fn.dataTableExt.oApi.fnSetFilteringDelay = function (oSettings, iDelay) {
    var _that = this;

    if (iDelay === undefined) {
        iDelay = Global.Config.DefaultGridDelay;
    }

    this.each(function (i) {
        $.fn.dataTableExt.iApiIndex = i;
        var
			oTimerId = null,
			sPreviousSearch = null,
			anControl = $('input', _that.fnSettings().aanFeatures.f);

        anControl.unbind('keyup search input').bind('keyup search input', function () {

            if (sPreviousSearch === null || sPreviousSearch != anControl.val()) {
                window.clearTimeout(oTimerId);
                sPreviousSearch = anControl.val();
                oTimerId = window.setTimeout(function () {
                    $.fn.dataTableExt.iApiIndex = i;
                    _that.fnFilter(anControl.val());
                }, iDelay);
            }
        });

        return this;
    });
    return this;
};
$.fn.dataTableExt.oApi.fnStandingRedraw = function (oSettings) {
    //redraw to account for filtering and sorting
    // concept here is that (for client side) there is a row got inserted at the end (for an add)
    // or when a record was modified it could be in the middle of the table
    // that is probably not supposed to be there - due to filtering / sorting
    // so we need to re process filtering and sorting
    // BUT - if it is server side - then this should be handled by the server - so skip this step
    console.log(oSettings);
    if (oSettings.oFeatures.bServerSide === true) {
        //iDisplayStart has been reset to zero - so lets change it back
        oSettings._iDisplayStart = 0;
        oSettings.oPreviousSearch.sSearch = "";
        this.fnFilter("");

        oSettings.oApi._fnCalculateEnd(oSettings);
    }

    //draw the 'current' page
    oSettings.oApi._fnDraw(oSettings);
};
