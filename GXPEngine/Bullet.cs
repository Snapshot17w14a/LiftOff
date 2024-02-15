using GXPEngine.Core;

namespace GXPEngine
{
    internal class Bullet : Sprite
    {
        private Vector2 _velocity;
        public Bullet(Vector2 velocity) : base("circle.png")
        {
            SetOrigin(width / 2, height / 2);
            scale = 0.2f;
            game.AddChild(this);
            collider.isTrigger = true;
            _velocity = velocity;
        }

        private void Update()
        {
            Move();
            CheckCollision();
        }

        private void Move()
        {
            x += _velocity.x;
            y += _velocity.y;
            if (x < 0 || x > game.width || y < 0 || y > game.height) LateDestroy();
        }

        private void CheckCollision()
        {
            GameObject[] collision = GetCollisions(true, false);
            foreach (GameObject gameObject in collision)
            {
                if (gameObject is Enemy enemy)
                {
                    enemy.OnHit();
                    LateDestroy();
                }
            }
        }
    }
}
