namespace Util
{
    public static class Extensions
    {
        public static System.Numerics.Vector2 ToWorldVector2(this UnityEngine.Vector3 position)
        {
            return new System.Numerics.Vector2(position.x, position.y);
        }
        
        public static System.Numerics.Vector2 ToWorldVector2(this UnityEngine.Vector2 position)
        {
            return new System.Numerics.Vector2(position.x, position.y);
        }
        
        public static UnityEngine.Vector2 ToUnityVector2(this System.Numerics.Vector2 position)
        {
            return new UnityEngine.Vector2(position.X, position.Y);
        }
    }
}