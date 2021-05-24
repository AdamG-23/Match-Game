using MatchGame.Controls;
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
using System.Windows.Threading;

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private Random random = new Random();
        private DispatcherTimer timer = new DispatcherTimer();
        private List<Card> cards = new List<Card>();
        private List<string> symbols = new List<string>() 
        { 
            "!", "!", "N", "N", ",", ",", "b", "b", "v", "v", "w", "w"
        };

        private Card card1 = null;
        private Card card2 = null;

        public GameWindow()
        {
            InitializeComponent();
            timer.Interval = new TimeSpan(0,0,1);
            timer.Tick += new EventHandler(TimerTick); 
        }

        public void RegisterCard(Card card)
        {
            card.State = Card.eState.Idle;
            int randSymbol = random.Next(symbols.Count);
            card.Symbol = symbols[randSymbol];
            symbols.RemoveAt(randSymbol);
            cards.Add(card);
        }

        public void SelectCard(Card card)
        {
            if(card1 == null)
            {
                card1 = card;
            }
            else
            {
                card2 = card;
                if(card1.Symbol == card2.Symbol)
                {
                    card1.State = Card.eState.Matched;
                    card2.State = Card.eState.Matched;
                    card1 = null;
                    card2 = null;
                }
                else
                {
                    timer.Stop();
                    
                    foreach(Card card0 in cards)
                    {
                        if(card0.State == Card.eState.Idle)
                        {
                            card0.State = Card.eState.Inactive;
                        }
                    }
                }

                timer.Start();
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            timer.Stop();
            foreach(Card card in cards)
            {
                if(card.State != Card.eState.Matched)
                {
                    card.State = Card.eState.Idle;
                }
                card1 = null;
                card2 = null;
            }
        }
    }
}
