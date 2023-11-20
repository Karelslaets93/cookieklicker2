using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace cookieklicker2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _cookieCount = 0;
        private double _originalWidth;
        private double _originalHeight;

        public MainWindow()
        {
            InitializeComponent();
            _originalWidth = imgCookie.Width;
            _originalHeight = imgCookie.Height;
        }

        private void imgCookie_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _cookieCount++;
            txtCookieCount.Text = _cookieCount + " Cookies";
            this.Title = _cookieCount + " Cookies";
            imgCookie.Width = _originalWidth * 0.9;
            imgCookie.Height = _originalHeight * 0.9;
        }

        private void imgCookie_MouseUp(object sender, MouseButtonEventArgs e)
        {
            imgCookie.Width = _originalWidth;
            imgCookie.Height = _originalHeight;
        }
    }
}


