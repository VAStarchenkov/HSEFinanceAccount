# **Учет финансов**

## **1. Описание проекта**
Проект представляет собой консольное приложение для управления личными финансами. Реализованы следующие функции:

- **Создание, редактирование и удаление счетов, категорий и операций (доходов/расходов).**
- **Аналитика:**
  - Подсчет разницы доходов и расходов за период.
  - Группировка доходов/расходов по категориям.
- **Импорт и экспорт данных:**
  - Экспорт всех данных в CSV, YAML, JSON.
  - Импорт данных из этих форматов.
- **Пересчет баланса:**
  - Проверка соответствия суммы операций балансу счета.
  - Корректировка баланса при несоответствиях.
- **Замер времени выполнения команд.**

## **2. Реализованные принципы SOLID и GRASP**

### **SOLID:**
- **S (Single Responsibility Principle)** – каждый класс выполняет одну задачу (например, `BankAccountService` отвечает только за работу со счетами).
- **O (Open/Closed Principle)** – легко добавить новый формат импорта/экспорта, используя `Visitor`.
- **L (Liskov Substitution Principle)** – `IExportVisitor` заменяется `JsonExportVisitor`, `CsvExportVisitor`, `YamlExportVisitor` без изменения клиентского кода.
- **I (Interface Segregation Principle)** – небольшие интерфейсы (`IBankAccountService`, `ICategoryService`, `IOperationService`).
- **D (Dependency Inversion Principle)** – зависимости внедряются через DI-контейнер.

### **GRASP:**
- **High Cohesion** – сервисы имеют четкие обязанности (`AnalyticsService`, `OperationService`).
- **Low Coupling** – зависимость между модулями минимизирована за счет использования интерфейсов.

## **3. Использованные паттерны GoF**

| **Паттерн**            | **Где используется** | **Обоснование** |
|------------------------|----------------------|-----------------|
| **Фасад (`Facade`)** | `BankAccountService`, `CategoryService`, `OperationService`, `AnalyticsService` | Упрощает взаимодействие с доменными объектами. |
| **Команда (`Command`)** | `CreateAccountCommand`, `AddOperationCommand`, `ExportDataCommand` | Каждое действие оформлено как отдельный объект. |
| **Декоратор (`Decorator`)** | `TimeLoggingCommand` | Добавляет логирование времени выполнения команд без изменения их кода. |
| **Шаблонный метод (`Template Method`)** | `DataExporterBase` | Позволяет переопределить способ экспорта данных без дублирования логики. |
| **Посетитель (`Visitor`)** | `JsonExportVisitor`, `CsvExportVisitor`, `YamlExportVisitor`,`JsonImportVisitor`, `CsvImportVisitor`, `YamlImportVisitor`| Упрощает экспорт и импорт в разные форматы, разделяя данные и логику экспорта и импорта. |
| **Фабрика (`Factory`)** | `FinanceFactory` (создание `BankAccount`, `Category`, `Operation`) | Гарантирует валидацию создаваемых объектов. |
| **Прокси (`Proxy`)** | `BankAccountProxy`, `CategoryProxy`, `OperationProxy` | Реализует кеширование данных. |

## **4. Инструкция по запуску**

### **Требования:**
- .NET 6.0 или выше
- Windows, macOS или Linux

### **Команды для запуска:**
```sh
# 1. Клонировать репозиторий
$ git clone https://github.com/VAStarchenkov/HSEFinanceAccount
$ cd HSEFinanceAccount

# 2. Скомпилировать проект
$ dotnet build

# 3. Запустить приложение
$ dotnet run
```

### **Пример использования:**
После запуска появится консольное меню:
```
1. Создать счет
2. Показать счета
3. Добавить операцию
4. Аналитика
5. Экспорт данных
6. Импорт данных
7. Пересчитать баланс
8. Выход
Выберите действие:
```
Выберите нужный пункт и следуйте инструкциям в консоли.

## **5. Заключение**
Проект построен с учетом **SOLID, GRASP и паттернов GoF**, что делает его **гибким, расширяемым и удобным в сопровождении**. Если потребуется добавить новый функционал (например, поддержку базы данных), архитектура легко позволит это сделать.
