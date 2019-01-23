using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleBook : MonoBehaviour
{
    [SerializeField]
    bool isAssetBundle;
    [SerializeField]
    string urlKey;
    [SerializeField]
    string versionKey;
    [SerializeField]
    string name;
    [SerializeField]
    int bookId;

    void Start()
    {
        AssetBundleInfo.BookScene.isAssetBundle = isAssetBundle;
        AssetBundleInfo.BookScene.urlKey = urlKey;
        AssetBundleInfo.BookScene.versionKey = versionKey;
        AssetBundleInfo.BookScene.name = name;
        AssetBundleInfo.BookScene.bookId = bookId;

    }

}
