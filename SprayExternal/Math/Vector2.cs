using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprayExternal.Math
{
    public struct Vector2
    {
        public float X, Y;

        public Vector2(float[] Values) : this(Values[0], Values[1]) { }

        public Vector2(float X, float Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public override string ToString()
        {
            return string.Format("X: {0}, Y: {1}", X, Y);
        }

        public static Vector2 operator +(Vector2 You, Vector2 Me)
        {
            return new Vector2(You.X + Me.X, You.Y + Me.Y);
        }
        public static Vector2 operator -(Vector2 You, Vector2 Me)
        {
            return new Vector2(You.X - Me.X, You.Y - Me.Y);
        }
        public static Vector2 operator *(Vector2 You, Vector2 Me)
        {
            return new Vector2(You.X * Me.X, You.Y * Me.Y);
        }

        public float this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return this.X;
                    case 1:
                        return this.Y;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (i)
                {
                    case 0:
                        this.X = value;
                        break;
                    case 1:
                        this.Y = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }
    }
}
