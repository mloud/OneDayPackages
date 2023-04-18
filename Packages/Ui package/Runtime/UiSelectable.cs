using UnityEngine.UI;

namespace OneDay.Ui
{
    public class UiSelectable : Selectable
    {
        public void Set(int state, bool instant, bool interactible)
        {
            this.interactable = interactible;
            DoStateTransition((SelectionState) state, instant);
        }
    }
}