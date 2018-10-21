﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskSchedulerCore;

namespace TaskScedulerDemoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.InitializeTaskList();
        }

        private void InitializeTaskList()
        {
            TaskCollection taskCollection = new TaskCollection();

            RecurringTask vacuum = new RecurringTask("Run vacuum.")
            {
                Interval = new TimeSpan(7, 23, 0, 0)
            };

            RecurringTask dishes = new RecurringTask("Wash dishes.")
            {
                Interval = new TimeSpan(1, 23, 0, 0)
            };

            dishes.AddOccurrence(new DateTime(2018, 10, 19), new TimeSpan(0, 20, 0));

            taskCollection.AddTask(dishes);
            taskCollection.AddTask(vacuum);

            this.taskList.ItemsSource = taskCollection;
        }
    }
}
