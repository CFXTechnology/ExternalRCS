using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprayExternal.Math
{
    public struct Vector3
    {
        public float X, Y, Z;

        public Vector3(float[] Values) : this(Values[0], Values[1], Values[2]) { }

        public Vector3(float X, float Y, float Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public override string ToString()
        {
            return string.Format("X: {0}, Y: {1}, Z: {2}", X, Y, Z);
        }

        public static Vector3 operator +(Vector3 You, Vector3 Me)
        {
            return new Vector3(You.X + Me.X, You.Y + Me.Y, You.Z + Me.Z);
        }
        public static Vector3 operator -(Vector3 You, Vector3 Me)
        {
            return new Vector3(You.X - Me.X, You.Y - Me.Y, You.Z - Me.Z);
        }
        public static Vector3 operator *(Vector3 You, Vector3 Me)
        {
            return new Vector3(You.X * Me.X, You.Y * Me.Y, You.Z * Me.Z);
        }
        public static Vector3 operator *(Vector3 v1, float scalar)
        {
            return new Vector3(v1.X * scalar, v1.Y * scalar, v1.Z * scalar);
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
                    case 2:
                        return this.Z;
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
                    case 2:
                        this.Z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }
    }
}
