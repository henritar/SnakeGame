namespace Project.Snake
{
    public static class SnakeAppConstants
    {
        public const string CreateAssetMenuPath = "Projects/Snake/";
        public const int CreateAssetMenuOrder = 1;

        public const string GameStatePref = "GameState";
        public const string MainParent = "MainParent";
        public const string SnakePlayerParent = "SnakePlayerParent";
        public const string SnakeAIParent = "SnakeAIParent";
        public const string BlockParent = "BlockParent";
        public const string CameraParent = "CameraParent";

        public const int AudioIndexBounce = 0;
        public const int AudioIndexRising = 1;
        public const int AudioIndexFalling = 2;

        public const float SnakeVelocity = 5f;
        public const float SnakeVelocityEnginePowerModifier = 1.5f;
        public const float SnakeVelocityDebuffModifier = 3f;

    }
}