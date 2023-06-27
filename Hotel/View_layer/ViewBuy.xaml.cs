﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hotel.Data_layer;
using Hotel.Entity_layer;


namespace Hotel.View_layer
{
    public partial class ViewBuy 
    {
        public ViewBuy()
        {
            InitializeComponent();
        }

        private void btncontrolDirect_Click(object sender, RoutedEventArgs e)
        {
            ViewOrder vieworder = new ViewOrder();
            contentControl.Content = vieworder;
        }
        private void btncontrolProgram_Click(object sender, RoutedEventArgs e)
        {
            ViewOrderProgram vieworderprogram = new ViewOrderProgram();
            contentControl.Content = vieworderprogram;
        }
    }
}
