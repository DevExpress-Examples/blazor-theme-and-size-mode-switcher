window.setCookie = function (cookie, value) {
    var date = new Date();
    date.setFullYear(date.getFullYear() + 1);

    document.cookie = encodeURIComponent(cookie) + '=' + encodeURIComponent(value) +
        '; expires=' + date.toUTCString() + '; path=/';
};
