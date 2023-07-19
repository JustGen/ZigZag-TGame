using System.Collections.Generic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.ObjectPool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Logic
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject _currentPointSpawn;
        [SerializeField] private int _maxScreenCount;
        [SerializeField] private int _maxCountZ = -4;
        [SerializeField] private int _maxCountX = 4;
        [SerializeField] private GameObject _pointGM0;
        [SerializeField] private GameObject _pointGM1;
        [SerializeField] private GameObject _pointGM2;

        private int _currentCountXZ = 1;
        private IObjectPool _objectPool;

        public List<GameObject> _listActivePointForGodMode = new List<GameObject>();
        public List<GameObject> ListActivePointForGodMode => _listActivePointForGodMode;

        private void Awake()
        {
            _objectPool = AllServices.Container.Single<IObjectPool>();
            _objectPool.OnReturnToPool += CreateItem;
        }

        private void OnDestroy() => 
            _objectPool.OnReturnToPool -= CreateItem;

        private void Start()
        {
            _listActivePointForGodMode.Add(_pointGM1);
            _listActivePointForGodMode.Add(_pointGM2);
            
            _objectPool.CreateFreeObject(_maxScreenCount + 10);
            GenerateStartGround();
        }

        private void GenerateStartGround()
        {
            for (int i = 0; i < _maxScreenCount; i++) 
                CreateItem();
        }

        private void CreateItem()
        {
            int direction = Random.Range(0, 2);

            Vector3 pointSpawn = direction switch
            {
                0 => _currentCountXZ > _maxCountZ ? AddLeft() : AddRight(),
                1 => _currentCountXZ < _maxCountX ? AddRight() : AddLeft(),
                _ => Vector3.zero
            };

            GameObject item = _objectPool.GetItem().gameObject;
            ResetItem(item.transform, pointSpawn);
            _listActivePointForGodMode.Add(item);
        }


        private void ResetItem(Transform item, Vector3 pointSpawn)
        {
            item.position = pointSpawn;
            item.parent = this.transform;
            item.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            _currentPointSpawn.transform.position = item.position;
        }

        private Vector3 AddRight()
        {
            _currentCountXZ++;
            return _currentPointSpawn.transform.position + Vector3.right;
        }

        private Vector3 AddLeft()
        {
            _currentCountXZ--;
            return _currentPointSpawn.transform.position + Vector3.forward;
        }
    }
}