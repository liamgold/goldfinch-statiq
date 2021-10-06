function toggleDarkMode() {
  const currentTheme = localStorage.getItem("theme")
    ? localStorage.getItem("theme")
    : null;

  if (currentTheme) {
    const newTheme = currentTheme === "light" ? "dark" : "light";
    document.documentElement.setAttribute("data-theme", newTheme);
    localStorage.setItem("theme", newTheme);
  } else {
    document.documentElement.setAttribute("data-theme", "light");
    localStorage.setItem("theme", "light");
  }
}
