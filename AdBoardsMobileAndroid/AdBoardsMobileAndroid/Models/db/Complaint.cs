﻿using System;
using System.Collections.Generic;
#nullable enable

namespace AdBoardsMobileAndroid.Models.db
{
    public partial class Complaint
    {
        public int Id { get; set; }

        public int? AdId { get; set; }

        public int? PersonId { get; set; }

        public virtual Ad? Ad { get; set; }

        public virtual Person? Person { get; set; }
    }
}
#nullable restore
