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
using System.IO;
using System.Windows;

namespace Sportik;

public partial class Sportiki : Window
{
    public Sportiki()
    {
        InitializeComponent();
        ShowTable(fullTable);
        FillStatus();
    }
    
    string fullTable = "SELECT спортсмены.id, спортсмены.ФИО, спортсмены.Возраст, пол.Пол, вид_спорта.Наименование_спорта, спортивный_разряд.Разряд, команды.Название_команды, тренера.ФИОТ FROM спортсмены JOIN пол on спортсмены.Пол = пол.ID JOIN вид_спорта on спортсмены.Вид_спорт = вид_спорта.id JOIN спортивный_разряд on спортсмены.Спортивный_Раз = спортивный_разряд.id JOIN команды on спортсмены.Команда = команды.id JOIN тренера on спортсмены.Тренер = тренера.id";

    private List<Sport> sports;
    private List<Pol> pols;
    string connStr = "server=localhost;database=kr;port=3306;User Id=root;password=root";
    private MySqlConnection conn;

    public void ShowTable(string sql)
    {
        sports = new List<Sport>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var Client = new Sport()
            {
                id = reader.GetInt32("id"),
                ФИО = reader.GetString("ФИО"),
                Возраст = reader.GetInt32("Возраст"),
                Пол = reader.GetString("Пол"),
                Наименование_спорта = reader.GetString("Наименование_спорта"),
                Разряд = reader.GetString("Разряд"),
                Название_команды = reader.GetString("Название_команды"),
                ФИОТ = reader.GetString("ФИОТ")
            };
            sports.Add(Client);
        }
        conn.Close();
        DataGrid.ItemsSource = sports;
    }
    
    private void SearchGoods(object? sender, TextChangedEventArgs e)
    {
        var gds = sports;
        gds = gds.Where(x => x.ФИО.Contains(Search_Goods.Text)).ToList();
        DataGrid.ItemsSource = gds;
    }
    
    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        MainWindow back = new MainWindow();
        Close();
        back.Show(); 
    }

    private void Reset_OnClick(object? sender, RoutedEventArgs e)
    {
        ShowTable(fullTable);
        Search_Goods.Text = string.Empty;
    }

    private void Del(object? sender, RoutedEventArgs e)
    {
        try
        {
            Sport usr = DataGrid.SelectedItem as Sport;
            if (usr == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM спортсмены WHERE id = " + usr.id;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            sports.Remove(usr);
            ShowTable(fullTable);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    
    private void AddData(object? sender, RoutedEventArgs e)
    {
        Sport newsport = new Sport();
        addoredit add = new addoredit(newsport, sports);
        add.Show();
        this.Close();
    }

    private void Edit(object? sender, RoutedEventArgs e)
    {
        Sport currensSport = DataGrid.SelectedItem as Sport;
        if (currensSport == null)
            return;
        addoredit edit = new  addoredit(currensSport, sports);
        edit.Show();
        this.Close();
    }

    private void CmbStatus(object? sender, SelectionChangedEventArgs e)
    {
        var genderComboBox = (ComboBox)sender;
        var currentGender = genderComboBox.SelectedItem as Pol;
        var filteredUsers = sports
            .Where(x => x.Пол == currentGender.Пол)
            .ToList();
        DataGrid.ItemsSource = filteredUsers;
    }
    
    public void FillStatus()
    {
        pols = new List<Pol>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand("select * from пол", conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentGender = new Pol()
            {
                ID = reader.GetInt32("ID"),
                Пол = reader.GetString("Пол"),

            };
            pols.Add(currentGender);
        }
        conn.Close();
        var genderComboBox = this.Find<ComboBox>("CmbGender");
        genderComboBox.ItemsSource = pols;
    }
}