using UnityEngine;
namespace ZinklofDev.Utils
{
    static public class MathZ
    {
        public static double Square(double one)
        {
            return (one * one);
        }

        static public double VectorDistanceSquared(Vector3 one, Vector3 two)
        {
            return (Square((one.x - two.x)) + Square((one.y - two.y)) + Square(one.z - two.z));
        }
    }
}
