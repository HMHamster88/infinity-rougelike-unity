using System.Linq;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private Equipment equipment;

    private float cooldownTime = 0;
    private Item currentWeapon;

    void Start()
    {
        equipment = GetComponent<Equipment>();
    }

    void Update()
    {
        if (currentWeapon != null)
        {
            currentWeapon.GetProperty<WeaponProperty>().Update();
        }
        cooldownTime -= Time.deltaTime;
        if (cooldownTime < 0)
        {
            cooldownTime = 0;
        }
    }

    public void Attack(Vector2 targetPoint)
    {
        if (cooldownTime > 0) 
        {
            return;
        }
        var allWeapons = equipment.AllItemSlots
            .Where(slot => slot.Item != null)
            .Select(slot => slot.Item)
            .Where(item => item.GetProperty<WeaponProperty>() != null)
            .ToList();
        if (currentWeapon == null)
        {
            currentWeapon = allWeapons.FirstOrDefault();
        }
        else
        {
            var currentWeaponIndex = allWeapons.IndexOf(currentWeapon);
            if (currentWeaponIndex == -1)
            {
                currentWeapon = allWeapons.FirstOrDefault();
            }
            else
            {
                currentWeaponIndex++;
                if (currentWeaponIndex >= allWeapons.Count) 
                { 
                    currentWeapon = allWeapons.FirstOrDefault();
                }
                else
                {
                    currentWeapon = allWeapons[currentWeaponIndex];
                }
            }
        }

        if (currentWeapon == null) 
        { 
            return; 
        }

        var weaponComponent = currentWeapon.GetProperty<WeaponProperty>();
        weaponComponent.Attack(gameObject, targetPoint, currentWeapon);

        cooldownTime = 1.0f / weaponComponent.AttacksPerSecond;
    }
}
