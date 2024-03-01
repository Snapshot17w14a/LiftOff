using GXPEngine.ParticleSystem;
using GXPEngine.Core;
using System;

namespace GXPEngine.LevelManager
{
    internal class NoteObject : Sprite
    {
        private Level _parentLevel;
        private Vector2 _spawnLocation;
        private Vector2 _tapLocation;
        private readonly int _index;

        public double TimeInstantiated { get; private set; }
        public double AssignedTime { get; private set; }
        public bool PlayParticle { get; set; } = true;
        public bool IsCorrect { get; private set; }

        public NoteObject(double assignedTime, int laneIndex, Level level) : base("circle.png", false, false)
        {            
            SetOrigin(width / 2, height / 2);
            _parentLevel = level;
            TimeInstantiated = level.GetAudioSourceTime();
            AssignedTime = assignedTime;
            _index = laneIndex;
            _spawnLocation = DataStorage.TargetVectors[laneIndex] - new Vector2(game.width / 2, game.height / 2);
            _tapLocation = DataStorage.TapVectors[laneIndex] - new Vector2(game.width / 2, game.height / 2);
            level.AddChild(this);
        }

        private void Update()
        {
            IsCorrect = CheckIfCorrect();
            color = IsCorrect ? _parentLevel.CurrentRedTint - 0x00AAAA : _parentLevel.CurrentRedTint;
            double timeSinceInstantiated = _parentLevel.GetAudioSourceTime() - TimeInstantiated;
            float t = (float)(timeSinceInstantiated / _parentLevel.NoteTime);
            if (t > 1.1f) { LateDestroy(); _parentLevel.LevelScoreManager.Miss(); }
            else
            {
                Vector2 position = Vector2.Lerp(_spawnLocation, _tapLocation, t);
                x = position.x;
                y = position.y;
            }
        }

        private bool CheckIfCorrect() => Math.Abs(_parentLevel.GetAudioSourceTime() - AssignedTime) <= _parentLevel.MarginOfError;

        protected override void OnDestroy()
        {
            _parentLevel.NoteCount--;
            _parentLevel?.LevelLanes[_index].RemoveNoteFromList(this);
            _parentLevel = null;
            if(PlayParticle) new Particle("explosion.png", 5, 5) { x = x + game.width / 2, y = y + game.height / 2 };
        }
    }
}
