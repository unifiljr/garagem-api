const { defineConfig } = require("cypress");

module.exports = defineConfig({
  e2e: {
    // Ajuste para a porta real da sua aplicação (confira no launchSettings.json
    // ou no terminal quando roda "dotnet run")
    baseUrl: "https://localhost:7001",
    chromeWebSecurity: false,
    supportFile: false,
    setupNodeEvents(on, config) {
      // implementação de node events, se precisar no futuro
    },
  },
});
