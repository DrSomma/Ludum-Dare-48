using System;
using UnityEngine;
using System.Collections;

namespace Tween_Library.Scripts.Effects
{
    public class ScaleEffect : ITweenEffect
    {
        private Transform _transform;
        private Vector3 _maxSize { get; }
        private float _scaleSpeed { get; }
        private YieldInstruction _wait { get; }
        public event Action<ITweenEffect> OnComplete;

        public ScaleEffect(Transform transform, Vector3 maxScaleSize, float scaleSpeed, YieldInstruction wait )
        {
            _transform = transform;
            _maxSize = maxScaleSize;
            _scaleSpeed = scaleSpeed;
            _wait = wait;
        }

        public IEnumerator Execute()
        {
            float time = 0f;
            Vector3 currentScale = _transform.localScale;

            while(_transform.localScale != _maxSize)
            {
                time += Time.deltaTime * _scaleSpeed;
                Vector3 scale = Vector3.Lerp(currentScale, _maxSize, time);
                _transform.localScale = scale;
                yield return null;
            }
            yield return _wait;

            time = 0f;
            currentScale = _transform.localScale;
            while (_transform.localScale != Vector3.one)
            {
                time += Time.deltaTime * _scaleSpeed;
                Vector3 scale = Vector3.Lerp(currentScale, Vector3.one, time);
                _transform.localScale = scale;
                yield return null;
            }

        }

        public void ExecuteReset()
        {
            _transform.localScale = Vector3.one;
        }
    }
}
