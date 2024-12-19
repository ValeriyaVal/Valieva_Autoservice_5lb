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

            if (_currentServise.DiscountInt == null)
                errors.AppendLine("Укажите скидку");

            if (_currentServise.DiscountInt > 100)
                errors.AppendLine("Укажите скидку");

            if (_currentServise.DiscountInt < 0)
                errors.AppendLine("Укажите скидку");

            if (_currentServise.DurationInSeconds == 0)
                errors.AppendLine("Укажите длительность услуги");

            if (_currentServise.DurationInSeconds > 240)
                errors.AppendLine("Длительность не может быть больше 240 минут");

            if (_currentServise.DurationInSeconds < 0)
                errors.AppendLine("Длительность не может быть меньше 0");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var allServices = ValievaAutoserviceEntities.GetContext().Service
                .Where(p => p.Title == _currentServise.Title && p.ID != _currentServise.ID)
                .ToList();

            if (allServices.Count == 0)
            {
                // Добавить в контекст текущие значения новой услуги
                if (_currentServise.ID == 0)
                    ValievaAutoserviceEntities.GetContext().Service.Add(_currentServise);

                // Сохранить изменения
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
            else
            {
                MessageBox.Show("Уже существует такая услуга");
            }
        }

        
    }
}
