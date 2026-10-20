using System;
using System.Globalization;

namespace Lab05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Підтримка українських літер у консолі
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            CultureInfo culture = CultureInfo.InvariantCulture;

            Console.WriteLine("=== Розрахунок вартості послуг мобільного зв'язку ===");

       
            Console.Write("Введіть кількість використаних хвилин: ");
            string minutesInput = Console.ReadLine()?.Replace(',', '.') ?? "";

            Console.Write("Введіть кількість використаних гігабайтів (ГБ): ");
            string gbInput = Console.ReadLine()?.Replace(',', '.') ?? "";
            // Перевірка коректності введених даних через TryParse
            bool isMinutesValid = double.TryParse(minutesInput, NumberStyles.Any, culture, out double usedMinutes);
            bool isGbValid = double.TryParse(gbInput, NumberStyles.Any, culture, out double usedGb);

            if (!isMinutesValid || !isGbValid || usedMinutes < 0 || usedGb < 0)
            {
                Console.WriteLine("\n[Помилка]: Введено некоректні дані. Хвилини та гігабайти мають бути невід'ємними числами.");
                return;
            }

            const double packageBasePrice = 150.0; 
            const double packageMinutesLimit = 300.0;
            const double packageGbLimit = 5.0;
            const double extraMinuteRate = 1.20; 
            const double extraGbRate = 12.0;   

            double extraMinutes = 0.0;
            double extraMinutesCost = 0.0;
            double extraGb = 0.0;
            double extraGbCost = 0.0;
            
            // Розрахунок доплати за хвилини понад пакет
            if (usedMinutes > packageMinutesLimit)
            {
                extraMinutes = usedMinutes - packageMinutesLimit;
                extraMinutesCost = extraMinutes * extraMinuteRate;
            }
            // Розрахунок доплати за інтернет понад пакет
            if (usedGb > packageGbLimit)
            {
                extraGb = usedGb - packageGbLimit;
                extraGbCost = extraGb * extraGbRate;
            }

    
            double totalCost = packageBasePrice + extraMinutesCost + extraGbCost;

            string billCategory = totalCost switch
            {
                <= 150.0 => "Базовий тариф (без перевитрат)",
                > 150.0 and <= 250.0 when extraGb > 10.0 => "Помірний рахунок з великим обсягом інтернету",
                > 150.0 and <= 250.0 => "Помірний рахунок з перевитратами",
                _ => "Значний рахунок (суттєве перевищення лімітів)"
            };

            // Вивід результату
            Console.WriteLine("\n------------- Деталізація нарахувань -------------");
            Console.WriteLine($"Використано: {usedMinutes:F1} хв., {usedGb:F2} ГБ");
            Console.WriteLine($"Базова вартість пакета: {packageBasePrice:F2} грн");
            Console.WriteLine($"Понадлімітні хвилини:   {extraMinutes:F1} хв. на суму {extraMinutesCost:F2} грн");
            Console.WriteLine($"Понадлімітний інтернет:  {extraGb:F2} ГБ на суму {extraGbCost:F2} грн");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"Загальна сума до сплати: {totalCost:F2} грн");
            Console.WriteLine($"Категорія нарахування:   {billCategory}");
            Console.WriteLine("--------------------------------------------------");
        }
    }
}