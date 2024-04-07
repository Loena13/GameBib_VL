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
using Microsoft.EntityFrameworkCore;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GameBib_VL.Games
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameWindow : Window
    {
        public User LoggedInUser;
        public GameWindow()
        {
            this.InitializeComponent();
            using var db = new AppDbContext();
            var games = db.Games
                .Include(g => g.Genres)
                .ToList();
            lvGame.ItemsSource = games;
        }

        private void bBack_Click(object sender, RoutedEventArgs e)
        {
            var backToPreviouspage = new MainWindow();
            backToPreviouspage.Activate();
            this.Close();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchInput = tbSearch.Text;
            using var db = new AppDbContext();
            var games = db.Games
                .Where(g => g.Name == searchInput);
                //.Where(g => g.Genres.Any(genre => genre.Name == searchInput)); Searches in the Genre model for right input
            lvGame.ItemsSource = games;  
        }

        private void bAddGame_Click(object sender, RoutedEventArgs e)
        {
            var createWindow = new CreateWindow();
            createWindow.Activate();
            createWindow.Closed += CreateWindow_Closed;
        }
        private void CreateWindow_Closed(object sender, WindowEventArgs args)
        {
            using var db = new AppDbContext();
            lvGame.ItemsSource = db.Games;
        }

        private void lvGame_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement element && element.DataContext is Game clickedGame)
            {
                var editWindow = new EditWindow(clickedGame);
                editWindow.Activate();
                editWindow.Closed += EditGame_Closed;
            }
        }
        private void EditGame_Closed(object sender, WindowEventArgs args)
        {
            using var db = new AppDbContext();
            lvGame.ItemsSource = db.Games;
        }

        private void lvGame_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (lvGame.SelectedItem is Game selectedGame && LoggedInUser != null)
            {
                if (LoggedInUser.FavoriteGames == null)
                {
                    LoggedInUser.FavoriteGames = new List<FavoriteGame>();
                }

                if (!LoggedInUser.FavoriteGames.Any(fav => fav.GameId == selectedGame.Id))
                {
                    var favoriteGame = new FavoriteGame
                    {
                        UserId = LoggedInUser.Id,
                        User = LoggedInUser,
                        GameId = selectedGame.Id,
                        Game = selectedGame
                    };

                    LoggedInUser.FavoriteGames.Add(favoriteGame);

                    using var db = new AppDbContext();
                    db.Update(LoggedInUser);
                    db.SaveChanges();

                    lvFavoriteGame.ItemsSource = LoggedInUser.FavoriteGames.Select(fav => fav.Game).ToList();
                }
            }
        }
        private void bDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvGame.SelectedItem is Game selectedGame)
            {
                using var db = new AppDbContext();
                db.Games.Remove(selectedGame);
                db.SaveChanges();

                lvGame.ItemsSource = db.Games.ToList();
            }
        }

        private void bGenre_Click(object sender, RoutedEventArgs e)
        {
            var createWindow = new Genres.GenreWindow();
            createWindow.Activate();
            this.Close();
        }
    }
}
