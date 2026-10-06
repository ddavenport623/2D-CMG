using UnityEngine;

public static class WarpLine
{
    public static LineRenderer CreateWarpLine(Vector3 start, Vector3 end)
    {
        // Create an empty GameObject to hold the line
        GameObject lineObj = new("PurpleLine");
        LineRenderer lr = lineObj.AddComponent<LineRenderer>();

        // Basic setup
        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        // Make sure it works in 2D
        lr.useWorldSpace = true;
        lr.sortingOrder = -5; // draw on top if needed

        // Line appearance
        lr.startWidth = 0.5f;
        lr.endWidth = 0.5f;

        // Purple color
        lr.startColor = new Color(0.6f, 0.0f, 1.0f); // purple
        lr.endColor = lr.startColor;

        // Use default sprite material so it shows in 2D URP
        lr.material = new Material(Shader.Find("Sprites/Default"));

        return lr;
    }
}