using GXPEngine.Core;

namespace GXPEngine
{
    internal class Player : Sprite
    {
        private readonly int[] _inputKeys = DataStorage.InputKeys;
        private float[] _angles = new float[6];
        private bool _isPunching = false;

        public Player(string filename, int cols, int rows) : base(filename) { CalculateAngles(); }

        private void Update()
        {
            CheckInput();
            if(_isPunching) { _isPunching = false; }
        }

        private void CheckInput() { foreach (int key in _inputKeys) if (Input.GetKey(key)) SetPlayerDirection(key); }

        private void CalculateAngles()
        {
            for (int i = 0; i < _angles.Length; i++)
            {
                _angles[i] = Vector2.CalculateAngle(DataStorage.SpawnVectors[i], new Vector2(game.width / 2, game.height / 2), new Vector2(game.width / 2, 1));
            }
        }

        private void SetPlayerDirection(int key)
        {
            //_isPunching = true;
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
            SetAnimCycle(key);
        }

        private void SetAnimCycle(int key)
        {

        }
    }
}
