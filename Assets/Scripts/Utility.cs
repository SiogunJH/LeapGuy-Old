using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityLib
{
    public static class Utility
    {
        public static float ToRadians(this float degrees) => degrees / 180 * Mathf.PI;
        public static void RestrictBetween(ref this float toRestrict, float min, float max, bool loop = false)
        {
            // Verify syntax
            if (min > max)
                Debug.LogError("Cannot RestrictBetween when minimum value is bigger than maximum value");

            // Restrict
            if (loop) toRestrict += toRestrict < min ? max - min : toRestrict > max ? min - max : 0;
            else toRestrict = toRestrict < min ? min : toRestrict > max ? max : toRestrict;
        }
        public static float Round(this float value, int decimalPlaces)
        {
            float multiplier = Mathf.Pow(10f, decimalPlaces);
            return Mathf.Round(value * multiplier) / multiplier;
        }
        public static bool SimilarTo(this float value1, float value2, float maximumDifference = 0.0001f)
        {
            return Math.Abs(value1 - value2) <= maximumDifference;
        }
    }
}