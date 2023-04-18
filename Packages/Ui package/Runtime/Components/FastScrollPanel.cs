using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


namespace OneDay.Ui.Components
{
    /// <summary>
    /// Panel that contains items that could be recycled inside scroll area
    /// items going out of the scroll view are reported as NOT visible items coming to
    /// scroll view are reported as VISIBLE
    /// In this way outer system connected to ElementVisibilityChanged can get instance of the
    /// transform from the pool and fill the data and release it (place it back to the pool) when becomes
    /// invisible
    /// </summary>
    public class FastScrollPanel : MonoBehaviour
    {
        public delegate void ElementVisibilityChanged(InternalElement.CustomData data, bool visible);

        [SerializeField] private bool horizontal;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private Vector2 gap;
        [SerializeField] private int visibilityCheckDistance;
        private List<InternalElement> Elements { get; } = new();
        private List<int> VisibleItemIndexes { get; } = new();
        private bool NeedsVisibilityRecalculate { get; set; }
        private bool NeedsSizeRecalculate { get; set; }

        public InternalElement.CustomData GetCustomData(int id)
        {
            int index = Elements.FindIndex(x => x.Data.Id == id);
            return index != -1 ? Elements[index].Data : null;
        }

        public void RemoveElement(int id)
        {
            var elementIndex = Elements.FindIndex(x => x.Data.Id == id);
            if (elementIndex == -1)
            {
                Debug.LogError($"Cannot remove element with index {elementIndex}, no such element exists");
                return;
            }

            // not the last
            if (elementIndex < Elements.Count - 1)
            {
                Vector2 distanceToNextElement = Elements[elementIndex + 1].Position - Elements[elementIndex].Position;
                for (int i = elementIndex + 1; i < Elements.Count; i++)
                {
                    Elements[i].Position -= distanceToNextElement;

                    if (Elements[i].Data.ExternalElement != null)
                    {
                        Elements[i].Data.ExternalElement.localPosition = Elements[i].Position;
                    }
                }
            }

            var contentSize = scrollRect.content.sizeDelta;
            if (horizontal)
            {
                contentSize.x -= Elements[elementIndex].Rect.width + gap.x;
            }
            else
            {
                contentSize.y -= Elements[elementIndex].Rect.height + gap.y;
            }

            scrollRect.content.sizeDelta = contentSize;
            Elements.RemoveAt(elementIndex);
            VisibleItemIndexes.Remove(elementIndex);
            NeedsVisibilityRecalculate = true;
        }

        public void InsertElementAt(int index, Rect rect, int id, ElementVisibilityChanged visibilityChanged)
        {
            Vector2 position = Vector2.zero;
            bool insertAsLast = index == Elements.Count;
            if (Elements.Count > 0)
            {
                if (insertAsLast)
                {
                    position = Elements[index - 1].Position;
                    position += horizontal
                            ? new Vector2(Elements[index - 1].Rect.width + gap.x, 0)
                            : new Vector2(0, -Elements[index - 1].Rect.height - gap.y);
                        
                }
                else
                {
                    position = Elements[index].Position;
                }
            }

            var element = new InternalElement(id, visibilityChanged)
            {
                Rect = rect,
                Position = position
            };
            Elements.Insert(index, element);

            if (!insertAsLast)
            {
                Vector2 shift = horizontal
                    ? new Vector2(rect.width + gap.x, 0)
                    : new Vector2(0,-rect.height - gap.y);

                for (int i = index + 1; i < Elements.Count; i++)
                {
                    Elements[i].Position += shift;
                }
            }
         
            var contentSize = scrollRect.content.sizeDelta;
            if (horizontal)
            {
                contentSize.x += element.Rect.width + gap.x;
            }
            else
            {
                contentSize.y += element.Rect.height + gap.y;
            }

            scrollRect.content.sizeDelta = contentSize;
            NeedsVisibilityRecalculate = true;
        }
        
        public void InsertElementAsFirst(Rect rect, int id, ElementVisibilityChanged visibilityChanged) =>
           InsertElementAt(0, rect, id, visibilityChanged);

        public void InsertElementAsLast(Rect rect, int id, ElementVisibilityChanged visibilityChanged) =>
            InsertElementAt(Elements.Count, rect, id, visibilityChanged);
     
        private void LateUpdate()
        {
            if (NeedsVisibilityRecalculate)
            {
                RecalculateFullVisibility();
                NeedsVisibilityRecalculate = false;
            }
            else
            {
                RecalculateVisibilityFromCurrentState();
            }
        }

        private void RecalculateVisibilityFromCurrentState()
        {
            if (VisibleItemIndexes.Count > 0)
            {
                int minIndex = Math.Max(0, VisibleItemIndexes.Min() - visibilityCheckDistance);
                int maxIndex = Math.Min(VisibleItemIndexes.Max() + visibilityCheckDistance, Elements.Count - 1);
                for (int i = minIndex; i <= maxIndex; i++)
                {
                    RecalculateVisibility(i);
                }
            }
            else 
            {
                RecalculateFullVisibility();
            }
        }

        private void RecalculateVisibility(int index)
        {
            bool isVisible = IsVisible(Elements[index]);
            // become visible
            if (isVisible)
            {
                bool needsPositionSync = Elements[index].NeedsPositionSync;
                if (!Elements[index].Visible)
                {
                    VisibleItemIndexes.Add(index);
                    Elements[index].Visible = true;
                    Elements[index].VisibilityChanged(Elements[index].Data, true);
                    if (Elements[index].Data.ExternalElement != null)
                    {
                        Elements[index].Data.ExternalElement.SetParent(scrollRect.content);
                        needsPositionSync = true;
                    }
                }

                if (needsPositionSync)
                {
                    Elements[index].Data.ExternalElement.localPosition = Elements[index].Position;
                    Elements[index].NeedsPositionSync = false;
                }
            }
            // become invisible
            else if (Elements[index].Visible)
            {
                Elements[index].Visible = false;
                Elements[index].VisibilityChanged(Elements[index].Data, false);
                VisibleItemIndexes.Remove(index);
            }
        }

        private void RecalculateFullVisibility()
        {
            VisibleItemIndexes.Clear();
            for (int i = 0; i < Elements.Count; i++)
            {
                RecalculateVisibility(i);
            }
        }

        private bool IsVisible(InternalElement internalElement)
        {
            var contentLocalPosition = scrollRect.content.localPosition;
            var positionInView = new Vector2(contentLocalPosition.x, contentLocalPosition.y) + internalElement.Position;

            var viewRect = ((RectTransform) scrollRect.content.parent).rect;
            if (positionInView.x > viewRect.width) return false;
            if ((positionInView.x + internalElement.Rect.width) < 0) return false;
            if (positionInView.y < -viewRect.height) return false;
            if ((positionInView.y - internalElement.Rect.height) > 0) return false;

            return true;
        }
    }
}
