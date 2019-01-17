using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeUsageException : Exception {

    public enum ErrorCode
    {
        NullReference
    }

    public ErrorCode Error { get; set; }

    public TimeUsageException(string message, ErrorCode error)
    :base(message)
    {
        Error = error;
    }
}
