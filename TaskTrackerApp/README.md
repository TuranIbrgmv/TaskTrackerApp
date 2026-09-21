# Система учёта задач сотрудников

Веб-приложение на ASP.NET Core для учёта задач, назначаемых сотрудникам.

## Стек технологий
- C#, .NET
- ASP.NET Core Web API
- Хранение данных: JSON-файл (Data/tasks.json)
- Интерфейс: HTML + JavaScript (fetch API)
- Архитектурные паттерны: Repository, Service Layer, Dependency Injection

## Структура проекта
- Models — модели данных (EmployeeTask, TaskState)
- Repositories — доступ к данным (ITaskRepository, JsonTaskRepository)
- Services — бизнес-логика (ITaskService, TaskService)
- Controllers — REST API (TasksController)
- wwwroot — веб-интерфейс (index.html, app.js)

## API
| Метод | URL | Описание |
|-------|-----|----------|
| GET | /api/tasks | получить все задачи |
| GET | /api/tasks/{id} | получить задачу по ID |
| POST | /api/tasks | создать задачу |
| PUT | /api/tasks/{id} | обновить задачу |
| PUT | /api/tasks/{id}/status | изменить статус задачи |
| DELETE | /api/tasks/{id} | удалить задачу |

## Запуск
1. Открыть проект в Visual Studio.
2. Нажать F5.
3. Приложение откроется в браузере со страницей учёта задач.