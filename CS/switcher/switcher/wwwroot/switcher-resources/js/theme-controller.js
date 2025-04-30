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
    
    async function switchTheme(isBsNative, isFluent, isDark, name, cookie, reference) {
        const html = document.querySelector("HTML");
        const body = html.querySelector("body");
        if(isBsNative) {
            if(isDark)
                html.setAttribute("data-bs-theme", "dark");
        } else {
            html.removeAttribute("data-bs-theme");
        }
        if(isFluent) {
            if(isDark)
                html.setAttribute("data-fluent-darkmode", "true");
            else
                html.removeAttribute("data-fluent-darkmode");
            body.classList.add("dxbl-theme-fluent");
        } else {
            html.removeAttribute("data-fluent-darkmode");
            body.classList.remove("dxbl-theme-fluent");
        }
        setThemeName(cookie, name);

        await reference.invokeMethodAsync("ThemeLoadedAsync");
    }

    return { switchTheme }
})();
