using UnityEngine;

public static class DataExtensions
{
    public static Vector3Data AsVectorData(this Vector3 position) => 
        new Vector3Data(position.x, position.y, position.z);

    public static Vector3 AsUnityVector(this Vector3Data vector3Data) =>
        new Vector3(vector3Data.X, vector3Data.Y, vector3Data.Z);

    public static Vector3 AddY(this Vector3 vector3, float value)
    {
        vector3.y += value;
        return vector3;
    }
        
    public static string ToJson(this object obj) =>
        JsonUtility.ToJson(obj);
        
    public static T ToDeserialized<T>(this string json) => JsonUtility.FromJson<T>(json);
}