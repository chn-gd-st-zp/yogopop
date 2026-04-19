namespace YogoPop.Core.Tool;

public class Printor
{
    public static void PrintText(string text)
    {
        Console.WriteLine(text);
    }

    public static void PrintLine()
    {
        PrintText("------------------------------------------------------------");
    }

    public static string PrintHtmlSpace(int times = 1)
    {
        var result = string.Empty;

        var unit = "&nbsp;&nbsp;";

        for (var i = 0; i < times; i++)
            result += unit;

        return result;
    }
}