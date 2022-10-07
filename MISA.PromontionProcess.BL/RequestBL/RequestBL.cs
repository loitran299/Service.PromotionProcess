using MISA.PromontionProcess.BL;
using MISA.PromotionProcess.Common.Model;
using MISA.PromotionProcess.DL;
using MISA.PromotionProcess.DL.RequestDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromotionProcess.BL.RequestBL
{
    public class RequestBL : BaseBL<Request>, IRequestBL
    {
        private readonly IRequestDL _requesDL;
        public RequestBL(IRequestDL requesDL) : base(requesDL)
        {
            _requesDL = requesDL;
        }

        #region Override
        protected override void BeforeSaveAsyn(Request entity)
        {
            entity.RequestID = Guid.NewGuid();
            base.BeforeSaveAsyn(entity);
        }
        #endregion
    }
}
