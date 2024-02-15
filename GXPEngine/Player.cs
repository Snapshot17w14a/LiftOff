using GXPEngine.Core;
using System;

namespace GXPEngine
{
    internal class Player : Sprite
    {
        private float _spawnTimer = 0;

        private readonly DataStorage _dataStorage;
        private static Player _instance;
        public static Player Instance
        {
            get
            {
                if(_instance == null) _instance = new Player();
                return _instance;
            }
        }
        private Player() : base("square.png")
        {
            SetOrigin(width / 2, height / 2);
            SetXY(game.width / 2, game.height / 2);
            game.AddChild(this);
            _dataStorage = DataStorage.Instance;
        }
        private void Update()
        {
            _spawnTimer += Time.deltaTime;
            Shoot();
            TEMPSpawnEnemy();
        }

        // This method is temporary and will be removed in the final version
        private void TEMPSpawnEnemy()
        {
            if(_spawnTimer >= _dataStorage.EnemySpawnInterval * 1000)
            {
                _spawnTimer = 0;
                int index = new Random().Next(0, _dataStorage.TargetVectors.Length);
                new Enemy((new Vector2(x, y) - _dataStorage.TargetVectors[index]).Normalize()) { x = _dataStorage.TargetVectors[index].x, y = _dataStorage.TargetVectors[index].y };
            }
        }

        private void Shoot()
        {
            for(int i = 0; i < _dataStorage.TargetVectors.Length; i++)
            {
                Vector2 deltaVelocity = new Vector2(0, 0);
                if (Input.GetKeyDown(_dataStorage.InputKeys[i]))
                {
                    deltaVelocity = _dataStorage.TargetVectors[i] - new Vector2(x, y);
                    deltaVelocity = deltaVelocity.Normalize() * _dataStorage.BulletSpeed * Time.deltaTime;
                }
                if (deltaVelocity.x != 0 || deltaVelocity.y != 0) new Bullet(deltaVelocity) { x = x, y = y };
            }
        }
    }
}
