using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoadSceneFromAssetBundleException : Exception {

   public enum ErrorCode
   {
      NullReference, MissingKey
   }

   public ErrorCode Error { get; set; }

   public LoadSceneFromAssetBundleException(string message, ErrorCode error)
   : base (message)
   {
      Error = error;
   }

  
}
