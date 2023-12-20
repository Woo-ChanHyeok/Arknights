using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class ImageAlphaCombine : MonoBehaviour
{
    public Texture2D imageTexture;  // 인스펙터에서 직접 이미지 할당
    public Texture2D alphaTexture;  // 인스펙터에서 직접 알파 이미지 할당
    public string savePath = "Assets/CombinedImage.png"; // 저장할 경로 및 파일명

    void Start()
    {
        if (imageTexture == null || alphaTexture == null)
        {
            Debug.LogError("Please assign both image and alpha textures in the inspector.");
            return;
        }

        // 이미지와 알파 이미지를 합치기
        Texture2D combinedTexture = CombineTextures(imageTexture, alphaTexture);

        // 결과를 PNG 파일로 저장
        SaveTextureToPNG(combinedTexture, savePath);
    }

    private Texture2D CombineTextures(Texture2D image, Texture2D alpha)
    {
        // 이미지와 알파 이미지를 합치기
        Texture2D combinedTexture = new Texture2D(image.width, image.height);
        for (int y = 0; y < image.height; y++)
        {
            for (int x = 0; x < image.width; x++)
            {
                Color imagePixel = image.GetPixel(x, y);
                Color alphaPixel = alpha.GetPixel(x, y);
                imagePixel.a = alphaPixel.r; // 알파 채널을 사용하여 투명도 설정
                combinedTexture.SetPixel(x, y, imagePixel);
            }
        }

        combinedTexture.Apply(); // 변경 사항을 적용

        return combinedTexture;
    }

    private void SaveTextureToPNG(Texture2D texture, string path)
    {
        // 결과를 PNG 파일로 저장
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(path, bytes);
    }

}
