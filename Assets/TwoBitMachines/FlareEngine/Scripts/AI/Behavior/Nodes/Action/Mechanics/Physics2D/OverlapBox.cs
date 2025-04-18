#region 
#if UNITY_EDITOR
using TwoBitMachines.Editors;
using UnityEditor;
#endif
#endregion
using TwoBitMachines.FlareEngine.AI.BlackboardData;
using UnityEngine;

namespace TwoBitMachines.FlareEngine.AI
{
        [AddComponentMenu("")]
        public class OverlapBox : Conditional
        {
                public LayerMask layer;
                public Blackboard origin;
                public CastType searchFor;
                public Vector2 size;
                public float angle = 0;

                public override NodeState RunNodeLogic (Root root)
                {
                        if (searchFor == CastType.SingleHit)
                        {
                                if (CastSingle(root))
                                        return NodeState.Success;
                        }
                        else
                        {
                                if (CastAll(root))
                                        return NodeState.Success;
                        }

                        return NodeState.Failure;
                }

                public bool CastSingle (Root root)
                {
                        Collider2D collider2D = Physics2D.OverlapBox(origin.GetTarget(), size, angle, layer);
                        if (origin.hasNoTargets)
                        {
                                return false;
                        }
                        if (collider2D != null)
                        {
                                Root.collider2DRef = collider2D;
                                return true;
                        }

                        return false;
                }

                public bool CastAll (Root root)
                {
                        root.SetLayerMask(layer);
                        int i = Physics2D.OverlapBox(origin.GetTarget(), size, angle, Root.filter2D, Root.colliderResults);
                        if (origin.hasNoTargets)
                        {
                                return false;
                        }
                        return i > 0;
                }

                #region ▀▄▀▄▀▄ Custom Inspector ▄▀▄▀▄▀
#if UNITY_EDITOR
#pragma warning disable 0414
                public override bool OnInspector (AIBase ai, SerializedObject parent, Color color, bool onEnable)
                {
                        if (parent.Bool("showInfo"))
                        {
                                Labels.InfoBoxTop(44, "Implement an OverlapBox using Physics2D. The results can be accessed by Nearest2DResults.");
                        }

                        FoldOut.Box(5, color, offsetY: -2);
                        parent.Field("Layer", "layer");
                        AIBase.SetRef(ai.data, parent.Get("origin"), 0);
                        parent.Field("Search For", "searchFor");
                        parent.Field("Size", "size");
                        parent.Field("Angle", "angle");
                        Layout.VerticalSpacing(3);
                        return true;
                }
#pragma warning restore 0414
#endif
                #endregion
        }
}
