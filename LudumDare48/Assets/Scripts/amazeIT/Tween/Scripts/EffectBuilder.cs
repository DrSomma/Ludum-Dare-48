using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tween_Library.Scripts
{
    public class EffectBuilder
    {
        private MonoBehaviour Owner { get; }
        private readonly List<ITweenEffect> _effects = new List<ITweenEffect>();

        private int _completedEffects = 0;

        public event Action OnAllEffectsComplete;

        public EffectBuilder(MonoBehaviour owner)
        {
            Owner = owner;
        }

        public EffectBuilder AddEffect(ITweenEffect effect)
        {
            _effects.Add(effect);
            effect.OnComplete += OnEffectComplete;
            return this;
        }

        public void ExecuteEffects()
        {
            Owner.StopAllCoroutines();
            foreach (ITweenEffect effect in _effects)
            {
                Owner.StartCoroutine(effect.Execute());
            }
        }

        private void OnEffectComplete(ITweenEffect effect)
        {
            _completedEffects += 1;
            if (_completedEffects < _effects.Count)
                return;
            AllEffectsComplete();
        }

        private void AllEffectsComplete()
        {
            _completedEffects = 0;
            OnAllEffectsComplete?.Invoke();
        }

        public void StopAllEffects()
        {
            Owner?.StopAllCoroutines();
            foreach (ITweenEffect effect in _effects)
            {
                effect.ExecuteReset();
            }
            AllEffectsComplete();
            //RESET?
        }
    }
}