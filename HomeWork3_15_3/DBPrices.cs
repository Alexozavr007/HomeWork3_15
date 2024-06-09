namespace HomeWork3_15_3;

public class DBPrices {
    private const int _maximumPricesCount = 2;
    private int _enteredPricesCount = 0;
    private Price[] _prices = new Price[_maximumPricesCount];

    private bool MenuItemF2Allowed { 
        get {
            return _enteredPricesCount < _maximumPricesCount;
        }
    }  

    private bool MenuItemF3Allowed {
        get {
            return _enteredPricesCount > 0;
        }
    }

    private bool MenuItemF4Allowed {
        get {
            return _enteredPricesCount > 0;
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
                            EnterPricesInfo();
                        }
                        break;

                    case ConsoleKey.F3:
                        if (MenuItemF3Allowed) {
                            SearchPrices();
                        }
                        break;

                    case ConsoleKey.F4:
                        if (MenuItemF4Allowed) {
                            _enteredPricesCount = 0;
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
        ConsoleUtility.DrawWindow(0, 0, 60, 20, "Домашня робота #15.3", ConsoleColor.Green);
        ConsoleUtility.DrawText(5, 4, $"База товарів ({_enteredPricesCount} з {_maximumPricesCount} заповнено)", ConsoleColor.Blue);
        ConsoleUtility.DrawText(5, 6, "Оберіть режим роботи:", ConsoleColor.White);
        ConsoleUtility.DrawText(5, 7, "F2 - введення інформації про товар", MenuItemF2Allowed ? ConsoleColor.White : ConsoleColor.DarkGray);
        ConsoleUtility.DrawText(5, 8, "F3 - пошук товарів", MenuItemF3Allowed ? ConsoleColor.White : ConsoleColor.DarkGray);
        ConsoleUtility.DrawText(5, 9, "F4 - очистити базу товарів", MenuItemF4Allowed ?  ConsoleColor.White : ConsoleColor.DarkGray);
        ConsoleUtility.DrawText(5, 10, "F5 - закрити програму", ConsoleColor.White);
        Console.CursorVisible = false;
    }

    private void EnterPricesInfo() {
        var Price = new Price();

        Console.Clear();
        ConsoleUtility.DrawWindow(0, 0, 80, 23, $"Введення інформації про товар #{_enteredPricesCount + 1} із {_maximumPricesCount}", ConsoleColor.Green);
        ConsoleUtility.DrawText(2, 21, "F1 - головне меню", ConsoleColor.Yellow);

        #region введення імя

        ConsoleUtility.DrawText(5, 4, "Введіть назву товару", ConsoleColor.Blue);
        ConsoleUtility.DrawTopHorizontalLine(5, 6, 50, ConsoleColor.Blue);
        Console.CursorVisible = true;
        Console.SetCursorPosition(5, 5);
        var cp = ConsoleUtility.ReadLineAdvanced();
        if (cp.F1Pressed) {
            throw new ReturnToMainMenuExcepion();
        } else {
            Price.Name = cp.Value;
        }
        #endregion

        #region веденя посади
        ConsoleUtility.DrawText(5, 8, "Введіть назву магазину", ConsoleColor.Blue);
        ConsoleUtility.DrawTopHorizontalLine(5, 10, 50, ConsoleColor.Blue);
        Console.SetCursorPosition(5, 9);
        cp = ConsoleUtility.ReadLineAdvanced();
        if (cp.F1Pressed) {
            throw new ReturnToMainMenuExcepion();
        } else {
            Price.Shop = cp.Value;
        }
        #endregion

        #region введеня ціни 
        ConsoleUtility.DrawText(5, 12, "Ціна", ConsoleColor.Blue);
        ConsoleUtility.DrawTopHorizontalLine(5, 14, 20, ConsoleColor.Blue);


        bool hasError;
        do {
            hasError = false;

            try {
                Console.SetCursorPosition(5, 13);
                cp = ConsoleUtility.ReadLineAdvanced();
                if (cp.F1Pressed) {
                    throw new ReturnToMainMenuExcepion();
                } else {
                    int tempMoney;
                    if (!Int32.TryParse(cp.Value, out tempMoney)) {
                        throw new InvalidMoneyValueException($"Значення {cp.Value} не є числом");
                    } else if (tempMoney < 0 ) {
                        throw new InvalidMoneyValueException($"Вартість має бути додатнім числом");
                    } else {
                        Price.Money = tempMoney;
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

        for (var i = 0; i < _enteredPricesCount; i++) {
            if (_prices[i].Name.CompareTo(Price.Name) == 1) {
                indexToPaste = i;
                break;
            }
        }

        if (indexToPaste > -1) {
            for (var i = _enteredPricesCount - 1; i >= indexToPaste; i--) {
                _prices[i + 1] = _prices[i];
            }
            _prices[indexToPaste] = Price;
        } else {
            _prices[_enteredPricesCount] = Price;
        }
        _enteredPricesCount++;
    }

    public void SearchPrices() {
        Console.Clear();
        ConsoleUtility.DrawWindow(0, 0, 80, 23, $"Пошук товарів за назвою магазину", ConsoleColor.Green);
        ConsoleUtility.DrawText(5, 4, "Назва магазину", ConsoleColor.Blue);
        ConsoleUtility.DrawTopHorizontalLine(5, 6, 50, ConsoleColor.Blue);
        ConsoleUtility.DrawText(2, 21, "F1 - головне меню", ConsoleColor.Yellow);
        Console.CursorVisible = true;
        do {

            try {
                string shopName = "";
                Console.SetCursorPosition(5, 5);
                var cr = ConsoleUtility.ReadLineAdvanced();

                // clear search results
                for (int i = 0; i < 5; i++) {
                    ConsoleUtility.DrawText(5, 9 + i, new string(' ', 74), ConsoleColor.White);
                }

                // clear previous error
                ConsoleUtility.DrawText(5, 8, new string(' ', 74), ConsoleColor.White);

                if (cr.F1Pressed) {
                    throw new ReturnToMainMenuExcepion();
                } else {
                    if (cr.Value.Trim() == "") {
                        throw new InvalidShopException("Пусте значення");
                    } else {
                        shopName = cr.Value;

                        //clear current shop name
                        ConsoleUtility.DrawText(5, 5, new string(' ', 74), ConsoleColor.White);
                    }
                }

                ConsoleUtility.DrawText(5, 8, $"Товари з магазину {shopName}:", ConsoleColor.Yellow);
                int b = 0;
                for (int i = 0; i < _enteredPricesCount; i++) {
                    if (_prices[i].Shop == shopName) {
                        b++;
                        ConsoleUtility.DrawText(5, 9 + b, $"{b}. {_prices[i].Name} - {_prices[i].Money} грн.", ConsoleColor.Green);
                    }
                }

                if (b == 0) {
                    throw new InvalidShopException($"товарів з магазину {shopName} не знайдено");
                }
            } catch (InvalidShopException e) {
                // clear previous error
                ConsoleUtility.DrawText(5, 8, new string(' ', 74), ConsoleColor.White);

                //clear current shop name
                ConsoleUtility.DrawText(5, 5, new string(' ', 74), ConsoleColor.White);

                //display error message
                ConsoleUtility.DrawText(5, 8, e.Message, ConsoleColor.Red);
            }

        } while (true);
    } 
}
