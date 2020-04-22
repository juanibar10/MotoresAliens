using UnityEngine;
using UnityEditor;

public class Example3DTexture : MonoBehaviour
{
    [MenuItem("Create 3D Texture/3D Texture")]
    static void CreateTexture3D()
    {
        int size = 32;

        TextureFormat format = TextureFormat.RGBA32;
        TextureWrapMode wrapMode = TextureWrapMode.Clamp;


        Texture3D texture = new Texture3D(size, size, size, format, false);
        texture.wrapMode = wrapMode;

        Color[] colors = new Color[size * size * size];

        float inverseResolution = 1f / (size - 1f);
        for (int z = 0; z < size; z++)
        {
            int zOffset = z * size * size;
            for (int y = 0; y < size; y++)
            {
                int yOffset = y * size;
                for (int x = 0; x < size; x++)
                {
                    colors[x + yOffset + zOffset] = new Color(x * inverseResolution, y * inverseResolution, z * inverseResolution, 1.0f);
                }
            }
        }

        texture.SetPixels(colors);

        texture.Apply();
        AssetDatabase.CreateAsset(texture, "Assets/3DTexture.asset");
    }
}
