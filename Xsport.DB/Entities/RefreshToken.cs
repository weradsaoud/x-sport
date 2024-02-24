﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.Entities
{
    public class RefreshToken
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public string Token { get; set; } = null!;

        public string JwtId { get; set; } = null!;

        public bool IsUsed { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime AddedDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public virtual XsportUser User { get; set; } = null!;
    }
}
