using System;
//using Bcan.Backend.SharedKernel;
using Shine.Backend.Common;
using Shine.Backend.Common.Interfaces;
using Shine.Backend.Core.ValuObjects;

namespace Shine.Backend.Core.Entities
{
    public class User : BaseEntity<string>, IAggregateRootWithId<string>
    {
        private User() : base(){}
        public User(string id, string nickName, FullName fullName) : base(id)
        {
            if(string.IsNullOrWhiteSpace(nickName))
                throw new ArgumentNullException(nameof(nickName));

            if(fullName is null)
                throw new ArgumentNullException(nameof(fullName));

            NickName = nickName;
            FullName = fullName;
        }

        public string NickName { get; private set; }
        public FullName FullName { get; private set; }
    }
}