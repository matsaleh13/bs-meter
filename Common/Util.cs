using System;

namespace AnalysisLib
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

                while ((1.0d + machEps) != 1.0d)
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

                while ((1.0f + machEps) != 1.0f)
                {
                    machEps *= 0.5f;
                }

                return machEps * 2.0f;
            }
        }

        #endregion
    }


}
