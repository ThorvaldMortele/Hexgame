using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.Views
{
    public class TileView : MonoBehaviour
    {
        internal Vector3 Size 
        { 
            set 
            {
                transform.localScale = Vector3.one;

                var meshRenderer = GetComponentInChildren<MeshRenderer>();
                var meshSize = meshRenderer.bounds.size;

                var ratioX = value.x / meshSize.x;
                var ratioY = value.y / meshSize.y;
                var ratioZ = value.y / meshSize.y;

                transform.localScale = new Vector3(ratioX, ratioY, ratioZ);
            } 
        }
    }
}
