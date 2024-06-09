namespace HomeWork3_15_2;
public class DBWorkers {
    private const int _maximumWorkersCount = 5;
    private int _enteredWorkersCount = 0;
    private Worker[] _workers = new Worker[_maximumWorkersCount];

    private bool MenuItemF2Allowed { 
        get {
            return _enteredWorkersCount < _maximumWorkersCount;
        }
    }  

    private bool MenuItemF3Allowed {
        get {
            return _enteredWorkersCount > 0;
        }
    }

    private bool MenuItemF4Allowed {
        get {
            return _enteredWorkersCount > 0;
        }
    }

    public void Start() {
        ConsoleKeyInfo k;
        do {
            DrawMainMenu();
            k = Console.ReadKey(true);
            try {
                switch (k.Key) {
                    case ConsoleKey.F2:
                        if (MenuItemF2Allowed) {
                            EnterWorkersInfo();
                        }
                        break;

                    case ConsoleKey.F3:
                        if (MenuItemF3Allowed) {
                            SearchWorkers();
                        }
                        break;

                    case ConsoleKey.F4:
                        if (MenuItemF4Allowed) {
                            _enteredWorkersCount = 0;
                        }
                        break;
                }
            } catch (ReturnToMainMenuExcepion e) {
               // DrawMainMenu();
            }
        } while (k.Key != ConsoleKey.F5);
    }

    private void DrawMainMenu() {
        Console.Clear();
        ConsoleUtility.DrawWindow(0, 0, 60, 20, "Домашня робота #15.2", ConsoleColor.Green);
        ConsoleUtility.DrawText(5, 4, $"База робітників ({_enteredWorkersCount} з {_maximumWorkersCount} заповнено)", ConsoleColor.Blue);
        ConsoleUtility.DrawText(5, 6, "Оберіть режим роботи:", ConsoleColor.White);
        ConsoleUtility.DrawText(5, 7, "F2 - введення інформації про робітників", MenuItemF2Allowed ? ConsoleColor.White : ConsoleColor.DarkGray);
        ConsoleUtility.DrawText(5, 8, "F3 - пошук робітників за стажем", MenuItemF3Allowed ? ConsoleColor.White : ConsoleColor.DarkGray);
        ConsoleUtility.DrawText(5, 9, "F4 - очистити базу робітників", MenuItemF4Allowed ?  ConsoleColor.White : ConsoleColor.DarkGray);
        ConsoleUtility.DrawText(5, 10, "F5 - закрити програму", ConsoleColor.White);
        Console.CursorVisible = false;
    }

    private void EnterWorkersInfo() {
        var worker = new Worker();

        Console.Clear();
        ConsoleUtility.DrawWindow(0, 0, 80, 23, $"Введення інформації про робітника #{_enteredWorkersCount + 1} із {_maximumWorkersCount}", ConsoleColor.Green);
        ConsoleUtility.DrawText(2, 21, "F1 - головне меню", ConsoleColor.Yellow);

        #region введення імя

        ConsoleUtility.DrawText(5, 4, "Введіть імя", ConsoleColor.Blue);
        ConsoleUtility.DrawTopHorizontalLine(5, 6, 50, ConsoleColor.Blue);
        Console.CursorVisible = true;
        Console.SetCursorPosition(5, 5);
        var cp = ConsoleUtility.ReadLineAdvanced();
        if (cp.F1Pressed) {
            throw new ReturnToMainMenuExcepion();
        } else {
            worker.Name = cp.Value;
        }
        #endregion

        #region веденя посади
        ConsoleUtility.DrawText(5, 8, "Введіть посаду", ConsoleColor.Blue);
        ConsoleUtility.DrawTopHorizontalLine(5, 10, 50, ConsoleColor.Blue);
        Console.SetCursorPosition(5, 9);
        cp = ConsoleUtility.ReadLineAdvanced();
        if (cp.F1Pressed) {
            throw new ReturnToMainMenuExcepion();
        } else {
            worker.Position = cp.Value;
        }
        #endregion

        #region введеня року 
        ConsoleUtility.DrawText(5, 12, "рік надходження працювати", ConsoleColor.Blue);
        ConsoleUtility.DrawTopHorizontalLine(5, 14, 50, ConsoleColor.Blue);


        bool hasError;
        do {
            hasError = false;

            try {
                Console.SetCursorPosition(5, 13);
                cp = ConsoleUtility.ReadLineAdvanced();
                if (cp.F1Pressed) {
                    throw new ReturnToMainMenuExcepion();
                } else {
                    int tempYear;
                    if (!Int32.TryParse(cp.Value, out tempYear)) {
                        throw new InvalidYearValueException($"Значення {cp.Value} не є числом");
                    } else if (tempYear < 1900 || tempYear > DateTime.Today.Year + 1) {
                        throw new InvalidYearValueException($"Рік {cp.Value} не можна використати в якості року надходження працювати");
                    } else {
                        worker.Year = tempYear;
                    }

                }

            } catch (ReturnToMainMenuExcepion x) {
                throw;
            } catch (Exception ex) {
                hasError = true;
                // clear line from 'previous' error message
                ConsoleUtility.DrawText(5, 15, new string(' ', 74), ConsoleColor.White);

                // draw error message
                ConsoleUtility.DrawText(5, 15, ex.Message, ConsoleColor.Red);

                // clear invalid year value
                ConsoleUtility.DrawText(5, 13, new string(' ', 74), ConsoleColor.White);
            }
        } while (hasError);
        #endregion

        int indexToPaste = -1;

        for (var i = 0; i < _enteredWorkersCount; i++) {
            if (_workers[i].Name.CompareTo(worker.Name) == 1) {
                indexToPaste = i;
                break;
            }
        }

        if (indexToPaste > -1) {
            for (var i = _enteredWorkersCount - 1; i >= indexToPaste; i--) {
                _workers[i + 1] = _workers[i];
            }
            _workers[indexToPaste] = worker;
        } else {
            _workers[_enteredWorkersCount] = worker;
        }
        _enteredWorkersCount++;
    }

    public void SearchWorkers() {
        Console.Clear();
        ConsoleUtility.DrawWindow(0, 0, 80, 23, $"Пошук робітників за стажем", ConsoleColor.Green);
        ConsoleUtility.DrawText(5, 4, "Введіть критерій пошуку (років)", ConsoleColor.Blue);
        ConsoleUtility.DrawTopHorizontalLine(5, 6, 50, ConsoleColor.Blue);
        ConsoleUtility.DrawText(2, 21, "F1 - головне меню", ConsoleColor.Yellow);
        Console.CursorVisible = true;
        bool hasError;
        int result = 0;
        do {
            do {

                hasError = false;
                Console.SetCursorPosition(5, 5);
                var cr = ConsoleUtility.ReadLineAdvanced();
                for (int i = 0; i < 5; i++) {
                    ConsoleUtility.DrawText(5, 9 + i, new string(' ', 74), ConsoleColor.White);
                }
                if (cr.F1Pressed) {
                    throw new ReturnToMainMenuExcepion();
                } else {
                    if (!Int32.TryParse(cr.Value, out result)) {
                        ConsoleUtility.DrawText(5, 8, new string(' ', 74), ConsoleColor.White);
                        ConsoleUtility.DrawText(5, 5, new string(' ', 74), ConsoleColor.White);
                        ConsoleUtility.DrawText(5, 8, "Введене вами значення не є числом", ConsoleColor.Red);
                        hasError = true;
                    } else if (result > 100 || result < 0) {
                        ConsoleUtility.DrawText(5, 8, new string(' ', 74), ConsoleColor.White);
                        ConsoleUtility.DrawText(5, 5, new string(' ', 74), ConsoleColor.White);
                        ConsoleUtility.DrawText(5, 8, "Введене вами значення некоректне", ConsoleColor.Red);
                        hasError = true;
                    } else {
                        ConsoleUtility.DrawText(5, 8, new string(' ', 74), ConsoleColor.White);
                        ConsoleUtility.DrawText(5, 5, new string(' ', 74), ConsoleColor.White);
                    }
                }
            }
            while (hasError);
            ConsoleUtility.DrawText(5, 8, $"Робітники зі стажем більше {result} р.:", ConsoleColor.Yellow);
            int b = 0;
            for (int i = 0; i < _enteredWorkersCount; i++) {
                var a = DateTime.Today.Year - _workers[i].Year;
                if (result < a) {
                    b++;
                    ConsoleUtility.DrawText(5, 9 + b, $"{b}. {_workers[i].Name} - стаж {a} років", ConsoleColor.Green);
                }
            }
            if (b == 0) {
                ConsoleUtility.DrawText(5, 9, "не знайдено", ConsoleColor.Red);
            }
        } while (true);
    } 
}
