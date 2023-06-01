using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CelikenVP
{
    [CreateAssetMenu(menuName = "Object")]
    public class ObjectSO : SerializedScriptableObject
    {
        [SerializeField] private Sprite objectIcon;
        [SerializeField] private string objectName;
        [SerializeField] private string objectDescription;

        [SerializeField] private List<Upgrade> upgrades = new List<Upgrade>();
    }
}