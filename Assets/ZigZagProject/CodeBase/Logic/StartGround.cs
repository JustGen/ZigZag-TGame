using System.Collections;
using UnityEngine;

namespace CodeBase.Logic
{
    public class StartGround : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.BACK_TRIGGER_OFF))
                StartCoroutine(TurnOff());
        }
        
        private IEnumerator TurnOff()
        {
            _rigidbody.isKinematic = false;
            yield return new WaitForSeconds(Constants.TIME_TURN_OFF_OBJECTS);
            _rigidbody.isKinematic = true;
        }
    }
}