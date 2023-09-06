#nullable enable

using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay
{
    public interface IShootTarget
    {
        void Inject(PlayerScoreController playerScoreController);
        void OnShot();
    }

    public class ShootTarget : MonoBehaviour, IShootTarget
    {
        [SerializeField] SkinnedMeshRenderer skinnedMesh = default!;
        [SerializeField] MeshCollider meshCollider = default!;

        private PlayerScoreController playerScoreController = default!;

        private void OnValidate()
        {
            if (skinnedMesh == null || meshCollider == null)
            {
                return;
            }

            Mesh colliderMesh = new Mesh();
            skinnedMesh.BakeMesh(colliderMesh, true);
            meshCollider.sharedMesh = colliderMesh;
        }

        public void Inject(PlayerScoreController playerScoreController)
        {
            this.playerScoreController = playerScoreController ?? throw new System.ArgumentNullException(nameof(playerScoreController));
        }

        public void OnShot()
        {
            // TODO: Add HP? Currently assume 1 hit kill
            // TODO: Make this not rely on knowing to disable parent
            transform.parent.gameObject.SetActive(false);
            _ = AutoRevive(Random.Range(5000, 10000));
            playerScoreController.AddScore(1);
        }

        private async Task AutoRevive(int delayInMs)
        {
            await Task.Delay(delayInMs);
            transform.parent.gameObject.SetActive(true);
        }
    }
}