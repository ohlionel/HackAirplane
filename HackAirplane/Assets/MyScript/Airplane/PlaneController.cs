using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.MyScrpt.Airplane
{
    public class PlaneController : MonoBehaviour
    {
        public float heath;
        public float speed;
        public float maxHeight;
        public float minHeight;
        public float radius;
        public Vector3 origin;

        private Vector3 startPosition;
        private Transform _transform;
        private Transform _targetTransform;
        private float _wrongTolerance = 1.0f;
        private bool _haveArrived = false;
        Vector3 _targetPosition;

        private void Awake()
        {
            origin = new Vector3(250, 0, 250);
            _transform = GetComponent<Transform>();
            _targetTransform = GameObject.Find("Player").transform;
        }

        private void Start()
        {
            // init start position
            startPosition.x = Random.Range(-radius, radius);
            startPosition.z = Mathf.Sqrt(radius*radius - startPosition.x*startPosition.x);
            if (Random.Range(0, 2) == 1)
                startPosition.z = -startPosition.z;
            startPosition.y = Random.Range(minHeight, maxHeight);
            startPosition += origin;
            _transform.position = startPosition;

            // target
            Debug.Log("target: " + _targetTransform.position);
            _targetPosition = _targetTransform.position;
            _targetPosition.y += 50f;
        }

        public void Update()
        {
            transform.LookAt(_targetPosition);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

            if (!_haveArrived && Mathf.Abs(transform.position.x - _targetPosition.x) < _wrongTolerance 
                && Mathf.Abs(transform.position.z - _targetPosition.z) < _wrongTolerance)
            {
                _haveArrived = true;
                _targetPosition = -startPosition;
                _targetPosition.y = -_targetPosition.y;
            }
        }

        public void FixedUpdate()
        {
            // m_transform.position += speed*Vector3.forward;
        }

        public void OnColiderBullet()
        {
        }

        public void OnColiderPlayer()
        {
        }

        private void PlaneExplode()
        {
        }

        private void PlaneFire()
        {
        }

    }
}