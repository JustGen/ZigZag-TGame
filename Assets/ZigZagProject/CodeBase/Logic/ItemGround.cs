using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Logic
{
    public class ItemGround : MonoBehaviour
    {
        [SerializeField] private Stone _stonePrefab;
        
        public event Action<ItemGround> OnHidden;
        public GameObject GetGameObject => gameObject;

        [SerializeField] private Rigidbody _rigidbody;

        private void OnEnable()
        {
            StoneVisibleOff();
            
            if (Random.Range(0f,1f) < Constants.PERCENT_ENABLE_STONE)
                StoneVisibleOn();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.BACK_TRIGGER_OFF))
                StartCoroutine(TurnOff());
        }

        private void StoneVisibleOn() => 
            _stonePrefab.gameObject.SetActive(true);

        private void StoneVisibleOff() => 
            _stonePrefab.gameObject.SetActive(false);

        private IEnumerator TurnOff()
        {
            _rigidbody.isKinematic = false;
            StoneVisibleOff();
            yield return new WaitForSeconds(Constants.TIME_TURN_OFF_OBJECTS);
            OnHidden?.Invoke(this);
        }
    }
}