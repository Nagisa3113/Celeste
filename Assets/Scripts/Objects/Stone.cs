using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Interacitve
{
    bool flag;

    float stayTime = 1f;
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
            StartCoroutine(Interactive());
    }


    IEnumerator Interactive()
    {
        flag = true;

        yield return new WaitForSeconds(stayTime);
        meshRenderer.enabled = false;
        boxCollider2D.enabled = false;

        yield return new WaitForSeconds(reTime);
        meshRenderer.enabled = true;
        boxCollider2D.enabled = true;

        flag = false;

    }

}
