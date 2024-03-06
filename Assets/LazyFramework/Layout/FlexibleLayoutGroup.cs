using UnityEngine;
using UnityEngine.UI;

public class FlexibleLayoutGroup : LayoutGroup
{
    [Header("Setting")]
    [SerializeField] FitType fitType;
    [SerializeField] bool fitX;
    [SerializeField] bool fitY;
    [Header("Config")]
    [SerializeField] int rows;
    [SerializeField] int collums;
    [SerializeField] Vector2 cellSize;
    [SerializeField] Vector2 spacing;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();
        if (fitType==FitType.Width||fitType==FitType.Height||fitType==FitType.Uniform)
        {
            float sqrt = Mathf.Sqrt(transform.childCount);
            rows=Mathf.CeilToInt(sqrt);
            collums=Mathf.FloorToInt(sqrt);
        }

        if (fitType==FitType.Width||fitType==FitType.FixedCollums)
        {
            rows=Mathf.CeilToInt(transform.childCount/(float)collums);
        }
        if (fitType==FitType.Height||fitType==FitType.FixedRow)
        {
            collums=Mathf.CeilToInt(transform.childCount/(float)rows);
        }

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = parentWidth/collums-((spacing.x/collums)*(collums-1))-(padding.left/(float)collums)-(padding.right/(float)collums);
        float cellHeight = parentHeight/rows-((spacing.y/rows)*(rows-1))-(padding.top/(float)rows)-(padding.bottom/(float)rows);

        cellSize.x=fitX ? cellWidth : cellSize.x;
        cellSize.y=fitY ? cellHeight : cellSize.y;

        int rowIndex = 0;
        int collumsIndex = 0;

        for (int i = 0; i< rectChildren.Count; i++)
        {
            rowIndex = i/collums;
            collumsIndex = i%collums;

            var item = rectChildren[i];

            var xPos = (cellSize.x*collumsIndex)+(spacing.x*collumsIndex)+padding.left;
            var yPos = (cellSize.y*rowIndex)+(spacing.y*rowIndex)+padding.top;

            SetChildAlongAxis(item , 0 , xPos , cellSize.x);
            SetChildAlongAxis(item , 1 , yPos , cellSize.y);
        }
    }
    public override void CalculateLayoutInputVertical()
    {

    }

    public override void SetLayoutHorizontal()
    {

    }

    public override void SetLayoutVertical()
    {

    }
    public enum FitType
    {
        Uniform,
        Width,
        Height,
        FixedRow,
        FixedCollums,
    }
}
