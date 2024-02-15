using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Enemy : Sprite
    {
        private Vector2 _velocity;
        public Enemy(Vector2 velocity) : base("triangle.png")
        {
            SetOrigin(width / 2, height / 2);
            collider.isTrigger = true;
            game.AddChild(this);
            _velocity = velocity * DataStorage.EnemySpeed * Time.deltaTime;
        }

        protected void Update()
        {
            x += _velocity.x;
            y += _velocity.y;
        }
        public void OnHit() => LateDestroy();
    }
}
