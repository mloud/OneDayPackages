using System.Collections.Generic;
using UnityEngine;

namespace OneDay.Ui.Components
{
    public class PagingDots : MonoBehaviour
    {
        [SerializeField] private List<UiToggle> dotToggles;
      
        public void OnPageChanged(int index)
        {
            if (index >= 0 && index < dotToggles.Count)
            {
                dotToggles[index].isOn = true;
            }
            else
            {
                Debug.LogError($"Index out of paging dots count");
            }
        }
    }
}