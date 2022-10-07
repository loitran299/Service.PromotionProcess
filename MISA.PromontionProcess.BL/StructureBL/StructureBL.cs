using MISA.PromontionProcess.BL;
using MISA.PromotionProcess.Common.Model;
using MISA.PromotionProcess.DL;
using MISA.PromotionProcess.DL.StructureDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromotionProcess.BL.StructureBL
{
    public class StructureBL : BaseBL<Structure>, IStructureBL
    {
        private readonly IStructureDL _structureDL;
        public StructureBL(IStructureDL structureDL) : base(structureDL)
        {
            _structureDL = structureDL;
        }
    }
}
