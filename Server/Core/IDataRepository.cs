namespace Server.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using TransportModels;

    public interface IDataRepository
    {
        #region methods

        IEnumerable<Data> GetDataSet();

        void Save(Data data);

        #endregion
    }
}