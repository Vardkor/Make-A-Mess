using UnityEngine;

public class TexturePainter : MonoBehaviour
{
    public Texture2D brushTexture;
    public Color paintColor = Color.red;
    public float brushSize = 20f;
    private Texture2D drawTexture;

    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        Texture2D baseTexture = (Texture2D)rend.material.mainTexture;

        drawTexture = new Texture2D(baseTexture.width, baseTexture.height);
        drawTexture.SetPixels(baseTexture.GetPixels());
        drawTexture.Apply();

        rend.material.mainTexture = drawTexture;
    }

    public void Paint(Vector2 uv)
    {
        int x = (int)(uv.x * drawTexture.width);
        int y = (int)(uv.y * drawTexture.height);

        for (int i = -Mathf.RoundToInt(brushSize / 2); i < Mathf.RoundToInt(brushSize / 2); i++)
        {
            for (int j = -Mathf.RoundToInt(brushSize / 2); j < Mathf.RoundToInt(brushSize / 2); j++)
            {
                if (x + i >= 0 && x + i < drawTexture.width && y + j >= 0 && y + j < drawTexture.height)
                {
                    drawTexture.SetPixel(x + i, y + j, paintColor);
                }
            }
        }
        drawTexture.Apply();
    }
}
