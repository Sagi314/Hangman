using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace HangmanWpfViews
{
    public class HangmanImage : Image
    {
        private readonly string pathToFolder;
        private readonly string[] imagesNames;

        private int imageNumber;
        public int ImageNumber
        {
            get => imageNumber;
            set
            {
                if (value >= 0 && value < imagesNames.Length)
                {
                    imageNumber = value;
                    SetSourceByLevel();
                }
            }
        }

        public HangmanImage(string pathToFolder, params string[] imagesNames)
        {
            this.pathToFolder = pathToFolder;
            this.imagesNames = imagesNames;
        }

        private void SetSourceByLevel()
        {
            Source = new BitmapImage(new Uri($"{pathToFolder}/{imagesNames[imageNumber]}"));
        }

        public void Reset()
        {
            ImageNumber = 0;
        }
    }
}