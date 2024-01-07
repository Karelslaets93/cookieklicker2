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
        // Lijsten voor de quests, beloningen en voltooiingsstatus van quests
        private List<string> quests = new List<string>();
        private List<double> questRewards = new List<double>();
        private List<bool> questCompleted = new List<bool>();

        // Variabelen voor het bijhouden van de cookie count en verschillende gebouwen
        private double _cookieCount = 1000000;
        private bool _isMouseDown = false;
        private int _cursorCount = 0;
        private int _grandmaCount = 0;
        private int _farmCount = 0;
        private int _mineCount = 0;
        private int _factoryCount = 0;
        private int _bankCount = 0;
        private int _templeCount = 0;

        // Timers voor het hoofdvenster en gouden koekjes
        private DispatcherTimer _timer;
        private DispatcherTimer _goldenCookieTimer;
        private Random _random = new Random();

        // Booleans om bij te houden of de cursor, oma, boerderij, mijn, fabriek, bank, en tempel ooit zichtbaar zijn geweest
        private bool _hasCursorEverBeenVisible = false;
        private bool _hasGrandmaEverBeenVisible = false;
        private bool _hasFarmEverBeenVisible = false;
        private bool _hasMineEverBeenVisible = false;
        private bool _hasFactoryEverBeenVisible = false;
        private bool _hasBankEverBeenVisible = false;
        private bool _hasTempleEverBeenVisible = false;

        // Dictionary om upgrade panels bij te houden
        private Dictionary<string, StackPanel> upgradePanels = new Dictionary<string, StackPanel>();

        // Volgorde van upgrades en kleuren voor achtergronden
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

        private int nextColorIndex = 0;  // Index voor het volgende kleurenschema

        // Constructor voor het hoofdvenster
        public MainWindow()
        {
            InitializeComponent();

            // Initialisatie van de timers
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(10);
            _timer.Tick += Timer_Tick;
            _timer.Start();

            _goldenCookieTimer = new DispatcherTimer();
            _goldenCookieTimer.Interval = TimeSpan.FromSeconds(10);
            _goldenCookieTimer.Tick += GoldenCookieTimer_Tick;
            _goldenCookieTimer.Start();

            // Animatie voor de voortgangsbalk
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0;
            doubleAnimation.To = 100;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(2));
            doubleAnimation.AutoReverse = true;
            doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;

            progressBar.BeginAnimation(ProgressBar.ValueProperty, doubleAnimation);

            // Initialisatie van de upgrade panels
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

            InitializeQuests();
        }
        // Methode om quests te initialiseren
        private void InitializeQuests()
        {
            // Voeg quests toe aan de lijsten
            quests.Add("Clicker Beginner");
            questRewards.Add(500);
            questCompleted.Add(false);

            quests.Add("Grandma Enthusiast");
            questRewards.Add(1000);
            questCompleted.Add(false);

            quests.Add("Farm Collector");
            questRewards.Add(1500);
            questCompleted.Add(false);

            quests.Add("Cookie Conqueror");
            questRewards.Add(2000);
            questCompleted.Add(false);

            quests.Add("Mining Magnate");
            questRewards.Add(3000);
            questCompleted.Add(false);

            quests.Add("Banking Tycoon");
            questRewards.Add(5000);
            questCompleted.Add(false);

            quests.Add("Temple Master");
            questRewards.Add(10000);
            questCompleted.Add(false);

            quests.Add("Cookie Kingpin");
            questRewards.Add(15000);
            questCompleted.Add(false);

            quests.Add("Grandma Overlord");
            questRewards.Add(8000);
            questCompleted.Add(false);

            quests.Add("Factory Maestro");
            questRewards.Add(12000);
            questCompleted.Add(false);

            quests.Add("Farm Emperor");
            questRewards.Add(6000);
            questCompleted.Add(false);

            quests.Add("Cursor Commander");
            questRewards.Add(3000);
            questCompleted.Add(false);

            quests.Add("Cookie Clicker Supreme");
            questRewards.Add(20000);
            questCompleted.Add(false);

            quests.Add("Grandma's Secret Recipe");
            questRewards.Add(10000);
            questCompleted.Add(false);

            quests.Add("Mine Maestro");
            questRewards.Add(7000);
            questCompleted.Add(false);

            quests.Add("Banking Guru");
            questRewards.Add(12000);
            questCompleted.Add(false);

            quests.Add("Temple Titan");
            questRewards.Add(15000);
            questCompleted.Add(false);

            quests.Add("Factory Mogul");
            questRewards.Add(18000);
            questCompleted.Add(false);

            quests.Add("Grandma's Legacy");
            questRewards.Add(12000);
            questCompleted.Add(false);

            quests.Add("Clicker Legend");
            questRewards.Add(2500);
            questCompleted.Add(false);

            quests.Add("Cookie Craze");
            questRewards.Add(4000);
            questCompleted.Add(false);

            quests.Add("Cursor Collector");
            questRewards.Add(3500);
            questCompleted.Add(false);
        }
        private void CheckQuests()
        {
            // Loop door alle quests
            for (int i = 0; i < quests.Count; i++)
            {
                // Controleer of de huidige quest nog niet is voltooid en of deze nu voltooid is
                if (!questCompleted[i] && IsQuestCompleted(i))
                {
                    // Markeer de quest als voltooid
                    questCompleted[i] = true;

                    // Toon een bericht over de voltooide quest met beloningen
                    ShowQuestCompletionMessage(quests[i], questRewards[i]);

                    // Voeg de beloningen toe aan het aantal cookies en update de cookie telling
                    _cookieCount += questRewards[i];
                    UpdateCookieCount();
                }
            }
        }

        // Controleert of een specifieke quest voltooid is op basis van de huidige spelersstatus
        private bool IsQuestCompleted(int questIndex)
        {
            // Gebruik een switch statement om de specifieke voltooiingsvoorwaarden voor elke quest te controleren
            switch (quests[questIndex])
            {
                case "Clicker Beginner":
                    return _cursorCount >= 100;
                case "Grandma Enthusiast":
                    return _grandmaCount >= 5;
                case "Farm Collector":
                    return _farmCount >= 3;
                case "Cookie Conqueror":
                    return _cursorCount >= 200 && _grandmaCount >= 10;
                case "Mining Magnate":
                    return _mineCount >= 5;
                case "Banking Tycoon":
                    return _bankCount >= 3;
                case "Temple Master":
                    return _templeCount >= 1;
                case "Cookie Kingpin":
                    return _cookieCount >= 50000;
                case "Grandma Overlord":
                    return _grandmaCount >= 20;
                case "Factory Maestro":
                    return _factoryCount >= 10;
                case "Farm Emperor":
                    return _farmCount >= 5;
                case "Cursor Commander":
                    return _cursorCount >= 300;
                case "Cookie Clicker Supreme":
                    return _cookieCount >= 100000;
                case "Grandma's Secret Recipe":
                    return _grandmaCount >= 30;
                case "Mine Maestro":
                    return _mineCount >= 10;
                case "Banking Guru":
                    return _bankCount >= 5;
                case "Temple Titan":
                    return _templeCount >= 3;
                case "Factory Mogul":
                    return _factoryCount >= 15;
                case "Grandma's Legacy":
                    return _grandmaCount >= 50;
                case "Clicker Legend":
                    return _cursorCount >= 500;
                case "Cookie Craze":
                    return _cookieCount >= 20000;
                case "Cursor Collector":
                    return _cursorCount >= 1000;
                default:
                    // Als de quest niet wordt herkend, wordt deze als niet voltooid beschouwd
                    return false;
            }
        }


        private void ShowQuestCompletionMessage(string questName, double rewardCookies)
        {
            // Toon een bericht wanneer een quest is voltooid, inclusief de naam en beloningen
            MessageBox.Show($"Quest Voltooid: {questName}\nBeloning: {FormatNumber(rewardCookies)} cookies", "Quest Voltooid", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnQuestLog_Click(object sender, RoutedEventArgs e)
        {
            // Initialiseer StringBuilder-objecten voor voltooide en onvoltooide quests
            StringBuilder completedQuestsLog = new StringBuilder();
            StringBuilder incompleteQuestsLog = new StringBuilder();

            // Loop door alle quests om informatie toe te voegen aan de bijbehorende logs
            for (int i = 0; i < quests.Count; i++)
            {
                string questEntry = $"{quests[i]} - {(questCompleted[i] ? "Voltooid" : "Niet voltooid")}";

                // Voeg vereisten toe als de quest niet is voltooid
                if (!questCompleted[i])
                {
                    string requirements = GetQuestRequirements(i);
                    questEntry += $"    Vereisten: {requirements}";
                }

                // Voeg de vermelding toe aan het juiste loggedeelte
                if (questCompleted[i])
                {
                    completedQuestsLog.AppendLine(questEntry);
                }
                else
                {
                    incompleteQuestsLog.AppendLine(questEntry);
                }
            }

            // Combineer de voltooide en onvoltooide secties
            string questLogMessage = $"{completedQuestsLog}{incompleteQuestsLog}";

            // Maak een ScrollViewer met een TextBlock-inhoud voor de quest log
            ScrollViewer scrollViewer = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                Content = new TextBlock { Text = questLogMessage }
            };

            // Maak een nieuw venster voor de quest log en toon het
            Window questLogWindow = new Window
            {
                Title = "Quest Log",
                Content = scrollViewer,
                SizeToContent = SizeToContent.WidthAndHeight,
                ResizeMode = ResizeMode.CanResize,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };

            questLogWindow.ShowDialog();
        }

        // Haal de vereisten op voor een specifieke quest op basis van de huidige spelersstatus
        private string GetQuestRequirements(int questIndex)
        {
            // Initialiseer een StringBuilder voor de vereisten
            StringBuilder requirements = new StringBuilder();

            // Gebruik een switch statement om specifieke vereisten voor elke quest toe te voegen
            switch (quests[questIndex])
            {
                case "Clicker Beginner":
                    requirements.AppendLine($"Cursor Telling: 100");
                    break;
                case "Grandma Enthusiast":
                    requirements.AppendLine($"Grandma Telling: 5");
                    break;
                case "Farm Collector":
                    requirements.AppendLine($"Farm Telling: 3");
                    break;
                case "Cookie Conqueror":
                    requirements.AppendLine($"Cursor Telling: 200");
                    requirements.AppendLine($"Grandma Telling: 10");
                    break;
                case "Mining Magnate":
                    requirements.AppendLine($"Mine Telling: 5");
                    break;
                case "Banking Tycoon":
                    requirements.AppendLine($"Bank Telling: 3");
                    break;
                case "Temple Master":
                    requirements.AppendLine($"Temple Telling: 1");
                    break;
                case "Cookie Kingpin":
                    requirements.AppendLine($"Cookie Telling: 50,000");
                    break;
                case "Grandma Overlord":
                    requirements.AppendLine($"Grandma Telling: 20");
                    break;
                case "Factory Maestro":
                    requirements.AppendLine($"Factory Telling: 10");
                    break;
                case "Farm Emperor":
                    requirements.AppendLine($"Farm Telling: 5");
                    break;
                case "Cursor Commander":
                    requirements.AppendLine($"Cursor Telling: 300");
                    break;
                case "Cookie Clicker Supreme":
                    requirements.AppendLine($"Cookie Telling: 100,000");
                    break;
                case "Grandma's Secret Recipe":
                    requirements.AppendLine($"Grandma Telling: 30");
                    break;
                case "Mine Maestro":
                    requirements.AppendLine($"Mine Telling: 10");
                    break;
                case "Banking Guru":
                    requirements.AppendLine($"Bank Telling: 5");
                    break;
                case "Temple Titan":
                    requirements.AppendLine($"Temple Telling: 3");
                    break;
                case "Factory Mogul":
                    requirements.AppendLine($"Factory Telling: 15");
                    break;
                case "Grandma's Legacy":
                    requirements.AppendLine($"Grandma Telling: 50");
                    break;
                case "Clicker Legend":
                    requirements.AppendLine($"Cursor Telling: 500");
                    break;
                case "Cookie Craze":
                    requirements.AppendLine($"Cookie Telling: 20,000");
                    break;
                case "Cursor Collector":
                    requirements.AppendLine($"Cursor Telling: 1,000");
                    break;
                default:
                    requirements.AppendLine("Onbekende Vereisten");
                    break;
            }

            return requirements.ToString();
        }

        private void GoldenCookieTimer_Tick(object sender, EventArgs e)
        {
            // Genereer een willekeurig getal tussen 0 en 99 (representeert percentage)
            int randomPercentage = _random.Next(100);

            // 30% kans op een gouden cookie
            if (randomPercentage < 30)
            {
                GenerateGoldenCookie();
            }
        }
        private void GenerateGoldenCookie()
        {
            System.Windows.Controls.Image goldenCookie = new System.Windows.Controls.Image();
            goldenCookie.Source = new BitmapImage(new Uri("C:\\Users\\karel\\Documents\\c#\\cookieklicker2\\cookieklicker2\\CookieClickerAfb\\golden.png"));
            goldenCookie.Width = 40;
            goldenCookie.Height = 40;

            // Plaats de gouden cookie in het midden van het scherm
            Canvas.SetTop(goldenCookie, (cookieViewbox.ActualHeight - goldenCookie.Height) / 2);
            Canvas.SetLeft(goldenCookie, (cookieViewbox.ActualWidth - goldenCookie.Width) / 2);

            cookieCanvas.Children.Add(goldenCookie);

            // Voeg een click event handler toe
            goldenCookie.MouseUp += GoldenCookie_MouseUp;

            // Start een timer voor de levensduur van de gouden cookie (bijv. 15 seconden)
            DispatcherTimer lifeTimer = new DispatcherTimer();
            lifeTimer.Interval = TimeSpan.FromMinutes(1);
            lifeTimer.Tick += (s, e) =>
            {
                cookieCanvas.Children.Remove(goldenCookie);
                lifeTimer.Stop();
            };
            lifeTimer.Start();
        }

        private void GoldenCookie_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Verwijder de gouden cookie van het scherm
            cookieCanvas.Children.Remove((System.Windows.Controls.Image)sender);

            // Beloon de speler met 15 minuten aan cookies (afhankelijk van cookieproductie)
            double cookiesPerSecond = CalculateTotalProductionPerSecond();
            double rewardCookies = cookiesPerSecond * 60 * 15; // 15 minuten aan cookies
            _cookieCount += rewardCookies;
            UpdateCookieCount();
        }

        private double CalculateTotalProductionPerSecond()
        {
            // Bereken en retourneer het totale koekjesproductie per seconde, vergelijkbaar met Timer_Tick-methode.
            double opbrengstPerTick = 0;
            opbrengstPerTick += _cursorCount * UpgradeProduction("Cursor");
            opbrengstPerTick += _grandmaCount * UpgradeProduction("Grandma");
            opbrengstPerTick += _farmCount * UpgradeProduction("Farm");
            opbrengstPerTick += _mineCount * UpgradeProduction("Mine");
            opbrengstPerTick += _factoryCount * UpgradeProduction("Factory");
            opbrengstPerTick += _bankCount * UpgradeProduction("Bank");
            opbrengstPerTick += _templeCount * UpgradeProduction("Tempel");

            return opbrengstPerTick * 1000 / _timer.Interval.TotalMilliseconds;
        }
        // Lijst om bij te houden welke koekjes naar beneden vallen
        private List<System.Windows.Controls.Image> fallingCookies = new List<System.Windows.Controls.Image>();

        // Methode om een nieuw vallend koekje te genereren
        private void GenerateFallingCookie()
        {
            // Controleer of er al 50 vallende koekjes zijn, anders stop met genereren
            if (fallingCookies.Count >= 50)
            {
                return;
            }

            // Creëer een nieuw Image-object voor het vallende koekje
            System.Windows.Controls.Image fallingCookie = new System.Windows.Controls.Image();
            // Wijs de afbeelding toe aan het nieuwe koekje
            fallingCookie.Source = new BitmapImage(new Uri("C:\\Users\\karel\\Documents\\c#\\cookieklicker2\\cookieklicker2\\CookieClickerAfb\\cookie.png"));
            // Stel de breedte en hoogte van het koekje in
            fallingCookie.Width = 20;
            fallingCookie.Height = 20;

            // Plaats het koekje bovenaan het canvas op een willekeurige horizontale positie
            Canvas.SetTop(fallingCookie, 0);
            Canvas.SetLeft(fallingCookie, new Random().Next(0, (int)cookieViewbox.ActualWidth));

            // Voeg het koekje toe aan het canvas en de lijst van vallende koekjes
            cookieCanvas.Children.Add(fallingCookie);
            fallingCookies.Add(fallingCookie);

            // Animatie om het koekje van boven naar beneden te laten vallen
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 0;
            doubleAnimation.To = cookieViewbox.ActualHeight;
            doubleAnimation.Duration = TimeSpan.FromSeconds(3);
            doubleAnimation.Completed += (sender, e) =>
            {
                // Wanneer de animatie is voltooid, verwijder het koekje uit het canvas en de lijst van vallende koekjes
                cookieCanvas.Children.Remove(fallingCookie);
                fallingCookies.Remove(fallingCookie);
            };

            // Start de animatie voor het vallende koekje
            fallingCookie.BeginAnimation(Canvas.TopProperty, doubleAnimation);
        }

        // Methode die wordt aangeroepen bij elke tick van de timer
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Totaalopbrengst per tick initialiseren
            double opbrengstPerTick = 0;
            // Bereken de opbrengst per tick voor elke productie-upgrade
            opbrengstPerTick += _cursorCount * UpgradeProduction("Cursor");
            opbrengstPerTick += _grandmaCount * UpgradeProduction("Grandma");
            opbrengstPerTick += _farmCount * UpgradeProduction("Farm");
            opbrengstPerTick += _mineCount * UpgradeProduction("Mine");
            opbrengstPerTick += _factoryCount * UpgradeProduction("Factory");
            opbrengstPerTick += _bankCount * UpgradeProduction("Bank");
            opbrengstPerTick += _templeCount * UpgradeProduction("Tempel");

            // Voeg de opbrengst per tick toe aan het totaal aantal koekjes
            _cookieCount += opbrengstPerTick;
            // Update de weergave van het aantal koekjes
            UpdateCookieCount();
            // Controleer of er quests zijn voltooid
            CheckQuests();
            // Update de status van de knoppen
            UpdateButtons();

            // Bereken en update het aantal koekjes per seconde
            double cookiesPerSecond = opbrengstPerTick * 1000 / _timer.Interval.TotalMilliseconds;
            txtCookiesPerSecondLabel.Text = "Cookies per seconde: " + FormatNumber(cookiesPerSecond);
        }





        // Methode die wordt aangeroepen wanneer de muis op de koekjesafbeelding wordt ingedrukt
        private void imgCookie_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Markeer dat de muisknop is ingedrukt en pas een schaaltransformatie toe op de koekjesafbeelding
            _isMouseDown = true;
            imgCookie.RenderTransform = new ScaleTransform(0.9, 0.9);
        }

        // Methode die wordt aangeroepen wanneer de muis wordt losgelaten na het indrukken van de koekjesafbeelding
        private void imgCookie_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Als de muisknop is ingedrukt
            if (_isMouseDown)
            {
                // Verhoog het aantal koekjes, update de weergave, genereer een vallend koekje en controleer quests
                _cookieCount++;
                UpdateCookieCount();
                GenerateFallingCookie();
                CheckQuests();
            }

            // Markeer dat de muisknop niet meer is ingedrukt en herstel de schaaltransformatie van de koekjesafbeelding
            _isMouseDown = false;
            imgCookie.RenderTransform = new ScaleTransform(1, 1);

            // Update de status van knoppen
            UpdateButtons();
        }

        // Methode die wordt aangeroepen wanneer op de Cursor-upgradeknop wordt geklikt
        private void btnCursor_Click(object sender, RoutedEventArgs e)
        {
            // Bepaal de nieuwe prijs voor een Cursor-upgrade op basis van het huidige aantal Cursors
            int basisprijs = 15;
            int nieuwePrijs = (int)Math.Ceiling(basisprijs * Math.Pow(1.15, _cursorCount));

            // Controleer of de speler genoeg koekjes heeft om de upgrade te kopen
            if (_cookieCount >= nieuwePrijs)
            {
                // Verminder het aantal koekjes, verhoog het aantal Cursors, update de weergave en knoppenstatus
                _cookieCount -= nieuwePrijs;
                _cursorCount++;
                UpdateCookieCount();
                txtCursorCount.Text = "Count: " + _cursorCount;

                UpdateButtons();

                // Voeg een afbeelding toe voor de Cursor-upgrade en maak de bonuswinkelknop zichtbaar
                AddUpgradeImage("Cursor");
                btnBonusStore.IsEnabled = true;
                btnBonusStore.Visibility = Visibility.Visible;
            }
        }

        // Methode die wordt aangeroepen wanneer op de Grandma-upgradeknop wordt geklikt
        private void btnGrandma_Click(object sender, RoutedEventArgs e)
        {
            // Bepaal de nieuwe prijs voor een Grandma-upgrade op basis van het huidige aantal Grandmas
            int basisprijs = 100;
            int nieuwePrijs = (int)Math.Ceiling(basisprijs * Math.Pow(1.15, _grandmaCount));

            // Controleer of de speler genoeg koekjes heeft om de upgrade te kopen
            if (_cookieCount >= nieuwePrijs)
            {
                // Verminder het aantal koekjes, verhoog het aantal Grandmas, update de weergave en knoppenstatus
                _cookieCount -= nieuwePrijs;
                _grandmaCount++;
                UpdateCookieCount();
                txtGrandmaCount.Text = "Count: " + _grandmaCount;

                UpdateButtons();

                // Voeg een afbeelding toe voor de Grandma-upgrade en maak de bonuswinkelknop zichtbaar
                AddUpgradeImage("Grandma");
                btnBonusStore.IsEnabled = true;
                btnBonusStore.Visibility = Visibility.Visible;
            }
        }

        // Methode die wordt aangeroepen wanneer op de Farm-upgradeknop wordt geklikt
        private void btnFarm_Click(object sender, RoutedEventArgs e)
        {
            // Bepaal de nieuwe prijs voor een Farm-upgrade op basis van het huidige aantal Farms
            int basisprijs = 1100;
            int nieuwePrijs = (int)Math.Ceiling(basisprijs * Math.Pow(1.15, _farmCount));

            // Controleer of de speler genoeg koekjes heeft om de upgrade te kopen
            if (_cookieCount >= nieuwePrijs)
            {
                // Verminder het aantal koekjes, verhoog het aantal Farms, update de weergave en knoppenstatus
                _cookieCount -= nieuwePrijs;
                _farmCount++;
                UpdateCookieCount();
                txtFarmCount.Text = "Count: " + _farmCount;

                UpdateButtons();

                // Voeg een afbeelding toe voor de Farm-upgrade en maak de bonuswinkelknop zichtbaar
                AddUpgradeImage("Farm");
                btnBonusStore.IsEnabled = true;
                btnBonusStore.Visibility = Visibility.Visible;
            }
        }

        // Methode die wordt aangeroepen wanneer op de Mine-upgradeknop wordt geklikt
        private void btnMine_Click(object sender, RoutedEventArgs e)
        {
            // Bepaal de nieuwe prijs voor een Mine-upgrade op basis van het huidige aantal Mines
            int basisprijs = 12000;
            int nieuwePrijs = (int)Math.Ceiling(basisprijs * Math.Pow(1.15, _mineCount));

            // Controleer of de speler genoeg koekjes heeft om de upgrade te kopen
            if (_cookieCount >= nieuwePrijs)
            {
                // Verminder het aantal koekjes, verhoog het aantal Mines, update de weergave en knoppenstatus
                _cookieCount -= nieuwePrijs;
                _mineCount++;
                UpdateCookieCount();
                txtMineCount.Text = "Count: " + _mineCount;

                UpdateButtons();

                // Voeg een afbeelding toe voor de Mine-upgrade en maak de bonuswinkelknop zichtbaar
                AddUpgradeImage("Mine");
                btnBonusStore.IsEnabled = true;
                btnBonusStore.Visibility = Visibility.Visible;
            }
        }


        // Methode die wordt aangeroepen wanneer op de Factory-upgradeknop wordt geklikt
        private void btnFactory_Click(object sender, RoutedEventArgs e)
        {
            // Bepaal de nieuwe prijs voor een Factory-upgrade op basis van het huidige aantal Factories
            int basisprijs = 130000;
            int nieuwePrijs = (int)Math.Ceiling(basisprijs * Math.Pow(1.15, _factoryCount));

            // Controleer of de speler genoeg koekjes heeft om de upgrade te kopen
            if (_cookieCount >= nieuwePrijs)
            {
                // Verminder het aantal koekjes, verhoog het aantal Factories, update de weergave en knoppenstatus
                _cookieCount -= nieuwePrijs;
                _factoryCount++;
                UpdateCookieCount();
                txtFactoryCount.Text = "Count: " + _factoryCount;

                UpdateButtons();

                // Voeg een afbeelding toe voor de Factory-upgrade en maak de bonuswinkelknop zichtbaar
                AddUpgradeImage("Factory");
                btnBonusStore.IsEnabled = true;
                btnBonusStore.Visibility = Visibility.Visible;
            }
        }

        // Methode die wordt aangeroepen wanneer op de Bank-upgradeknop wordt geklikt
        private void btnBank_Click(object sender, RoutedEventArgs e)
        {
            // Bepaal de nieuwe prijs voor een Bank-upgrade op basis van het huidige aantal Banks
            int basisprijs = 1400000;
            int nieuwePrijs = (int)Math.Ceiling(basisprijs * Math.Pow(1.15, _bankCount));

            // Controleer of de speler genoeg koekjes heeft om de upgrade te kopen
            if (_cookieCount >= nieuwePrijs)
            {
                // Verminder het aantal koekjes, verhoog het aantal Banks, update de weergave en knoppenstatus
                _cookieCount -= nieuwePrijs;
                _bankCount++;
                UpdateCookieCount();
                txtBankCount.Text = "Count: " + _bankCount;

                UpdateButtons();

                // Voeg een afbeelding toe voor de Bank-upgrade en maak de bonuswinkelknop zichtbaar
                AddUpgradeImage("Bank");
                btnBonusStore.IsEnabled = true;
                btnBonusStore.Visibility = Visibility.Visible;
            }
        }

        // Methode die wordt aangeroepen wanneer op de Temple-upgradeknop wordt geklikt
        private void btnTemple_Click(object sender, RoutedEventArgs e)
        {
            // Bepaal de nieuwe prijs voor een Temple-upgrade op basis van het huidige aantal Temples
            int basisprijs = 20000000;
            int nieuwePrijs = (int)Math.Ceiling(basisprijs * Math.Pow(1.15, _templeCount));

            // Controleer of de speler genoeg koekjes heeft om de upgrade te kopen
            if (_cookieCount >= nieuwePrijs)
            {
                // Verminder het aantal koekjes, verhoog het aantal Temples, update de weergave en knoppenstatus
                _cookieCount -= nieuwePrijs;
                _templeCount++;
                UpdateCookieCount();
                txtTempleCount.Text = "Count: " + _templeCount;

                UpdateButtons();

                // Voeg een afbeelding toe voor de Temple-upgrade en maak de bonuswinkelknop zichtbaar
                AddUpgradeImage("Tempel");
                btnBonusStore.IsEnabled = true;
                btnBonusStore.Visibility = Visibility.Visible;
            }
        }


        // Methode om de zichtbaarheid en toegankelijkheid van verschillende knoppen bij te werken op basis van koekjesaantallen en kosten
        private void UpdateButtons()
        {
            // Bereken de kosten voor elke upgrade op basis van het huidige aantal gebouwen
            double cursorPrijs = (double)Math.Ceiling(15 * Math.Pow(1.15, _cursorCount));
            double grandmaPrijs = (double)Math.Ceiling(100 * Math.Pow(1.15, _grandmaCount));
            double farmPrijs = (double)Math.Ceiling(1100 * Math.Pow(1.15, _farmCount));
            double minePrijs = (double)Math.Ceiling(12000 * Math.Pow(1.15, _mineCount));
            double factoryPrijs = (double)Math.Ceiling(130000 * Math.Pow(1.15, _factoryCount));
            double bankPrijs = (double)Math.Ceiling(1400000 * Math.Pow(1.15, _bankCount));
            double templePrijs = (double)Math.Ceiling(20000000 * Math.Pow(1.15, _templeCount));

            // Controleer of de speler genoeg koekjes heeft om de Cursor-upgrade te kopen
            if (_cookieCount >= cursorPrijs)
            {
                _hasCursorEverBeenVisible = true;
            }
            // Pas de zichtbaarheid van de Cursor-knop aan op basis van eerdere zichtbaarheid
            btnCursor.Visibility = _hasCursorEverBeenVisible ? Visibility.Visible : Visibility.Collapsed;

            // Herhaal hetzelfde proces voor elk ander type gebouw
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

            // Controleer of de speler genoeg koekjes heeft om elke upgrade te kopen en pas de toegankelijkheid van de knoppen aan
            btnCursor.IsEnabled = _cookieCount >= cursorPrijs;
            btnGrandma.IsEnabled = _cookieCount >= grandmaPrijs;
            btnFarm.IsEnabled = _cookieCount >= farmPrijs;
            btnMine.IsEnabled = _cookieCount >= minePrijs;
            btnFactory.IsEnabled = _cookieCount >= factoryPrijs;
            btnBank.IsEnabled = _cookieCount >= bankPrijs;
            btnTemple.IsEnabled = _cookieCount >= templePrijs;

            // Update de weergegeven kosten van elke upgrade
            txtCursorCost.Text = "Cursor: " + FormatNumber(cursorPrijs);
            txtGrandmaCost.Text = "Grandma: " + FormatNumber(grandmaPrijs);
            txtFarmCost.Text = "Farm: " + FormatNumber(farmPrijs);
            txtMineCost.Text = "Mine: " + FormatNumber(minePrijs);
            txtFactoryCost.Text = "Factory: " + FormatNumber(factoryPrijs);
            txtBankCost.Text = "Bank: " + FormatNumber(bankPrijs);
            txtTempleCost.Text = "Temple: " + FormatNumber(templePrijs);

            // Update de zichtbaarheid van bonuswinkelknoppen op basis van een bepaalde volgorde
            foreach (var upgradeName in upgradeOrder)
            {
                UpdateBonusButtonVisibility(upgradeName);
            }
        }
        // Methode om een getal te formatteren naar een leesbare tekstuele representatie met scheidingstekens en benamingen
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

        // Methode om het aantal koekjes bij te werken in de weergave en titel van het venster
        private void UpdateCookieCount()
        {
            // Formatteer het aantal koekjes en pas de weergave en titel aan
            string formattedNumber = FormatNumber(_cookieCount);
            txtCookieCount.Text = formattedNumber;
            this.Title = formattedNumber + " Cookies";
        }

        // Methode om een nieuwe naam aan de bakkerij toe te wijzen bij het klikken op de bakkerijnaam
        private void lblBakeryName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Toon een dialoogvenster voor het invoeren van een nieuwe bakkerijnaam
            string newBakeryName = Microsoft.VisualBasic.Interaction.InputBox("Geef een nieuwe naam aan je bakkerij:", "Bakkerij Naam", lblBakeryName.Content.ToString());

            // Controleer of de ingevoerde naam geldig is en pas deze toe
            if (!string.IsNullOrWhiteSpace(newBakeryName))
            {
                lblBakeryName.Content = newBakeryName;
            }
            else
            {
                MessageBox.Show("Naam mag niet leeg zijn of uit witruimte bestaan", "Foutieve Naam", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Methode om een afbeelding toe te voegen voor een specifieke upgrade aan de upgradecontainer
        private void AddUpgradeImage(string upgradeName)
        {
            System.Windows.Controls.Image upgradeImage = new System.Windows.Controls.Image();

            // Laad de afbeelding vanuit het opgegeven pad
            upgradeImage.Source = new BitmapImage(new Uri($"C:\\Users\\karel\\Documents\\c#\\cookieklicker2\\cookieklicker2\\CookieClickerAfb\\{upgradeName}.png"));

            upgradeImage.Width = 50;
            upgradeImage.Height = 50;

            // Voeg de afbeelding toe aan de juiste container voor de upgrade
            upgradePanels[upgradeName].Children.Add(upgradeImage);
        }

        // Methode om de bonuswinkelknoppen te genereren en weer te geven
        private void btnBonusStore_Click(object sender, RoutedEventArgs e)
        {
            // Roep een methode aan om de bonusknoppen te genereren
            GenerateBonusButtons();
        }

        // Lijst van bonusknoppen en prijzen voor de bonuswinkel
        private List<Button> bonusButtons = new List<Button>();
        private Dictionary<string, int> bonusPrices = new Dictionary<string, int>();




        // Methode om bonuswinkelknoppen te genereren en weer te geven
        private void GenerateBonusButtons()
        {
            // Controleer of er nog geen bonusknoppen zijn gegenereerd
            if (bonusButtons.Count == 0)
            {
                // Loop door de lijst met upgrade namen en genereer een bonusknop voor elke upgrade
                for (int i = 0; i < upgradeOrder.Count; i++)
                {
                    string upgradeName = upgradeOrder[i];
                    int basisprijs = GetUpgradeBasisPrijs(upgradeName);
                    int bonusPrijs = basisprijs * 2;

                    // Wijs de bonusprijs toe aan de upgrade in de prijsdictionary
                    bonusPrices[upgradeName] = bonusPrijs;

                    // Maak een nieuwe bonusknop aan en configureer de weergave en gedrag
                    Button bonusButton = new Button();
                    bonusButton.Width = 200;
                    bonusButton.Height = 50;
                    bonusButton.Content = $"Bonus {upgradeName}\nPrijs: {FormatNumber(bonusPrices[upgradeName])}";
                    bonusButton.Tag = upgradeName;
                    bonusButton.Margin = new System.Windows.Thickness(5);

                    // Voeg een klikgebeurtenis toe voor de bonusknop
                    bonusButton.Click += BonusButton_Click;

                    // Voeg de bonusknop toe aan de stapel van bonusknoppen en de lijst van bonusknoppen
                    BonusStack.Children.Add(bonusButton);
                    bonusButtons.Add(bonusButton);

                    // Werk de zichtbaarheid van de bonusknop bij
                    UpdateBonusButtonVisibility(upgradeName);
                }
            }
            else
            {
                // Als er al bonusknoppen zijn, verberg ze dan allemaal en wis de lijst
                foreach (var button in bonusButtons)
                {
                    button.Visibility = Visibility.Collapsed;
                }
                bonusButtons.Clear();
            }
        }

        // Methode om de zichtbaarheid van een specifieke bonusknop bij te werken op basis van eigendom van de bijbehorende upgrade
        private void UpdateBonusButtonVisibility(string upgradeName)
        {
            // Zoek de bonusknop in de lijst op basis van de upgrade-naam
            Button bonusButton = bonusButtons.FirstOrDefault(btn => btn.Tag.ToString() == upgradeName);

            // Controleer of de bonusknop is gevonden en update de zichtbaarheid
            if (bonusButton != null)
            {
                bonusButton.Visibility = UpgradeOwned(upgradeName) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        // Methode om te controleren of een bepaalde upgrade eigendom is op basis van de upgrade-naam
        private bool UpgradeOwned(string upgradeName)
        {
            // Controleer voor elke upgrade of deze eigendom is
            switch (upgradeName)
            {
                case "Cursor":
                    return _cursorCount > 0;
                case "Grandma":
                    return _grandmaCount > 0;
                case "Farm":
                    return _farmCount > 0;
                case "Mine":
                    return _mineCount > 0;
                case "Factory":
                    return _factoryCount > 0;
                case "Bank":
                    return _bankCount > 0;
                case "Tempel":
                    return _templeCount > 0;
                default:
                    return false;
            }
        }

        // Methode om de basisprijs van een upgrade op te halen op basis van de upgrade-naam
        private int GetUpgradeBasisPrijs(string upgradeName)
        {
            // Bepaal de basisprijs voor elke upgrade
            switch (upgradeName)
            {
                case "Cursor":
                    return 15;
                case "Grandma":
                    return 100;
                case "Farm":
                    return 1100;
                case "Mine":
                    return 12000;
                case "Factory":
                    return 130000;
                case "Bank":
                    return 1400000;
                case "Tempel":
                    return 20000000;
                default:
                    return 0;
            }
        }

        // Dictionary om bonusvermenigvuldigers bij te houden voor elke upgrade
        private Dictionary<string, int> bonusMultipliers = new Dictionary<string, int>();

        // Methode die wordt aangeroepen wanneer er op een bonusknop wordt geklikt
        private void BonusButton_Click(object sender, RoutedEventArgs e)
        {
            // Controleer of de verzender een knop is
            if (sender is Button bonusButton)
            {
                // Haal de upgrade-naam op vanuit de knoptag
                string upgradeName = bonusButton.Tag.ToString();

                // Controleer of de upgrade eigendom is en er genoeg koekjes zijn om de bonus te activeren
                if (UpgradeOwned(upgradeName) && _cookieCount >= bonusPrices[upgradeName])
                {
                    // Verminder het aantal koekjes en update de bonusvermenigvuldiger en prijs
                    _cookieCount -= bonusPrices[upgradeName];

                    if (bonusMultipliers.ContainsKey(upgradeName))
                    {
                        bonusMultipliers[upgradeName] *= 2;
                    }
                    else
                    {
                        bonusMultipliers[upgradeName] = 2;
                    }

                    bonusPrices[upgradeName] *= 2;

                    // Werk de weergave van de bonusknop bij
                    bonusButton.Content = $"Bonus {upgradeName}\nPrijs: {FormatNumber(bonusPrices[upgradeName])}";

                    // Werk het aantal koekjes bij
                    UpdateCookieCount();
                }
                else
                {
                    // Als er niet genoeg koekjes zijn, deactiveer de knop
                    bonusButton.IsEnabled = false;
                }
            }
        }

        // Methode om de productie van een upgrade te berekenen op basis van de basisproductie en bonusvermenigvuldiger
        private double UpgradeProduction(string upgradeName)
        {
            // Bepaal de basisproductie voor elke upgrade
            double baseProduction = 0;

            switch (upgradeName)
            {
                case "Cursor":
                    baseProduction = 0.001;
                    break;
                case "Grandma":
                    baseProduction = 0.01;
                    break;
                case "Farm":
                    baseProduction = 0.08;
                    break;
                case "Mine":
                    baseProduction = 0.47;
                    break;
                case "Factory":
                    baseProduction = 2.6;
                    break;
                case "Bank":
                    baseProduction = 14;
                    break;
                case "Tempel":
                    baseProduction = 780;
                    break;
            }

            // Haal de bonusvermenigvuldiger op, of gebruik 1 als deze niet bestaat
            int bonusMultiplier = bonusMultipliers.ContainsKey(upgradeName) ? bonusMultipliers[upgradeName] : 1;

            return baseProduction * bonusMultiplier;
        }
    }
}
