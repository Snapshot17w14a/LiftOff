namespace GXPEngine.ParticleSystem
{
    internal class Particle : AnimationSprite
    {
        public Particle(string filename, int cols, int rows) : base(filename, cols, rows, -1, false, false) { SetOrigin(width / 2, height / 2); game.AddChild(this); }

        private void Update()
        {
            Animate(DataStorage.Instance.AnimationSpeed);
            if (currentFrame == frameCount - 1) LateDestroy();
        }
    }
}
