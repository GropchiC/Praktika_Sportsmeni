using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Sportik;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    private void Exit_OnClick(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }

    private void Sportiki(object? sender, RoutedEventArgs e)
    {
        Sportiki spr  = new Sportiki();
        this.Hide();
        spr.Show();
    }
}