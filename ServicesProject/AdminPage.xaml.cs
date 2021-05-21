using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        List<Service> ServicesList1 = BaseClass.Base.Service.ToList();
        List<Client> ClientsList = BaseClass.Base.Client.ToList();
        List<Service> ServicesList = new List<Service>(); // для отрисовки
        public AdminPage()
        {
            InitializeComponent();
            ServicesList = ServicesList1;
            ServicesTable.ItemsSource = ServicesList;
            addNewZakaz__peopleList.ItemsSource = ClientsList;
            addNewZakaz__peopleList.SelectedValuePath = "ID";
            addNewZakaz__peopleList.DisplayMemberPath = "People";
        }
        int i = -1;
        int indexChange;
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
            addNew__service.Visibility = Visibility.Collapsed;
            Button btnChange = (Button)sender;
            int ind = Convert.ToInt32(btnChange.Uid);
            indexChange = Convert.ToInt32(btnChange.Uid);
            Service S = ServicesList[ind];
            Table__stack.Visibility = Visibility.Collapsed;
            change__stack.Visibility = Visibility.Visible;
            id__service.Text = Convert.ToString(S.ID);
            title__service.Text = S.Title;
            price__service.Text = Convert.ToInt32(S.Cost) + "";
            timeInSeconds__service.Text = Convert.ToInt32(S.DurationInSeconds) / 60 + "";
            discount__service.Text = Convert.ToDouble(S.Discount) * 100 + "";
            pathImg__textBlock.Text = S.MainImagePath;


        }
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            Button btnDel = (Button)sender;
            int ind = Convert.ToInt32(btnDel.Uid);
            Service S = ServicesList[ind];
            BaseClass.Base.Service.Remove(S);
            MessageBox.Show("Запись удалена");
            BaseClass.Base.SaveChanges();
            FrameClass.mainFrame.Navigate(new AdminPage());

        }
        private void btnNewZakaz_Click(object sender, RoutedEventArgs e)
        {

            Button btnNewZakaz = (Button)sender;
            int ind = Convert.ToInt32(btnNewZakaz.Uid);
            indexChange = Convert.ToInt32(btnNewZakaz.Uid);
            Service S = ServicesList[ind];
            addNewZakaz__stack.Visibility = Visibility.Visible;
            Table__stack.Visibility = Visibility.Collapsed;
            addNew__service.Visibility = Visibility.Collapsed;
            addNewZakaz__titleService.Text = "Название услуги: " + S.Title;
            addNewZakaz__timeService.Text = "Время: " + S.DurationInSeconds / 60 + " минут";
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
                if (S.Discount != 0)
                {
                    TB.Text = "Скидка " + Convert.ToDouble(S.Discount) * 100 + "%";
                }
            }
        }

        private void btn__changeImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.ShowDialog();
            string Path = OFD.FileName;
            if (Path != "")
            {
                int c = Path.IndexOf('У');
                string length = Path.Substring(c);
                pathImg__textBlock.Text = length;
            }

        }

        private void saveChanges_btn_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDouble(discount__service.Text) <= 100 && Convert.ToInt32(timeInSeconds__service.Text) <= 240)
            {
                int ind = indexChange;
                Service S = ServicesList[ind];
                S.Title = title__service.Text;
                S.Cost = Convert.ToInt32(price__service.Text);
                S.DurationInSeconds = Convert.ToInt32(timeInSeconds__service.Text) * 60;
                S.Discount = Convert.ToDouble(discount__service.Text) / 100;
                S.MainImagePath = pathImg__textBlock.Text;
                MessageBox.Show("Изменения сохранены");
                BaseClass.Base.SaveChanges();
                FrameClass.mainFrame.Navigate(new AdminPage());
            }
            else if (Convert.ToDouble(discount__service.Text) > 100)
            {
                MessageBox.Show("Значение скидки не может быть больше, чем 100%");
            }
            else if (Convert.ToInt32(timeInSeconds__service.Text) * 60 > 240)
            {
                MessageBox.Show("Время услуги не может быть больше, чем 240 минут");
            }

        }

        private void hidden__stack_Click(object sender, RoutedEventArgs e)
        {
            addNew__service.Visibility = Visibility.Visible;
            change__stack.Visibility = Visibility.Collapsed;
            Table__stack.Visibility = Visibility.Visible;
        }

        private void addNew__service_Click(object sender, RoutedEventArgs e)
        {
            addNew__service.Visibility = Visibility.Collapsed;
            Table__stack.Visibility = Visibility.Collapsed;
            addNew__stack.Visibility = Visibility.Visible;
        }
        private void imagePath__add_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.ShowDialog();
            string Path = OFD.FileName;
            if (Path != "")
            {
                int c = Path.IndexOf('У');
                string length = Path.Substring(c);
                imagePath__textBox.Text = length;
            }
        }
        private void addNew__saveBtn_Click(object sender, RoutedEventArgs e)
        {
            Service ServiceObject = new Service()
            {
                Title = title__add.Text,
                Cost = Convert.ToInt32(price__add.Text),
                DurationInSeconds = Convert.ToInt32(time__add.Text) * 60,
                Discount = Convert.ToDouble(discount__add.Text) / 100,
                MainImagePath = Convert.ToString(imagePath__textBox.Text)
            };
            if (Convert.ToDouble(discount__add.Text) <= 100 && Convert.ToDouble(discount__add.Text) >= 0 && Convert.ToInt32(time__add.Text) <= 240)
            {
                BaseClass.Base.Service.Add(ServiceObject);
                MessageBox.Show("Запись добавлена");
                BaseClass.Base.SaveChanges();
                FrameClass.mainFrame.Navigate(new AdminPage());
            }
            else if (Convert.ToDouble(discount__add.Text) > 100 || Convert.ToDouble(discount__add.Text) < 0)
            {
                MessageBox.Show("Значение скидки не может быть больше, чем 100%");
            }
            else if (Convert.ToInt32(time__add.Text) * 60 > 240)
            {
                MessageBox.Show("Время услуги не может быть больше, чем 240 минут");
            }

        }

        private void addNew__hidden_Click(object sender, RoutedEventArgs e)
        {
            addNew__service.Visibility = Visibility.Visible;
            addNew__stack.Visibility = Visibility.Collapsed;
            Table__stack.Visibility = Visibility.Visible;
        }

        private void addNewZakaz__hidden_Click(object sender, RoutedEventArgs e)
        {
            addNewZakaz__stack.Visibility = Visibility.Collapsed;
            Table__stack.Visibility = Visibility.Visible;
            addNew__service.Visibility = Visibility.Visible;
        }

        private void addNewZakaz__peopleList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = addNewZakaz__peopleList.SelectedIndex + 1;
        }
        DateTime DT;
        private void addNewZakaz__changeSecondTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Regex r1 = new Regex("[0-1][0-9]:[0-5][0-9]");
                Regex r2 = new Regex("2[0-3]:[0-5][0-9]");
                if ((r1.IsMatch(addNewZakaz__changeSecondTime.Text) || r2.IsMatch(addNewZakaz__changeSecondTime.Text)) && addNewZakaz__changeSecondTime.Text.Length == 5)
                {
                    MessageBox.Show(addNewZakaz__changeSecondTime.Text);
                    TimeSpan TS = TimeSpan.Parse(addNewZakaz__changeSecondTime.Text);
                    DT = Convert.ToDateTime(addNewZakaz__datePicker.SelectedDate);
                    DT = DT.Add(TS);
                    if (DT > DateTime.Now)
                    {
                        MessageBox.Show(DT + "");
                    }
                    else
                    {
                        MessageBox.Show("Слишком поздно");
                        addNewZakaz__saveBtn.IsEnabled = false;
                    }
                }
                else
                {
                    if (addNewZakaz__changeSecondTime.Text.Length > 5)
                    {
                        MessageBox.Show("Время указано неверно");
                        addNewZakaz__saveBtn.IsEnabled = false;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так");
            }
        }

        private void addNewZakaz__saveBtn_Click(object sender, RoutedEventArgs e)
        {
            int ind = indexChange;
            Service S = ServicesList[ind];
            int client = addNewZakaz__peopleList.SelectedIndex + 1;

            ClientService ClientServiceObject = new ClientService()
            {
                ClientID = client,
                ServiceID = S.ID,
                StartTime = DT,
            };


            BaseClass.Base.ClientService.Add(ClientServiceObject);
            MessageBox.Show("Запись добавлена");
            BaseClass.Base.SaveChanges();
            FrameClass.mainFrame.Navigate(new AdminPage());

        }

        private void SortPrice__up_Click(object sender, RoutedEventArgs e)
        {

            i = -1;
            ServicesList.Sort((x, y) => (x.CostWithDiscount).CompareTo(y.CostWithDiscount));
            ServicesTable.Items.Refresh();

        }

        private void SortPrice__down_Click(object sender, RoutedEventArgs e)
        {
            i = -1;
            ServicesList.Sort((x, y) => x.CostWithDiscount.CompareTo(y.CostWithDiscount));
            ServicesList.Reverse();
            ServicesTable.Items.Refresh();

        }
        List<Service> ServicesListFilter = new List<Service>();
        private void filterDiscountList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            i = -1;

            switch (filterDiscountList.SelectedIndex)
            {
                case 0:
                    ServicesList = ServicesList1;
                    ServicesListFilter = ServicesList.Where(x => x.Discount <= 0.05).ToList();
                    ServicesList = ServicesListFilter;
                    ServicesTable.ItemsSource = ServicesList;
                    count__lettersSearch.Text = ("Найдено " + Convert.ToString(ServicesList.Count) + " записей");
                    break;
                case 1:
                    ServicesList = ServicesList1;
                    ServicesListFilter = ServicesList.Where(x => x.Discount <= 0.15 && x.Discount >= 0.05).ToList();
                    ServicesList = ServicesListFilter;
                    ServicesTable.ItemsSource = ServicesList;
                    count__lettersSearch.Text = ("Найдено " + Convert.ToString(ServicesList.Count) + " записей");
                    break;
                case 2:
                    ServicesList = ServicesList1;
                    ServicesListFilter = ServicesList.Where(x => x.Discount <= 0.30 && x.Discount >= 0.15).ToList();
                    ServicesList = ServicesListFilter;
                    ServicesTable.ItemsSource = ServicesList;
                    count__lettersSearch.Text = ("Найдено " + Convert.ToString(ServicesList.Count) + " записей");
                    break;
                case 3:
                    ServicesList = ServicesList1;
                    ServicesListFilter = ServicesList.Where(x => x.Discount <= 0.70 && x.Discount >= 0.30).ToList();
                    ServicesList = ServicesListFilter;
                    ServicesTable.ItemsSource = ServicesList;
                    count__lettersSearch.Text = ("Найдено " + Convert.ToString(ServicesList.Count) + " записей");
                    break;
                case 4:
                    ServicesList = ServicesList1;
                    ServicesListFilter = ServicesList.Where(x => x.Discount <= 1 && x.Discount >= 0.70).ToList();
                    ServicesList = ServicesListFilter;
                    ServicesTable.ItemsSource = ServicesList;
                    count__lettersSearch.Text = ("Найдено " + Convert.ToString(ServicesList.Count) + " записей");
                    break;
                case 5:
                    ServicesList = ServicesList1;
                    ServicesTable.ItemsSource = ServicesList;
                    count__lettersSearch.Text = ("Найдено " + Convert.ToString(ServicesList.Count) + " записей");
                    break;
            }
        }

        private void search__services_TextChanged(object sender, TextChangedEventArgs e)
        {
            i = -1;

            for (int i = 0; i < ServicesList.Count; i++)
            {
                if (search__services.Text != "")
                {
                    List<Service> ServiseListPoisk = new List<Service>();
                    ServiseListPoisk = ServicesList.Where(x => x.Title.Contains(search__services.Text)).ToList();
                    ServicesList = ServiseListPoisk;
                    ServicesTable.ItemsSource = ServicesList;
                    count__lettersSearch.Text = ("Найдено " + Convert.ToString(ServicesList.Count) + " записей");
                }
                else
                {
                    if (ServicesListFilter.Count == 0)
                    {
                        ServicesList = ServicesList1;
                        ServicesTable.ItemsSource = ServicesList;
                        count__lettersSearch.Text = ("Найдено " + Convert.ToString(ServicesList.Count) + " записей");
                    }
                    else
                    {
                        ServicesList = ServicesListFilter;
                        ServicesTable.ItemsSource = ServicesList;
                        count__lettersSearch.Text = ("Найдено " + Convert.ToString(ServicesList.Count) + " записей");
                    }
                }
            }
        }

        private void count__lettersAll_Initialized(object sender, EventArgs e)
        {
            count__lettersAll.Text = ("Всего " + Convert.ToString(ServicesList1.Count) + " записей");
        }

        private void SortPrice__all_Click(object sender, RoutedEventArgs e)
        {
            i = -1;
            ServicesList = ServicesList1;
            ServicesTable.ItemsSource = ServicesList;
            ServicesTable.Items.Refresh();
        }
    }
}
