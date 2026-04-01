# Програмування на C# — Лабораторні роботи

Репозиторій містить реалізацію Лабораторних робіт 1–3 в межах тематики **«University» (Кафедри та Викладачі)**.

---

## Виконано згідно з критеріями

### Лабораторна робота 1 (10 балів)
- Розподіл класів між проєктами — ✓  
- Реалізація моделей — ✓  
- Реалізація сховища — ✓  
- Консольний інтерфейс — ✓  
- Архітектура — ✓  
- Стиль та коментарі — ✓  

### Лабораторна робота 2 (10 балів)
- UI застосунок на MAUI — ✓  
- Навігація між сторінками — ✓  
- DI та IoC — ✓  
- Відображення даних — ✓  
- Стиль та README — ✓  

### Лабораторна робота 3 (10 балів)
- Перехід на MVVM — ✓  
- Винесення логіки у ViewModel — ✓  
- Реалізація Repository layer — ✓  
- Реалізація Service layer — ✓  
- Використання DTO — ✓  
- Dependency Injection та IoC — ✓  
- Навігація через ViewModel — ✓  

---

# Лабораторна робота 1

## Тема
Основи C#, об’єктно-орієнтоване програмування, багатошарова архітектура, консольний застосунок.

## Мета
Розробити базову модель застосунку та консольний інтерфейс.

## Реалізовано

- Сутності: Кафедра, Викладач  
- Enum  
- Штучне сховище  
- Сервіс доступу  
- Консольний UI  
- Принцип Single Responsibility  

## Структура

- `University.Models`  
- `University.Services`  
- `University.Presentation`  
- `University.ConsoleApp`  

## Запуск

```bash
dotnet run --project University.ConsoleApp
```

---

# Лабораторна робота 2

## Тема
UI-застосунок, навігація, Dependency Injection.

## Технології
.NET MAUI (Android), Code-Behind.

## Реалізовано

3 сторінки:
Список кафедр
Деталі кафедри
Деталі викладача
Навігація
DI
Повторне використання моделей

## Структура

- `University.UI`  

## Запуск

```bash
dotnet build University.UI -f net10.0-android
dotnet run --project University.UI -f net10.0-android
```

---

# Лабораторна робота 3

## Тема
MVVM та багатошарова архітектура.

## Мета
Перебудувати застосунок ЛР2 з використанням сучасної архітектури.

## Реалізовано

- Шари:
Repositories
Services
UI
- Інтерфейси:
IUniversityRepository
IUniversityService
- DTO:
DepartmentListItemDto
DepartmentDetailsDto
TeacherListItemDto
TeacherDetailsDto
- ViewModel:
DepartmentsViewModel
DepartmentDetailsViewModel
TeacherDetailsViewModel
Логіка винесена з UI у ViewModel
UI працює через DTO
Реалізовано Dependency Injection

## Архітектура
UI → ViewModel → Service → Repository → Data

## Структура

- `University.Repositories`  
- `University.Services`  
- `University.UI`   

## Запуск

```bash
dotnet build University.UI -f net10.0-android
dotnet run --project University.UI -f net10.0-android
```

---

## Автор
Деркач Єлизавета Романівна
