using Microsoft.Extensions.Configuration;
using TaskManagement.DAL.DbConnection;
using TaskManagement.DAL.Repositories;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var connection = new SqlServerConnection(configuration);
var repository = new TaskRepository(connection);

await RunApplication(repository);

async Task RunApplication(ITaskRepository repository)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== Управление задачами ===");
        Console.WriteLine("1. Просмотр всех задач");
        Console.WriteLine("2. Добавление новой задачи");
        Console.WriteLine("3. Обновление состояния задачи");
        Console.WriteLine("4. Удаление задачи по Id");
        Console.WriteLine("5. Выход");
        Console.Write("Выберите действие: ");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                await ViewAllTasks(repository);
                break;
            case "2":
                await AddTask(repository);
                break;
            case "3":
                await UpdateTaskStatus(repository);
                break;
            case "4":
                await DeleteTask(repository);
                break;
            case "5":
                return;
            default:
                Console.WriteLine("Неверное действие. Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                break;
        }
    }
}
async Task ViewAllTasks(ITaskRepository repository)
{
    var tasks = await repository.GetAllAsync();
    Console.WriteLine("\n=== Все задачи ===");

    if (!tasks.Any())
    {
        Console.WriteLine("Задачи не найдены.");
    }
    else
    {
        foreach (var task in tasks)
        {
            Console.WriteLine($"[{task.Id}] {task.Title} - {task.Description} - " +
                $"{(task.IsCompleted ? "выполнена" : "не выполнена")} - {task.CreatedAt:yyyy-MM-dd HH:mm}");
        }
    }

    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
    Console.ReadKey();
}

async Task AddTask(ITaskRepository repository)
{
    Console.Write("Введите название: ");
    var title = Console.ReadLine() ?? string.Empty;

    if (string.IsNullOrWhiteSpace(title))
    {
        Console.WriteLine("Название не может быть пустым");
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
        return;
    }

    Console.Write("Введите описание: ");
    var description = Console.ReadLine() ?? string.Empty;

    var newTask = new TaskManagement.DAL.Models.Task
    {
        Title = title,
        Description = description,
        IsCompleted = false,
        CreatedAt = DateTime.Now
    };

    await repository.AddAsync(newTask);
    Console.WriteLine("Задача успешно добавлена!");
    Console.WriteLine("Нажмите любую клавишу для продолжения...");
    Console.ReadKey();
}

async Task UpdateTaskStatus(ITaskRepository repository)
{
    Console.Write("Введите ID задачи: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Неверный формат ID");
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
        return;
    }

    var task = await repository.GetByIdAsync(id);
    if (task == null)
    {
        Console.WriteLine("Задача не найдена");
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
        return;
    }

    string response;
    while (true)
    {
        Console.WriteLine($"Текущий статус: {(task.IsCompleted ? "выполнена" : "не выполнена")}");
        Console.Write("Отметить как выполненную? (y/n): ");
        response = Console.ReadLine()?.ToLower();

        if (response == "y" || response == "n") break;

        Console.WriteLine("Пожалуйста, введите 'y' (да) или 'n' (нет)");
    }
    var isCompleted = response == "y";

    var success = await repository.UpdateCompletionStatusAsync(id, isCompleted);
    if (success)
        Console.WriteLine("Статус задачи успешно обновлен!");
    else
        Console.WriteLine("Ошибка при обновлении статуса.");

    Console.WriteLine("Нажмите любую клавишу для продолжения...");
    Console.ReadKey();
}

async Task DeleteTask(ITaskRepository repository)
{
    Console.Write("Введите ID для удаления: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Неверный формат ID");
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
        return;
    }

    var task = await repository.GetByIdAsync(id);
    if (task == null)
    {
        Console.WriteLine("Задача не найдена");
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
        return;
    }

    Console.WriteLine($"Вы уверены, что хотите удалить задачу: \"{task.Title}\"?");
    Console.Write("Подтвердите удаление (y/n): ");
    var confirmation = Console.ReadLine()?.ToLower();

    if (confirmation != "y")
    {
        Console.WriteLine("Удаление отменено");
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
        return;
    }

    var success = await repository.DeleteAsync(id);
    if (success)
        Console.WriteLine("Задача успешно удалена!");
    else
        Console.WriteLine("Ошибка при удалении задачи.");

    Console.WriteLine("Нажмите любую клавишу для продолжения...");
    Console.ReadKey();
}
