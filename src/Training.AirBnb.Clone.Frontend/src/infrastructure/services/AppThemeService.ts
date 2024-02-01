/*
    Provides app theme functionality.
 */
export class AppThemeService {
    /**
     * Checks if the dark mode is enabled.
     *
     * @returns {boolean} True if dark mode is enabled, false otherwise.
     */
    public isDarkMode(): boolean {
        if(localStorage.getItem("darkMode") !== null)
            return localStorage.getItem("darkMode") === "true";

        return window.matchMedia("(prefers-color-scheme: dark)").matches
    }

    /**
     * Toggles the dark mode of the application.
     * It adds or removes the "dark" class from the document body and updates the darkMode value in localStorage.
     */
    public toggleDarkMode(): void {
        document.body.classList.toggle("dark");
        const darkMode= localStorage.getItem("darkMode") !== null ? localStorage.getItem("darkMode") == "true" : false;
        localStorage.setItem("darkMode", (!darkMode).toString());
    }

    /**
     * Sets the application theme based on the user's preference.
     */
    public setAppTheme(): void {
        if (this.isDarkMode()) {
            document.body.classList.add("dark");
        } else {
            document.body.classList.remove("dark");
        }

        localStorage.setItem("darkMode", this.isDarkMode().toString());
    }
}