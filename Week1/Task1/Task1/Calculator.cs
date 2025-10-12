namespace Task1
{
    public class Calculator
    {
        static void Main(string[] args)
        {
            bool continueInput = true;

            while (continueInput)
            {
                try
                {
                    double firstNumber = GetNumber("Введите первое число: ");

                    char operation = GetOperation();

                    double secondNumber = GetNumber("Введите второе число: ");

                    double result = DoOperation(firstNumber, secondNumber, operation);

                    Console.WriteLine($"Ответ: {firstNumber} {operation} {secondNumber} = {result}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка: {e.Message}");
                }

                continueInput = AskToContinue();
            }

        }
        static double DoOperation(double first, double second, char operation)
        {
            switch (operation)
            {
                case '+': return first + second;
                case '-': return first - second;
                case '*': return first * second;
                case '/':
                    if (second == 0)
                    {
                        throw new DivideByZeroException("Деление на ноль невозможно");
                    }
                    return first / second;
                default: throw new ArgumentException("Неизвестная операция");
            }
        }

        static double GetNumber(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (double.TryParse(input, out double number))
                {
                    return number;
                }
                else
                {
                    Console.WriteLine("Ошибка: пожалуйста, введите число правильно");
                }
            }
        }

        static char GetOperation()
        {
            while (true)
            {
                Console.Write("Выберите операцию (+, -, *, /):");
                string inputOperation = Console.ReadLine();

                if (!string.IsNullOrEmpty(inputOperation) && inputOperation.Length == 1 && "+-*/".Contains(inputOperation[0]))
                {
                    return inputOperation[0];
                }
                else
                {
                    Console.WriteLine("Ошибка: выберите одну из следующих операций: +, -, *, /");
                }
            }
        }
        static bool AskToContinue()
        {
            while (true)
            {
                Console.Write("Хотите ли вы провести еще одну операцию? (y/n):");
                string input = Console.ReadLine().ToLower();

                if (input == "y")
                {
                    return true;
                }
                else if (input == "n")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Введите «y» для продолжения или «n» для выхода.");
                }
            }
        }
    }
}
