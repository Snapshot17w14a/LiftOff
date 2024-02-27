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
        private readonly Level _parentLevel;
        private Vector2 _spawnLocation;
        private Vector2 _tapLocation;
        private int _index;

        public double TimeInstantiated => _timeInstantiated;
        public double AssignedTime => _assignedTime;
        public int LaneIndex => _index;
        public NoteObject(double assignedTime, int laneIndex, Scene parentScene, Level level) : base("circle.png", false, false)
        {            
            SetOrigin(width / 2, height / 2);
            _parentLevel = level;
            _timeInstantiated = level.GetAudioSourceTime();
            _assignedTime = assignedTime;
            _index = laneIndex;
            _spawnLocation = DataStorage.TargetVectors[laneIndex];
            _tapLocation = DataStorage.TapVectors[laneIndex];
            parentScene.AddChild(this);
        }

        private void Update()
        {
            double timeSinceInstantiated = _parentLevel.GetAudioSourceTime() - _timeInstantiated;
            float t = (float)(timeSinceInstantiated / _parentLevel.NoteTime);
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
            _parentLevel.LevelLanes[_index].RemoveNoteFromList(this);
            new Particle("explosion.png", 5, 5) { x = x, y = y };
        }
    }
}
