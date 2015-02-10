using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using Microsoft.Win32;
using System.Diagnostics;
using System.Net;
using System.Windows.Threading;
using G510.App.API_Contract;
using System.Windows.Controls.Primitives;
using System.Runtime.InteropServices;

namespace G510.App
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Cheating a bit i dont wnat to search my workaround for this
        private void __streams_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
            ScrollBar sb = e.OriginalSource as ScrollBar;

            if (sb != null && sb.Orientation == Orientation.Horizontal)
                return;

            if (sb.Value == sb.Maximum)
            {
                (this.DataContext as MainWindow_ViewModel).User_Action("STREAMSLOADNEXT");
            }
        }

        private void __games_Scroll(object sender, ScrollEventArgs e)
        {
            ScrollBar sb = e.OriginalSource as ScrollBar;

            if (sb != null && sb.Orientation == Orientation.Horizontal)
                return;

            if (sb.Value == sb.Maximum)
            {
                (this.DataContext as MainWindow_ViewModel).User_Action("GAMESLOADNEXT");
            }
        }
    }
}
