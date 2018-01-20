// Write your JavaScript code.
$(document).ready(function () {
    if ($(window).width() <= '500')
        $('.pop').webuiPopover({ style: 'inverse', animation: 'pop', trigger: 'hover', placement: 'auto', width: $(window).width() -75, delay: { show: null, hide: 500 } });
    else
        $('.pop').webuiPopover({ style: 'inverse', animation: 'pop', trigger: 'hover', placement: 'auto',  width: 'auto', delay: { show: null, hide: 500 } });

    $(document).on('click', '#logout', function () { $('#logoutForm').submit(); });
});

