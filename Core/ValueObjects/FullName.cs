using System;
using System.Collections.Generic;
using Bcan.Backend.SharedKernel;

namespace Shine.Backend.Core.ValuObjects
{
    public class FullName : ValueObject
    {
        private FullName(){}
        public FullName(string first, string last)
        {
            if(string.IsNullOrWhiteSpace(first))
                throw new ArgumentNullException(nameof(first));
            
            if(string.IsNullOrWhiteSpace(last))
                throw new ArgumentNullException(nameof(last));

            First = first;
            Last = last;
        }

        public string First { get; private set; }
        public string Last { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return First;
            yield return Last;
        }

        public string AsFormatted()
        {
            return First + " " + Last;
        }

        public string AsReverseFormatted()
        {
            return Last + ", " + First;
        }

        public override string ToString()
        {
            return "FullName [firstName=" + First + ", lastName=" + Last + "]";
        }
    }
}