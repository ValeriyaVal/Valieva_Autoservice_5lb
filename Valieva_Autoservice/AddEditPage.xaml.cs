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

namespace Valieva_Autoservice
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        //добавим новое поле, которое будет хранить в себе экземпляр добавляемого сервиса
        private Service _currentServise = new Service();
        public AddEditPage(Service SelectedService)
        {
            InitializeComponent();

            if(SelectedService != null) 
                _currentServise = SelectedService;

            //при инициаллизации установим DataContext страницы - этот созданный объект
            DataContext = _currentServise;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentServise.Title))
                errors.AppendLine("Укажите название услуги");

            if (_currentServise.Cost == 0)
                errors.AppendLine("Укажите стоимость услуги");

            if (_currentServise.Discount == null)
                errors.AppendLine("Укажите скидку");
            if (_currentServise.Discount > 100)
                errors.AppendLine("Укажите скидку");
            if (_currentServise.Discount < 0)
                errors.AppendLine("Укажите скидку");
            if (string.IsNullOrWhiteSpace(_currentServise.DurationInSeconds))
                errors.AppendLine("Укажите длительность услуги");

            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            //добавить в контекст текущие значения новой услуги
            if (_currentServise.ID == 0) 
                ValievaAutoserviceEntities.GetContext().Service.Add(_currentServise);

            //сохранить изменения, если никаких ошибок не получилось при этом
            try
            {
                ValievaAutoserviceEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
