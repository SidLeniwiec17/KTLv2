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
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        MainWindow window;
        public StartPage(MainWindow window)
        {
            this.window = window;
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            SetupGame setupGamePage = new SetupGame(window, this);
            this.window.Content = setupGamePage;
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
          
        }
    }
}
