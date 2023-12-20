using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class ImageAlphaCombine : MonoBehaviour
{
    public Texture2D imageTexture;  // �ν����Ϳ��� ���� �̹��� �Ҵ�
    public Texture2D alphaTexture;  // �ν����Ϳ��� ���� ���� �̹��� �Ҵ�
    public string savePath = "Assets/CombinedImage.png"; // ������ ��� �� ���ϸ�

    void Start()
    {
        if (imageTexture == null || alphaTexture == null)
        {
            Debug.LogError("Please assign both image and alpha textures in the inspector.");
            return;
        }

        // �̹����� ���� �̹����� ��ġ��
        Texture2D combinedTexture = CombineTextures(imageTexture, alphaTexture);

        // ����� PNG ���Ϸ� ����
        SaveTextureToPNG(combinedTexture, savePath);
    }

    private Texture2D CombineTextures(Texture2D image, Texture2D alpha)
    {
        // �̹����� ���� �̹����� ��ġ��
        Texture2D combinedTexture = new Texture2D(image.width, image.height);
        for (int y = 0; y < image.height; y++)
        {
            for (int x = 0; x < image.width; x++)
            {
                Color imagePixel = image.GetPixel(x, y);
                Color alphaPixel = alpha.GetPixel(x, y);
                imagePixel.a = alphaPixel.r; // ���� ä���� ����Ͽ� ���� ����
                combinedTexture.SetPixel(x, y, imagePixel);
            }
        }

        combinedTexture.Apply(); // ���� ������ ����

        return combinedTexture;
    }

    private void SaveTextureToPNG(Texture2D texture, string path)
    {
        // ����� PNG ���Ϸ� ����
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(path, bytes);
    }

}
