using UnityEngine;

public class RollBrick : RollGen, IPhysics, IRollAction
{
    Vector2 Force;

    #region Physics Functions
    /// <summary>
    /// 리지드바디, 콜라이더 씌우고, 위로 던짐
    /// </summary>
    public void AddDumpRollPhysics()
    {
        Rigidbody2D rb = this.gameObject.AddComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
        rb.gravityScale = 5f;
        BoxCollider2D boxCol = this.gameObject.AddComponent<BoxCollider2D>();
        boxCol.size = new Vector2(1.6f, 1.3f);
        Destroy(gameObject, .3f);
    }
    /// <summary>
    /// 리지드바디, 콜라이더 씌우고, 마우스 방향, 롤의 속도 데이터 방향으로 던짐
    /// </summary>
    public void AddHitRollPhysics(float mouseDir, float movementInputDir, float playerFacingDir, float xVelocity)
    {
        Rigidbody2D rb = this.gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 5f;
        BoxCollider2D boxCol = this.gameObject.AddComponent<BoxCollider2D>();
        boxCol.size = new Vector2(1.6f, 1.3f);
        BoxCollider2D boxColTrg = this.gameObject.AddComponent<BoxCollider2D>();
        boxColTrg.size = new Vector2(1.7f, 1.4f);
        boxColTrg.isTrigger = true;

        CalForce(mouseDir, movementInputDir, playerFacingDir, xVelocity);
        rb.AddForce(Force, ForceMode2D.Impulse);
    }
    #endregion

    public void RollAction()
    {
        IFlavourAction flavourAction = GetComponentInChildren<IFlavourAction>();
        if (flavourAction != null)
        {
            flavourAction.FlavourAction();
        }
    }

    void CalForce(float mouseDir, float movementInputDir, float playerFacingDir, float playerVelocity) 
    {
        Vector2 rollVelocity = RecipeRoll.instance.GetRollSo(GetComponent<RollHealth>().RollType).velocity;

        if (mouseDir == movementInputDir)
        {
            Force = new Vector2(rollVelocity.x * mouseDir + playerFacingDir * playerVelocity, rollVelocity.y);
        }
        else
        {
            Force = new Vector2(rollVelocity.x * mouseDir, rollVelocity.y);
        }
    }
}
