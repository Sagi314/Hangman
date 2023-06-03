using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HangmanWpfViews
{
    public class WordView : StackPanel
    {
        private static readonly Brush underlineBackground = new ImageBrush()
        {
            ImageSource = new BitmapImage(new Uri("pack://application:,,,/HangmanWpfViews;Component/res/letter.png", UriKind.RelativeOrAbsolute)),
            Stretch = Stretch.Fill
        };
        private const double RATIO = 1.2;

        private readonly List<TextBlock> lettersBlocks = new List<TextBlock>();

        private bool allCaps = false;
        public bool AllCaps
        {
            get => allCaps;
            set
            {
                allCaps = value;
                OnAllCapsChanged();
            }
        }
        private void OnAllCapsChanged()
        {
            foreach (TextBlock block in lettersBlocks)
            {
                block.Text = AllCaps ? block.Text.ToUpper() : block.Text.ToUpper();
            }
        }


        public WordView()
        {
            Init();
        }


        public void SetWord(string s)
        {
            if (s.Length < lettersBlocks.Count)
            {
                RemoveCells(lettersBlocks.Count - s.Length);
            }
            else if (s.Length > lettersBlocks.Count)
            {
                AddCells(s.Length - lettersBlocks.Count);
            }

            s = AllCaps ? s.ToUpper() : s.ToLower();
            for (int i = 0; i < s.Length; i++)
            {
                lettersBlocks[i].Text = "";
                (Children[i] as Border).Background = underlineBackground;
                if (s[i] == ' ')
                {
                    (Children[i] as Border).Background = new SolidColorBrush(Colors.Transparent);
                }
                else if (s[i] != '_')
                {
                    lettersBlocks[i].Text = $"{s[i]}";
                }
            }
            UpdateSize();
        }


        private void Init()
        {
            SetStackPanelParams();
            AddOnSizedChangedListener();
        }
        private void SetStackPanelParams()
        {
            Orientation = Orientation.Horizontal;
            HorizontalAlignment = HorizontalAlignment.Center;
        }
        private void AddOnSizedChangedListener()
        {
            SizeChanged += (s, e) => UpdateSize();
        }
        private void UpdateSize()
        {
            double width = ActualWidth - lettersBlocks.Count * 2;
            double height = ActualHeight;
            if (width > lettersBlocks.Count)
            {
                double maximalWidth = Math.Min(width / lettersBlocks.Count, height / RATIO);

                foreach (FrameworkElement child in Children)
                {
                    child.Width = maximalWidth;
                    child.Height = maximalWidth * RATIO;
                }

                if (Children.Count > 0)
                {
                    (Children[0] as FrameworkElement).Margin = new Thickness((width - maximalWidth * Children.Count) / 2, 0, 0, 0);
                }
            }
        }


        private void RemoveCells(int cellToRemove)
        {
            for (int i = 0; i < cellToRemove; i++)
            {
                Children.RemoveAt(lettersBlocks.Count - 1);
                lettersBlocks.RemoveAt(lettersBlocks.Count - 1);
            }
        }
        private void AddCells(int cellToAdd)
        {
            for (int i = 0; i < cellToAdd; i++)
            {
                AddCell();
            }
        }
        private void AddCell()
        {
            Border border = new Border
            {
                Margin = new Thickness(1)
            };
            TextBlock textBlock = CreateTextBlock();
            Viewbox viewbox = CreateViewBox(textBlock);

            border.Child = viewbox;
            Children.Add(border);
        }

        private TextBlock CreateTextBlock()
        {
            TextBlock textBlock = new TextBlock();
            lettersBlocks.Add(textBlock);

            return textBlock;
        }
        private Viewbox CreateViewBox(TextBlock textBlock)
        {
            Viewbox viewbox = new Viewbox
            {
                StretchDirection = StretchDirection.Both,
                Stretch = Stretch.Uniform
            };

            viewbox.Child = textBlock;

            return viewbox;
        }
    }
}