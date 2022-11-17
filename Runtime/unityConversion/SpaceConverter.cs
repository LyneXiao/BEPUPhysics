using System;
using System.Collections.Generic;
using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.CollisionShapes;
using BEPUphysics.Entities.Prefabs;
using BEPUphysics.Materials;
using BEPUutilities;
using FixMath.NET;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Space = BEPUphysics.Space;
using Vector3 = UnityEngine.Vector3;

namespace UnityConversion
{
    public static class SpaceConverter
    {
        public static void CopyPhysicsMaterial(this IMaterialOwner target, Collider collider)
        {
            var material = collider.material;
            if (material == null)
                material = collider.sharedMaterial;

            if (material == null)
                return;

            var bepuMaterial = PhysicsMaterialManager.CreateMaterial(material);
            target.Material = bepuMaterial;
        }
        
        public static StaticMesh AddStaticMesh(this Space space, MeshCollider collider)
        {
            var unitymesh = collider.sharedMesh;
            var transform = collider.transform;
            var scale = transform.lossyScale;
            var vertices = unitymesh.vertices;
            if (!scale.Equals(Vector3.one))
            {
                var len = vertices.Length;
                for (int i = 0; i < len; i++)
                {
                    var vertex = vertices[i];
                    vertex.Scale(scale);
                    vertices[i] = vertex;
                }
            }
            
            var verLen = vertices.Length;
            BEPUutilities.Vector3[] bepuVertices = new BEPUutilities.Vector3[verLen];
            for (int i = 0; i < verLen; i++)
            {
                MathConverter.Convert(in vertices[i], out bepuVertices[i]);
            }

            var indices = unitymesh.GetIndices(0);
            MathConverter.Convert(transform.position, out var bepuPoisiton);
            MathConverter.Convert(transform.rotation, out var bepuQuaternion);
            var mesh = new StaticMesh(bepuVertices, indices, new AffineTransform(bepuQuaternion, bepuPoisiton));
            CopyPhysicsMaterial(mesh, collider);
            space.Add(mesh);
            return mesh;
        }
        
        public static List<StaticMesh> AddStaticMeshes(this Space space, MeshCollider collider)
        {
            var unitymesh = collider.sharedMesh;
            var transform = collider.transform;
            var scale = transform.lossyScale;
            var vertices = unitymesh.vertices;
            if (!scale.Equals(Vector3.one))
            {
                var len = vertices.Length;
                for (int i = 0; i < len; i++)
                {
                    var vertex = vertices[i];
                    vertex.Scale(scale);
                    vertices[i] = vertex;
                }
            }
            
            var verLen = vertices.Length;
            BEPUutilities.Vector3[] bepuVertices = new BEPUutilities.Vector3[verLen];
            for (int i = 0; i < verLen; i++)
            {
                MathConverter.Convert(in vertices[i], out bepuVertices[i]);
            }

            MathConverter.Convert(transform.position, out var bepuPoisiton);
            MathConverter.Convert(transform.rotation, out var bepuQuaternion);

            var result = new List<StaticMesh>();
            for (int i = 0; i < unitymesh.subMeshCount; i++)
            {
                var indices = unitymesh.GetIndices(i);
                var mesh = new StaticMesh(bepuVertices, indices, new AffineTransform(bepuQuaternion, bepuPoisiton));
                CopyPhysicsMaterial(mesh, collider);
                space.Add(mesh);
                result.Add(mesh);
            }

            return result;
        }
        
        public static List<MobileMesh> AddKinematicMeshes(this Space space, MeshCollider collider, MobileMeshSolidity solidity)
        {
            var unitymesh = collider.sharedMesh;
            var transform = collider.transform;
            var scale = transform.lossyScale;
            var vertices = unitymesh.vertices;
            if (!scale.Equals(Vector3.one))
            {
                var len = vertices.Length;
                for (int i = 0; i < len; i++)
                {
                    var vertex = vertices[i];
                    vertex.Scale(scale);
                    vertices[i] = vertex;
                }
            }
            
            MathConverter.Convert(transform.position, out var bepuPoisiton);
            MathConverter.Convert(transform.rotation, out var bepuQuaternion);

            var result = new List<MobileMesh>();
            for (int i = 0; i < unitymesh.subMeshCount; i++)
            {
                var indices = unitymesh.GetIndices(i);
//                var mesh = AddKinematicMesh(space, in vertices, in indices, Vector3.zero, Quaternion.identity, solidity);
                var mesh = AddKinematicMesh(space, in vertices, in indices, transform.position, transform.rotation, solidity);
                CopyPhysicsMaterial(mesh, collider);

//                mesh.Position = bepuPoisiton;
//                mesh.Orientation = bepuQuaternion;
                
                result.Add(mesh);
            }

            return result;
        }
        
        public static MobileMesh AddKinematicMesh(this Space space, MeshCollider collider, MobileMeshSolidity solidity)
        {
            var unitymesh = collider.sharedMesh;
            var transform = collider.transform;
            var scale = transform.lossyScale;
            var vertices = unitymesh.vertices;
            if (!scale.Equals(Vector3.one))
            {
                var len = vertices.Length;
                for (int i = 0; i < len; i++)
                {
                    var vertex = vertices[i];
                    vertex.Scale(scale);
                    vertices[i] = vertex;
                }
            }

            var indices = unitymesh.GetIndices(0);
            var mesh = AddKinematicMesh(space, in vertices, in indices, Vector3.zero, Quaternion.identity, solidity);
            CopyPhysicsMaterial(mesh, collider);

            MathConverter.Convert(transform.position, out var bepuPoisiton);
            MathConverter.Convert(transform.rotation, out var bepuQuaternion);
            mesh.Position = bepuPoisiton;
            mesh.Orientation = bepuQuaternion;
            return mesh;
        }
        
        public static MobileMesh AddKinematicMesh(this Space space, in Vector3[] vertices, in int[] indices, in Vector3 localPosition,
            in Quaternion localRotation, MobileMeshSolidity solidity)
        {
            var verLen = vertices.Length;
            BEPUutilities.Vector3[] bepuVertices = new BEPUutilities.Vector3[verLen];
            for (int i = 0; i < verLen; i++)
            {
                MathConverter.Convert(in vertices[i], out bepuVertices[i]);
            }

            MathConverter.Convert(in localPosition, out var bepuPoisiton);
            MathConverter.Convert(in localRotation, out var bepuQuaternion);
            var mesh = new MobileMesh(bepuVertices, indices, new AffineTransform(bepuQuaternion, bepuPoisiton), solidity);
            space.Add(mesh);
            return mesh;
        }
        
        public static Box AddStaticBox(this Space space, BoxCollider collider)
        {
            return AddKinematicBox(space, collider, true);
        }

        public static Box AddKinematicBox(this Space space, BoxCollider collider)
        {
            return AddKinematicBox(space, collider, false);
        }

        private static Box AddKinematicBox(this Space space, BoxCollider collider, bool isStatic)
        {
            var size = collider.size;
            var transform = collider.transform;
            var scale = transform.lossyScale;
            var position = transform.position;
            var rotation = transform.rotation;
            var center = collider.center;
            if (!center.Equals(Vector3.zero))
            {
                if (!isStatic)
                    throw new Exception("Cannot add non static box which center is not zero");
                
                center.Scale(scale);
                position += rotation * center;

            }
            size.Scale(scale);
            var box = AddKinematicBox(space, size.x, size.y, size.z, position, rotation);
            CopyPhysicsMaterial(box, collider);
            return box;
        }

        public static Box AddKinematicBox(this Space space, float width, float height, float length, in Vector3 position, in Quaternion quaternion)
        {
            MathConverter.Convert(in position, out var bepuPosition);
            MathConverter.Convert(in quaternion, out var bepuQuaternion);
            var box = new Box(bepuPosition, (Fix64) width, (Fix64) height, (Fix64) length);
            box.Orientation = bepuQuaternion;
            space.Add(box);
            return box;
        }

        public static Sphere AddStaticSphere(this Space space, SphereCollider collider)
        {
            return AddKinematicSphere(space, collider, true);
        }
        
        public static Sphere AddKinematicSphere(this Space space, SphereCollider collider)
        {
            return AddKinematicSphere(space, collider, false);
        }

        private static Sphere AddKinematicSphere(this Space space, SphereCollider collider, bool isStatic)
        {
            var transform = collider.transform;
            var position = transform.position;
            var rotation = transform.rotation;
            var scale = transform.lossyScale;
            var radius = collider.radius * Mathf.Max(scale.x, scale.y, scale.z);
            var center = collider.center;
            if (!center.Equals(Vector3.zero))
            {
                center.Scale(scale);
                position += rotation * center;
            }
            var sphere = AddKinematicSphere(space, radius, position, rotation);
            CopyPhysicsMaterial(sphere, collider);
            return sphere;
        }

        public static Sphere AddKinematicSphere(this Space space, float radius, in Vector3 position, in Quaternion quaternion)
        {
            MathConverter.Convert(in position, out var bepuPosition);
            MathConverter.Convert(in quaternion, out var bepuQuaternion);
            var sphere = new Sphere(bepuPosition, (Fix64) radius);
            sphere.Orientation = bepuQuaternion;
            space.Add(sphere);
            return sphere;
        }

        public static Capsule AddStaticCapsule(this Space space, CapsuleCollider collider)
        {
            return AddKinematicCapsule(space, collider, true);
        }
        
        public static Capsule AddKinematicCapsule(this Space space, CapsuleCollider collider)
        {
            return AddKinematicCapsule(space, collider, false);
        }

        private static Capsule AddKinematicCapsule(this Space space, CapsuleCollider collider, bool isStatic)
        {
            var transform = collider.transform;
            var position = transform.position;
            var rotation = transform.rotation;
            var scale = transform.lossyScale;
            
            var center = collider.center;
            var height = collider.height;
            var radius = collider.radius;
            if (!center.Equals(Vector3.zero))
            {
                if (!isStatic)
                    throw new Exception("BEPU physics cannot add non static capsule which center is not zero");
                
                center.Scale(scale);
                position += rotation * center;
            }

            var direction = collider.direction;
            if (direction != 1)
            {
                if (!isStatic)
                    throw new Exception("BEPU physics cannot add non static capsule which direction is not Y-Axis");

                if (direction == 0) // X-Axis
                {
                    rotation *= Quaternion.Euler(0, 0, 90);
                    
                    height *= scale.x;
                    radius *= Mathf.Max(scale.y, scale.z);
                }
                else if (direction == 2) // Z-Axis
                {
                    rotation *= Quaternion.Euler(90, 0, 0);
                    
                    height *= scale.z;
                    radius *= Mathf.Max(scale.y, scale.x);
                }
            }
            else
            {
                height *= scale.y;
                radius *= Mathf.Max(scale.x, scale.z);
            }

            var capsule = AddKinematicCapsule(space, height, radius, position, rotation);
            CopyPhysicsMaterial(capsule, collider);
            return capsule;
        }


        public static Capsule AddKinematicCapsule(this Space space, float height, float radius, in Vector3 position,
            in Quaternion quaternion)
        {
            MathConverter.Convert(in position, out var bepuPosition);
            MathConverter.Convert(in quaternion, out var bepuQuaternion);
            var capsule = new Capsule(bepuPosition, (Fix64) height, (Fix64) radius);
            capsule.Orientation = bepuQuaternion;
            space.Add(capsule);
            return capsule;
        }

        public static MobileMesh AddDynamicMesh(this Space space, MeshCollider collider, Rigidbody rigidBody, MobileMeshSolidity solidity)
        {
            var unitymesh = collider.sharedMesh;
            var transform = collider.transform;
            var scale = transform.lossyScale;
            var vertices = unitymesh.vertices;
            if (!scale.Equals(Vector3.one))
            {
                var len = vertices.Length;
                for (int i = 0; i < len; i++)
                {
                    var vertex = vertices[i];
                    vertex.Scale(scale);
                    vertices[i] = vertex;
                }
            }
            
            var indices = unitymesh.GetIndices(-1);
            var mesh = AddDynamicMesh(space, in vertices, in indices, rigidBody.mass, Vector3.zero, Quaternion.identity, solidity);
            MathConverter.Convert(transform.position, out var bepuPoisiton);
            MathConverter.Convert(transform.rotation, out var bepuQuaternion);
            CopyPhysicsMaterial(mesh, collider);
            mesh.Position = bepuPoisiton;
            mesh.Orientation = bepuQuaternion;

            if (!rigidBody.useGravity)
            {
                mesh.Gravity = BEPUutilities.Vector3.Zero;
            }
            
            return mesh;
        }
        
        public static MobileMesh AddDynamicMesh(this Space space, in Vector3[] vertices, in int[] indices, float mass, in Vector3 localPosition,
            in Quaternion localRotation, MobileMeshSolidity solidity)
        {
            var verLen = vertices.Length;
            BEPUutilities.Vector3[] bepuVertices = new BEPUutilities.Vector3[verLen];
            for (int i = 0; i < verLen; i++)
            {
                MathConverter.Convert(in vertices[i], out bepuVertices[i]);
            }

            MathConverter.Convert(in localPosition, out var bepuPoisiton);
            MathConverter.Convert(in localRotation, out var bepuQuaternion);
            var mesh = new MobileMesh(bepuVertices, indices, new AffineTransform(bepuQuaternion, bepuPoisiton), solidity, (Fix64) mass);
            space.Add(mesh);
            return mesh;
        }

        public static Box AddDynamicBox(this Space space, BoxCollider collider, Rigidbody rigidBody)
        {
            var size = collider.size;
            var transform = collider.transform;
            var scale = transform.lossyScale;
            var position = transform.position;
            var rotation = transform.rotation;
            var center = collider.center;
            if (!center.Equals(Vector3.zero))
            {
                throw new Exception("Cannot add non static box which center is not zero");
            }

            var box = AddDynamicBox(space, size.x * scale.x, size.y * scale.y, size.z * scale.z, rigidBody.mass,
                in position, in rotation);
            CopyPhysicsMaterial(box, collider);
            
            if (!rigidBody.useGravity)
            {
                box.Gravity = BEPUutilities.Vector3.Zero;
            }

            return box;
        }
        
        public static Box AddDynamicBox(this Space space, float width, float height, float length, float mass, in Vector3 position, in Quaternion quaternion)
        {
            MathConverter.Convert(in position, out var bepuPosition);
            MathConverter.Convert(in quaternion, out var bepuQuaternion);
            var box = new Box(bepuPosition, (Fix64) width, (Fix64) height, (Fix64) length, (Fix64) mass);
            box.Orientation = bepuQuaternion;
            space.Add(box);
            return box;
        }

        public static Sphere AddDynamicSphere(this Space space, SphereCollider collider, Rigidbody rigidBody)
        {
            var transform = collider.transform;
            var scale = transform.lossyScale;
            var radius = collider.radius * Mathf.Max(scale.x, scale.y, scale.z);
            var sphere = AddDynamicSphere(space, radius, rigidBody.mass, transform.position, transform.rotation);
            CopyPhysicsMaterial(sphere, collider);
            if (!rigidBody.useGravity)
            {
                sphere.Gravity = BEPUutilities.Vector3.Zero;
            }
            return sphere;
        }
        
        public static Sphere AddDynamicSphere(this Space space, float radius, float mass, in Vector3 position, in Quaternion quaternion)
        {
            MathConverter.Convert(in position, out var bepuPosition);
            MathConverter.Convert(in quaternion, out var bepuQuaternion);
            var sphere = new Sphere(bepuPosition, (Fix64) radius, (Fix64) mass);
            sphere.Orientation = bepuQuaternion;
            space.Add(sphere);
            return sphere;
        }


        public static Capsule AddDynamicCapsule(this Space space, CapsuleCollider collider, Rigidbody rigidBody)
        {
            var transform = collider.transform;
            var position = transform.position;
            var rotation = transform.rotation;
            var scale = transform.lossyScale;
            
            var center = collider.center;
            var height = collider.height;
            var radius = collider.radius;
            if (!center.Equals(Vector3.zero))
            {
                throw new Exception("BEPU physics cannot add non static capsule which center is not zero");
            }

            var direction = collider.direction;
            if (direction != 1)
            {
                throw new Exception("BEPU physics cannot add non static capsule which direction is not Y-Axis");
            }
            else
            {
                height *= scale.y;
                radius *= Mathf.Max(scale.x, scale.z);
            }

            var capsule = AddDynamicCapsule(space, height, radius, rigidBody.mass, position, rotation);
            CopyPhysicsMaterial(capsule, collider);
            
            if (!rigidBody.useGravity)
            {
                capsule.Gravity = BEPUutilities.Vector3.Zero;
            }
            return capsule;
        }
        
        
        public static Capsule AddDynamicCapsule(this Space space, float height, float radius, float mass, in Vector3 position,
            in Quaternion quaternion)
        {
            MathConverter.Convert(in position, out var bepuPosition);
            MathConverter.Convert(in quaternion, out var bepuQuaternion);
            var capsule = new Capsule(bepuPosition, (Fix64) height, (Fix64) radius, (Fix64) mass);
            capsule.Orientation = bepuQuaternion;
            space.Add(capsule);
            return capsule;
        }

        public static List<BroadPhaseEntry> GetEntries(this Space space, BoundingBox box)
        {
            var list = new List<BroadPhaseEntry>();
            space.BroadPhase.QueryAccelerator.GetEntries(box, list);
            return list;
        }
        
        private static readonly BEPUutilities.Vector3 MinPoint = new BEPUutilities.Vector3(Fix64.MinValue, Fix64.MinValue, Fix64.MinValue);
        private static readonly BEPUutilities.Vector3 MaxPoint = new BEPUutilities.Vector3(Fix64.MaxValue, Fix64.MaxValue, Fix64.MaxValue);
        private static readonly BoundingBox FullArea = new BoundingBox(MinPoint, MaxPoint);
        public static List<BroadPhaseEntry> GetEntries(this Space space)
        {
            return GetEntries(space, FullArea);
        }
    }
}