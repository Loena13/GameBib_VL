using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using GameBib_VL.Model;
using GameBib_VL.Data;
using System.Collections.ObjectModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GameBib_VL.Games
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditWindow : Window
    {
        private Game _clickedGame;
        public EditWindow(Game clickedGame)
        {
            this.InitializeComponent();

            _clickedGame = clickedGame;

            using var db = new AppDbContext();
            db.Games.Attach(clickedGame);

            var genreList = db.Genres.ToList();
            cbGenre.ItemsSource = genreList;

            cbGenre.SelectedItem = genreList.FirstOrDefault(genre => genre.Name == clickedGame.Genre);

            tbName.Text = clickedGame.Name;
            tbDate.Text = clickedGame.Issue_Date.ToString();
            tbRating.Text = clickedGame.Rating.ToString();
        }

        private void BEditGame_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();

            var clickedGame = db.Games.Find(_clickedGame.Id);

            clickedGame.Name = tbName.Text;
            clickedGame.Issue_Date = DateTime.Parse(tbDate.Text);
            clickedGame.Rating = int.Parse(tbRating.Text);

            var selectedGenre = (Genre)cbGenre.SelectedItem;

            if (selectedGenre != null)
            {
                clickedGame.Genre = selectedGenre.Name;
            }
            db.SaveChanges();
            this.Close();
        }

    }
}
