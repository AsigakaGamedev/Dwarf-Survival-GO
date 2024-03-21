using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningTileAttack : AAttack
{
    [SerializeField] private LayerMask tilesLayers;
    [SerializeField] private float mineRayDistance = 2;
    [SerializeField] private float miningPower = 1;

    private WorldManager worldManager;

    public override void OnInit()
    {
        worldManager = WorldManager.Instance;
    }

    public override void OnAttack(Vector2 dir)
    {
        RaycastHit2D mineHit = Physics2D.Raycast(transform.position, dir, mineRayDistance, tilesLayers);

        if (mineHit.collider)
        {
            Vector2 minePoint = mineHit.point;
            minePoint -= mineHit.normal / 2;
            minePoint = new Vector2((int)minePoint.x, (int)minePoint.y);
            //minePoint += (mineHit.point - (Vector2)transform.position).normalized / 2;

            //Debug.DrawRay(mineHit.point, mineHit.normal, Color.blue, 2);
            //Debug.DrawLine(transform.position, mineHit.point, Color.red, 2);

            //Debug.DrawLine(transform.position, minePoint, Color.green, 2);

            if (worldManager.TryMineCell(minePoint, miningPower))
            {
                //Debug.DrawLine(transform.position, minePoint, Color.green, 2);
            }
        }
    }
}
