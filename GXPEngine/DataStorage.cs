using GXPEngine.Core;

namespace GXPEngine
{
    internal static class DataStorage
    {
        //The coodinates of the targets where the enemies will spawn, and where the player can shoot at
        private static Vector2[] _targetVectors = { new Vector2(25, 95), new Vector2(400, 20), new Vector2(775, 95), new Vector2(775, 500), new Vector2(400, 575), new Vector2(25, 500) };

        //The keys that the player can use to shoot at the targets
        private static int[] _inputKeys = { Key.ONE, Key.TWO, Key.THREE, Key.FOUR, Key.FIVE, Key.SIX };

        //The speed of the bullets and the enemies
        private static float _bulletSpeed = 1.5f;
        private static float _enemySpeed = 1f;

        //The time in second between enemy spawns
        private static float _enemySpawnInterval = 5;


        //Getters and Setters
        public static Vector2[] TargetVectors => _targetVectors;
        public static int[] InputKeys => _inputKeys;
        public static float BulletSpeed => _bulletSpeed;
        public static float EnemySpeed  => _enemySpeed;
        public static float EnemySpawnInterval => _enemySpawnInterval;
    }
}
