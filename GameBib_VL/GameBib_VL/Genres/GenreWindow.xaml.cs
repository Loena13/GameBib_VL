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
using Microsoft.EntityFrameworkCore;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GameBib_VL.Genres
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GenreWindow : Window
    {
        public GenreWindow()
        {
            this.InitializeComponent();

            using var db = new AppDbContext();
            var genres = db.Genres;
            lvGenre.ItemsSource = genres;
        }

        private void bDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvGenre.SelectedItem is Genre selectedGenre)
            {
                using var db = new AppDbContext();
                db.Genres.Remove(selectedGenre);
                db.SaveChanges();

                lvGenre.ItemsSource = db.Genres.ToList();
            }
        }

        private void bAddGenre_Click(object sender, RoutedEventArgs e)
        {
            var createWindow = new CreateWindow();
            createWindow.Activate();
            createWindow.Closed += CreateWindow_Closed;
        }
        private void CreateWindow_Closed(object sender, WindowEventArgs args)
        {
            using var db = new AppDbContext();
            lvGenre.ItemsSource = db.Genres;
        }

        private void bBack_Click(object sender, RoutedEventArgs e)
        {
            var backToPreviouspage = new MainWindow();
            backToPreviouspage.Activate();
            this.Close();
        }

        private void lvGenre_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement element && element.DataContext is Genre clickedGenre)
            {
                var editWindow = new EditWindow(clickedGenre);
                editWindow.Activate();
                editWindow.Closed += EditGame_Closed;
            }
        }
        private void EditGame_Closed(object sender, WindowEventArgs args)
        {
            using var db = new AppDbContext();
            lvGenre.ItemsSource = db.Genres;
        }
    }
}
