using System;

namespace _Assetbundle
{
    public class DownloadProgress{

        public DownloadProgress(){
        }
        public DownloadProgress(String downloadURL) {
            //StartCoroutine(IEGetAssetBundleSize(downloadURL));
        }

        private float progress;
        private float contentLen;

        public float Progress {
            set {
                progress = value;
            }
            get {
                return progress;
            }
        }

        public float ContentLength {
            get { return contentLen; }
        }       
    }
}
