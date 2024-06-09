namespace HomeWork3_15_5;

public  class Manager {

    private Calculator _calculator = new Calculator();

    public void Start() {
        ConsoleKeyInfo k;
        DrawMainMenu();

        do {
            Console.CursorVisible = false;
            k = Console.ReadKey(true);
            try {
                switch (k.Key) {
                    case ConsoleKey.F1:
                        Enter1stNumber();
                        break;

                    case ConsoleKey.F2:
                        Enter2stNumber();
                        break;

                    case ConsoleKey.F3:
                        var a = _calculator.Add();
                        PutResult(a, "+");                       
                        break;

                    case ConsoleKey.F4:
                       var b = _calculator.Sub();
                        PutResult(b, "-");
                        break;

                    case ConsoleKey.F5:
                        var c = _calculator.Mul();
                        PutResult(c, "*");
                        break;
                    case ConsoleKey.F6:
                        var d = _calculator.Div();
                        PutResult(d, "/");
                        break;
                }
            } catch (Exception e) {
                DrawError(e);
                ClearResult();
            }
        } while (k.Key != ConsoleKey.Escape);
    }

    private void PutResult(int value, string op) {
        ConsoleUtility.DrawText(5, 14, new string(' ', 80), ConsoleColor.Yellow);
        ConsoleUtility.DrawText(5, 14, $"{value}", ConsoleColor.Yellow);
        ConsoleUtility.DrawText(5, 17, new string(' ', 80), ConsoleColor.Yellow);

        ConsoleUtility.DrawText(4, 10, op , ConsoleColor.Red);
    }

    private void PutResult(float a, string op) {
        ConsoleUtility.DrawText(5, 14, new string(' ', 80), ConsoleColor.Yellow);
        ConsoleUtility.DrawText(5, 14, $"{a}", ConsoleColor.Yellow);
        ConsoleUtility.DrawText(5, 17, new string(' ', 80), ConsoleColor.Yellow);

        ConsoleUtility.DrawText(4, 10, op, ConsoleColor.Red);
    }

    private void ClearResult() {
        ConsoleUtility.DrawText(5, 14,new string(' ', 80), ConsoleColor.Blue);
    }

    private void Enter2stNumber() {
        ClearResult();
        ConsoleUtility.DrawText(4, 10, " ", ConsoleColor.Red);
        ConsoleUtility.DrawText(5, 10, new string(' ', 80), ConsoleColor.White);
        Console.SetCursorPosition(5, 10);
        Console.CursorVisible = true;

        var cr = ConsoleUtility.ReadLineAdvanced();
        int tmp;
        if (Int32.TryParse(cr.Value, out tmp)) {
            _calculator.Number2 = tmp;
        } else {
            _calculator.Number2 = null;
            throw new Exception($"Значення {cr.Value} некоректне");
        }
    }

    private void DrawError(Exception e) {
        ConsoleUtility.DrawText(5, 17, e.Message, ConsoleColor.Red);
    }

    private void Enter1stNumber() {
        ClearResult();
        ConsoleUtility.DrawText(4, 10, " ", ConsoleColor.Red);
        ConsoleUtility.DrawText(5, 6, new string(' ', 80), ConsoleColor.White);
        Console.SetCursorPosition(5, 6);
        Console.CursorVisible = true;

        var cr = ConsoleUtility.ReadLineAdvanced();
        int tmp;
        if (Int32.TryParse(cr.Value, out tmp)) {
            _calculator.Number1 = tmp;
        } else {
            _calculator.Number1 = null;
            throw new Exception($"Значення {cr.Value} некоректне");
        }
    }

    private void DrawMainMenu() {
        Console.Clear();
        ConsoleUtility.DrawWindow(0, 0, 110, 21, "Домашня робота #15.5", ConsoleColor.Green);

        ConsoleUtility.DrawText(5, 5, "Перше число", ConsoleColor.Blue);
        ConsoleUtility.DrawTopHorizontalLine(5, 7, 30, ConsoleColor.Blue);

        ConsoleUtility.DrawText(5, 9, "Друге число", ConsoleColor.Blue);
        ConsoleUtility.DrawTopHorizontalLine(5, 11, 30, ConsoleColor.Blue);

        ConsoleUtility.DrawText(5, 13, "Результат", ConsoleColor.Blue);
        ConsoleUtility.DrawText(4, 14, "=", ConsoleColor.Red);
        ConsoleUtility.DrawTopHorizontalLine(5, 15, 30, ConsoleColor.Blue);

        var btn1Text = "F1 Ввод першого числа";
        ConsoleUtility.DrawText(2, 19, btn1Text, ConsoleColor.Black, ConsoleColor.Yellow);

        var btn2Text = "F2 Ввод другого числа";
        ConsoleUtility.DrawText(2 + btn1Text.Length + 1, 19, btn2Text, ConsoleColor.Black, ConsoleColor.Yellow);

        var btn3Text = "F3 Додати";
        ConsoleUtility.DrawText(2 + btn1Text.Length + btn2Text.Length + 2, 19, btn3Text, ConsoleColor.Black, ConsoleColor.Yellow);

        var btn4Text = "F4 Відняти";
        ConsoleUtility.DrawText(2 + btn1Text.Length + btn2Text.Length + btn3Text.Length + 3, 19, btn4Text, ConsoleColor.Black, ConsoleColor.Yellow);

        var btn5Text = "F5 Множити";
        ConsoleUtility.DrawText(2 + btn1Text.Length + btn2Text.Length + btn3Text.Length + btn4Text.Length + 4, 19, btn5Text, ConsoleColor.Black, ConsoleColor.Yellow);

        var btn6Text = "F6 Ділити";
        ConsoleUtility.DrawText(2 + btn1Text.Length + btn2Text.Length + btn3Text.Length + btn4Text.Length + btn5Text.Length + 5, 19, btn6Text, ConsoleColor.Black, ConsoleColor.Yellow);

        ConsoleUtility.DrawText(2 + btn1Text.Length + btn2Text.Length + btn3Text.Length + btn4Text.Length + btn5Text.Length + btn6Text.Length + 6, 19, "Escape - вихід", ConsoleColor.Black, ConsoleColor.Yellow);
    }

}
