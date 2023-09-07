#nullable enable

using System;
using UnityEngine;

namespace Shared
{
    public class UnityLifeCycle : MonoBehaviour
    {
        public event Action? WhenUpdate;

        private void Update()
        {
            WhenUpdate?.Invoke();
        }
    }
}