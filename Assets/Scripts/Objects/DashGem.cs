using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashGem : Interacitve
{
    bool flag;

    float reTime = 2f;

    MeshRenderer meshRenderer;
    BoxCollider2D boxCollider2D;


    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public override void Interact(Player player)
    {
        if (!flag)
        {
            player.canDash = true;
            StartCoroutine(Interactive());
        }
    }

    IEnumerator Interactive()
    {
        flag = true;

        meshRenderer.enabled = false;
        boxCollider2D.enabled = false;

        yield return new WaitForSeconds(reTime);
        meshRenderer.enabled = true;
        boxCollider2D.enabled = true;

        flag = false;

    }

}
