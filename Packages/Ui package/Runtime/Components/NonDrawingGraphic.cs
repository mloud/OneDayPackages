﻿using UnityEngine.UI;

namespace OneDay.Ui.Components
{
    public class NonDrawingGraphic : Graphic
    {
        public override void SetMaterialDirty()
        { }

        public override void SetVerticesDirty()
        { }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
        }
    }
}