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

namespace KTL_game.Pages
{
    /// <summary>
    /// Interaction logic for SetupGame.xaml
    /// </summary>
    public partial class SetupGame : Page
    {
        private MainWindow Window { get; set; }
        private StartPage StartPage { get; set; }
        private int GameLength { get; set; }
        private int SeriesLength { get; set; }
        private int ColorsCount { get; set; }
        private int ColorListCount { get; set; }
        public SetupGame(MainWindow window, StartPage startPage)
        {
            this.Window = window;
            this.StartPage = startPage;
            InitializeComponent();
        }

        private void PrevPageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Window.Content = StartPage;
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            ValidateAndAssingInputValues();
            GameplayPage gameplayPage = new GameplayPage(Window, StartPage, GameLength, SeriesLength, ColorsCount, ColorListCount);
            Window.Content = gameplayPage;
        }

        //TODO: Proper validation
        private void ValidateAndAssingInputValues()
        {
            GameLength = int.Parse(GameLengthTextBox.Text);
            SeriesLength = int.Parse(SeriesLengthTextBox.Text);
            ColorsCount = int.Parse(ColorCountTextBox.Text);
            ColorListCount = int.Parse(ListColorCountTextBox.Text);
        }

    }
}
