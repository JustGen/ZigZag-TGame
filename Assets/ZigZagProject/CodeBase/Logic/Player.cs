using System;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Events;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.States;
using UnityEngine;

namespace CodeBase.Logic
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private LevelGenerator _levelGenerator;
        [SerializeField] private float _speed = 1f;

        private Vector3 _direction;
        private IGameStateInfo _gameStateInfo;
        private IPersistentProgressService _progress;
        
        private int _currentScore;
        public int CurrentScore
        {
            get => _currentScore;
            set
            {
                _currentScore = value;
                _gameStateInfo.CurrentScore = _currentScore;
            }
        }

        private void Awake()
        {
            _gameStateInfo = AllServices.Container.Single<IGameStateInfo>();
            _progress = AllServices.Container.Single<IPersistentProgressService>();
            _direction = Vector3.right;
            
            CurrentScore = 0;
        }

        private void Update()
        {
            if (!_gameStateInfo.IsActive)
                return;

            if (CanMoveByCheatMode())
            {
                CheatModeMove();
                return;
            }

            Move();
            CheckPositionPlayer();
            
            if (_gameStateInfo.IsMobileDevice)
            {
                if (Input.touchCount != 1)
                    return;

                Touch touch = Input.GetTouch(0);

                if (touch.phase != TouchPhase.Began)
                    return;

                _direction = SwitchDirection();
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                    _direction = SwitchDirection();
            }
        }

        private void CheckPositionPlayer()
        {
            if (!(this.gameObject.transform.position.y <= -0.5f) || !_gameStateInfo.IsActive) return;
            
            _gameStateInfo.IsActive = false;
            EventsProvider.InvokeEvent(Events.OnLose);
            _progress.GamesPlayed += 1;
        }

        private bool CanMoveByCheatMode() =>
            _gameStateInfo.CheatMode;

        private void CheatModeMove()
        {
            Move();
            CheckPositionPlayer();
            
            GameObject item = _levelGenerator.ListActivePointForGodMode[0];

            if (!PositionValidatorX(item) || !PositionValidatorZ(item)) return;

            Vector3 nextDirection = _levelGenerator.ListActivePointForGodMode[1].transform.position -
                                    item.transform.position;

            if (Vector3.Dot(_direction, nextDirection) == 0)
                _direction = SwitchDirection();

            _levelGenerator.ListActivePointForGodMode.RemoveAt(0);
        }

        private bool PositionValidatorX(GameObject item) =>
            Math.Abs(transform.position.x - item.transform.position.x) < Constants.EPS
            || transform.position.x >= item.transform.position.x;

        private bool PositionValidatorZ(GameObject item) =>
            Math.Abs(transform.position.z - item.transform.position.z) < Constants.EPS
            || transform.position.z >= item.transform.position.z;

        private Vector3 SwitchDirection()
        {
            CurrentScore += 1;
            CheckBestScore();
            EventsProvider.InvokeEvent(Events.OnTap);
            return _direction == Vector3.right
                ? Vector3.forward
                : Vector3.right;
        }

        private void CheckBestScore()
        {
            if (CurrentScore > _progress.BestScore)
                _progress.BestScore = CurrentScore;
        }

        private void Move() =>
            transform.localPosition += _direction * (_speed * Time.deltaTime);
    }
}