using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace core.webui.Models
{
    public class CommentModel
    {

        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double? Rate { get; set; }

    }
}