#nullable enable

using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "Gameplay/Gameplay Config")]
    public class GameplayConfigScriptable : ScriptableObject
    {
        [SerializeField] private GameplayConfig config = default!;
        public GameplayConfig Config => config;
    }
}
