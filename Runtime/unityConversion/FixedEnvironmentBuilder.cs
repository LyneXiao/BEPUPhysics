using System.Collections.Generic;
using BEPUphysics;
using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.CollisionRuleManagement;
using BEPUphysics.CollisionShapes;
using BEPUphysics.Entities;
using UnityEngine;
using Space = BEPUphysics.Space;

namespace UnityConversion
{
    public class FixedEnvironmentGroupFlagAttribute : PropertyAttribute
    {
        
    }
    
    public class FixedEnvironmentBuilder: MonoBehaviour
    {
        [Tooltip("环境包含的所有方块")]
        public BoxCollider[] cubes;

        [Tooltip("环境包含的所有圆球")]
        public SphereCollider[] spheres;

        [Tooltip("环境包含的所有胶囊")]
        public CapsuleCollider[] capsules;

        [Tooltip("环境包含的所有网格")]
        public MeshCollider[] meshes;

        public MobileMeshSolidity meshSolidity = MobileMeshSolidity.Solid;

        [FixedEnvironmentGroupFlag]
        [Tooltip("该环境构建器包含的所有物理对象所属的碰撞群主标识")]
        public int groupFlag;

        private CollisionGroup _collisionGroup;

        public void BuildAll(Space space)
        {
            _collisionGroup = CollisionRulesDefine.GetGroup(groupFlag);

            if (cubes != null)
            {
                foreach (var cube in cubes)
                {
                    var listener = AddOrGetListener(cube);
                    listener.Build(space, CreateBox);
                }
            }

            if (spheres != null)
            {
                foreach (var sphere in spheres)
                {
                    var listener = AddOrGetListener(sphere);
                    listener.Build(space, CreateSphere);
                }
            }

            if (capsules != null)
            {
                foreach (var capsule in capsules)
                {
                    var listener = AddOrGetListener(capsule);
                    listener.Build(space, CreateCapsule);
                }
            }

            if (meshes != null)
            {
                var useStatic = false;// meshSolidity == MobileMeshSolidity.Solid;
                foreach (var mesh in meshes)
                {
                    var listener = AddOrGetListener(mesh);
                    if (useStatic)
                        listener.Build(space, CreateStaticMeshes);
                    else
                        listener.Build(space, CreateKinematicMeshes);
                }
            }
        }

        private static FixedObjectLifeListener AddOrGetListener(Collider collider)
        {
            var cmp = collider.GetComponent<FixedObjectLifeListener>();
            if (cmp == null)
                cmp = collider.gameObject.AddComponent<FixedObjectLifeListener>();

            return cmp;
        }

        private ISpaceObject CreateBox(Space space, Collider box)
        {
            return ResetEntity(space.AddStaticBox((BoxCollider) box), box.tag);
        }

        private ISpaceObject CreateSphere(Space space, Collider sphere)
        {
            return ResetEntity(space.AddStaticSphere((SphereCollider) sphere), sphere.tag);
        }

        private ISpaceObject CreateCapsule(Space space, Collider capsule)
        {
            return ResetEntity(space.AddStaticCapsule((CapsuleCollider) capsule), capsule.tag);
        }

        private ISpaceObject[] CreateStaticMeshes(Space space, Collider mesh)
        {
            return ResetMeshes(space.AddStaticMeshes((MeshCollider) mesh), mesh.tag);
        }
        
        private ISpaceObject[] CreateKinematicMeshes(Space space, Collider mesh)
        {
            return ResetEntities(space.AddKinematicMeshes((MeshCollider) mesh, meshSolidity), mesh.tag);
        }

        private Entity ResetEntity(Entity entity, string objTag)
        {
            entity.CollisionInformation.CollisionRules.Group = _collisionGroup;
            entity.SetTag(objTag);
            
            return entity;
        }
        
        private ISpaceObject[] ResetEntities(IReadOnlyList<Entity> entities, string objTag)
        {
            var count = entities.Count;
            var result = new ISpaceObject[count];
            for (int i = 0; i < count; i++)
            {
                var entity = entities[i];
                entity.CollisionInformation.CollisionRules.Group = _collisionGroup;
                entity.SetTag(objTag);
                result[i] = entity;
            }
            
            return result;
        }

        private ISpaceObject[] ResetMeshes(List<StaticMesh> staticMeshes, string objTag)
        {
            var count = staticMeshes.Count;
            var result = new ISpaceObject[count];
            for (int i = 0; i < count; i++)
            {
                var mesh = staticMeshes[i];
                mesh.CollisionRules.Group = _collisionGroup;
                mesh.Tag = objTag;
                result[i] = mesh;
            }
            
            return result;
        }
    }
}