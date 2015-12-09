using System;

namespace Common
{
    public static class Util
    {
        public static bool NearlyEqual(float v1, float v2) => Math.Abs(v2 - v1) < MachineEpsilonFloat;
        public static bool NearlyEqual(double v1, double v2) => Math.Abs(v2 - v1) < MachineEpsilonDouble;

        #region Details

        public static readonly double MachineEpsilonDouble = __MachineEpsilonDouble;

        /// <summary>
        /// see http://stackoverflow.com/questions/9392869/where-do-i-find-the-machine-epsilon-in-c
        /// </summary>
        static double __MachineEpsilonDouble
        {
            get
            {
                double machEps = 1.0d;

#pragma warning disable RECS0018 // Comparison of floating point numbers with equality operator
                while ((1.0d + machEps) != 1.0d)
#pragma warning restore RECS0018 // Comparison of floating point numbers with equality operator
                {
                    machEps *= 0.5d;
                }

                return machEps * 2.0d;
            }
        }


        public static readonly double MachineEpsilonFloat = __MachineEpsilonFloat;

        /// <summary>
        /// see http://stackoverflow.com/questions/9392869/where-do-i-find-the-machine-epsilon-in-c
        /// </summary>
        static float __MachineEpsilonFloat
        {
            get
            {
                float machEps = 1.0f;

#pragma warning disable RECS0018 // Comparison of floating point numbers with equality operator
                while ((1.0f + machEps) != 1.0f)
#pragma warning restore RECS0018 // Comparison of floating point numbers with equality operator
                {
                    machEps *= 0.5f;
                }

                return machEps * 2.0f;
            }
        }

        #endregion
    }


}
