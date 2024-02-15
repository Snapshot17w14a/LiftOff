using GXPEngine.Core;
using System;

namespace GXPEngine
{
    internal class Player : Sprite
    {
        private float _spawnTimer = 0;
        public Player() : base("square.png")
        {
            SetOrigin(width / 2, height / 2);
            SetXY(game.width / 2, game.height / 2);
            game.AddChild(this);
        }
        private void Update()
        {
            _spawnTimer += Time.deltaTime;
            Shoot();
            TEMPSpawnEnemy();
        }

        private void TEMPSpawnEnemy()
        {
            if(_spawnTimer >= DataStorage.EnemySpawnInterval * 1000)
            {
                _spawnTimer = 0;
                int index = new Random().Next(0, 6);
                Enemy enemy = new Enemy((new Vector2(x, y) - DataStorage.TargetVectors[index]).Normalize());
                enemy.SetXY(DataStorage.TargetVectors[index].x, DataStorage.TargetVectors[index].y);
            }
        }

        private void Shoot()
        {
            for(int i = 0; i < 6; i++)
            {
                Vector2 deltaVelocity = new Vector2(0, 0);
                if (Input.GetKeyDown(DataStorage.InputKeys[i]))
                {
                    deltaVelocity = DataStorage.TargetVectors[i] - new Vector2(x, y);
                    deltaVelocity = deltaVelocity.Normalize() * DataStorage.BulletSpeed;
                }
                if (deltaVelocity.x != 0 || deltaVelocity.y != 0)
                {
                    Bullet bullet = new Bullet(deltaVelocity);
                    bullet.SetXY(x, y);
                    game.AddChild(bullet);
                }
            }
        }
    }
}
