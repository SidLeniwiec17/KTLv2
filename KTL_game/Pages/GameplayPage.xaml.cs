using KTL_game.Helper;
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
    /// Interaction logic for GameplayPage.xaml
    /// </summary>
    public partial class GameplayPage : Page
    {
        private int GameLength { get; set; }
        private int SeriesLength { get; set; }
        private int ColorsCount { get; set; }
        private int ColorListCount { get; set; }
        private MainWindow Window { get; set; }
        private StartPage StartPage { get; set; }
        private List<Button> ButtonList { get; set; }
        private List<int> ButtonColorIndexList { get; set; } 
        private int NumberOfFieldsInRow { get; set; }
        private int NumberOfRows { get; set; }
        private List<Color> ColorsList {get; set;}
        private AI computer { get; set; }
        public GameplayPage(MainWindow window, StartPage startPage, int gameLenght, int seriesLength, int colorsCount, int colorListCount)
        {
            this.Window = window;
            this.StartPage = startPage;
            this.GameLength = gameLenght;
            this.SeriesLength = seriesLength;
            this.ColorsCount = colorsCount;
            this.ColorListCount = colorListCount;
            this.NumberOfFieldsInRow = 15;
            this.ButtonColorIndexList = new List<int>();
            this.computer = new AI(gameLenght, seriesLength, colorsCount);
            InitializeComponent();
            InitLabelsValues();
            InitGrid();
            InitFields();
            InitColorsList();
        }

        private void InitLabelsValues()
        {
            GameLengthLabel.Content = "Game length: "+ GameLength + "   ";
            SeriesLengthLabel.Content = "Series length " + SeriesLength + "   ";
            ColorCountLabel.Content = "Colors count " + ColorsCount + "  ";
            ColorCountListLabel.Content = "List colors count " + ColorListCount + "  ";
        }

        private void InitGrid()
        {
            NumberOfRows = 1 + GameLength / NumberOfFieldsInRow;
            GameGrid.Height = NumberOfRows * 100;
            for(int i=0;i<NumberOfFieldsInRow;i++)
            {
                var columnDefinition = new ColumnDefinition();
                GameGrid.ColumnDefinitions.Add(columnDefinition);
            }

            for(int i=0;i<NumberOfRows;i++)
            {
                var rowDefinition = new RowDefinition();
                GameGrid.RowDefinitions.Add(rowDefinition);
            }
        }

        private void InitFields()
        {
            for(int i=0;i<GameLength;i++)
            {
                ButtonColorIndexList.Add(-1);
            }
            int counter = 0;
            for (int j = 0; j < NumberOfRows; j++)
            {
                for (int i = 0; i < NumberOfFieldsInRow; i++)
                {

                    var button = new Button();
                    button.Content = counter + 1;
                    button.Click += new RoutedEventHandler(FieldButtonClick);
                    Grid.SetRow(button, j);
                    Grid.SetColumn(button, i);
                    GameGrid.Children.Add(button);
                    counter++;
                    if (counter == GameLength)
                    {
                        j++;
                        break;
                    }
                }
            }
        }

        private void InitColorsList()
        {
            Random rand = new Random();
            ColorsList = new List<Color>();
            for(int i=0;i<ColorsCount;i++)
            {
                byte red = (byte)rand.Next(255);
                byte green = (byte)rand.Next(255);
                byte blue = (byte)rand.Next(255);
                var color = Color.FromRgb(red, green, blue);
                ColorsList.Add(color);
            }
        }

        private void FieldButtonClick(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            //Losuję listę kolorów
            var tmpColorList = new List<Color>();
            List<int> randColors = new List<int>();
            for (int i = 0; i < ColorListCount; i++)
            {
                while (true)
                {
                    bool foundRand = true;
                    int tmpNum = rand.Next(ColorsCount - 1);
                    for(int j=0;j<tmpColorList.Count;j++)
                    {
                        if(ColorsList[tmpNum] == tmpColorList[j])
                        {
                            foundRand = false;
                        }
                    }
                    if(foundRand == true)
                    {
                        tmpColorList.Add(ColorsList[tmpNum]);
                        randColors.Add(tmpNum);
                        break;
                    }
                }
            }
            //DO POPRAWY
            //Losuję --- Wybieram kolor z listy kolorów
            //int colorIndex = rand.Next(tmpColorList.Count - 1);
            
            var button = (Button)sender;
            int buttonIndex = int.Parse(button.Content.ToString());
           
            //int colorIndex = this.logic.chooseColor(buttonIndex, randColors);
            //TUTAJ WCHODZI LOGIKA
            int colorIndex = computer.chooseColor(buttonIndex, randColors);
            //int colorIndex = rand.Next(tmpColorList.Count - 1);
            


            int tmp;
            for(tmp = 0; tmp< ColorsCount;tmp++)
            {
                if(ColorsList[tmp] == tmpColorList[colorIndex])
                {
                    ButtonColorIndexList[buttonIndex - 1] = tmp;
                    break;
                }
            }
            
            button.Background = new SolidColorBrush(tmpColorList[colorIndex]);
            button.IsEnabled = false;
            //sprawdzam czy powstał ciąg
            int currentSeriesLength = CheckForSeries(tmp);
            if (currentSeriesLength == SeriesLength)
            {
                MessageBox.Show("End of the game");
            }
        }

        //Returns length of maximal series for given color
        private int CheckForSeries(int colorIndex)
        {
            int maxLengthOfSeries = 0;
            int i = ButtonColorIndexList.FindIndex(x => x == colorIndex);
            int z = ButtonColorIndexList.FindLastIndex(x => x == colorIndex);
            int k = SeriesLength;
            int jmax = (z - i) / (k - 1)+1;
            for(int m = 1; m < jmax+1;m++)
            {
                int currentIndex = i;
                bool do_work = true;
                while(do_work)
                {
                    int currentSeriesLength = 0;
                    for(int n=0; n < SeriesLength+1;n++)
                    {
                        int nextIndex = ButtonColorIndexList.FindIndex(currentIndex+1,x => x == colorIndex);
                        if(nextIndex == -1)
                        {
                            if (currentSeriesLength > maxLengthOfSeries) maxLengthOfSeries = currentSeriesLength;
                            do_work = false;
                            break;
                        }
                        if(nextIndex-currentIndex != m)
                        {
                            if (currentSeriesLength > maxLengthOfSeries) maxLengthOfSeries = currentSeriesLength;
                            currentIndex = nextIndex;
                            currentSeriesLength = 0;
                            n = 0;
                            if(nextIndex == z)
                            {
                                do_work = false;
                                break;                               
                            }
                        }
                        else
                        {
                            currentIndex = nextIndex;
                            currentSeriesLength++;
                        }
                    }
                }
            }
            return maxLengthOfSeries + 1;
        }
    }
}
