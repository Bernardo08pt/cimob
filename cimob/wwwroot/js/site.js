// Write your JavaScript code.
$(document).ready(function () {
    $('.pop').webuiPopover({ style: 'inverse', animation: 'pop', trigger: 'hover', placement: 'top', width: 300, delay: { show: null, hide: 500 } });
    $(document).on('click', '#logout', function () { $('#logoutForm').submit(); });
});

