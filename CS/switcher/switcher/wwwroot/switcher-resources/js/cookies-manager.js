window.setCookie = function (cookie, value) {
    var date = new Date();
    date.setFullYear(date.getFullYear() + 1);
    document.cookie = escape(cookie) + '=' + escape(value.toString()) + '; expires=' + date.toGMTString() + '; path=/';
}
