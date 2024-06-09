namespace HomeWork3_15_5;

public static class ConsoleUtility {

    public static ConsoleInputResult ReadLineAdvanced() {
        var res = new ConsoleInputResult();
        res.Value = "";
        //res.F1Pressed = false;

        Console.CursorVisible = true;
        while (true) {
            var userKey = Console.ReadKey(true);
            if (userKey.Key == ConsoleKey.Enter) {
                break;
            //} else if (userKey.Key == ConsoleKey.F1) {
            //    res.F1Pressed = true;
            //    break;
            } else if (userKey.Key == ConsoleKey.Backspace) {
                if (res.Value.Length > 0) {
                    res.Value = res.Value.Substring(0, res.Value.Length - 1);
                    var cp = Console.GetCursorPosition();
                    Console.SetCursorPosition(cp.Left - 1, cp.Top);
                    Console.Write(' ');
                    Console.SetCursorPosition(cp.Left - 1, cp.Top);
                }
            } else {
                if (userKey.KeyChar != 0) {
                    res.Value += userKey.KeyChar;
                    Console.Write(userKey.KeyChar);
                }
            }
        }

        return res;
    }

    public static void DrawWindow(int startX, int startY, int width, int height, string title, ConsoleColor titleColor) {
        if (height < 4) {
            throw new Exception("Height cannot be less than 4");
        }

        // draw 1st line
        Console.SetCursorPosition(startX, startY);
        Console.Write("\u250c");
        for (int i = 0; i < width - 2; i++) {
            Console.Write("\u2500");
        }
        Console.Write("\u2510");

        // draw title
        Console.SetCursorPosition(startX + 2, startY + 1);
        var bu = Console.ForegroundColor;
        Console.ForegroundColor = titleColor;
        Console.WriteLine(title);
        Console.ForegroundColor = bu;

        // draw vertical lines for title
        // |                                             |
        Console.SetCursorPosition(startX, startY + 1);
        Console.Write("\u2502");
        Console.SetCursorPosition(startX + width - 1, startY + 1);
        Console.Write("\u2502");

        // draw under-title line
        Console.SetCursorPosition(startX, startY + 2);
        Console.Write("\u251c");
        for (int i = 0; i < width - 2; i++) {
            Console.Write("\u2500");
        }
        Console.Write("\u2524");

        // |                                             |
        for (int i = 0; i < height - 4; i++) {
            Console.SetCursorPosition(startX, startY + 3 + i);
            Console.Write("\u2502");

            Console.SetCursorPosition(startX + width - 1, startY + 3 + i);
            Console.Write("\u2502");
        }

        // draw last line
        Console.SetCursorPosition(startX, startY + height - 1);
        Console.Write("\u2514");
        for (int i = 0; i < width - 2; i++) {
            Console.Write("\u2500");
        }
        Console.Write("\u2518");
    }

    public static void DrawRectangle(int startX, int startY, int width, int height) {
        Console.SetCursorPosition(startX, startY);
        Console.Write("\u250c");
        for (int i = 0; i < width - 2; i++) {
            Console.Write("\u2500");
        }
        Console.Write("\u2510");


        for (int i = 0; i < height - 2; i++) {
            Console.SetCursorPosition(startX, startY + 1 + i);
            Console.Write("\u2502");

            Console.SetCursorPosition(startX + width - 1, startY + 1 + i);
            Console.Write("\u2502");
        }

        Console.SetCursorPosition(startX, startY + height - 1);
        Console.Write("\u2514");
        for (int i = 0; i < width - 2; i++) {
            Console.Write("\u2500");
        }
        Console.Write("\u2518");
    }

    public static void DrawText(int startX, int startY, string text, ConsoleColor color) {
        Console.SetCursorPosition(startX, startY);
        var backupColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = backupColor;
    }

    public static void DrawText(int startX, int startY, string text, ConsoleColor txtColor, ConsoleColor bgColor) {
        Console.SetCursorPosition(startX, startY);
        var backupColor = Console.ForegroundColor;
        var backupBgColor = Console.BackgroundColor;

        Console.ForegroundColor = txtColor;
        Console.BackgroundColor = bgColor;
        Console.WriteLine(text);
        Console.ForegroundColor = backupColor;
        Console.BackgroundColor= backupBgColor;
    }

    public static void DrawBottomHorizontalLine(int startX, int startY, int width, ConsoleColor color) {
        Console.SetCursorPosition(startX, startY);
        var backupColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        for (int i = 0; i < width; i++) {
            Console.Write("\u2581");
        }
        Console.ForegroundColor = backupColor;
    }

    public static void DrawTopHorizontalLine(int startX, int startY, int width, ConsoleColor color) {
        Console.SetCursorPosition(startX, startY);
        var backupColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        for (int i = 0; i < width; i++) {
            Console.Write("\u2594");
        }
        Console.ForegroundColor = backupColor;
    }

}

public class ConsoleInputResult {
    public string Value { get; set; }
    //public bool F1Pressed { get; set; }
}