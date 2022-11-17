using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.Entities;
using FixMath.NET;
using UnityEngine;

namespace UnityConversion
{
    public static class EntityConvert
    {
        public static Vector3 GetPosition(this Entity entity)
        {
            var position = entity.Position;
            MathConverter.Convert(in position, out var unityPosition);
            return unityPosition;
        }

        public static Quaternion GetRotation(this Entity entity)
        {
            var rotation = entity.Orientation;
            MathConverter.Convert(in rotation, out var unityQuaternion);
            return unityQuaternion;
        }

        public static Vector3 GetVelocity(this Entity entity)
        {
            var velocity = entity.LinearVelocity;
            MathConverter.Convert(in velocity, out var unityVelocity);
            return unityVelocity;
        }

        public static void SetPosition(this Entity entity, decimal x, decimal y, decimal z)
        {
            SetPosition(entity, new BEPUutilities.Vector3((Fix64) x, (Fix64) y, (Fix64) z));
        }

        public static void SetPosition(this Entity entity, in Vector3 position)
        {
            MathConverter.Convert(in position, out var bepuVector);
            SetPosition(entity, bepuVector);
        }
        
        public static void SetPosition(this Entity entity, in BEPUutilities.Vector3 position)
        {
            entity.Position = position;
            entity.CollisionInformation.UpdateBoundingBox();
        }

        public static void SetRotation(this Entity entity, in Vector3 euler)
        {
            var rotation = Quaternion.Euler(euler);
            SetRotation(entity, in rotation);
        }

        public static void SetRotation(this Entity entity, in Quaternion quaternion)
        {
            MathConverter.Convert(in quaternion, out var bepuQuaternion);
            entity.Orientation = bepuQuaternion;
        }

        public static void SetForward(this Entity entity, in Vector3 forward)
        {
            SetRotation(entity, Quaternion.LookRotation(forward, Vector3.up));
        }

        public static void SetVelocity(this Entity entity, in Vector3 velocity)
        {
            MathConverter.Convert(in velocity, out var bepuVector);
            entity.LinearVelocity = bepuVector;
        }

        public static void SetVelocity(this Entity entity, decimal x, decimal y, decimal z)
        {
            entity.LinearVelocity = new BEPUutilities.Vector3((Fix64) x, (Fix64) y, (Fix64) z);
        }

        public static void SetTag(this Entity entity, string tag)
        {
            entity.CollisionInformation.Tag = tag;
        }

        public static string GetTag(this Entity entity)
        {
            return (string)entity.CollisionInformation.Tag;
        }

        public static bool CompareTag(this Entity entity, string tag)
        {
            return tag.Equals(entity.CollisionInformation.Tag);
        }
        
        public static void SetTag(this BroadPhaseEntry entry, string tag)
        {
            entry.Tag = tag;
        }

        public static string GetTag(this BroadPhaseEntry entry)
        {
            return (string)entry.Tag;
        }

        public static bool CompareTag(this BroadPhaseEntry entry, string tag)
        {
            return tag.Equals(entry.Tag);
        }
    }
}