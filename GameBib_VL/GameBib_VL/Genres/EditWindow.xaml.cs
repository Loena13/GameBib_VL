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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace GameBib_VL.Genres
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditWindow : Window
    {
        private Genre _clickedGenre;
        public EditWindow(Genre clickedGenre)
        {
            this.InitializeComponent();

            _clickedGenre = clickedGenre;

            using var db = new AppDbContext();
            db.Genres.Attach(clickedGenre);

            tbName.Text = clickedGenre.Name;
        }

        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();

            var selectedGenre = db.Genres.Find(_clickedGenre.Id);

            selectedGenre.Name = tbName.Text;

            db.SaveChanges();
            this.Close();
        }
    }
}
