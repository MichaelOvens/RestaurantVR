using UnityEngine;

namespace Player
{
    public class PlayerIdleXR : MonoBehaviour
    {
        [SerializeField] private PlayerInstance _player;
        [SerializeField] private float _timeOut;
        [SerializeField] private bool _isIdle = false;
        
        private float _elapsed = 0f;

        private void Update()
        {
            bool notMoving = Mathf.Approximately(_player.Head.localPosition.magnitude, 0f);
            bool notRotating = Mathf.Approximately(_player.Head.localEulerAngles.magnitude, 0f);

            if (notMoving && notRotating)
            {
                _elapsed += Time.deltaTime;

                if (_elapsed > _timeOut && !_isIdle) 
                {
                    _isIdle = true;
                    _player.CallOnPlayerIdleStart();
                }
            }
            else
            {
                _elapsed = 0f;

                if (_isIdle)
                {
                    _isIdle = false;
                    _player.CallOnPlayerIdleStop();
                }
            }
        }
    }
}
