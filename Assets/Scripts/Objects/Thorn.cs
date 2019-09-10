using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : Interacitve
{
    public float reTime = 1f;

    public override void Interaction(Player player)
    {
        player.StopAllCoroutines();
        player.gameObject.SetActive(false);
        StartCoroutine(Fade(player));
    }


    IEnumerator Fade(Player player)
    {
        yield return new WaitForSeconds(reTime);
        player.transform.position = Camera.main.GetComponent<SceneManager>().curArchivePos.position;
        player.gameObject.SetActive(true);

    }


}
