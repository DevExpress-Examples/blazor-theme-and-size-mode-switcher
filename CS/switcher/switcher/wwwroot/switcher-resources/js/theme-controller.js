"use strict";

export const ThemeController = (function () {
    function setCookie(cookie, value) {
        var date = new Date();
        date.setFullYear(date.getFullYear() + 1);
        document.cookie = escape(cookie) + '=' + escape(value.toString()) + '; expires=' + date.toGMTString() + '; path=/';
    }

    async function switchTheme(name, themeState, cookie, stateCookie, reference) {
        setCookie(cookie, name);
        setCookie(stateCookie, themeState);

        await reference.invokeMethodAsync("ThemeLoadedAsync");
    }
    
    function setSizeMode(sizeMode, cookie) {
        setCookie(cookie, sizeMode);
    }

    return { switchTheme, setSizeMode }
})();
