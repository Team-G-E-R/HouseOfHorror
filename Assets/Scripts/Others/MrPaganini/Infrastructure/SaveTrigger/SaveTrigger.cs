using UnityEngine;

namespace MrPaganini.Infrastructure.SaveTrigger
{
    public class SaveTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider _collider;
        private ISaveLoadService _saveLoadService;

        private void Awake()
        {
            _saveLoadService = AllServices.Singleton.Single<ISaveLoadService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();
            Debug.Log("Progress saved.");
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if (_collider == false)
                return;

            Gizmos.color = new Color32(30, 200, 20, 130);
            Gizmos.DrawCube(transform.position + _collider.center, _collider.size);

        }
    }
}
