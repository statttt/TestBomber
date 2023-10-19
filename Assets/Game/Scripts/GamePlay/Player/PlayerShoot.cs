using Game.Scripts.Core;
using Game.Scripts.GamePlay.Setup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.GamePlay
{
    public class PlayerShoot : MonoBehaviour
    {
        [SerializeField]
        private Projectile _projectilePrefab;
        [SerializeField]
        private GameObject _cursor;
        [SerializeField]
        private Transform _shootPoint;
        [SerializeField]
        private LayerMask _layer;
        [SerializeField]
        private LineRenderer _lineVisual;
        [SerializeField]
        private int _lineSegment = 10;
        [SerializeField]
        private float _flightTime = 1f;
        [SerializeField]
        private int _startCountProjectilesForCreate = 20;

        private Camera _cam;

        private bool _isAiming = false;
        private List<Projectile> _projectilesList = new List<Projectile>();
        private GameControl _gameControl;

        public bool IsAminig => _isAiming;

        public void Initialize(GameControl gameControl)
        {
            _gameControl = gameControl;
        }

        private void Start()
        {
            _cam = Camera.main;
            _lineVisual.positionCount = _lineSegment + 1;
            Deactivate();
            CreateProjectiles();
        }

        private void Update()
        {
            if (!_gameControl.IsHaveBomb())
            {
                return;
            }
            if (Input.GetMouseButtonDown(0))
            {
                Activate();
            }
            if (_isAiming)
            {
                LaunchProjectile();
            }
            if (Input.GetMouseButtonUp(0))
            {
                Deactivate();
            }
        }

        public void Activate()
        {
            _isAiming = true;
            _cursor.SetActive(true);
            _lineVisual.gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            _isAiming = false;
            _cursor.SetActive(false);
            _lineVisual.gameObject.SetActive(false);
        }

        public void LaunchProjectile()
        {
            Ray camRay = _cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(camRay, out hit, float.MaxValue, _layer))
            {
                _cursor.transform.position = hit.point + Vector3.up * 0.1f;

                Vector3 velocity = CalculateVelocty(hit.point, _shootPoint.position, _flightTime);

                Visualize(velocity, _cursor.transform.position);

                transform.LookAt(_cursor.transform.position);

                if (Input.GetMouseButtonUp(0))
                {
                    Projectile projectile = GetProjectile();
                    projectile.SetVelocity(velocity, hit.point);
                    _gameControl.RemoveBomb();
                }
            }
        }

        void Visualize(Vector3 velocity, Vector3 finalPos)
        {
            for (int i = 0; i < _lineSegment; i++)
            {
                Vector3 pos = CalculatePosInTime(velocity, (i / (float)_lineSegment) * _flightTime);
                _lineVisual.SetPosition(i, pos);
            }

            _lineVisual.SetPosition(_lineSegment, finalPos);
        }

        Vector3 CalculateVelocty(Vector3 target, Vector3 origin, float time)
        {
            Vector3 distance = target - origin;
            Vector3 distanceXz = distance;
            distanceXz.y = 0f;

            float distanceY = distance.y;
            float distanceXZ = distanceXz.magnitude;

            float velocityXZ = distanceXZ / time;
            float velocityY = (distanceY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);

            Vector3 result = distanceXz.normalized;
            result *= velocityXZ;
            result.y = velocityY;

            return result;
        }

        Vector3 CalculatePosInTime(Vector3 velocity, float time)
        {
            Vector3 velocityXZ = velocity;
            velocityXZ.y = 0f;

            Vector3 result = _shootPoint.position + velocity * time;
            float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (velocity.y * time) + _shootPoint.position.y;

            result.y = sY;

            return result;
        }
        public void CreateProjectiles()
        {
            for (int i = 0; i < _startCountProjectilesForCreate; i++)
            {
                Projectile projectile = Instantiate(_projectilePrefab, _shootPoint.position, Quaternion.identity);
                projectile.Initialize(this.transform);
                _projectilesList.Add(projectile);
            }
        }

        public Projectile GetProjectile()
        {
            Projectile newProjectile = null;
            while(newProjectile == null)
            {
                foreach (Projectile projectile in _projectilesList)
                {
                    if (!projectile.gameObject.activeInHierarchy)
                    {
                        newProjectile = projectile;
                        newProjectile.transform.localPosition = _shootPoint.position;
                        newProjectile.Activate(null, _gameControl.GetActiveBombInfo());
                        return newProjectile;
                    }
                }
                if(newProjectile == null)
                {
                    CreateProjectiles();
                }
            }
            return null;
        }
    }

}

