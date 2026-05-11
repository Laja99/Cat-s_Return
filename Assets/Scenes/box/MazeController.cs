using UnityEngine;

public class MazeController : MonoBehaviour
{
    public float gravityStrength = 9.8f;

    void Update()
    {
        // ÇáÍÕæá Úáì ÇáÅÏÎÇá ãä ÇáÃÓåã Ãæ WASD
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // ÊÛííÑ ÇÊÌÇå ÇáÌÇĞÈíÉ ÈäÇÁğ Úáì ÇáãíáÇä
        Vector2 newGravity = new Vector2(moveX, moveY) * gravityStrength;
        Physics2D.gravity = newGravity;

        // ÇÎÊíÇÑí: ÊÏæíÑ ÇáãÊÇåÉ ÈÕÑíÇğ ÈÔßá ÈÓíØ áÊÚÒíÒ ÇáÔÚæÑ ÈÇáãíáÇä
        transform.rotation = Quaternion.Euler(moveY * 5f, 0, -moveX * 5f);
    }
}