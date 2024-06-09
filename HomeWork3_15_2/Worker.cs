namespace HomeWork3_15_2;
public struct Worker {
    public string Name { get; set; }
    public string Position { get; set; }
    public int Year { get; set; }

    internal void Clear() {
        this.Name = null;
        this.Position = null;
        this.Year = 0;


    }
}

