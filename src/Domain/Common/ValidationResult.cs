using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public struct DomainValidation
    {
        public static DomainValidation Success => new DomainValidation() { IsSuccess = true};
        public string Message { get; set; }
        public bool IsSuccess { get; set; }

        public DomainValidation(string message) : this()
        {
            this.Message = message;
        }


    }
}
