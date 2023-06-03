using HangmanModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using HangmanWpfViews;

namespace HangmanWpfUi
{
    internal class UIManager
    {
        private readonly Canvas canvas;

        private static readonly string pathToHangmanImagesFolder = "pack://application:,,,/HangmanWpfUi;Component/res";
        private static readonly string[] hangmanImagesNames = { "hml1.png", "hml2.png", "hml3.png",
                                                                "hml4.png", "hml5.png", "hml6.png",
                                                                "hml7.png", "hml8.png", "hml9.png",
                                                                "hml10.png" };

        private readonly HangmanGame hangmanGame = new HangmanGame();
        private readonly HangmanImage hangmanImage = new HangmanImage(pathToHangmanImagesFolder, hangmanImagesNames);
        private readonly VirtualKeyboard virtualKeyboard = new VirtualKeyboard(VirtualKeyboard.Kind.QWERTY, true);
        private readonly WordView wordView = new WordView();

        private HangmanGame.Difficulty difficulty = HangmanGame.Difficulty.Easy;

        public UIManager(Canvas canvas)
        {
            this.canvas = canvas;

            Init();
        }

        private void Init()
        {
            DesignCanvas();
            AddViewsToScreen();
            AddOnSizeChangedListener();

            StartNewGame();
        }
        private void DesignCanvas()
        {
            canvas.Background = new SolidColorBrush(Colors.Thistle);
        }

        private void AddViewsToScreen()
        {
            AddVirtualKeyboardToScreen();
            AddWordViewToScreen();
            AddHangmanImageToScreen();
        }
        private void AddVirtualKeyboardToScreen()
        {
            virtualKeyboard.Focusable = true;

            canvas.Children.Add(virtualKeyboard);
        }
        private void AddWordViewToScreen()
        {
            wordView.AllCaps = true;
            canvas.Children.Add(wordView);
        }
        private void AddHangmanImageToScreen()
        {
            canvas.Children.Add(hangmanImage);
        }


        private void StartNewGame()
        {
            ResetViews();
            GenerateNewWord();
            UpdateWordView();
            AddOnKeyboardKeysClickListener();
            virtualKeyboard.Focus();
        }
        private void ResetViews()
        {
            virtualKeyboard.EnableAll();
            hangmanImage.Reset();
        }
        private void GenerateNewWord()
        {
            hangmanGame.GenerateWord(difficulty);
        }
        private void UpdateWordView()
        {
            wordView.SetWord(Parse(hangmanGame.CorrectGuessedLettersByPlace));
        }
        private void AddOnKeyboardKeysClickListener()
        {
            virtualKeyboard.OnKeyPressed = Guess;
        }


        private void Guess(char letter)
        {
            DisableKey(letter);

            if (hangmanGame.Guess(letter))
            {
                UpdateWordView();
                if (hangmanGame.Win) { EndGame(true); }
            }
            else
            {
                UpdateHangmanImage();
                if (hangmanGame.Lose) { EndGame(false); }
            }
        }
        private void UpdateHangmanImage()
        {
            hangmanImage.ImageNumber = hangmanGame.Strikes;
        }
        private void DisableKey(char letter)
        {
            virtualKeyboard.Disable(letter);
        }


        private void EndGame(bool win)
        {
            ShowEndGameDialog(win);
            virtualKeyboard.OnKeyPressed = null;
        }
        private void ShowEndGameDialog(bool win)
        {
            string message = $"YOU {(win ? "WIN" : "LOSE")}\n THE WORD WAS \"{hangmanGame.Word.ToUpper()}\"";

            OptionsMessageDialog.Show(canvas, message, "NEW GAME", "EXIT",
            (difficulty) =>
            {
                this.difficulty = difficulty.ToLower() == "hard" ? HangmanGame.Difficulty.Hard : HangmanGame.Difficulty.Easy;
                DismissEndGamePanel();
                StartNewGame();
            },
            () =>
            {
                DismissEndGamePanel();
                CloseWindow();
            },
            difficulty == HangmanGame.Difficulty.Easy ? 0 : 1, "EASY", "HARD");
        }
        private void DismissEndGamePanel()
        {
            OptionsMessageDialog.Dismiss();
        }
        private void CloseWindow()
        {
            Application.Current.Windows[0].Close();
        }


        private void AddOnSizeChangedListener()
        {
            canvas.SizeChanged += OnParentSizeChanged;
        }
        private void OnParentSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateSizesAndLocations();
        }

        private void UpdateSizesAndLocations()
        {
            UpdateVirtualKeyboardSizeAndLocation();
            UpdateWordViewSizeAndLocation();
            UpdateHangmanImageSizeAndLocation();
        }
        private void UpdateWordViewSizeAndLocation()
        {
            wordView.Height = canvas.ActualHeight * 0.3;
            wordView.Width = canvas.ActualWidth;

            CenterViewHorizontaly(wordView);
        }
        private void UpdateHangmanImageSizeAndLocation()
        {
            hangmanImage.Height = canvas.ActualHeight * 0.3;
            hangmanImage.Width = hangmanImage.Height;

            CenterViewHorizontaly(hangmanImage);
            CenterViewVertically(hangmanImage);
        }
        private void UpdateVirtualKeyboardSizeAndLocation()
        {
            virtualKeyboard.Width = canvas.ActualWidth * 0.7;
            virtualKeyboard.Height = canvas.ActualHeight * 0.3;
            CenterViewHorizontaly(virtualKeyboard);
            Canvas.SetTop(virtualKeyboard, canvas.ActualHeight * 0.7);

            virtualKeyboard.Focus();
        }

        private void CenterViewHorizontaly(FrameworkElement element)
        {
            Canvas.SetLeft(element, (canvas.ActualWidth - element.ActualWidth) / 2);
        }
        private void CenterViewVertically(FrameworkElement element)
        {
            Canvas.SetTop(element, (canvas.ActualHeight - element.ActualHeight) / 2);
        }


        private string Parse(char?[] letters)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char? letterChar in letters)
            {
                if (letterChar == null)
                {
                    builder.Append("_");
                }
                else
                {
                    builder.Append(letterChar);
                }
            }

            return builder.ToString();
        }
    }
}