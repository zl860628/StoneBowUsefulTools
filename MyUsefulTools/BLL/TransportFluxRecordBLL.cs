
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyUsefulTools.Interface;

namespace MyUsefulTools.BLL
{
    public class TransportFluxRecordBLL
    {
        private ITransportFluxStore transportFluxObject = null;

        public TransportFluxRecordBLL(ITransportFluxStore _transportFluxObject)
        {
            this.transportFluxObject = _transportFluxObject;
        }

        public void InsertRecord()
        {
            if (this.transportFluxObject == null) return;

            DAO.TransportFlux dao = new MyUsefulTools.DAO.TransportFlux();
            dao.SetProperties(transportFluxObject.ItemName, transportFluxObject.UploadFlux, transportFluxObject.DownloadFlux,
                transportFluxObject.BeginTransportTime);
            dao.InsertNewRecord();
        }
    }
}
