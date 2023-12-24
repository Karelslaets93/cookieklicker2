using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace cookieklicker2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double _cookieCount = 0;
        private double _originalWidth;
        private double _originalHeight;
        private bool _isMouseDown = false;
        private int _cursorCount = 0;
        private int _grandmaCount = 0;
        private int _farmCount = 0;
        private int _mineCount = 0;
        private int _factoryCount = 0;

        private DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();
            _originalWidth = imgCookie.Width;
            _originalHeight = imgCookie.Height;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(10);
            _timer.Tick += Timer_Tick;
            _timer.Start();


            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0;
            doubleAnimation.To = 100;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(2));
            doubleAnimation.AutoReverse = true;
            doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;

            progressBar.BeginAnimation(ProgressBar.ValueProperty, doubleAnimation);
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            double opbrengstPerTick = 0;
            opbrengstPerTick += _cursorCount * 0.001;
            opbrengstPerTick += _grandmaCount * 0.01;
            opbrengstPerTick += _farmCount * 0.08;
            opbrengstPerTick += _mineCount * 0.47;
            opbrengstPerTick += _factoryCount * 2.6;

            _cookieCount += opbrengstPerTick;
            UpdateCookieCount();

            UpdateButtons();
        }

        private void imgCookie_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _isMouseDown = true;
            imgCookie.Width = _originalWidth * 0.9;
            imgCookie.Height = _originalHeight * 0.9;
        }

        private void imgCookie_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_isMouseDown)
            {
                _cookieCount++;
                UpdateCookieCount();
            }
            _isMouseDown = false;
            imgCookie.Width = _originalWidth;
            imgCookie.Height = _originalHeight;

            UpdateButtons();

        }

        private void btnCursor_Click(object sender, RoutedEventArgs e)
        {
            int basisprijs = 15;
            int nieuwePrijs = (int)Math.Ceiling(basisprijs * Math.Pow(1.15, _cursorCount));

            if (_cookieCount >= nieuwePrijs)
            {
                _cookieCount -= nieuwePrijs;
                _cursorCount++;
                UpdateCookieCount();
                txtCursorCount.Text = "Count: " + _cursorCount;

                UpdateButtons();
                UpdateCookiesPerSecond();
            }
        }

        private void btnGrandma_Click(object sender, RoutedEventArgs e)
        {
            int basisprijs = 100;
            int nieuwePrijs = (int)Math.Ceiling(basisprijs * Math.Pow(1.15, _grandmaCount));

            if (_cookieCount >= nieuwePrijs)
            {
                _cookieCount -= nieuwePrijs;
                _grandmaCount++;
                UpdateCookieCount();
                txtGrandmaCount.Text = "Count: " + _grandmaCount;

                UpdateButtons();
                UpdateCookiesPerSecond();
            }
        }

        private void btnFarm_Click(object sender, RoutedEventArgs e)
        {
            int basisprijs = 1100;
            int nieuwePrijs = (int)Math.Ceiling(basisprijs * Math.Pow(1.15, _farmCount));

            if (_cookieCount >= nieuwePrijs)
            {
                _cookieCount -= nieuwePrijs;
                _farmCount++;
                UpdateCookieCount();
                txtFarmCount.Text = "Count: " + _farmCount;

                UpdateButtons();
                UpdateCookiesPerSecond();
            }
        }

        private void btnMine_Click(object sender, RoutedEventArgs e)
        {
            int basisprijs = 12000;
            int nieuwePrijs = (int)Math.Ceiling(basisprijs * Math.Pow(1.15, _mineCount));

            if (_cookieCount >= nieuwePrijs)
            {
                _cookieCount -= nieuwePrijs;
                _mineCount++;
                UpdateCookieCount();
                txtMineCount.Text = "Count: " + _mineCount;

                UpdateButtons();
                UpdateCookiesPerSecond();
            }
        }

        private void btnFactory_Click(object sender, RoutedEventArgs e)
        {
            int basisprijs = 130000;
            int nieuwePrijs = (int)Math.Ceiling(basisprijs * Math.Pow(1.15, _factoryCount));

            if (_cookieCount >= nieuwePrijs)
            {
                _cookieCount -= nieuwePrijs;
                _factoryCount++;
                UpdateCookieCount();
                txtFactoryCount.Text = "Count: " + _factoryCount;

                UpdateButtons();
                UpdateCookiesPerSecond();
            }
        }

        private void UpdateButtons()
        {
            double cursorPrijs = (double)Math.Ceiling(15 * Math.Pow(1.15, _cursorCount));
            double grandmaPrijs = (double)Math.Ceiling(100 * Math.Pow(1.15, _grandmaCount));
            double farmPrijs = (double)Math.Ceiling(1100 * Math.Pow(1.15, _farmCount));
            double minePrijs = (double)Math.Ceiling(12000 * Math.Pow(1.15, _mineCount));
            double factoryPrijs = (double)Math.Ceiling(130000 * Math.Pow(1.15, _factoryCount));

            btnCursor.IsEnabled = _cookieCount >= cursorPrijs;
            btnGrandma.IsEnabled = _cookieCount >= grandmaPrijs;
            btnFarm.IsEnabled = _cookieCount >= farmPrijs;
            btnMine.IsEnabled = _cookieCount >= minePrijs;
            btnFactory.IsEnabled = _cookieCount >= factoryPrijs;

            txtCursorCost.Text = "Cursor: " + FormatNumber(cursorPrijs);
            txtGrandmaCost.Text = "Grandma: " + FormatNumber(grandmaPrijs);
            txtFarmCost.Text = "Farm: " + FormatNumber(farmPrijs);
            txtMineCost.Text = "Mine: " + FormatNumber(minePrijs);
            txtFactoryCost.Text = "Factory: " + FormatNumber(factoryPrijs);
        }

        private string FormatNumber(double num)
        {
            if (num >= 1e18)
            {
                return Math.Round(num / 1e18, 3).ToString("0.###") + " Quintillion";
            }
            else if (num >= 1e15)
            {
                return Math.Round(num / 1e15, 3).ToString("0.###") + " Quadrillion";
            }

            else if (num >= 1e12)
            {
                return Math.Round(num / 1e12, 3).ToString("0.###") + " Triljoen";
            }
            else if (num >= 1e9)
            {
                return Math.Round(num / 1e9, 3).ToString("0.###") + " Biljard";
            }
            else if (num >= 1e6)
            {
                return Math.Round(num / 1e6, 3).ToString("0.###") + " Miljoen";
            }
            else
            {
                return Math.Round(num).ToString();
            }
        }

        private void UpdateCookieCount()
        {
            string formattedNumber = FormatNumber(_cookieCount);
            txtCookieCount.Text = formattedNumber;
            this.Title = formattedNumber + " Cookies";
        }


        private void UpdateCookiesPerSecond()
        {
            double cookiesPerSecond = _cursorCount * 0.1 + _grandmaCount * 1 + _farmCount * 8 + _mineCount * 47 + _factoryCount * 260;
            txtCookiesPerSecond.Text = string.Format("{0:0.0}", cookiesPerSecond) + " cookies/second";
        }
    }
}
