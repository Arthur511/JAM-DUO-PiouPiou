using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
/// <summary>
/// Represents the image used as a mask to make a circular fade
/// </summary>
public class CircularFade : Image
{
    public override Material materialForRendering
    {
        get { 
            Material material = new Material(base.materialForRendering);
            material.SetInt("_StencilComp", (int)CompareFunction.NotEqual);
            return material; 
        }    
    }
}
