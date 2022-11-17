using BEPUphysics;
using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
using BEPUphysics.Entities;
using BEPUphysics.NarrowPhaseSystems.Pairs;

namespace UnityConversion
{
    public class FixedObjectEventListener<T>: FixedObjectLifeListener where T: Entity
    {
        protected override void OnSpaceObjectsCreated(in ISpaceObject[] spaceObjects)
        {
            foreach (var spaceObject in spaceObjects)
            {
                if (spaceObject is T entity)
                    entity.CollisionInformation.Events.InitialCollisionDetected += OnInitialCollisionDetected;
            }
        }

        protected override void OnSpaceObjectsDestroy(in ISpaceObject[] spaceObjects)
        {
            foreach (var spaceObject in spaceObjects)
            {
                if (spaceObject is T entity)
                    entity.CollisionInformation.Events.InitialCollisionDetected -= OnInitialCollisionDetected;
            }
        }

        protected virtual void OnInitialCollisionDetected(EntityCollidable sender, Collidable other, CollidablePairHandler pair)
        {
        }
    }
}