using UnityEngine;

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public float fireRate;
    public float projectileSpeed;

    public GameObject projectilePrefab;
    public Color weaponColor;  //change gun color 


}
