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
        Color irisColor = new Color(128, 64, 0);
        Vector2 position = new Vector2(200, 200);
        float corneaR = 50;
        float irisR = 30;
        float pupilR = 15;

        public void Setup()
        {
            Window.SetTitle("Eyeball with Class");
            Window.SetSize(400, 400);
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.OffWhite);
            DrawEyeball();
        }
        void DrawEyeball()
        {
            // General draw config
            Draw.LineColor = Color.Black;
            Draw.LineSize = 1;

            // Draw Cornea
            Draw.FillColor = Color.White;
            Draw.Circle(position, corneaR);

            // Math out vector from eye to mouse
            Vector2 mousePosition = Input.GetMousePosition();
            Vector2 fromEyeToMouse = mousePosition - position;
            float distanceToMouse = fromEyeToMouse.Length();
            Vector2 directionToMouse = Vector2.Normalize(fromEyeToMouse);

            //Calculate where iris and pupil will go
            Vector2 irisPosition;
            if (distanceToMouse < corneaR - irisR)
            {
                // Mouse is on top of eye, draw at mouse position
                irisPosition = mousePosition;
            }
            else
            {
                // Mouse is outside of eye, draw eye towards mouse
                irisPosition = position + directionToMouse * (corneaR - irisR);
            }

            // Draw Iris
            Draw.FillColor = irisColor;
            Draw.Circle(irisPosition, irisR);

            // Draw Pupil
            Draw.FillColor = Color.Black;
            Draw.Circle(irisPosition, pupilR);
        }
    }

}
