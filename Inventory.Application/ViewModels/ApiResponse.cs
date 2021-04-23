﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.ViewModels
{
    public class ApiResponse<T> where T : class
    {
        public ApiResponse(bool success, string message, T result = null)
        {
            Success = success;
            Message = message;
            Result = result;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}
