namespace HomeWork3_15_5;

public  class Calculator {

    public int? Number1 { get; set; }
    public int? Number2 { get; set; }

    private void CheckNumbers() {
        if (Number1 == null)
            throw new ArgumentException("Число №1 не задано");

        if (Number2 == null)
            throw new ArgumentException("Число №2 не задано");
    }

    public int Add() {
        CheckNumbers();
        return Number1.Value + Number2.Value;
    }

    public int Sub() {
        CheckNumbers();
        return Number1.Value - Number2.Value;
    }

    public int Mul() {
        CheckNumbers();
        return Number1.Value * Number2.Value;
    }

    public float Div() {
        CheckNumbers();
        if (Number2.Value == 0) {
            throw new DivideByZeroException($"ділення на 0 неможливе");
        }
        return Number1.Value / (float)Number2.Value; 
    }

}
