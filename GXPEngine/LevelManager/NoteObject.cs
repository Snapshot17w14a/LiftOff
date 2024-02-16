using GXPEngine.Core;

namespace GXPEngine.LevelManager
{
    internal class NoteObject : Sprite
    {
        private readonly double _timeInstantiated;
        private readonly float _assignedTime;
        private Vector2 _spawnLocation;
        private Vector2 _tapLocation;
        private int _index;

        public float AssignedTime => _assignedTime;
        public NoteObject(float assignedTime, int laneIndex) : base("circle.png", false, false)
        {
            SetOrigin(width / 2, height / 2);
            _timeInstantiated = Level.GetAudioSourceTime();
            _assignedTime = assignedTime;
            _index = laneIndex;
            _spawnLocation = DataStorage.Instance.TargetVectors[laneIndex];
            _tapLocation = DataStorage.Instance.TapVectors[laneIndex];
            game.AddChild(this);
        }

        private void Update()
        {
            double timeSinceInstantiated = Level.GetAudioSourceTime() - _timeInstantiated;
            float t = (float)(timeSinceInstantiated / Level.NoteTime);
            if (t > 1.2f)
            {
                Level.LevelLanes[_index].RemoveNoteFromList(this);
                LateDestroy();
            }
            else
            {
                Vector2 position = Vector2.Lerp(_spawnLocation, _tapLocation, t);
                x = position.x;
                y = position.y;
            }
        }
    }
}
