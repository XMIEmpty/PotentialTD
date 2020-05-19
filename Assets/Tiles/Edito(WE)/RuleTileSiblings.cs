using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class RuleTileSiblings : RuleTile<RuleTileSiblings.Neighbor>
{
    public List<TileBase> siblings = new List<TileBase>();



    public class Neighbor : RuleTile.TilingRule.Neighbor
    {
        public const int Sibling = 3;
    }



    public override bool RuleMatch(int neighbor, TileBase tile)
    {
        switch (neighbor)
        {
            case Neighbor.Sibling: return siblings.Contains(tile);
        }
        return base.RuleMatch(neighbor, tile);
    }
}
