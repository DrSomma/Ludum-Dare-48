using System;
using System.Collections;
using UnityEngine;

namespace Tween_Library.Scripts.Effects
{
    public class ShakeEffect : ITweenEffect
    {
        public event Action<ITweenEffect> OnComplete;
        private Transform _transform { get; }
        private float _wiggleSpeed { get; }
        private float _maxRotation { get; }


        public ShakeEffect(Transform rectTransform, float maxRotation, float speed, Action<ITweenEffect> onComplete = null)
        {
            _transform = rectTransform;
            _wiggleSpeed = speed;
            _maxRotation = maxRotation;
            OnComplete += onComplete;
        }

        public IEnumerator Execute()
        {
            Quaternion rotateTo = new Quaternion
            {
                eulerAngles = new Vector3(0, 0, _maxRotation)
            };


            float currentRotation = _transform.rotation.z;
            float nextRotation = _maxRotation * -1f;

            float time = 0f;

            while (Mathf.Abs(nextRotation) > 0.15f)
            {
                time += Time.deltaTime * _wiggleSpeed;
                float newRotation = Mathf.Lerp(currentRotation, nextRotation, time);
                rotateTo.eulerAngles = new Vector3(0, 0, newRotation);
                _transform.rotation = rotateTo;
                if (time >= 1)
                {
                    currentRotation = nextRotation;
                    nextRotation = (nextRotation * 0.9f) * -1;
                    time = 0;
                }

                yield return null;
            }

            rotateTo.eulerAngles = new Vector3(0, 0, 0);
            _transform.rotation = rotateTo;

            OnComplete?.Invoke(this);
        }

        public void ExecuteReset()
        {
            Quaternion rotateTo = new Quaternion
            {
                eulerAngles = new Vector3(0, 0, 0)
            };
            _transform.rotation = rotateTo;
        }
    }
}