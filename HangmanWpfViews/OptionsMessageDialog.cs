using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HangmanWpfViews
{
    public static class OptionsMessageDialog
    {
        private static Canvas canvas = null;

        private static Grid grid;

        private static Button positiveButton;
        private static Button negativeButton;

        private static TextBlock messageText;

        private static ComboBox optionsComboBox;

        private static string message;
        private static string positiveButtonContent;
        private static string negativeButtonContent;

        private static string[] options;

        private static bool initallized = false;

        private static Action<string> onPositiveButtonClick;
        private static Action onNegativeButtonClick;

        private static int defaultIndex;

        public static void Show(Canvas canvasToDrawOn, string messageToPresent, string positiveButtonText,
                                string negativeButtonText, Action<string> onPositivePress, Action onNegativePress,
                                int defaultOptionIndex, params string[] optionsList)
        {
            canvas = canvasToDrawOn;
            message = messageToPresent;
            positiveButtonContent = positiveButtonText;
            negativeButtonContent = negativeButtonText;
            onPositiveButtonClick = onPositivePress;
            onNegativeButtonClick = onNegativePress;
            defaultIndex = defaultOptionIndex;
            options = optionsList;

            Init();
        }
        public static void Dismiss()
        {
            if (canvas != null)
            {
                canvas.Children.Remove(grid);
                canvas.SizeChanged -= OnCanvasSizeChanged;
                canvas = null;
            }
        }


        private static void Init()
        {
            if (!initallized)
            {
                AddViews();
                initallized = true;
            }

            UpdateViewsContentAndClicks();
            PutOnCenter();
            AddOnCanvasChangedListener();
        }
        private static void UpdateViewsContentAndClicks()
        {
            messageText.Text = message;

            positiveButton.Content = positiveButtonContent;
            positiveButton.Click += (o, e) => onPositiveButtonClick?.Invoke(optionsComboBox.SelectedValue.ToString());

            negativeButton.Content = negativeButtonContent;
            negativeButton.Click += (o, e) => onNegativeButtonClick?.Invoke();

            optionsComboBox.ItemsSource = options;
            optionsComboBox.SelectedIndex = defaultIndex;

            canvas.Children.Add(grid);
        }
        private static void PutOnCenter()
        {
            Canvas.SetTop(grid, (canvas.ActualHeight - grid.Height) / 2);
            Canvas.SetLeft(grid, (canvas.ActualWidth - grid.Width) / 2);
        }

        private static void AddViews()
        {
            AddGrid();
            AddMessageText();
            AddDifficulties();
            AddButtons();
        }

        private static void AddGrid()
        {
            grid = new Grid
            {
                Background = new SolidColorBrush(Color.FromArgb(128, 255, 255, 255)),
                Width = 250,
                Height = 150
            };

            AddGridColumns(3);
            AddGridRows(3);
        }
        private static void AddGridColumns(int columns)
        {
            for (int i = 0; i < columns; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }
        private static void AddGridRows(int rows)
        {
            for (int i = 0; i < rows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
        }

        private static void AddMessageText()
        {
            messageText = new TextBlock
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                TextAlignment = TextAlignment.Center
            };

            Viewbox viewbox = new Viewbox();

            Grid.SetRow(viewbox, 0);
            Grid.SetColumn(viewbox, 0);
            Grid.SetColumnSpan(viewbox, 3);

            viewbox.Child = messageText;

            grid.Children.Add(viewbox);
        }
        private static void AddDifficulties()
        {
            optionsComboBox = new ComboBox();
            Grid.SetRow(optionsComboBox, 1);
            Grid.SetColumn(optionsComboBox, 0);
            Grid.SetColumnSpan(optionsComboBox, 3);

            optionsComboBox.VerticalAlignment = VerticalAlignment.Center;
            optionsComboBox.HorizontalAlignment = HorizontalAlignment.Center;

            grid.Children.Add(optionsComboBox);
        }

        private static void AddButtons()
        {
            AddPositiveButton();
            AddNegativeButton();
        }
        private static void AddPositiveButton()
        {
            positiveButton = new Button();
            Grid.SetRow(positiveButton, 2);
            Grid.SetColumn(positiveButton, 2);

            grid.Children.Add(positiveButton);
        }
        private static void AddNegativeButton()
        {
            negativeButton = new Button();
            Grid.SetRow(negativeButton, 2);
            Grid.SetColumn(negativeButton, 0);

            grid.Children.Add(negativeButton);
        }


        private static void AddOnCanvasChangedListener()
        {
            canvas.SizeChanged += OnCanvasSizeChanged;
        }
        private static void OnCanvasSizeChanged(object sender, SizeChangedEventArgs e)
        {
            PutOnCenter();
        }
    }
}