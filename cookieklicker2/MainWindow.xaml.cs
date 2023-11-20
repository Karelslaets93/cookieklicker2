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
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace cookieklicker2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double _cookieCount = 10000;
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
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            double opbrengstPerTick = 0;
            opbrengstPerTick += _cursorCount * 0.001;
            opbrengstPerTick += _grandmaCount * 0.01;
            opbrengstPerTick += _farmCount * 0.08;
            opbrengstPerTick += _mineCount * 0.47;
            opbrengstPerTick += _factoryCount * 2.60;

            _cookieCount += opbrengstPerTick;
            int cookieCountAsInt = (int)Math.Floor(_cookieCount);
            txtCookieCount.Text = cookieCountAsInt + " Cookies";
            this.Title = cookieCountAsInt + " Cookies";

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
                txtCookieCount.Text = _cookieCount + " Cookies";
                this.Title = _cookieCount + " Cookies";
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
                txtCookieCount.Text = _cookieCount + " Cookies";
                this.Title = _cookieCount + " Cookies";
                txtCursorCount.Text = "Count: " + _cursorCount;

                UpdateButtons();
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
                txtCookieCount.Text = _cookieCount + " Cookies";
                this.Title = _cookieCount + " Cookies";
                txtGrandmaCount.Text = "Count: " + _grandmaCount;

                UpdateButtons();
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
                txtCookieCount.Text = _cookieCount + " Cookies";
                this.Title = _cookieCount + " Cookies";
                txtFarmCount.Text = "Count: " + _farmCount;

                UpdateButtons();
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
                txtCookieCount.Text = _cookieCount + " Cookies";
                this.Title = _cookieCount + " Cookies";
                txtMineCount.Text = "Count: " + _mineCount;

                UpdateButtons();
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
                txtCookieCount.Text = _cookieCount + " Cookies";
                this.Title = _cookieCount + " Cookies";
                txtFactoryCount.Text = "Count: " + _factoryCount;

                UpdateButtons();
            }
        }


        private void UpdateButtons()
        {
            int cursorPrijs = (int)Math.Ceiling(15 * Math.Pow(1.15, _cursorCount));
            int grandmaPrijs = (int)Math.Ceiling(100 * Math.Pow(1.15, _grandmaCount));
            int farmPrijs = (int)Math.Ceiling(1100 * Math.Pow(1.15, _farmCount));
            int minePrijs = (int)Math.Ceiling(12000 * Math.Pow(1.15, _mineCount));
            int factoryPrijs = (int)Math.Ceiling(130000 * Math.Pow(1.15, _factoryCount));

            btnCursor.IsEnabled = _cookieCount >= cursorPrijs;
            btnGrandma.IsEnabled = _cookieCount >= grandmaPrijs;
            btnFarm.IsEnabled = _cookieCount >= farmPrijs;
            btnMine.IsEnabled = _cookieCount >= minePrijs;
            btnFactory.IsEnabled = _cookieCount >= factoryPrijs;

            txtCursorCost.Text = "Cursor: " + cursorPrijs;
            txtGrandmaCost.Text = "Grandma: " + grandmaPrijs;
            txtFarmCost.Text = "Farm: " + farmPrijs;
            txtMineCost.Text = "Mine: " + minePrijs;
            txtFactoryCost.Text = "Factory: " + factoryPrijs;
        }

    }
}