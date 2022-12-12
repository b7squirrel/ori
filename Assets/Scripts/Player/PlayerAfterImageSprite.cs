using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{
    float activeTime = .1f;
    float timeActivated;
    float alpha;
    float alphaSet = .8f;
    float alphaMultiplier = .85f;


    Transform player;

    SpriteRenderer SR;
    SpriteRenderer playerSR;

    Color color;
}
