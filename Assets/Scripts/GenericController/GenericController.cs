using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WI
{
    [RequireComponent(typeof(Camera))]
    public abstract class GenericController : MonoBehaviour
    {
        public new Camera camera;
        public OrbitalControllerOption option;
        public MaxRangeLimitter maxRangeLimitter = new MaxRangeLimitter();
        protected Vector3 moveVector;
        protected Vector3 cameraPosition;
        public Vector3 nextPosition;
        
        public void Awake()
        {
            camera = GetComponent<Camera>();
            option.Apply(this);
            Collider maxRange = transform.parent.Find("MaxRange").GetComponent<BoxCollider>();
            maxRangeLimitter.SetRange(maxRange);
        }

        public virtual void Movement()
        {
            Move();
            Zoom();
            Rotate();
        }
        protected abstract void Move();
        protected abstract void Zoom();
        protected abstract void Rotate();
        public abstract void LastPositioning(bool limit);
        public abstract void Rewind();

        protected UserInput input;

        
        protected virtual void LateUpdate()
        {
            input.GetInput();
            Movement();
            var limitCheck = maxRangeLimitter.MoveRangeLimit(nextPosition);
            LastPositioning(limitCheck);
        }
    }
}