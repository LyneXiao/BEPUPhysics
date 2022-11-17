using UnityEngine;

namespace UnityConversion
{
    /// <summary>
    /// Helps convert between XNA math types and the BEPUphysics replacement math types.
    /// A version of this converter could be created for other platforms to ease the integration of the engine.
    /// </summary>
    public static class MathConverter
    {
        //Vector2
        public static Vector2 Convert(BEPUutilities.Vector2 bepuVector)
        {
            Vector2 toReturn;
            toReturn.x = (float)bepuVector.X;
            toReturn.y = (float)bepuVector.Y;
            return toReturn;
        }

        public static void Convert(in BEPUutilities.Vector2 bepuVector, out Vector2 unityVector)
        {
            unityVector.x = (float)bepuVector.X;
            unityVector.y = (float)bepuVector.Y;
        }

        public static BEPUutilities.Vector2 Convert(Vector2 unityVector)
        {
            BEPUutilities.Vector2 toReturn;
            toReturn.X = unityVector.x;
            toReturn.Y = unityVector.y;
            return toReturn;
        }

        public static void Convert(in Vector2 unityVector, out BEPUutilities.Vector2 bepuVector)
        {
            bepuVector.X = unityVector.x;
            bepuVector.Y = unityVector.y;
        }

        //Vector3
        public static Vector3 Convert(BEPUutilities.Vector3 bepuVector)
        {
            Vector3 toReturn;
            toReturn.x = (float)bepuVector.X;
            toReturn.y = (float)bepuVector.Y;
            toReturn.z = (float)bepuVector.Z;
            return toReturn;
        }

        public static void Convert(in BEPUutilities.Vector3 bepuVector, out Vector3 unityVector)
        {
            unityVector.x = (float)bepuVector.X;
            unityVector.y = (float)bepuVector.Y;
            unityVector.z = (float)bepuVector.Z;
        }

        public static BEPUutilities.Vector3 Convert(Vector3 unityVector)
        {
            BEPUutilities.Vector3 toReturn;
            toReturn.X = unityVector.x;
            toReturn.Y = unityVector.y;
            toReturn.Z = unityVector.z;
            return toReturn;
        }

        public static void Convert(in Vector3 unityVector, out BEPUutilities.Vector3 bepuVector)
        {
            bepuVector.X = unityVector.x;
            bepuVector.Y = unityVector.y;
            bepuVector.Z = unityVector.z;
        }

        public static Vector3[] Convert(BEPUutilities.Vector3[] bepuVectors)
        {
            Vector3[] xnaVectors = new Vector3[bepuVectors.Length];
            for (int i = 0; i < bepuVectors.Length; i++)
            {
                Convert(in bepuVectors[i], out xnaVectors[i]);
            }
            return xnaVectors;

        }

        public static BEPUutilities.Vector3[] Convert(Vector3[] xnaVectors)
        {
            var bepuVectors = new BEPUutilities.Vector3[xnaVectors.Length];
            for (int i = 0; i < xnaVectors.Length; i++)
            {
                Convert(in xnaVectors[i], out bepuVectors[i]);
            }
            return bepuVectors;

        }

        //Matrix
        public static Matrix4x4 Convert(BEPUutilities.Matrix matrix)
        {
            Matrix4x4 toReturn;
            Convert(in matrix, out toReturn);
            return toReturn;
        }

        public static BEPUutilities.Matrix Convert(Matrix4x4 matrix)
        {
            BEPUutilities.Matrix toReturn;
            Convert(in matrix, out toReturn);
            return toReturn;
        }

        public static void Convert(in BEPUutilities.Matrix matrix, out Matrix4x4 unityMatrix)
        {
            unityMatrix.m00 = (float)matrix.M11;
            unityMatrix.m01 = (float)matrix.M12;
            unityMatrix.m02 = (float)matrix.M13;
            unityMatrix.m03 = (float)matrix.M14;

            unityMatrix.m10 = (float)matrix.M21;
            unityMatrix.m11 = (float)matrix.M22;
            unityMatrix.m12 = (float)matrix.M23;
            unityMatrix.m13 = (float)matrix.M24;

            unityMatrix.m20 = (float)matrix.M31;
            unityMatrix.m21 = (float)matrix.M32;
            unityMatrix.m22 = (float)matrix.M33;
            unityMatrix.m23 = (float)matrix.M34;

            unityMatrix.m30 = (float)matrix.M41;
            unityMatrix.m31 = (float)matrix.M42;
            unityMatrix.m32 = (float)matrix.M43;
            unityMatrix.m33 = (float)matrix.M44;

        }

        public static void Convert(in Matrix4x4 matrix, out BEPUutilities.Matrix bepuMatrix)
        {
            bepuMatrix.M11 = matrix.m00;
            bepuMatrix.M12 = matrix.m01;
            bepuMatrix.M13 = matrix.m02;
            bepuMatrix.M14 = matrix.m03;
                                              
            bepuMatrix.M21 = matrix.m10;
            bepuMatrix.M22 = matrix.m11;
            bepuMatrix.M23 = matrix.m12;
            bepuMatrix.M24 = matrix.m13;
                                              
            bepuMatrix.M31 = matrix.m20;
            bepuMatrix.M32 = matrix.m21;
            bepuMatrix.M33 = matrix.m22;
            bepuMatrix.M34 = matrix.m23;
                                              
            bepuMatrix.M41 = matrix.m30;
            bepuMatrix.M42 = matrix.m31;
            bepuMatrix.M43 = matrix.m32;
            bepuMatrix.M44 = matrix.m33;

        }

        public static Matrix4x4 Convert(BEPUutilities.Matrix3x3 matrix)
        {
            Matrix4x4 toReturn;
            Convert(in matrix, out toReturn);
            return toReturn;
        }

        public static void Convert(in BEPUutilities.Matrix3x3 matrix, out Matrix4x4 unityMatrix)
        {
            unityMatrix.m00 = (float)matrix.M11;
            unityMatrix.m01 = (float)matrix.M12;
            unityMatrix.m02 = (float)matrix.M13;
            unityMatrix.m03 = 0;
                           
            unityMatrix.m10 = (float)matrix.M21;
            unityMatrix.m11 = (float)matrix.M22;
            unityMatrix.m12 = (float)matrix.M23;
            unityMatrix.m13 = 0;
                           
            unityMatrix.m20 = (float)matrix.M31;
            unityMatrix.m21 = (float)matrix.M32;
            unityMatrix.m22 = (float)matrix.M33;
            unityMatrix.m23 = 0;
                           
            unityMatrix.m30 = 0;
            unityMatrix.m31 = 0;
            unityMatrix.m32 = 0;
            unityMatrix.m33 = 1;
        }

        public static void Convert(in Matrix4x4 matrix, out BEPUutilities.Matrix3x3 bepuMatrix)
        {
            bepuMatrix.M11 = matrix.m00;
            bepuMatrix.M12 = matrix.m01;
            bepuMatrix.M13 = matrix.m02;

            bepuMatrix.M21 = matrix.m10;
            bepuMatrix.M22 = matrix.m11;
            bepuMatrix.M23 = matrix.m12;

            bepuMatrix.M31 = matrix.m20;
            bepuMatrix.M32 = matrix.m21;
            bepuMatrix.M33 = matrix.m22;

        }

        //Quaternion
        public static Quaternion Convert(BEPUutilities.Quaternion quaternion)
        {
            Quaternion toReturn;
            toReturn.x = (float)quaternion.X;
            toReturn.y = (float)quaternion.Y;
            toReturn.z = (float)quaternion.Z;
            toReturn.w = (float)quaternion.W;
            return toReturn;
        }

        public static BEPUutilities.Quaternion Convert(Quaternion quaternion)
        {
            BEPUutilities.Quaternion toReturn;
            toReturn.X = quaternion.x;
            toReturn.Y = quaternion.y;
            toReturn.Z = quaternion.z;
            toReturn.W = quaternion.w;
            return toReturn;
        }

        public static void Convert(in BEPUutilities.Quaternion bepuQuaternion, out Quaternion quaternion)
        {
            quaternion.x = (float)bepuQuaternion.X;
            quaternion.y = (float)bepuQuaternion.Y;
            quaternion.z = (float)bepuQuaternion.Z;
            quaternion.w = (float)bepuQuaternion.W;
        }

        public static void Convert(in Quaternion quaternion, out  BEPUutilities.Quaternion bepuQuaternion)
        {
            bepuQuaternion.X = quaternion.x;
            bepuQuaternion.Y = quaternion.y;
            bepuQuaternion.Z = quaternion.z;
            bepuQuaternion.W = quaternion.w;
        }

//        //Ray
//        public static BEPUutilities.Ray Convert(Ray ray)
//        {
//            BEPUutilities.Ray toReturn;
//            Convert(ref ray.origin, out toReturn.Position);
//            Convert(ref ray.direction, out toReturn.Direction);
//            return toReturn;
//        }
//
//        public static void Convert(ref Ray ray, out BEPUutilities.Ray bepuRay)
//        {
//            Convert(ref ray.origin, out bepuRay.Position);
//            Convert(ref ray.direction, out bepuRay.Direction);
//        }
//
//        public static Ray Convert(BEPUutilities.Ray ray)
//        {
//            Ray toReturn;
//            Convert(ref ray.Position, out toReturn.origin);
//            Convert(ref ray.Direction, out toReturn.direction);
//            return toReturn;
//        }
//
//        public static void Convert(ref BEPUutilities.Ray ray, out Ray xnaRay)
//        {
//            Convert(ref ray.Position, out xnaRay.origin);
//            Convert(ref ray.Direction, out xnaRay.direction);
//        }

        //BoundingBox
        public static Rect Convert(BEPUutilities.BoundingBox boundingBox)
        {
            var min = Convert(boundingBox.Min);
            var max = Convert(boundingBox.Max);
            return Rect.MinMaxRect(min.x, min.y, max.x, max.y);
        }

        public static BEPUutilities.BoundingBox Convert(Rect boundingBox)
        {
            BEPUutilities.BoundingBox toReturn;
            Convert(boundingBox.min, out toReturn.Min);
            Convert(boundingBox.max, out toReturn.Max);
            return toReturn;
        }

        public static void Convert(in BEPUutilities.BoundingBox boundingBox, out Rect unityRect)
        {
            var min = Convert(boundingBox.Min);
            var max = Convert(boundingBox.Max);
            unityRect = Rect.MinMaxRect(min.x, min.y, max.x, max.y);
        }

        public static void Convert(ref Rect boundingBox, out BEPUutilities.BoundingBox bepuBoundingBox)
        {
            Convert(boundingBox.min, out bepuBoundingBox.Min);
            Convert(boundingBox.max, out bepuBoundingBox.Max);
        }

        //BoundingSphere
        public static BoundingSphere Convert(BEPUutilities.BoundingSphere boundingSphere)
        {
            Convert(in boundingSphere.Center, out var center);
            BoundingSphere toReturn = new BoundingSphere(center, (float)boundingSphere.Radius);
            return toReturn;
        }

        public static BEPUutilities.BoundingSphere Convert(BoundingSphere boundingSphere)
        {
            BEPUutilities.BoundingSphere toReturn;
            Convert(in boundingSphere.position, out toReturn.Center);
            toReturn.Radius = boundingSphere.radius;
            return toReturn;
        }

        public static void Convert(ref BEPUutilities.BoundingSphere boundingSphere, out BoundingSphere unityBoundingSphere)
        {
            Convert(in boundingSphere.Center, out var center);
            unityBoundingSphere.position = center;
            unityBoundingSphere.radius = (float)boundingSphere.Radius;
        }

        public static void Convert(ref BoundingSphere boundingSphere, out BEPUutilities.BoundingSphere bepuBoundingSphere)
        {
            Convert(in boundingSphere.position, out bepuBoundingSphere.Center);
            bepuBoundingSphere.Radius = boundingSphere.radius;
        }

        //Plane
        public static Plane Convert(BEPUutilities.Plane plane)
        {
            Convert(in plane.Normal, out var normal);
            Plane toReturn = new Plane(normal, (float)plane.D);
            return toReturn;
        }

        public static BEPUutilities.Plane Convert(Plane plane)
        {
            BEPUutilities.Plane toReturn;
            Convert(plane.normal, out toReturn.Normal);
            toReturn.D = plane.distance;
            return toReturn;
        }

        public static void Convert(ref BEPUutilities.Plane plane, out Plane unityPlane)
        {
            Convert(in plane.Normal, out var normal);
            unityPlane = new Plane(normal, (float)plane.D);
        }

        public static void Convert(ref Plane plane, out BEPUutilities.Plane bepuPlane)
        {
            Convert(plane.normal, out bepuPlane.Normal);
            bepuPlane.D = plane.distance;
        }
    }
}
