using System;
using GXPEngine.Core;

namespace GXPEngine
{
    internal class Player : AnimationSprite
    {
        private readonly int[] _inputKeys = DataStorage.InputKeys;
        private float[] _angles = new float[3];
        private bool _isPunching = false;
        private bool _isGameOver = false;

        public Player(string filename, int cols, int rows) : base(filename, cols, rows, -1, false, false) 
        { 
            SetOrigin(width / 2, height / 2);
            CalculateAngles(); 
            SetCycle(9, 9); 
            rotation = 0;
            scale = 0.5f;
        }

        private void Update()
        {
            Animate(DataStorage.Instance.AnimationSpeed / 3f);
            CheckInput();
            if (_isPunching && currentFrame == 6) { _isPunching = false; SetCycle(9, 9); }
        }

        private void CheckInput() { foreach (int key in _inputKeys) if (!_isGameOver && Input.GetKey(key)) SetPlayerDirection(key); }

        private void CalculateAngles()
        {
            _angles[0] = Vector2.CalculateAngle(new Vector2(game.width / 2, game.height / 2), DataStorage.SpawnVectors[0]);
            _angles[1] = Vector2.CalculateAngle(new Vector2(game.width / 2, game.height / 2), DataStorage.SpawnVectors[4]);
            _angles[2] = Vector2.CalculateAngle(new Vector2(game.width / 2, game.height / 2), DataStorage.SpawnVectors[5]);
        }

        public void GameOver()
        {
            _isGameOver = true;
            rotation = 0;
            SetCycle(7, 1);
        }

        private void SetPlayerDirection(int key)
        {
            _isPunching = true;
            for (int i = 0; i < _inputKeys.Length; i++) if (key == _inputKeys[i]) 
            { 
                rotation = _angles[i >= 3 ? i - 3 : i];
                if(i >= 1 && i <= 3) Mirror(true, false);
                else Mirror(false, false);
                break;
            }
            SetCycle(0, 7);
            currentFrame = 0;
        }
    }
}
