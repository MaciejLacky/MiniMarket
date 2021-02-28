using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MiniMarket.Views
{
    /// <summary>
    /// Logika interakcji dla klasy FakturaSprzedazyView.xaml
    /// </summary>
    public partial class FakturaSprzedazyView : UserControl
    {
        public FakturaSprzedazyView()
        {
            InitializeComponent();
        }

        private void DataPozycje_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var ue = e.OriginalSource as FrameworkElement;
                ue.MoveFocus(new TraversalRequest(FocusNavigationDirection.Up));
                var tabKeyEvent = new KeyEventArgs(
                 e.KeyboardDevice, e.InputSource, e.Timestamp, Key.Tab);
                tabKeyEvent.RoutedEvent = Keyboard.KeyDownEvent;
                InputManager.Current.ProcessInput(tabKeyEvent);

            }
            
        }
        
    }
}
