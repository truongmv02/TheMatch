using UnityEngine;

namespace Game.Weapons
{
    public class OptionalSpriteMarket : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer => gameObject.GetComponent<SpriteRenderer>();
    }
}