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

namespace ServicesProject
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        List<Service> ServicesList = BaseClass.Base.Service.ToList();
        public AdminPage()
        {
            InitializeComponent();
            ServicesTable.ItemsSource = ServicesList;
        }
        int i = -1;
        private void MediaElement_Initialized(object sender, EventArgs e)
        {
            if (i < ServicesList.Count)
            {
                i++;
                MediaElement ME = (MediaElement)sender;
                Service S = ServicesList[i];
                Uri U = new Uri(S.MainImagePath, UriKind.RelativeOrAbsolute);
                ME.Source = U;
            }
        }
        private void TextBlock_Initialized(object sender, EventArgs e)
        {
            if (i < ServicesList.Count)
            {
                TextBlock TB = (TextBlock)sender;
                Service S = ServicesList[i];
                TB.Text = S.Title;
            }

        }

        private void btnChange_Initialized(object sender, EventArgs e)
        {
            Button btnChange = (Button)sender;
            if (btnChange != null)
            {
                btnChange.Uid = Convert.ToString(i);
            }
        }
        private void btnDel_Initialized(object sender, EventArgs e)
        {
            Button btnDel = (Button)sender;
            if (btnDel != null)
            {
                btnDel.Uid = Convert.ToString(i);
            }
        }
        private void btnNewZakaz_Initialized(object sender, EventArgs e)
        {
            Button btnNewZakaz = (Button)sender;
            if (btnNewZakaz != null)
            {
                btnNewZakaz.Uid = Convert.ToString(i);
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            Button btnChange = (Button)sender;
            int ind = Convert.ToInt32(btnChange.Uid);
            Service S = ServicesList[ind];
            MessageBox.Show(S.Title);

        }
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            Button btnDel = (Button)sender;
            int ind = Convert.ToInt32(btnDel.Uid);
            Service S = ServicesList[ind];
            MessageBox.Show(S.Title);

        }
        private void btnNewZakaz_Click(object sender, RoutedEventArgs e)
        {
            Button btnNewZakaz = (Button)sender;
            int ind = Convert.ToInt32(btnNewZakaz.Uid);
            Service S = ServicesList[ind];
            MessageBox.Show(S.Title);

        }

        private void StackPanel_Initialized(object sender, EventArgs e)
        {
            if (i < ServicesList.Count)
            {
                StackPanel SP = (StackPanel)sender;
                Service S = ServicesList[i];
                if (S.Discount != 0)
                {
                    SP.Background = new SolidColorBrush(Color.FromRgb(231, 250, 191));
                }
            }
        }

        private void services__price_Initialized(object sender, EventArgs e)
        {
            if (i < ServicesList.Count)
            {
                TextBlock TB = (TextBlock)sender;
                Service S = ServicesList[i];
                TB.Text = Convert.ToInt32(S.Cost) + " рублей";
                if (S.Discount > 0)
                {
                    TB.Text = Convert.ToInt32(S.Cost) + " ";
                    TB.TextDecorations = TextDecorations.Strikethrough;
                }
            }
        }
        private void services__priceWithDiscount_Initialized(object sender, EventArgs e)
        {
            if (i < ServicesList.Count)
            {
                TextBlock TB = (TextBlock)sender;
                Service S = ServicesList[i];
                if (S.Discount != 0)
                {
                    TB.Text = Convert.ToInt32(S.Cost) * (1 - S.Discount) + " рублей";
                }

            }
        }
        private void services__time_Initialized(object sender, EventArgs e)
        {
            if (i < ServicesList.Count)
            {
                TextBlock TB = (TextBlock)sender;
                Service S = ServicesList[i];
                TB.Text = " за " + Convert.ToInt32(S.DurationInSeconds) / 60 + " минут";
            }
        }
        private void services__discount_Initialized(object sender, EventArgs e)
        {
            if (i < ServicesList.Count)
            {
                TextBlock TB = (TextBlock)sender;
                Service S = ServicesList[i];
                if(S.Discount != 0)
                {
                    TB.Text = "Скидка " + Convert.ToDouble(S.Discount) * 100 + "%";
                }
            }
        }
    }
}
