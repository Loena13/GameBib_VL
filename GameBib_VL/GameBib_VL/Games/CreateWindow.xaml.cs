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
using GameBib_VL.Data;
using GameBib_VL.Model;
using GameBib_VL.Validation;
using System.Collections.ObjectModel;
using Windows.UI.ViewManagement;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GameBib_VL.Games
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateWindow : Window
    {
        public CreateWindow()
        {
            this.InitializeComponent();

            using var db = new AppDbContext();

            var genreList = db.Genres.ToList();

            cbGenre.ItemsSource = genreList;
        }

        private void BAddGame_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();

            var selectedGenre = (Genre)cbGenre.SelectedItem;

            if (selectedGenre != null)
            {
                db.Games.Add(new Game
                {
                    Name = tbName.Text,
                    Genre = selectedGenre.Name,
                    Rating = int.Parse(tbRating.Text),
                    Issue_Date = dbIssueDate.SelectedDate.Value.DateTime,
                });
     
                db.SaveChanges();
                this.Close();
            }
        }
    }
}
