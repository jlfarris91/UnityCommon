namespace CommonLib.Behaviours
{
    using System.Collections.Generic;
    using UnityEngine;

    [ExecuteInEditMode]
    public class GridContainer : GridTransform
    {
        public IEnumerable<GridTransform> Children
        {
            get { return this.GetComponentsInChildren<GridTransform>(); }
        }
    }
}