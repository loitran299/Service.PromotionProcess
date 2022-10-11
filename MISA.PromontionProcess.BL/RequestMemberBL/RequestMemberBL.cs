﻿using MISA.PromontionProcess.BL;
using MISA.PromotionProcess.Common.Model;
using MISA.PromotionProcess.DL;
using MISA.PromotionProcess.DL.RequestMemberDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.PromotionProcess.BL.RequestMemberBL
{
    public class RequestMemberBL : BaseBL<RequestMember>, IRequestMemberBL
    {
        private readonly IRequestMemberDL _requestMemberDL;
        public RequestMemberBL(IRequestMemberDL requestMemberDL) : base(requestMemberDL)
        {
            _requestMemberDL = requestMemberDL;
        }
    }
}