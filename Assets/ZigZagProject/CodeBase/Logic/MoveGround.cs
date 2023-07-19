using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.States;
using UnityEngine;

namespace CodeBase.Logic
{
    public class MoveGround : MonoBehaviour
    {
        [SerializeField] private float _speed = 0.9f;
        
        private Vector3 _direction;
        public bool IsActive { get; set; }

        private IGameStateInfo _gameStateInfo;

        private void Awake()
        {
            _gameStateInfo = AllServices.Container.Single<IGameStateInfo>();
        }

        private void Start() => 
            _direction = (Vector3.right + Vector3.forward) * -1;

        private void FixedUpdate()
        {
            if (!_gameStateInfo.IsActive)
                return;

            Move();
        }

        private void Move() => 
            transform.position += _direction * (_speed * Time.fixedDeltaTime);
    }
}