using UnityEngine;

public static class DataExtensions
{
    public static Vector3 Up(this Vector3 vector) =>
        new Vector3(vector.x,vector.y+1f,vector.z);
    public static Vector3 Down(this Vector3 vector) =>
        new Vector3(vector.x, vector.y - 1f, vector.z);

}