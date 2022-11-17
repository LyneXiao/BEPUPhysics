using System;
using System.Collections.Generic;
using BEPUphysics.Materials;
using FixMath.NET;
using UnityEngine;
using Material = BEPUphysics.Materials.Material;

namespace UnityConversion
{
    public class PhysicsMaterialManager: MonoBehaviour
    {
        private static readonly Fix64 Half = (Fix64) 0.5m;
        
        private static readonly Dictionary<string, Material> _materials = new Dictionary<string, Material>();

        private static Fix64 Max(in Fix64 a, in Fix64 b)
        {
            return a > b ? a : b;
        }
        
        private static Fix64 Min(in Fix64 a, in Fix64 b)
        {
            return a < b ? a : b;
        }

        public static string GetMaterialKey(PhysicMaterial material)
        {
            return $"{material.dynamicFriction}_{material.staticFriction}_{material.bounciness}_" + 
                   $"{material.frictionCombine}_{material.bounceCombine}";
        }
        
        public static Material CreateMaterial(PhysicMaterial material)
        {
            Material result;
            var key = GetMaterialKey(material);
            if (!_materials.TryGetValue(key, out result))
            {
                result = new Material((Fix64)material.staticFriction, (Fix64)material.dynamicFriction, (Fix64)material.bounciness);
                _materials.Add(key, result);
            }
            
            return result;
        }

        public static void CombineMaterial(Material materialA, Material materialB,
            PhysicMaterialCombine frictionCombine, PhysicMaterialCombine bounceCombine)
        {
            var pair = new MaterialPair(materialA, materialB);
            MaterialManager.MaterialInteractions[pair] = delegate(Material material, Material material1,
                out InteractionProperties properties)
            {
                Fix64 kineticFriction;
                Fix64 staticFriction;
                Fix64 bounciness;

                switch (frictionCombine)
                {
                    case PhysicMaterialCombine.Average:
                        kineticFriction =
                            (materialA.KineticFriction + materialB.KineticFriction) * Half;
                        staticFriction =
                            (materialA.StaticFriction + materialB.StaticFriction) * Half;
                        break;
                    case PhysicMaterialCombine.Maximum:
                        kineticFriction = Max(materialA.KineticFriction, materialB.KineticFriction);
                        staticFriction = Max(materialA.StaticFriction, materialB.StaticFriction);
                        break;
                    case PhysicMaterialCombine.Minimum:
                        kineticFriction = Min(materialA.KineticFriction, materialB.KineticFriction);
                        staticFriction = Min(materialA.StaticFriction, materialB.StaticFriction);
                        break;
                    case PhysicMaterialCombine.Multiply:
                    default:
                        kineticFriction = materialA.KineticFriction * materialB.KineticFriction;
                        staticFriction = materialA.StaticFriction * materialB.StaticFriction;
                        break;
                }

                switch (bounceCombine)
                {
                    case PhysicMaterialCombine.Average:
                        bounciness = (materialA.Bounciness + materialB.Bounciness) * Half;
                        break;
                    case PhysicMaterialCombine.Maximum:
                        bounciness = Max(materialA.Bounciness, materialB.Bounciness);
                        break;
                    case PhysicMaterialCombine.Minimum:
                        bounciness = Min(materialA.Bounciness, materialB.Bounciness);
                        break;
                    case PhysicMaterialCombine.Multiply:
                    default:
                        bounciness = materialA.Bounciness * materialB.Bounciness;
                        break;
                }
                
                properties = new InteractionProperties{
                    KineticFriction = kineticFriction,
                    StaticFriction = staticFriction,
                    Bounciness = bounciness,
                };
            };
        }
        
        [Serializable]
        public struct MaterialCombine
        {
            [SerializeField]
            public PhysicMaterial materialA;
            [SerializeField]
            public PhysicMaterial materialB;
            [SerializeField]
            public PhysicMaterialCombine frictionCombine;
            [SerializeField]
            public PhysicMaterialCombine bounceCombine;
        }

        [Tooltip("物理材质间的组合关系")]
        public MaterialCombine[] combines;

        private void Start()
        {
            foreach (var combine in combines)
            {
                var materialA = CreateMaterial(combine.materialA);
                var materialB = CreateMaterial(combine.materialB);
                CombineMaterial(materialA, materialB, combine.frictionCombine, combine.bounceCombine);
            }
        }
    }
}