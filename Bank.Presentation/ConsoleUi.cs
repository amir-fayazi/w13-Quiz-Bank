namespace Bank.Presentation;

public static class ConsoleUi
{
    public static string? ReadOrBack(string message)
    {
        Console.Write($"{message} (0 = Back): ");

        var input = Console.ReadLine()?.Trim();

        return input == "0" ? null : input;
    }

    public static decimal? ReadDecimalOrBack(string message)
    {
        while (true)
        {
            var input = ReadOrBack(message);

            if (input is null)
                return null;

            if (decimal.TryParse(input, out var value))
                return value;

            ShowError("Please enter a valid number.");
        }
    }

    public static bool AskRetry()
    {
        Console.WriteLine();
        Console.Write("Try again? (Y = Yes / any other key = Back): ");

        var input = Console.ReadLine();

        return string.Equals(
            input,
            "Y",
            StringComparison.OrdinalIgnoreCase);
    }

    public static void ShowError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void ShowSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(true);
    }
}