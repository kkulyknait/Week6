using UnityEngine;

public class ColorMod : MonoBehaviour
{
   public void ChangeColor(Material newMat)
    {
        GetComponent<Renderer>().material.color = newMat.color;
    }
    
  
}
