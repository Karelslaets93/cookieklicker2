using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
        private double _cookieCount =0;
        private bool _isMouseDown = false;
        private int _cursorCount = 0;
        private int _grandmaCount = 0;
        private int _farmCount = 0;
        private int _mineCount = 0;
        private int _factoryCount = 0;
        private int _bankCount = 0;
        private int _templeCount = 0;
        private DispatcherTimer _timer;
        private bool _hasCursorEverBeenVisible = false;
        private bool _hasGrandmaEverBeenVisible = false;
        private bool _hasFarmEverBeenVisible = false;
        private bool _hasMineEverBeenVisible = false;
        private bool _hasFactoryEverBeenVisible = false;
        private bool _hasBankEverBeenVisible = false;
        private bool _hasTempleEverBeenVisible = false;

        private Dictionary<string, StackPanel> upgradePanels = new Dictionary<string, StackPanel>();


        private List<string> upgradeOrder = new List<string>
        {
            "Cursor",
            "Grandma",
            "Farm",
            "Mine",
            "Factory",
            "Bank",
            "Tempel" 
        };

        private List<Brush> backgroundColors = new List<Brush>
        {
            Brushes.LightBlue,
            Brushes.LightCoral,
            Brushes.LightGreen,
            Brushes.LightYellow,
            Brushes.LightPink,
            Brushes.LimeGreen

         };

     
        private int nextColorIndex = 0;

        public MainWindow()
        {
            InitializeComponent();



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

            foreach (string upgradeName in upgradeOrder)
            {
                StackPanel panel = new StackPanel
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 10, 0, 10),  
                    Background = backgroundColors[nextColorIndex]  
                };
                nextColorIndex = (nextColorIndex + 1) % backgroundColors.Count;
                upgradePanels[upgradeName] = panel;
                upgradeImagesPanel.Children.Add(panel);
            }
        }

        private List<System.Windows.Controls.Image> fallingCookies = new List<System.Windows.Controls.Image>();

        private void GenerateFallingCookie()
        {
            if (fallingCookies.Count >= 50)
            {
                return;
            }

            System.Windows.Controls.Image fallingCookie = new System.Windows.Controls.Image();
            fallingCookie.Source = new BitmapImage(new Uri("C:\\Users\\karel\\Documents\\c#\\cookieklicker2\\cookieklicker2\\CookieClickerAfb\\cookie.png"));
            fallingCookie.Width = 20;
            fallingCookie.Height = 20;

            Canvas.SetTop(fallingCookie, 0);
            Canvas.SetLeft(fallingCookie, new Random().Next(0, (int)cookieViewbox.ActualWidth));

            cookieCanvas.Children.Add(fallingCookie);
            fallingCookies.Add(fallingCookie);

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0;
            doubleAnimation.To = cookieViewbox.ActualHeight;
            doubleAnimation.Duration = TimeSpan.FromSeconds(3);
            doubleAnimation.Completed += (sender, e) =>
            {
                cookieCanvas.Children.Remove(fallingCookie);
                fallingCookies.Remove(fallingCookie);
            };

            fallingCookie.BeginAnimation(Canvas.TopProperty, doubleAnimation);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            double opbrengstPerTick = 0;
            opbrengstPerTick += _cursorCount * 0.001;
            opbrengstPerTick += _grandmaCount * 0.01;
            opbrengstPerTick += _farmCount * 0.08;
            opbrengstPerTick += _mineCount * 0.47;
            opbrengstPerTick += _factoryCount * 2.6;
            opbrengstPerTick += _bankCount * 14;
            opbrengstPerTick += _templeCount * 780;

            _cookieCount += opbrengstPerTick;
            UpdateCookieCount();

            UpdateButtons();
        }

    

    private void imgCookie_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _isMouseDown = true;
            imgCookie.RenderTransform = new ScaleTransform(0.9, 0.9);
        }

        private void imgCookie_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_isMouseDown)
            {
                _cookieCount++;
                UpdateCookieCount();
                // Genereer een cookie bij elke klik
                GenerateFallingCookie();
            }
            _isMouseDown = false;
            imgCookie.RenderTransform = new ScaleTransform(1, 1);

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
                AddUpgradeImage("Cursor");
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
                AddUpgradeImage("Grandma");
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
                AddUpgradeImage("Farm");
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
                AddUpgradeImage("Mine");
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
                AddUpgradeImage("Factory");
            }
        }

        private void btnBank_Click(object sender, RoutedEventArgs e)
        {
            int basisprijs = 1400000;
            int nieuwePrijs = (int)Math.Ceiling(basisprijs * Math.Pow(1.15, _bankCount));

            if (_cookieCount >= nieuwePrijs)
            {
                _cookieCount -= nieuwePrijs;
                _bankCount++;
                UpdateCookieCount();
                txtBankCount.Text = "Count: " + _bankCount;

                UpdateButtons();
                UpdateCookiesPerSecond();
                AddUpgradeImage("Bank");
            }
        }

        private void btnTemple_Click(object sender, RoutedEventArgs e)
        {
            int basisprijs = 20000000;
            int nieuwePrijs = (int)Math.Ceiling(basisprijs * Math.Pow(1.15, _templeCount));

            if (_cookieCount >= nieuwePrijs)
            {
                _cookieCount -= nieuwePrijs;
                _templeCount++;
                UpdateCookieCount();
                txtTempleCount.Text = "Count: " + _templeCount;

                UpdateButtons();
                UpdateCookiesPerSecond();
                AddUpgradeImage("Tempel");
            }
        }

        private void UpdateButtons()
        {
            double cursorPrijs = (double)Math.Ceiling(15 * Math.Pow(1.15, _cursorCount));
            double grandmaPrijs = (double)Math.Ceiling(100 * Math.Pow(1.15, _grandmaCount));
            double farmPrijs = (double)Math.Ceiling(1100 * Math.Pow(1.15, _farmCount));
            double minePrijs = (double)Math.Ceiling(12000 * Math.Pow(1.15, _mineCount));
            double factoryPrijs = (double)Math.Ceiling(130000 * Math.Pow(1.15, _factoryCount));
            double bankPrijs = (double)Math.Ceiling(1400000 * Math.Pow(1.15, _bankCount));
            double templePrijs = (double)Math.Ceiling(20000000 * Math.Pow(1.15, _templeCount));


            if (_cookieCount >= cursorPrijs)
            {
                _hasCursorEverBeenVisible = true;
            }
            btnCursor.Visibility = _hasCursorEverBeenVisible ? Visibility.Visible : Visibility.Collapsed;

            if (_cookieCount >= grandmaPrijs)
            {
                _hasGrandmaEverBeenVisible = true;
            }
            btnGrandma.Visibility = _hasGrandmaEverBeenVisible ? Visibility.Visible : Visibility.Collapsed;

            if (_cookieCount >= farmPrijs)
            {
                _hasFarmEverBeenVisible = true;
            }
            btnFarm.Visibility = _hasFarmEverBeenVisible ? Visibility.Visible : Visibility.Collapsed;

            if (_cookieCount >= minePrijs)
            {
                _hasMineEverBeenVisible = true;
            }
            btnMine.Visibility = _hasMineEverBeenVisible ? Visibility.Visible : Visibility.Collapsed;

            if (_cookieCount >= factoryPrijs)
            {
                _hasFactoryEverBeenVisible = true;
            }
            btnFactory.Visibility = _hasFactoryEverBeenVisible ? Visibility.Visible : Visibility.Collapsed;

            if (_cookieCount >= bankPrijs)
            {
                _hasBankEverBeenVisible = true;
            }
            btnBank.Visibility = _hasBankEverBeenVisible ? Visibility.Visible : Visibility.Collapsed;

            if (_cookieCount >= templePrijs)
            {
                _hasTempleEverBeenVisible = true;
            }
            btnTemple.Visibility = _hasTempleEverBeenVisible ? Visibility.Visible : Visibility.Collapsed;



            btnCursor.IsEnabled = _cookieCount >= cursorPrijs;
            btnGrandma.IsEnabled = _cookieCount >= grandmaPrijs;
            btnFarm.IsEnabled = _cookieCount >= farmPrijs;
            btnMine.IsEnabled = _cookieCount >= minePrijs;
            btnFactory.IsEnabled = _cookieCount >= factoryPrijs;
            btnBank.IsEnabled = _cookieCount >= bankPrijs;
            btnTemple.IsEnabled = _cookieCount >= templePrijs;

            txtCursorCost.Text = "Cursor: " + FormatNumber(cursorPrijs);
            txtGrandmaCost.Text = "Grandma: " + FormatNumber(grandmaPrijs);
            txtFarmCost.Text = "Farm: " + FormatNumber(farmPrijs);
            txtMineCost.Text = "Mine: " + FormatNumber(minePrijs);
            txtFactoryCost.Text = "Factory: " + FormatNumber(factoryPrijs);
            txtBankCost.Text = "Bank: " + FormatNumber(bankPrijs);
            txtTempleCost.Text = "Temple: " + FormatNumber(templePrijs);
        }
        private string FormatNumber(double num)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberGroupSeparator = " ";

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
            else if (num >= 1000)
            {
                return num.ToString("N0", nfi);
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



        private void lblBakeryName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            string newBakeryName = Microsoft.VisualBasic.Interaction.InputBox("Geef een nieuwe naam aan je bakkerij:", "Bakkerij Naam", lblBakeryName.Content.ToString());

            if (!string.IsNullOrWhiteSpace(newBakeryName))
            {
                lblBakeryName.Content = newBakeryName;
            }
            else
            {
                MessageBox.Show("Naam mag niet leeg zijn of uit witruimte bestaan", "Foutieve Naam", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }




        private void AddUpgradeImage(string upgradeName)
        {
           
            System.Windows.Controls.Image upgradeImage = new System.Windows.Controls.Image();
           
            upgradeImage.Source = new BitmapImage(new Uri($"C:\\Users\\karel\\Documents\\c#\\cookieklicker2\\cookieklicker2\\CookieClickerAfb\\{upgradeName}.png"));
  
            upgradeImage.Width = 50;  
            upgradeImage.Height = 50; 

            upgradePanels[upgradeName].Children.Add(upgradeImage);
        }


        private void UpdateCookiesPerSecond()
        {
            double cookiesPerSecond = _cursorCount * 0.1 + _grandmaCount * 1 + _farmCount * 8 + _mineCount * 47 + _factoryCount * 260 + _templeCount * 7800 + _bankCount * 1400;
            txtCookiesPerSecond.Text = FormatNumber(cookiesPerSecond) + " cookies/second";
        }

    }
}
