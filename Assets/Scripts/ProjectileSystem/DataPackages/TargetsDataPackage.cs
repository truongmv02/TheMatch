using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ProjectileSystem.DataPackages
{
    [Serializable]
    public class TargetsDataPackage : ProjectileDataPackage
    {
        public List<Transform> targets;
    }
}