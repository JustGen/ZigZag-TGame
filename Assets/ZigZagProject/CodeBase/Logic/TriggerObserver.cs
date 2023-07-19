using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Events;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Logic
{
    public class TriggerObserver : MonoBehaviour
    {
        [SerializeField] private Player _player;
    
        private IPersistentProgressService _progress;
    
        private void Awake()
        {
            _progress = AllServices.Container.Single<IPersistentProgressService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.STONE))
            {
                EventsProvider.InvokeEvent(Events.OnTakeStone);
                _player.CurrentScore += 2;
                CheckBestScore();
            }
        }
    
        private void CheckBestScore()
        {
            if (_player.CurrentScore > _progress.BestScore)
                _progress.BestScore = _player.CurrentScore;
        }
    }
}