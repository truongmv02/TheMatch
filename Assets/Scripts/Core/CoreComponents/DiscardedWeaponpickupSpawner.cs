/*using Game.Interaction.Interactables;
using UnityEngine;
namespace Game.CoreSystem
{
    public class DiscardedWeaponpickupSpawner : CoreComponent
    {
        [SerializeField] private Vector2 spawnDirection;
        [SerializeField] float spawnVelocity;
        [SerializeField] WeaponPickup weaponPickupPrefab;
        [SerializeField] private Vector2 spawnOffset;

        private WeaponSwap weaponSwap;

        private Movement movement;

        private void HandleWeaponDiscarded(WeaponDataSO discardWeaponData)
        {
            var spawnPoint = movement.FindRelativePoint(spawnOffset);
            var weaponPickup = Instantiate(weaponPickupPrefab, spawnPoint, Quaternion.identity);

            weaponPickup.SetContext(discardWeaponData);

            var adjustedSpawnDirecton = new Vector2(
                spawnDirection.x * movement.FacingDirection, spawnDirection.y
            );
            weaponPickup.Rigidbody2D.velocity = adjustedSpawnDirecton * spawnVelocity;
        }

        protected override void Awake()
        {
            base.Awake();

            weaponSwap = core.GetCoreComponent<WeaponSwap>();
            movement = core.GetCoreComponent<Movement>();
        }

        private void OnEnable()
        {
            weaponSwap.OnWeaponDiscarded += HandleWeaponDiscarded;
        }

        private void OnDisable()
        {
            weaponSwap.OnWeaponDiscarded -= HandleWeaponDiscarded;
        }
    }
}*/