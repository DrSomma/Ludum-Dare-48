using System;
using System.Collections;

namespace Tween_Library.Scripts
{
    public interface ITweenEffect
    {
        event Action<ITweenEffect> OnComplete;

        IEnumerator Execute();
        void ExecuteReset();
    }
}
