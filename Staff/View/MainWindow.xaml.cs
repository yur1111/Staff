﻿using Staff.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Staff.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ListView AllDepartmentsView;
        public static ListView AllPositionsView;
        public static ListView AllUsersView;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DataManageVM();
            AllDepartmentsView = ViewAllDepartments;
            AllPositionsView = ViewAllPositions;
            AllUsersView = ViewAllUsers;
        }
    }
}
