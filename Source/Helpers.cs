using System;
using UnityEngine;

namespace GameStateSkillTree
{
    public static class Helpers
    {
        public static Quaternion Rotate2D(Vector3 start, Vector3 end) {
            var dir = end - start;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}