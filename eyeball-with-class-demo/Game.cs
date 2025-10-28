// Include the namespaces (code libraries) you need below.
using System;
using System.Collections.Specialized;
using System.Numerics;

// The namespace your code is in.
namespace MohawkGame2D
{
    /// <summary>
    ///     Your game code goes inside this class!
    /// </summary>
    public class Game
    {
        int numberofEyesClicked = 0;
        float gameTimeWon;
        Eyeball[] eyeballs = [];
        public void Setup()
        {
            Window.SetTitle("Eyeball with Class");
            Window.SetSize(400, 400);
            numberofEyesClicked = 0;
            gameTimeWon = 0;
            Time.SecondsElapsed = 0; // Reset timer
            eyeballs = [
                new Eyeball(new Vector2(100,200)),
                new Eyeball(new Vector2(300, 200)),
                new Eyeball(new Vector2(200, 100)),
            ];
        }
        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            bool isGameWon = numberofEyesClicked == eyeballs.Length;
            if (isGameWon)
            {
                GameWinScreen();
            }
            else
            {
                GameClickedOnEyes();
            }
        }
        void GameClickedOnEyes()
        {
            Window.ClearBackground(Color.OffWhite);
            for (int i = 0; i < eyeballs.Length; i++)
            {
                Eyeball eyeball = eyeballs[i];
                bool isClicked = eyeballs[i].IsEyeClicked();
                eyeballs[i].DrawEyeball();

                // Check if eye is clicked
                if (isClicked)
                {
                    numberofEyesClicked++;
                    gameTimeWon = Time.SecondsElapsed;
                }
            }
            // Draw current progress to screen
            string text = $"{Time.SecondsElapsed:0.00}";
            Text.Color = Color.Black;
            Text.Draw(text, 10, 10);
        }
        void GameWinScreen()
        {
            Window.ClearBackground(Color.Yellow);
            Text.Color = Color.Black;
            string text = $"Winner!\nTime: {gameTimeWon}";
            float y = Window.Height / 2 - Text.Size;
            Text.Draw(text, 10, y);

            if (Input.IsKeyboardKeyPressed(KeyboardInput.Space))
            {
                // Reset game variables
                Setup();
            }
        }
    }
}
