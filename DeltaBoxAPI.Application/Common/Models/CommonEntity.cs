using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Common.Models
{
    public class CommonEntity
    {
        [Column("created_by")]
        public int CreatedBy { get; set; }
        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
        [Column("updated_by")]
        public int UpdatedBy { get; set; }
        [Column("updated_date")]
        public DateTime? UpdatedDate { get; set; }
    }
}
