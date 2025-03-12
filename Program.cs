using Microsoft.Extensions.DependencyInjection;
using FinanceAccounting.Services;
using FinanceAccounting.Services.Proxy;
using FinanceAccounting.UI;

var serviceProvider = new ServiceCollection()
    // Сначала регистрируем реальные сервисы
    .AddSingleton<BankAccountService>()  
    .AddSingleton<CategoryService>()  
    .AddSingleton<OperationService>()  

    // Затем регистрируем интерфейсы с прокси
    .AddSingleton<IBankAccountService>(sp => new BankAccountProxy(sp.GetRequiredService<BankAccountService>()))
    .AddSingleton<ICategoryService>(sp => new CategoryProxy(sp.GetRequiredService<CategoryService>()))
    .AddSingleton<IOperationService>(sp => new OperationProxy(sp.GetRequiredService<OperationService>()))

    // Регистрация дополнительных сервисов
    .AddSingleton<IAnalyticsService, AnalyticsService>()  
    .AddSingleton<IDataExporter, JsonDataExporter>()  

    // Регистрируем App
    .AddSingleton<App>()
    .BuildServiceProvider();

var app = serviceProvider.GetRequiredService<App>();
app.Run();
