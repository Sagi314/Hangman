# Hangman Game

This is a Hangman game built using C# and WPF (Windows Presentation Foundation). The game features an on-screen keyboard, an image display to depict the condition of the hanging man, and a word display to show the correct letters in their respective positions.

## Installation

To run the Hangman game on your local machine, follow these steps:

1. Ensure that you have the .NET Framework version 4.7.2 installed on your computer.
2. Clone or download the repository to your local machine.
3. Open the project in a compatible IDE, such as Visual Studio.
4. Build the project to restore the NuGet packages and compile the code.
5. Run the application.

## Gameplay

When you launch the Hangman game, the game will start automatically.

- The application will randomly select a word for you to guess.
- You will see a series of underscores representing the letters in the word.
- You can use your own physical keyboard or the on-screen keyboard to choose letters and try to guess the word.
- If the chosen letter is correct, it will be revealed in its respective position(s) in the word.
- If the chosen letter is incorrect, the image of the hanging man will change accordingly.
- Keep guessing letters until you either guess the entire word or make too many incorrect guesses resulting in the hanging man being fully depicted.

## Dependencies

This Hangman game was developed using the following dependencies:

- .NET Framework 4.7.2
- Windows Presentation Foundation (WPF)

Please make sure you have the necessary dependencies installed before running the application.

## Screenshots

<div>
  <h3>Game Interface</h3>
  <img src="https://github.com/Sagi314/Hangman/assets/42316417/53a9d00c-c0f7-4ebe-b8bd-cb82620d9f72" alt="image" width="50%">
  
  <h3>Correct Guess</h3>
  <img src="https://github.com/Sagi314/Hangman/assets/42316417/532657a5-3e46-437a-8983-dda9b6ceb133" alt="image" width="50%">
  
  <h3>Incorrect Guess</h3>
  <img src="https://github.com/Sagi314/Hangman/assets/42316417/c46d71c7-d03b-41bc-bd6b-09a2f769c184" alt="image" width="50%">
</div>
