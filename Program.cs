using System;
using System.Globalization;

namespace Lab05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Розрахунок індексу маси тіла");

            // Введення даних як рядки, щоб перевірити через TryParse
            Console.Write("Зріст (м): ");
            string heightInput = Console.ReadLine() ?? "";
            Console.Write("Маса (кг): ");
            string massInput = Console.ReadLine() ?? "";
            Console.Write("Чи займаєтесь спортом регулярно? (так/ні): ");
            string sportInput = (Console.ReadLine() ?? "").Trim().ToLower();

            double height;
            double mass;
            bool IsAdiposity = false;

            // Перевірка коректності введених даних.
            if (!double.TryParse(heightInput.Replace(',', '.'), NumberStyles.Float,
                CultureInfo.InvariantCulture, out height))
            {
                Console.WriteLine("Помилка: зріст має бути числом.");
            }
            else if (height < 0.5 || height > 2.5)
            {
                // якщо нереалістичний зріст
                Console.WriteLine("Помилка: зріст має бути в межах від 0,5 до 2,5 м.");
            }
            else if (!double.TryParse(massInput.Replace(',', '.'), NumberStyles.Float,
                CultureInfo.InvariantCulture, out mass))
            {
                Console.WriteLine("Помилка: маса має бути числом.");
            }
            else if (mass < 10 || mass > 400)
            {
                // Нереалістична маса
                Console.WriteLine("Помилка: маса має бути в межах від 10 до 400 кг.");
            }
            else if (sportInput != "так" && sportInput != "ні")
            {
                Console.WriteLine("Помилка: відповідь на питання про спорт має бути «так» або «ні».");
            }
            else
            {
                // Обчислення ІМТ = маса / зріст^2
                double bmi = mass / (height * height);

                // Основне розгалуження
                string category;
                if (bmi < 18.5)
                {
                    category = "дефіцит маси";
                }
                else if (bmi < 25)
                {
                    category = "норма";
                }
                else if (bmi < 30)
                {
                    category = "надлишкова маса";
                }
                else
                {
                    category = "ожиріння";
                    IsAdiposity = true;
                }

                // підсумкова категорія виразом switch з умовою when
                bool isSporty = sportInput == "так";

                string summary = bmi switch
                {
                    < 16 => "критичний дефіцит: потрібна консультація лікаря",
                    >= 16 and < 18.5 => "помірний дефіцит: варто збільшити калорійність раціону",
                    >= 18.5 and < 25 => "здорова вага: підтримуйте поточний режим",
                    >= 25 and < 30 when isSporty => "підвищений ІМТ, імовірно через м'язову масу",
                    >= 25 and < 30 => "надлишкова маса: варто збільшити фізичну активність",
                    >= 30 and < 35 => "ожиріння I ступеня: рекомендовано консультацію лікаря",
                    _ => "ожиріння II–III ступеня: потрібна консультація лікаря"
                };

                // Виведення результату
                Console.WriteLine($"\nІндекс маси тіла: {bmi:F2}");
                Console.WriteLine($"Категорія: {category}");
                Console.WriteLine($"Підсумок: {summary}");

            }
        }
    }
}