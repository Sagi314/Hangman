namespace HangmanModels
{
    public class HangmanGame
    {
        public enum Difficulty { Hard, Easy }
        private Difficulty difficulty;

        private bool[] allGuessedLetters;
        private char?[] correctGuesssedLettersByPlace;
        public char?[] CorrectGuessedLettersByPlace { get => correctGuesssedLettersByPlace; }

        public string Word { get; private set; }

        public bool Win { get; private set; }
        public bool Lose { get; private set; }

        public int Strikes { get; private set; }


        public void GenerateWord(Difficulty difficulty)
        {
            Reset();

            SetDifficulty(difficulty);
            GenerateWord();
            InitCorrectGuesssedLettersByPlace();
        }
        private void Reset()
        {
            Strikes = 0;
            allGuessedLetters = new bool[26];
        }
        private void SetDifficulty(Difficulty difficulty)
        {
            this.difficulty = difficulty;
        }
        private void GenerateWord()
        {
            Word = WordsBank.GenerateWord().ToLower();
        }
        private void InitCorrectGuesssedLettersByPlace()
        {
            correctGuesssedLettersByPlace = new char?[Word.Length];
            for (int i = 0; i < Word.Length; i++)
            {
                if (Word[i] == ' ')
                {
                    correctGuesssedLettersByPlace[i] = ' ';
                }
            }
        }


        public bool Guess(char letter)
        {
            letter = MakeLowerCase(letter);
            if (IsLetter(letter, out int letterIndex) && !Guessed(letterIndex))
            {
                MarkAsGuessed(letterIndex);

                if (IsRightGuess(letter))
                {
                    Win = CheckIfwin();
                    return true;
                }

                AddStrike();
                Lose = CheckIfLose();
            }

            return false;
        }
        private bool Guessed(int index) => allGuessedLetters[index];
        private bool IsRightGuess(char letter)
        {
            bool isRightGuess = false;
            for (int i = 0; i < Word.Length; i++)
            {
                if (Word[i] == letter)
                {
                    correctGuesssedLettersByPlace[i] = letter;

                    isRightGuess = true;
                }
            }

            return isRightGuess;
        }
        private void AddStrike()
        {
            if (difficulty == Difficulty.Hard)
            {
                switch (Strikes)
                {
                    case 1:
                    case 5:
                    case 7:
                        Strikes++;
                        break;
                }
            }

            Strikes++;
        }


        private bool CheckIfwin()
        {
            for (int i = 0; i < correctGuesssedLettersByPlace.Length; i++)
            {
                if (correctGuesssedLettersByPlace[i] == null)
                {
                    return false;
                }
            }

            return true;
        }
        private bool CheckIfLose()
        {
            return Strikes == 9;
        }


        private void MarkAsGuessed(int indexToMark)
        {
            if (indexToMark >= 0 && indexToMark < allGuessedLetters.Length)
            {
                allGuessedLetters[indexToMark] = true;
            }
        }
        private bool IsLetter(char letterToCheck, out int outIndex)
        {
            letterToCheck = MakeLowerCase(letterToCheck);

            if (letterToCheck >= 'a' && letterToCheck <= 'z')
            {
                //97 is the ascii number of 'a'. we reduce 97 of the letter so it will give us a number between 0 - 25.
                outIndex = letterToCheck - 97;
                return true;
            }

            outIndex = -1;
            return false;
        }
        private char MakeLowerCase(char toLower)
        {
            return (char)(toLower | ' ');
        }
    }
}
