using System;
using System.Collections.Generic;
using BEPUphysics.CollisionRuleManagement;

namespace UnityConversion
{
    public static class CollisionRulesDefine
    {
        private static Dictionary<int, CollisionGroup> _groups = new Dictionary<int, CollisionGroup>();

        public static CollisionGroup GetGroup(int groupFlag)
        {
            if (!_groups.TryGetValue(groupFlag, out var result))
                throw new Exception("Please set collision group and rules first");

            return result;
        }

        private static CollisionGroup AddGroup(int groupFlag)
        {
            if (_groups.ContainsKey(groupFlag))
                return _groups[groupFlag];

            var newGroup = new CollisionGroup();
            _groups.Add(groupFlag, newGroup);
            return newGroup;
        }

        public static void DefineCollisionRule(int groupA, int groupB, CollisionRule rule)
        {
            CollisionGroup.DefineCollisionRule(AddGroup(groupA), AddGroup(groupB), rule);
        }
        
        public static void DefineCollisionRulesBetweenSets(int[] aGroups, int[] bGroups, CollisionRule rule)
        {
            foreach (var group in aGroups)
            {
                DefineCollisionRulesWithSet(group, bGroups, rule);
            }
        }
        
        public static void DefineCollisionRulesWithSet(int group, int[] groups, CollisionRule rule)
        {
            foreach (var g in groups)
            {
                DefineCollisionRule(group, g, rule);
            }
        }
        
        public static void DefineCollisionRulesInSet(int[] groups, CollisionRule self, CollisionRule other)
        {
            for (int i = 0; i < groups.Length; i++)
            {
                DefineCollisionRule(groups[i], groups[i], self);
            }
            for (int i = 0; i < groups.Length - 1; i++)
            {
                for (int j = i + 1; j < groups.Length; j++)
                {
                    DefineCollisionRule(groups[i], groups[j], other);
                }
            }
        }
    }
}