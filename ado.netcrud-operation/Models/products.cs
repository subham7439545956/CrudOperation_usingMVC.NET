using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ado.netcrud_operation.Models
{
    public class products
    {
        [Key]
        public int productid { get; set; }

        [Required]
        public string productName { get; set; }

        public int HSN { get; set; }

        [Required]
        public string mfg_com { get; set; }

        [Required]
        public string batch{ get; set; }

        [Required]
        public DateTime expiry { get; set; }

        [Required]
        public string pkg { get; set; }

        public int qty { get; set; }

        [Required]
        public int rate { get; set; }
    }
}