using System;

[Serializable]
public class PositionOnLevel
{
    public string Level;
    public Vector3Data Position;

    public PositionOnLevel(string level, Vector3Data position)
    {
        Level = level;
        Position = position;
    }

    public PositionOnLevel(string initialLevel)
    {
        Level = initialLevel;
        Position = new Vector3Data(0,0,0);
    }
}