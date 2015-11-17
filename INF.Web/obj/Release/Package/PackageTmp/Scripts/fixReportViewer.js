function fixReportingServices(container) {
     //if ($.browser.safari) { // toolbars appeared on separate lines.
//        $('#' + container + ' #ctl00_ContentPlaceHolder_BasicMonthlyReportViewer_ctl10').each(function (i, item) {
//            $(item).css('display', 'inline-block');
//            $(item).css('overflow', 'visible');
//        });
    //}
    $("div[id*='_ctl']").css('overflow', 'visible');
   }
    // needed when AsyncEnabled=true. 
    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function () { fixReportingServices('rpt-container'); });
    /*$(document).ready(function () { fixReportingServices('rpt-container');});*/

