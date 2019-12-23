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

namespace Electonnic_Journal
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool leftPanelIsOpen;
        GridLengthConverter converter = new GridLengthConverter();
        GridLength minimazeLeftPanel;
        GridLength maximazeLeftPanel;

        public MainWindow()
        {
            InitializeComponent();
            leftPanelIsOpen = true;
            maximazeLeftPanel = LeftPanel.Width;
            minimazeLeftPanel = new GridLength(OpenPanelButton.Height);
        }

        private void OpenPanelButton_Click(object sender, RoutedEventArgs e)
        {
            if (leftPanelIsOpen)
            {
                // Скрываем. Заменяем картинками
                LeftPanel.Width = minimazeLeftPanel;
                OpenPanelButton.Content = ">>";
                SaveButton.Content = "";
                UploadButton.Content = "";
                SettingButton.Content = "";
            }
            else
            {
                // расскрываем. Возвращаем текст
                LeftPanel.Width = maximazeLeftPanel;
                OpenPanelButton.Content = "Скрыть панель";
                SaveButton.Content = "Сохранить";
                UploadButton.Content = "Загрузить";
                SettingButton.Content = "Настройки";
            }
            leftPanelIsOpen = !leftPanelIsOpen;
        }
    }
}
