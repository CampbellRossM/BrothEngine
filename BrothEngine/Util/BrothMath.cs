using System;
using SFML.Graphics;

namespace Broth.Util
{
    public static class BrothMath
    {
        /// <summary> An instance of a random generator. All random numbers should be called through here. </summary>
        public static Random Random { get; } = new Random();

        /// <summary> Linear interpolation of two values. </summary>
        /// <param name="value"> Expressed as a percent of the difference between x and y. </param>
        /// <returns> May be outside x and y if value is less than 0 or greater than 1 </returns>
        public static float Lerp(float value, float x, float y)
        {
            return (value * (y - x)) + x;
        }

        /// <summary> Clamped linear interpolation between two values. </summary>
        /// <param name="value"> Expressed as a percent of the difference between x and y. </param>
        /// <returns> Will always be between x and y </returns>
        public static float Clerp(float value, float x, float y)
        {
            return Lerp(Math.Clamp(value, 0, 1), x, y);
        }

        /// <summary> Linear interpolation of each of the ARGB values between two colors </summary>
        /// <param name="value"> Expressed as a percent of the difference between colors x and y. </param>
        public static Color Lerp(float value, Color x, Color y)
        {
            return new Color
                (
                    (byte)Lerp(value, x.R, y.R),
                    (byte)Lerp(value, x.G, y.G),
                    (byte)Lerp(value, x.B, y.B),
                    (byte)Lerp(value, x.A, y.A)
                );
        }
    }
}
