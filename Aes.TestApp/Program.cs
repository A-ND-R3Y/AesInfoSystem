using System;
using System.Linq;
using Aes.DAL;
using Aes.DAL.Repositories;
using Aes.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Aes.DAL.UnitOfWork;


class Program
{
    static void Main()
    {
        using var ctx = new AesContext();
        ctx.Database.EnsureCreated();

        using var uow = new EFUnitOfWork();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n===============================");
            Console.WriteLine("  КЕРУВАННЯ БАЗОЮ ОБ’ЄКТІВ АЕС");
            Console.WriteLine("===============================");
            Console.WriteLine("1. Показати всі об'єкти");
            Console.WriteLine("2. Додати об'єкт");
            Console.WriteLine("3. Видалити об'єкт");
            Console.WriteLine("4. Очистити базу");
            Console.WriteLine("5. Вийти");
            Console.Write("Виберіть дію: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1": ShowAll(uow); break;
                case "2": AddObject(uow); break;
                case "3": DeleteObject(uow); break;
                case "4": ClearDatabase(ctx); break;
                case "5": exit = true; break;
                default: Console.WriteLine("Невірний вибір!"); break;
            }
        }
    }

    static void ShowAll(EFUnitOfWork uow)
    {
        var list = uow.FacilityObjects.GetAll();
        if (!list.Any()) { Console.WriteLine("База порожня."); return; }
        Console.WriteLine("\n--- Всі об'єкти ---");
        foreach (var o in list)
            Console.WriteLine($"{o.Id}: {o.Name} ({o.Type}) - {o.Location}");
    }

    static void AddObject(EFUnitOfWork uow)
    {
        Console.Write("Назва: "); string? name = Console.ReadLine();
        Console.Write("Тип: "); string? type = Console.ReadLine();
        Console.Write("Локація: "); string? loc = Console.ReadLine();
        Console.Write("Опис: "); string? desc = Console.ReadLine();

        var obj = new FacilityObject
        {
            Name = name ?? "",
            Type = type ?? "",
            Location = loc ?? "",
            Description = desc ?? ""
        };
        uow.FacilityObjects.Create(obj);
        uow.Save();
        Console.WriteLine($"Додано об'єкт із ID = {obj.Id}");
    }

    static void DeleteObject(EFUnitOfWork uow)
    {
        Console.Write("ID для видалення: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            try
            {
                uow.FacilityObjects.Delete(id);
                uow.Save();
                Console.WriteLine("Видалено.");
            }
            catch { Console.WriteLine("Не знайдено."); }
        }
    }

    static void ClearDatabase(AesContext ctx)
    {
        ctx.FacilityObjects.RemoveRange(ctx.FacilityObjects);
        ctx.Employees.RemoveRange(ctx.Employees);
        ctx.SaveChanges();
        ctx.Database.ExecuteSqlRaw("DELETE FROM sqlite_sequence WHERE name='FacilityObjects';");
        ctx.Database.ExecuteSqlRaw("DELETE FROM sqlite_sequence WHERE name='Employees';");
        Console.WriteLine("Очищено базу, лічильники скинуто.");
    }
}
