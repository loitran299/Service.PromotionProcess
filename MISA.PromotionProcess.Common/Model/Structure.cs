using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromotionProcess.Common.Model
{
    [Table("structure")]
    public class Structure
    {
        /// <summary>
        /// ID cơ cấu
        /// </summary>
        [Key]
        public Guid StructureID { get; set; }

        /// <summary>
        /// Tên cơ cấu
        /// </summary>
        public string StructureName { get; set; }
    }
}
