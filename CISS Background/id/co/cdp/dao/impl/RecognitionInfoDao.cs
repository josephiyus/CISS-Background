using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CISS_Background.id.co.cdp.vo;
using CISS_Background.id.co.cdp.common.persistence;
using CISS_Background.id.co.cdp.common.persistence.abstracts;
using CISS_Background.id.co.cdp.util;
using CISS_Background.id.co.cdp.model;
using CISS.id.co.cdp.model;
using CISS_Background.id.co.cdp.constant;

namespace CISS_Background.id.co.cdp.dao.impl
{
    public class RecognitionInfoDao : IRecognitionInfoDao
    {
        ICommonDatabase repo;

        public static RecognitionInfoDao getInstance(ICommonDatabase securosDb) 
        {
            return new RecognitionInfoDao(securosDb);
        }

        public RecognitionInfoDao(ICommonDatabase securosDb) 
        {
            this.repo = securosDb;
        }

        public RecognitionInfoVo getRecognitionInfo(Event ev)
        {
            RecognitionInfoVo input = new RecognitionInfoVo();
            input.line = TransactionUtil.getGateInfo(ev).line;
            input.line_type = TransactionUtil.getGateInfo(ev).lineType;
            return repo.getOneOrDefault<RecognitionInfoVo>(input, null, null, null);
        }


        public TLogVo getLastTransactionByGateId(string gateId)
        {
            TLogVo param = new TLogVo();
            param.lpr_id = gateId;
            return repo.getOneOrDefault(param, "tid", AppConstant.DESC, null);
        }
    }
}
