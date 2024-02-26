using GXPEngine.Core;
using System;

namespace GXPEngine
{
    internal class Player : Sprite
    {
        //private float _spawnTimer = 0;

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
        }
        private void Update() { }

        private void Shoot()
        {
            for(int i = 0; i < DataStorage.TargetVectors.Length; i++)
            {
                Vector2 deltaVelocity = new Vector2(0, 0);
                if (Input.GetKeyDown(DataStorage.InputKeys[i]))
                {
                    deltaVelocity = DataStorage.TargetVectors[i] - new Vector2(x, y);
                    deltaVelocity = deltaVelocity.Normalize() * DataStorage.BulletSpeed * Time.deltaTime;
                }
                if (deltaVelocity.x != 0 || deltaVelocity.y != 0) { new Bullet(deltaVelocity) { x = x, y = y }; }
            }
        }
    }
}
