namespace Systems
{
    static class DynamicParameter
    {
        /// <summary>
        /// キャラクターの移動にかかる時間
        /// </summary>
        public static float StepTime { get; set; } = 0.1f;

    }

    static class StaticSetting
    {
        public static float WalkTime => 0.15f;
        public static float RunTime => 0.02f;
    }
}
