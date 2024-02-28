using GXPEngine.Core;

namespace GXPEngine
{
    internal class Player : Sprite
    {
        private readonly int[] _inputKeys = DataStorage.InputKeys;
        private float[] _angles = new float[6];

        public Player(string filename) : base(filename) { CalculateAngles(); }

        private void Update() => CheckInput();

        private void CheckInput() { foreach (int key in _inputKeys) if (Input.GetKey(key)) SetPlayerDirection(key); }

        private void CalculateAngles()
        {
            for (int i = 0; i < _angles.Length; i++)
            {
                _angles[i] = Vector2.CalculateAngle(DataStorage.TargetVectors[i], new Vector2(game.width / 2, game.height / 2), new Vector2(game.width / 2, game.height));
            }
        }

        public void SetPlayerDirection(int key)
        {
            float rot = 0;
            for (int i = 0; i < _inputKeys.Length; i++)
            {
                if (key == _inputKeys[i])
                {
                    if(i == 0 || i >= 4) rot = -_angles[i];
                    else rot = _angles[i];
                    break;
                }
            }
            rotation = rot;
        }
    }
}
