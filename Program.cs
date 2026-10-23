using System;
using System.Globalization;

namespace Lab05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Розрахунок індексу маси тіла");

            double height = ReadDouble("Зріст (м): ", 0.5, 2.5);
            double mass = ReadDouble("Маса (кг): ", 10, 400);
            bool isSporty = ReadYesNo("Чи займаєтесь спортом регулярно? (так/ні): ");

            double bmi = mass / (height * height);

            Console.WriteLine($"\nІндекс маси тіла: {bmi:F2}");
            Console.WriteLine($"Категорія: {GetCategory(bmi)}");
            Console.WriteLine($"Підсумок: {GetSummary(bmi, isSporty)}");

            bool isAdiposity = bmi >= 30;
            if (isAdiposity)
            {
                Console.WriteLine("У вас ожиріння");
            }
        }

        static double ReadDouble(string prompt, double min, double max)
        {
            while (true)
            {
                Console.Write(prompt);
                string text = (Console.ReadLine() ?? "").Replace(',', '.');

                if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out double value)
                    && value >= min && value <= max)
                {
                    return value;
                }
                Console.WriteLine($"Помилка: введіть число від {min} до {max}.");
            }
        }

        static bool ReadYesNo(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string answer = (Console.ReadLine() ?? "").Trim().ToLower();

                if (answer == "так") return true;
                if (answer == "ні") return false;
                Console.WriteLine("Помилка: введіть «так» або «ні».");
            }
        }

        static string GetCategory(double bmi)
        {
            if (bmi < 18.5) return "дефіцит маси";
            if (bmi < 25) return "норма";
            if (bmi < 30) return "надлишкова маса";
            return "ожиріння";
        }

        static string GetSummary(double bmi, bool isSporty)
        {
            if (bmi < 16) return "критичний дефіцит: потрібна консультація лікаря";
            if (bmi < 18.5) return "помірний дефіцит: варто збільшити калорійність раціону";
            if (bmi < 25) return "здорова вага: підтримуйте поточний режим";
            if (bmi < 30)
            {
                return isSporty
                    ? "підвищений ІМТ, імовірно через м'язову масу"
                    : "надлишкова маса: варто збільшити фізичну активність";
            }
            if (bmi < 35) return "ожиріння I ступеня: рекомендовано консультацію лікаря";
            return "ожиріння II–III ступеня: потрібна консультація лікаря";
        }
    }
}