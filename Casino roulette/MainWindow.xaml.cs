using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RouletteApp
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
       


        private readonly Random random = new Random();
        private int rouletteNumber;
        private SolidColorBrush betColor;
        private int betAmount = 150;
        private int chipCount = 500;
        private BetProcessor betProcessor = new BetProcessor();

        public int RouletteNumber
        {
            get { return rouletteNumber; }
            set
            {
                if (rouletteNumber != value)
                {
                    rouletteNumber = value;
                    OnPropertyChanged("RouletteNumber");
                }
            }
        }


        public SolidColorBrush BetColor
        {
            get { return betColor; }
            set
            {
                if (betColor != value)
                {
                    betColor = value;
                    OnPropertyChanged("BetColor");
                }
            }
        }

        public int BetAmount
        {
            get { return betAmount; }
            set
            {
                if (betAmount != value)
                {
                    betAmount = value;
                    OnPropertyChanged("BetAmount");
                }
            }
        }

        public int ChipCount
        {
            get { return chipCount; }
            set
            {
                if (chipCount != value)
                {
                    chipCount = value;
                    OnPropertyChanged("ChipCount");
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            InitializeRouletteGrid();
        }

        private void InitializeRouletteGrid()
        {
            RouletteNumber = random.Next(0, 37);
            BetColor = GetBetColor(RouletteNumber);
        }

        private SolidColorBrush GetBetColor(int number)
        {
            if (number == 0 || number == 36)
            {
                return Brushes.Green;
            }
            else if ((number >= 1 && number <= 10) || (number >= 19 && number <= 28))
            {
                return (number % 2 == 0) ? Brushes.Black : Brushes.Red;
            }
            else if ((number >= 11 && number <= 18) || (number >= 29 && number <= 36))
            {
                return (number % 2 == 0) ? Brushes.Red : Brushes.Black;
            }
            else
            {
                return Brushes.Transparent;
            }
        }
        private void PlaceBetOnNumber_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            int betNumber = int.Parse(textBlock.Text);
            PlaceBet(BetAmount, $"You placed a bet on number {betNumber} and the result is {RouletteNumber}. {(betNumber == RouletteNumber ? "Congratulations! You win!" : "Sorry, you lose. Try again!")}");
        }


        private void PlaceBet(int betAmount, string message)
        {
            int winAmount = betProcessor.ProcessBet(betAmount);
            ChipCount += winAmount;
            MessageBox.Show(message, "Result", MessageBoxButton.OK, (winAmount > 0 ? MessageBoxImage.Information : MessageBoxImage.Error));
        }

        private void PlaceBetInRange(int min, int max, string message)
        {
            int winAmount = betProcessor.ProcessBet(BetAmount);
            ChipCount += winAmount;
            MessageBox.Show(message, "Result", MessageBoxButton.OK, (winAmount > 0 ? MessageBoxImage.Information : MessageBoxImage.Error));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PlaceBetOn0_Click(object sender, RoutedEventArgs e)
        {
            PlaceBet(BetAmount, $"The rolled number is {RouletteNumber}. {(0 == RouletteNumber ? "Congratulations! You win!" : "Sorry, you lose. Try again!")}");
        }

        private void PlaceBetOn1To12_Click(object sender, RoutedEventArgs e)
        {
            PlaceBetInRange(1, 18, $"The rolled number is {RouletteNumber}. {(RouletteNumber >= 1 && RouletteNumber <= 18 ? "Congratulations! You win!" : "Sorry, you lose. Try again!")}");
        }

        private void PlaceBetOn25To36_Click(object sender, RoutedEventArgs e)
        {
            PlaceBetInRange(19, 36, $"The rolled number is {RouletteNumber}. {(RouletteNumber >= 19 && RouletteNumber <= 36 ? "Congratulations! You win!" : "Sorry, you lose. Try again!")}");
        }

        private void PlaceBetOnEven_Click(object sender, RoutedEventArgs e)
        {
            int randomNumber = random.Next(0, 37);
            MessageBox.Show($"You placed a bet on EVEN and the result is {randomNumber}. {(randomNumber % 2 == 0 ? "You win!" : "You lose!")}", "Result", MessageBoxButton.OK, (randomNumber % 2 == 0 ? MessageBoxImage.Information : MessageBoxImage.Error));
        }

        private void PlaceBetOnOdd_Click(object sender, RoutedEventArgs e)
        {
            int randomNumber = random.Next(0, 37);
            MessageBox.Show($"You placed a bet on ODD and the result is {randomNumber}. {(randomNumber % 2 != 0 ? "You win!" : "You lose!")}", "Result", MessageBoxButton.OK, (randomNumber % 2 != 0 ? MessageBoxImage.Information : MessageBoxImage.Error));
        }

        private void PlaceBetOnRed_Click(object sender, RoutedEventArgs e)
        {
            int randomNumber = random.Next(0, 37);
            MessageBox.Show($"You placed a bet on RED and the result is {randomNumber}. {(IsRed(randomNumber) ? "You win!" : "You lose!")}", "Result", MessageBoxButton.OK, (IsRed(randomNumber) ? MessageBoxImage.Information : MessageBoxImage.Error));
        }

        private void PlaceBetOnBlack_Click(object sender, RoutedEventArgs e)
        {
            int randomNumber = random.Next(0, 37);
            MessageBox.Show($"You placed a bet on BLACK and the result is {randomNumber}. {(!IsRed(randomNumber) ? "You win!" : "You lose!")}", "Result", MessageBoxButton.OK, (!IsRed(randomNumber) ? MessageBoxImage.Information : MessageBoxImage.Error));
        }

        private void PlaceBetOn1st12_Click(object sender, RoutedEventArgs e)
        {
            PlaceBetInRange(1, 12, $"The rolled number is {RouletteNumber}. {(RouletteNumber >= 1 && RouletteNumber <= 12 ? "Congratulations! You win!" : "Sorry, you lose. Try again!")}");
        }

        private void PlaceBetOn2nd12_Click(object sender, RoutedEventArgs e)
        {
            PlaceBetInRange(13, 24, $"The rolled number is {RouletteNumber}. {(RouletteNumber >= 13 && RouletteNumber <= 24 ? "Congratulations! You win!" : "Sorry, you lose. Try again!")}");
        }

        private void PlaceBetOn3rd12_Click(object sender, RoutedEventArgs e)
        {
            PlaceBetInRange(25, 36, $"The rolled number is {RouletteNumber}. {(RouletteNumber >= 25 && RouletteNumber <= 36 ? "Congratulations! You win!" : "Sorry, you lose. Try again!")}");
        }

        private bool IsRed(int number)
        {
            return number == 1 || number == 3 || number == 5 || number == 7 || number == 9 ||
                   number == 12 || number == 14 || number == 16 || number == 18 || number == 19 ||
                   number == 21 || number == 23 || number == 25 || number == 27 || number == 30 ||
                   number == 32 || number == 34 || number == 36;
        }
        private bool IsInLine(int number, int[] line)
        {
            return line.Contains(number);
        }
        private void PlaceBetOn2To1Top_Click(object sender, RoutedEventArgs e)
        {
            int[] topLine = { 2, 5, 8, 11, 14, 17, 20, 23, 26, 29, 32, 35 };

            PlaceBetOnLine(topLine);
        }

        private void PlaceBetOn2To1Mid_Click(object sender, RoutedEventArgs e)
        {
            int[] middleLine = { 3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36 };
            PlaceBetOnLine(middleLine);
        }

        private void PlaceBetOn2To1Bottom_Click(object sender, RoutedEventArgs e)
        {
            int[] bottomLine = { 1, 4, 7, 10, 13, 16, 19, 22, 25, 28, 31, 34 };
            PlaceBetOnLine(bottomLine);
        }

        private void PlaceBetOnLine(int[] line)
        {
            int winAmount = betProcessor.ProcessBet(BetAmount, line);
            ChipCount += winAmount;
            int randomNumber = RouletteNumber;

            if (line.Contains(randomNumber))
            {
                MessageBox.Show($"Congratulations! You won! The number is {randomNumber}.", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show($"Sorry, you lost. The number is {randomNumber}.", "Result", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BetAmountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }


    public class BetProcessor
    {
        private readonly Random random = new Random();

        public int ProcessBet(int betAmount)
        {
            int rouletteNumber = random.Next(0, 37);
            if (rouletteNumber == 0) // Выигрыш на 0
            {
                return betAmount * 35; // Выигрыш 1:35
            }
            return 0; // Проигрыш
        }

        public int ProcessBet(int betAmount, int[] numbersToBetOn)
        {
            int rouletteNumber = random.Next(0, 37);
            if (numbersToBetOn.Contains(rouletteNumber))
            {
                return betAmount * 2; // Выигрыш 1:1
            }
            return 0; // Проигрыш
        }
    }
}
