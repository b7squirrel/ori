using System;
using System.Collections.Generic;
using UnityEngine;

public class RollBrick : RollGen, IPhysics
{
    /// <summary>
    /// 리지드바디, 콜라이더 씌우고, 위로 던짐
    /// </summary>
    public void AddDumpRollPhysics()
    {
        Rigidbody2D rb = this.gameObject.AddComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
        rb.gravityScale = 5f;
        BoxCollider2D boxCol = this.gameObject.AddComponent<BoxCollider2D>();
        boxCol.size = new Vector2(1f, .8f);
        Destroy(gameObject, .3f);
    }
    /// <summary>
    /// 리지드바디, 콜라이더 씌우고, 마우스 방향, 롤의 속도 데이터 방향으로 던짐
    /// </summary>
    public void AddHitRollPhysics(float xDir)
    {
        Rigidbody2D rb = this.gameObject.AddComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
        rb.gravityScale = 5f;
        BoxCollider2D boxCol = this.gameObject.AddComponent<BoxCollider2D>();
        boxCol.size = new Vector2(1f, .8f);

        Vector2 velocity = RecipeRoll.instance.GetRollSo(rollType).velocity;
        rb.AddForce(new Vector2(velocity.x * xDir, velocity.y), ForceMode2D.Impulse);
    }
}
