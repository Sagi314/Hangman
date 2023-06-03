using System;
using System.Windows.Controls;

namespace HangmanWpfViews
{
    public class VirtualKeyboard : Canvas
    {
        public enum Kind { QWERTY }
        private readonly Kind kind;

        private static readonly char[][] qwertyLayout = new char[][]
        {
            new char[]{'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P'},
            new char[]   { 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L' },
            new char[]      { 'Z', 'X', 'C', 'V', 'B', 'N', 'M' }
        };
        private Button[][] keys = new Button[][] { new Button[10], new Button[9], new Button[7] };

        public Action<char> OnKeyPressed { private get; set; }


        public VirtualKeyboard(Kind kind, bool enablePhisicalKeyboard)
        {
            this.kind = kind;

            Init(enablePhisicalKeyboard);
        }


        private void Init(bool enablePhisicalKeyboard)
        {
            AddOnSizeChangedListener();
            if (kind == Kind.QWERTY)
            {
                InitAsQWERTY();
            }
            if (enablePhisicalKeyboard)
            {
                AddOnPhisicalKeyboardKeyDown();
            }
        }
        private void InitAsQWERTY()
        {
            for (int i = 0; i < keys.GetLength(0); i++)
            {
                for (int j = 0; j < keys[i].GetLength(0); j++)
                {
                    keys[i][j] = CreateKey(i, j, (10 - keys[i].GetLength(0)) / 2.0, qwertyLayout[i][j]);
                    Children.Add(keys[i][j]);
                    AddOnKeyClickListener(keys[i][j]);
                }
            }
        }


        private Button CreateKey(int row, int column, double leftOffset, char letter)
        {
            Button key = new Button();
            SetKeySize(key);
            SetKeyLocation(key, row, column, leftOffset);

            key.Content = letter;

            return key;
        }
        private void SetKeySize(Button key)
        {
            key.Width = ActualWidth / 10;
            key.Height = ActualHeight / 3;
        }
        private void SetKeyLocation(Button key, int row, int column, double leftOffset)
        {
            SetLeft(key, (column + leftOffset) * key.Width);
            SetTop(key, row * key.Height);
        }


        private void AddOnPhisicalKeyboardKeyDown()
        {
            KeyDown += (s, e) =>
            {
                if (e.Key.ToString().Length == 1)
                {
                    PerformClick(e.Key.ToString()[0]);
                }
            };
        }
        private void AddOnKeyClickListener(Button key)
        {
            key.Click += (s, e) =>
            {
                KeyPressed(s as Button);
            };
        }
        public void PerformClick(char key)
        {
            Button FoundKey = FindKey(key);
            if (FoundKey != null && FoundKey.IsEnabled)
            {
                KeyPressed(FoundKey);
            }
        }
        private void KeyPressed(Button key)
        {
            OnKeyPressed?.Invoke(key.Content.ToString()[0]);
        }


        public void Disable(char key)
        {
            Button FoundKey = FindKey(key);
            if (FoundKey != null)
            {
                FoundKey.IsEnabled = false;
            }
        }
        public void Enable(char key)
        {
            Button FoundKey = FindKey(key);
            if (FoundKey != null)
            {
                FoundKey.IsEnabled = true;
            }
        }
        public void EnableAll()
        {
            foreach (Button[] keysArr in keys)
            {
                foreach (Button key in keysArr)
                {
                    key.IsEnabled = true;
                }
            }
        }


        private Button FindKey(char keyContent)
        {
            foreach (Button[] keysArr in keys)
            {
                foreach (Button keyButton in keysArr)
                {
                    if (keyButton.Content.ToString() == keyContent.ToString())
                    {
                        return keyButton;
                    }
                }
            }

            return null;
        }


        private void AddOnSizeChangedListener()
        {
            SizeChanged += OnSizeChanged;
        }
        private void OnSizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            for (int i = 0; i < keys.GetLength(0); i++)
            {
                for (int j = 0; j < keys[i].GetLength(0); j++)
                {
                    SetKeySize(keys[i][j]);
                    SetKeyLocation(keys[i][j], i, j, (10 - keys[i].GetLength(0)) / 2.0);
                }
            }
        }
    }
}