using UnityEngine;

namespace CodeBase.Logic
{
    public class Stone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.PLAYER)) 
                this.gameObject.SetActive(false);
        }
    }
}