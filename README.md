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

### Game Interface
![GameInterface](https://github.com/Sagi314/Hangman/assets/42316417/ff0932cd-710d-4be2-a97e-66cc80d73839)

### Correct Guess
![CorrectGuess](https://github.com/Sagi314/Hangman/assets/42316417/4be5ee36-8f41-44c0-8116-c03031457689)

### Incorrect Guess
![IncorrectGuess](https://github.com/Sagi314/Hangman/assets/42316417/b39c7c01-d295-43da-8112-d92189f0e164)
