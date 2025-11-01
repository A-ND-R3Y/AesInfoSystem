namespace Aes.DAL.Models
{
    public class FacilityObject
    {
        public int Id { get; set; }

        // Назва об’єкта (наприклад: "Насос 1", "Турбіна 2")
        public string Name { get; set; } = "";

        // Тип об’єкта (наприклад: "Обладнання", "Енергоблок")
        public string Type { get; set; } = "";

        // Місце розташування (наприклад: "Зона 1", "Цех 3")
        public string Location { get; set; } = "";

        // Опис або додаткова інформація
        public string Description { get; set; } = "";
    }
}
