using MohawkGame2D;
using System.ComponentModel.Design;
using System.Numerics;

public class Eyeball
{
    public Color irisColor = new Color(128, 64, 0);
    public Vector2 position = new Vector2(200, 200);
    public float corneaR = 50;
    public float irisR = 30;
    public float pupilR = 15;
    private bool isEyeOpen = true;

    Color[] eyeColors = [
        new Color(128,  64,   0), // Brown
        new Color( 64, 128,  64), // Green
        new Color( 64, 100, 160), // Blue
        ];
    public Eyeball() { }
    public Eyeball(Vector2 position)
    {
        this.position = position;
        this.irisColor = eyeColors[Random.Integer(eyeColors.Length)];
    }
    public void IsEyeClicked()
    {
        // Only run if we are clicking left mouse button
        if (Input.IsMouseButtonPressed(MouseInput.Left))
        {
            // Compute distance between centre of eye and mouse
            Vector2 mousePosition = Input.GetMousePosition();
            Vector2 fromEyeToMouse = mousePosition - position;

            // Check if mouse is on top of eye (Inside circle)
            float distanceEyeToMouse = fromEyeToMouse.Length();
            if (distanceEyeToMouse <= corneaR)
            {
                isEyeOpen = false;
            }
        }
    }
    public void DrawEyeball()
    {
        // General draw config
        Draw.LineColor = Color.Black;
        Draw.LineSize = 1;

        // Draw Cornea
        Draw.FillColor = Color.White;
        Draw.Circle(position, corneaR);

        if (isEyeOpen)
        {
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
        else // If eye is closed
        {
            Vector2 offset = new Vector2(corneaR, 0);
            Draw.Line(position - offset, position + offset);
        }
    }
}