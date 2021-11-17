using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CISS_Background.id.co.cdp.model;
using CISS_Background.id.co.cdp.vo;
using CISS.id.co.cdp.model;

namespace CISS_Background.id.co.cdp.dao
{
    public interface IRecognitionInfoDao
    {
        RecognitionInfoVo getRecognitionInfo(Event ev);

        TLogVo getLastTransactionByGateId(string gateId);
    }
}
