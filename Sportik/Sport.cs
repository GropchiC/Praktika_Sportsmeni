namespace Sportik;

public class Sport
{
    public int id { get; set; }
    public string ФИО { get; set; }
    public int Возраст { get; set; }
    public string Пол { get; set; }
    public string Наименование_спорта { get; set; }
    public string Разряд { get; set; }
    public string Название_команды { get; set; }
    public string ФИОТ { get; set; }
}

public class Pol
{
    public int ID { get; set; }
    public string Пол { get; set; }
}
