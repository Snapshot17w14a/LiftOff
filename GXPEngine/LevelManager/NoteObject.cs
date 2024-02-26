using System;
using System.Collections.Generic;
using GXPEngine.Core;
using GXPEngine.ParticleSystem;
using GXPEngine.UI;

namespace GXPEngine.LevelManager
{
    internal class NoteObject : Sprite
    {
        private readonly double _timeInstantiated;
        private readonly double _assignedTime;
        private readonly Level _level;
        private Vector2 _spawnLocation;
        private Vector2 _tapLocation;
        private int _index;

        public double TimeInstantiated => _timeInstantiated;
        public double AssignedTime => _assignedTime;
        public int LaneIndex => _index;
        public NoteObject(double assignedTime, int laneIndex) : base("circle.png", false, false)
        {
            SetOrigin(width / 2, height / 2);
            _level = SceneManager.Instance.CurrentScene.SceneLevel;
            _timeInstantiated = Level.GetAudioSourceTime();
            _assignedTime = assignedTime;
            _index = laneIndex;
            _spawnLocation = DataStorage.TargetVectors[laneIndex];
            _tapLocation = DataStorage.TapVectors[laneIndex];
            game.AddChild(this);
        }

        private void Update()
        {
            double timeSinceInstantiated = Level.GetAudioSourceTime() - _timeInstantiated;
            float t = (float)(timeSinceInstantiated / _level.NoteTime);
            if (t > 1.2f) LateDestroy();
            else
            {
                Vector2 position = Vector2.Lerp(_spawnLocation, _tapLocation, t);
                x = position.x;
                y = position.y;
            }
        }

        protected override void OnDestroy()
        {
            _level.LevelLanes[_index].RemoveNoteFromList(this);
            new Particle("explosion.png", 5, 5) { x = x, y = y };
        }
    }
}
