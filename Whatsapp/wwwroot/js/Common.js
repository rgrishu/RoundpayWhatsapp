﻿var Base = {
    Logout: function () {
        document.getElementById('logoutForm').submit();
    }
}
function RaiseUserBalanceRequest(id) {
    $.ajax({
        type: 'post',
        url: '/Users/GetRaiseFundRequest/' + id,
        success: function (response) {
            Q.alert({
                title: "Raise Fund Request",
                body: response,
                width: '500px',
            });
        },
        error: function (data) {
        },
    });
}