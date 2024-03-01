using GXPEngine.ParticleSystem;
using GXPEngine.Core;
using System;

namespace GXPEngine.LevelManager
{
    internal class NoteObject : AnimationSprite
    {
        private Level _parentLevel;
        private Vector2 _spawnLocation;
        private Vector2 _tapLocation;
        private readonly int _index;

        public double TimeInstantiated { get; private set; }
        public double AssignedTime { get; private set; }
        public bool PlayParticle { get; set; } = true;
        public bool IsCorrect { get; private set; }

        public NoteObject(double assignedTime, int laneIndex, Level level) : base("goose.png", 3, 3, -1, false, false)
        {            
            SetOrigin(width / 2, height / 2);
            SetCycle(0, 9);
            scale = 0.8f;
            rotation += 165;
            _parentLevel = level;
            TimeInstantiated = level.GetAudioSourceTime();
            AssignedTime = assignedTime;
            _index = laneIndex;
            _spawnLocation = DataStorage.SpawnVectors[laneIndex] - new Vector2(game.width / 2, game.height / 2);
            _tapLocation = DataStorage.TapVectors[laneIndex] - new Vector2(game.width / 2, game.height / 2);
            SetXY(_spawnLocation.x, _spawnLocation.y);
            CalculateRotation();
            level.AddChild(this);
        }


        private void Update()
        {
            Animate(DataStorage.Instance.AnimationSpeed);
            IsCorrect = CheckIfCorrect();
            color = IsCorrect ? _parentLevel.CurrentRedTint - 0x005555 : _parentLevel.CurrentRedTint;
            double timeSinceInstantiated = _parentLevel.GetAudioSourceTime() - TimeInstantiated;
            float t = (float)(timeSinceInstantiated / _parentLevel.NoteTime);
            if (t >= 1.1f) { LateDestroy(); _parentLevel.LevelScoreManager.Miss(); }
            else Move(t);
        }

        private void CalculateRotation() => rotation += -Vector2.CalculateAngle(_spawnLocation, _tapLocation, false);

        private void Move(float t)
        {
            Vector2 position = Vector2.Lerp(_spawnLocation, _tapLocation, t);
            x = position.x;
            y = position.y;
        }

        private bool CheckIfCorrect() => Math.Abs(_parentLevel.GetAudioSourceTime() - AssignedTime) <= _parentLevel.MarginOfError;

        protected override void OnDestroy()
        {
            if(_parentLevel != null) { _parentLevel.NoteCount--; _parentLevel.LevelLanes[_index].RemoveNoteFromList(this); _parentLevel.CheckForGameOver(); _parentLevel = null; }
            if (PlayParticle) new Particle("explosion.png", 3, 3) { x = x + game.width / 2, y = y + game.height / 2 };
        }
    }
}
