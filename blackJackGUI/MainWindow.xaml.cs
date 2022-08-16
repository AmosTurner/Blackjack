using Microsoft.Win32;
using System;
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

namespace blackJackGUI
{
    public partial class MainWindow : Window
    {
        Blackjack game = new Blackjack();
        private MediaPlayer sound = new MediaPlayer();
        Card dealersCard;

        public MainWindow()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            btnHit.IsEnabled = true;
            btnStand.IsEnabled = true;
            playersHand.Children.Clear();
            dealersHand.Children.Clear();
            game.Start();
            ShuffleSoundEffect();
            CreatePlayersHand(game.getPlayerHand());
            CreateDealersHand(game.getDealerHand());
        }

        private void CreatePlayersHand(List<Card> playerHand)
        {
            for (int i = 0; i < playerHand.Count; i++)
            {
                Card card = playerHand[i];
                CreatePlayerCard(card);
            }
            DisplayPlayerSum();
        }

        private void CreateDealersHand(List<Card> dealerHand)
        {
            Card hidden = dealerHand[0];
            int rank = hidden.getRank();
            String suit = hidden.getSuit();
            dealersCard = new Card(suit, rank);

            Image cardImage = new Image();
            cardImage.Width = 150;
            cardImage.Height = 375;
            cardImage.Margin = new Thickness(10, 0, 10, 0);

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("pack://siteoforigin:,,/Images/back_of_card.jpg");
            bitmap.EndInit();
            cardImage.Source = bitmap;

            dealersHand.Children.Add(cardImage);

            for (int i = 1; i < dealerHand.Count; i++)
            {
                Card card = dealerHand[i];
                CreateDealerCard(card);
            }
            DisplayDealersSumBefore();
        }

        private void DisplayPlayerSum()
        {
            playersScore.Text = "Players Score: " + game.GetPlayerSum().ToString();
        }

        private void DisplayDealersSumBefore()
        {
            dealersScore.Text = "Dealers Score: " + game.GetDealerSumBefore().ToString();
        }

        private void DisplayDealerSum()
        {
            dealersScore.Text = "Dealers Score: " + game.GetDealerSum().ToString();
        }

        private void BtnHit_Click(object sender, RoutedEventArgs e)
        {
            CardSoundEffect();
            Card card = game.Hit();
            CreatePlayerCard(card);
            DisplayPlayerSum();
            if (game.IsBust())
            {
                dealersHand.Children.RemoveAt(0);
                dealersHand.Children.Insert(0, CreateCardImage(dealersCard));
                DisplayDealerSum();
                MessageBox.Show("Bust");
                IsPlaying();
            }
        }

        private void BtnStand_Click(object sender, RoutedEventArgs e)
        {
            List<Card> cards = game.Stand();
            for (int i = 0; i < cards.Count; i++)
            {
                CardSoundEffect();
                CreateDealerCard(cards[i]);
            }

            End();
        }

        private void End()
        {
            DisplayDealerSum();
            dealersHand.Children.RemoveAt(0);
            dealersHand.Children.Insert(0, CreateCardImage(dealersCard));
            if (game.CheckWinner())
            {
                MessageBox.Show("Player wins!");
            }
            else
            {
                MessageBox.Show("Dealer wins :(");
            }

            IsPlaying();
        }

        private void IsPlaying()
        {
            btnHit.IsEnabled = false;
            btnStand.IsEnabled = false;

            const String message = "Would you like to continue playing BlackJack?";
            const String title = "Keep playing?";

            MessageBoxResult isPlaying = MessageBox.Show(message, title, MessageBoxButton.YesNo);
            if (MessageBoxResult.Yes == isPlaying)
            {
                Start();
            }
            else
            {
                System.Environment.Exit(0);
            }
        }

        private void CreatePlayerCard(Card card)
        {
            Image playersCard = CreateCardImage(card);

            playersHand.Children.Add(playersCard);
        }

        private void CreateDealerCard(Card card)
        {
            Image dealersCard = CreateCardImage(card);

            dealersHand.Children.Add(dealersCard);
        }

        private Image CreateCardImage(Card card)
        {
            int rank = card.getRank();
            String suit = card.getSuit();

            Image cardImage = new Image();
            cardImage.Width = 150;
            cardImage.Height = 375;
            cardImage.Margin = new Thickness(10, 0, 10, 0);

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("pack://siteoforigin:,,/Images/" + rank + suit + ".jpg");
            bitmap.EndInit();

            cardImage.Source = bitmap;
            return cardImage;
        }

        private void CardSoundEffect()
        {
            Uri uri = new Uri("pack://siteoforigin:,,/Sound/dealCard.mp3");
            sound.Open(uri);
            sound.Play();
        }

        private void ShuffleSoundEffect()
        {
            Uri uri = new Uri("pack://siteoforigin:,,/Sound/shuffle.mp3");
            sound.Open(uri);
            sound.Play();
        }
    }
}
