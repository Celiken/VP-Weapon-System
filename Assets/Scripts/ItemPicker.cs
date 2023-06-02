using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CelikenVP
{
    public class ItemPicker : MonoBehaviour
    {
        [SerializeField] private List<ObjectSO> listItem = new();

        [SerializeField] private Transform parentContent;
        [SerializeField] private GameObject pfObjectRow;

        private List<GameObject> instantiatedRow = new();

        private void Start()
        {
            Invoke(nameof(ShowRandomChoice), .1f);
        }

        private void ShowRandomChoice()
        {
            List<ObjectSO> list = PickRandomItems(3);
            foreach (ObjectSO item in list)
            {
                GameObject go = Instantiate(pfObjectRow, parentContent);
                go.GetComponent<ObjectRow>().Init(item, this);
                instantiatedRow.Add(go);
            }
        }

        public void OnClick(ObjectSO item)
        {
            if (Player.Instance.PickItem(item))
                RemoveItem(item);
            ClearChoices();
            Invoke(nameof(ShowRandomChoice), .1f);
        }

        private void ClearChoices()
        {
            foreach (var go in instantiatedRow)
            {
                Destroy(go);
            }
            instantiatedRow.Clear();
        }

        private List<ObjectSO> PickRandomItems(int amount)
        {
            List<ObjectSO> items = new();
            if (amount > listItem.Count) return listItem;
            while (items.Count < amount)
            {
                int id = Random.Range(0, listItem.Count);
                if (items.Contains(listItem[id]))
                    continue;
                items.Add(listItem[id]);
            }
            return items;
        }

        private void RemoveItem(ObjectSO item)
        {
            listItem.Remove(item);
        }
    }
}