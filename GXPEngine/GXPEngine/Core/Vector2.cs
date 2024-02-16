using System;

namespace GXPEngine.Core
{
	public struct Vector2
	{
		public float x;
		public float y;
		
		public Vector2 (float x, float y)
		{
			this.x = x;
			this.y = y;
		}
		
		override public string ToString() {
			return "[Vector2 " + x + ", " + y + "]";
		}

		public Vector2 Normalize()
		{
			float len = Magnitude();
			if (len == 0) return this;
			else return this / len;
		}
		public float Magnitude() => (float)Math.Sqrt(x * x + y * y);
		public float DistanceTo(Vector2 other) => (this - other).Magnitude();
		public static Vector2 Lerp(Vector2 a, Vector2 b, float t) => a * (1 - t) + b * t;
		public static Vector2 operator /(Vector2 a, float b) => new Vector2(a.x / b, a.y / b);
		public static Vector2 operator *(Vector2 a, float b) => new Vector2(a.x * b, a.y * b);
		public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.x - b.x, a.y - b.y);
		public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.x + b.x, a.y + b.y);
    }
}

