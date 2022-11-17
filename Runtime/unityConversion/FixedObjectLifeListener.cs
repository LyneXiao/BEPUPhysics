using BEPUphysics;
using UnityEngine;
using Space = BEPUphysics.Space;

namespace UnityConversion
{
    [RequireComponent(typeof(Collider))]
    public class FixedObjectLifeListener: MonoBehaviour
    {
        internal delegate ISpaceObject SingleSpaceObjectCreator(Space space, Collider collider);

        internal delegate ISpaceObject[] MultiSpaceObjectCreator(Space space, Collider collider);
        
        private Collider selfCollider;

        protected Space _space;
        private ISpaceObject[] _spaceObjects;

        private SingleSpaceObjectCreator singleCreator;
        private MultiSpaceObjectCreator multiCreator;

        internal void Build(Space space, SingleSpaceObjectCreator creator)
        {
            _space = space;
            singleCreator = creator;
            if (enabled)
                CreateSpaceObjects();
        }
        
        internal void Build(Space space, MultiSpaceObjectCreator creator)
        {
            _space = space;
            multiCreator = creator;
            if (enabled)
                CreateSpaceObjects();
        }
        
        private void Awake()
        {
            selfCollider = GetComponent<Collider>();
        }

        private void OnEnable()
        {
            if (_space == null)
                return;
            
            CreateSpaceObjects();
        }

        private void OnDisable()
        {
            if (_space == null)
                return;
            
            DestroySpaceObjects();
        }

        private void CreateSpaceObjects()
        {
            if (singleCreator != null)
            {
                var spaceObject = singleCreator(_space, selfCollider);
                _spaceObjects = new[] {spaceObject};
            }
            else if (multiCreator != null)
            {
                var spaceObjects = multiCreator(_space, selfCollider);
                _spaceObjects = spaceObjects;
            }
            
            if (_spaceObjects != null && _spaceObjects.Length > 0)
                OnSpaceObjectsCreated(_spaceObjects);
        }

        private void DestroySpaceObjects()
        {
            if (_spaceObjects == null || _spaceObjects.Length <= 0)
                return;

            OnSpaceObjectsDestroy(_spaceObjects);
            
            foreach (var spaceObject in _spaceObjects)
            {
                _space.Remove(spaceObject);
            }

            _spaceObjects = null;
        }

        protected virtual void OnSpaceObjectsCreated(in ISpaceObject[] spaceObjects)
        {
            
        }
        
        protected virtual void OnSpaceObjectsDestroy(in ISpaceObject[] spaceObjects)
        {
            
        }
    }
}