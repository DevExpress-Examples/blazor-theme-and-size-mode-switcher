"use strict";

export const ThemeController = (function () {
    function setCookie(name, value, date) {
        document.cookie = escape(name) + '=' + escape(value.toString()) + '; expires=' + date.toGMTString() + '; path=/';
    }

    function setThemeName(cookieName, themeName) {
        var date = new Date();
        date.setFullYear(date.getFullYear() + 1);
        setCookie(cookieName, themeName, date);
    }

    async function switchBsThemeMode(bsThemeMode, cookie, reference) {
        document.querySelector("HTML").setAttribute("data-bs-theme", bsThemeMode);
        const html = document.querySelector("HTML");
        setThemeName(cookie, name);

        await reference.invokeMethodAsync("ThemeLoadedAsync");
    }

    return { switchBsThemeMode }
})();
