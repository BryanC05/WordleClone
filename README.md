# 🟩 WordleClone 🟩

A simple clone of the popular word-guessing game Wordle, built entirely with .NET MAUI and C\#. This project was created to practice .NET MAUI layout, C\# game logic, and app state management.

## ✨ Features

  * **Classic Game Grid:** A 6x5 grid for guessing words.
  * **Proportional Keyboard:** A custom-built, responsive on-screen keyboard that scales to fit any phone screen size.
  * **Complete Game Logic:**
      * Color-coded tiles for **Correct**, **Present**, and **Absent** letters.
      * **Active Row Highlighting** to show the user where they are typing.
      * Input validation that checks guesses against a master word list.
  * **Random Word Selection:** The secret word is randomly chosen from a large, bundled text file every time a new game starts.
  * **Bundled Word List:** The app loads a `wordlist.txt` file from the app's resources on startup to populate its word list.

## 🛠️ Tech Stack

  * **.NET MAUI:** The core framework used for building the cross-platform UI.
  * **C\#:** Used for all game logic, state management, and UI event handling.
  * **XAML:** Used to define the layout of the game board and keyboard.
  * **`MauiAsset`:** The `wordlist.txt` file is bundled as a "MauiAsset" and read at runtime.

## 🚀 How to Run

### Prerequisites

1.  **[.NET 8 SDK](https://dotnet.microsoft.com/download)** (or newer)
2.  **.NET MAUI Workload:** Install by running `dotnet workload install maui`.
3.  **Android Studio:** Required to install the Android SDK, emulators, and accept SDK licenses.
4.  **Java SDK:** The build requires the Microsoft OpenJDK (version 17).

### Running the App from Terminal

1.  **Clone the Repository:**
    ```bash
    git clone [Your-GitHub-Repo-URL]
    cd WordleClone/WordleClone
    ```
2.  **Restore Dependencies:**
    ```bash
    dotnet restore
    ```
3.  **Run on Android:**
      * Plug in your Android phone (with USB Debugging enabled) OR start an Android Emulator.
      * Run the following command:
    <!-- end list -->
    ```bash
    dotnet build -t:Run -f net8.0-android
    ```

## 📂 Key Files

  * **`MainPage.xaml`:** Defines the layout for the 6x5 game grid and the proportional keyboard using XAML `Grid`s.
  * **`MainPage.xaml.cs`:** Contains all C\# game logic, including:
      * `CreateGameBoard()`: Dynamically creates the 30 `Border` and `Label` elements.
      * `LoadWordsAsync()`: Asynchronously reads `wordlist.txt` on startup.
      * `OnKeyboardClicked()`, `OnDeleteClicked()`, `OnEnterClicked()`: Handle all user input.
      * `CheckGuess()`: The core logic that colors the tiles.
      * `ResetGame()`: Clears the board and picks a new random word.
  * **`Resources/Raw/wordlist.txt`:** The bundled text file containing thousands of valid 5-letter words.
  * **`WordleClone.csproj`:** The project file. It has been modified to:
    1.  Only target `net8.0-android`.
    2.  Include `wordlist.txt` as a `MauiAsset`.
