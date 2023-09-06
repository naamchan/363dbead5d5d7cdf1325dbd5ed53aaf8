#nullable enable

using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "Gameplay/Player Character Controller Config")]
    public class PlayerCharacterControllerConfigScriptable : ScriptableObject
    {
        [SerializeField] private PlayerCharacterControllerConfig config = default!;
        public PlayerCharacterControllerConfig Config => config;
    }
}