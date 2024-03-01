using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;
using Avalonia;


namespace Sportik;

public partial class addoredit : Window
{
    private List<Sport> Sports;
    private Sport CurrenSport;
    public addoredit(Sport currenSport, List<Sport> sports)
    {
        InitializeComponent();
        CurrenSport = currenSport;
        this.DataContext = currenSport;
        Sports = sports;
    }
    
    
    private MySqlConnection conn;
    string connStr = "server=localhost;database=kr;port=3306;User Id=root;password=root";

    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var usr = Sports.FirstOrDefault(x => x.id == CurrenSport.id);
        if (usr == null)
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO спортсмены VALUES (" + Convert.ToInt32(id.Text) + ", '" + ФИО.Text + "', " + Convert.ToInt32(Возраст.Text) + ", " + Convert.ToInt32(Пол.Text) + ", " + Convert.ToInt32(Вид_спорт.Text) + ", " + Convert.ToInt32(Спортивный_Раз.Text) + ", " + Convert.ToInt32(Команда.Text) + ", " + Convert.ToInt32(Тренер.Text) + ");";
                MySqlCommand cmd = new MySqlCommand(add, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error" + exception);
            }
        }
        else
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string upd = "UPDATE спортсмены SET ФИО = '" + ФИО.Text + "', Возраст = " + Convert.ToInt32(Возраст.Text) + ", Пол = " + Convert.ToInt32(Пол.Text) + ", Вид_спорт = " + Convert.ToInt32(Вид_спорт.Text) + ", Спортивный_Раз = " + Convert.ToInt32(Спортивный_Раз.Text) + ", Команда = " + Convert.ToInt32(Команда.Text) + ", Тренер = " + Convert.ToInt32(Тренер.Text) + " WHERE id = " + Convert.ToInt32(id.Text) + ";";
                MySqlCommand cmd = new MySqlCommand(upd, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.Write("Error" + exception);
            }
        }
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        Sportiki back = new Sportiki();
        this.Close();
        back.Show(); 
    }
}