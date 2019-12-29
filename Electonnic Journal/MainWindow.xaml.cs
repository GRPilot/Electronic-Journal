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
using Electonnic_Journal.Controls;

namespace Electonnic_Journal
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool leftPanelIsOpen;
        GridLength minimazeLeftPanel;
        GridLength maximazeLeftPanel;

        ExcelTabelsController controller;

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

        //TODO: Создавать TabControl, если несколько вкладок или просто создавать таблицу.
        private void ElemDragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string file in files)
                {
                    if (file.Contains(".xlsx") || file.Contains(".xlsm"))
                    {
                        controller = new ExcelTabelsController(file);
                        break;
                    }
                }

                CreateDataGrid();
            }
        }

        private void ElemDragEnter(object sender, DragEventArgs e)
        {

            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                e.Effects = DragDropEffects.Copy;
                LeftPanelTextBlock.Text = "Отпустите, чтобы загрузить";
            }
            else
            {
                LeftPanelTextBlock.Text = "Я такое не ем";
                e.Effects = DragDropEffects.None;
            }
        }
        private void ElemDragLeave(object sender, DragEventArgs e)
        {
            LeftPanelTextBlock.Text = "Перетащи в меня файл";
        }

        // TODO: Разобраться с контентом в tabControl
        private void CreateDataGrid(int countOfTabs = 1)
        {
            LeftPanelTextBlock.Visibility = Visibility.Hidden;

            DataGrid grid = new DataGrid
            {
                Width = MainPanel.ActualWidth,
            };
            grid.AutoGenerateColumns = true;
            
            List<TabelItems> items = new List<TabelItems>();
            items.Add(controller.GetHeader());

            grid.Columns.Add(new DataGridTextColumn { Header = items[0].id_item });
            grid.Columns.Add(new DataGridTextColumn { Header = items[0].FIO });
            for (int i = 0; i < items[0].marks.Length; i++)
                grid.Columns.Add(new DataGridTextColumn { Header = items[0].marks[i] });

            for (int i = 1; i < controller.rowsCount; i++)
                items.Add(controller.GetFromId(i));

            grid.ItemsSource = items;
            StackMainPan.Children.Add(grid);
        }
    }
}
