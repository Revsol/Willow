namespace Server.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using TransportModels;

    public class DataRepository : IDataRepository
    {
        #region member vars

        private readonly Dictionary<string, Data> _dataSet = new Dictionary<string, Data>();

        #endregion

        #region explicit interfaces

        public IEnumerable<Data> GetDataSet()
        {
            return _dataSet.Select(kvp => kvp.Value).ToList();
        }

        public void Save(Data data)
        {
            _dataSet[data.DeviceName] = data;
        }

        #endregion
    }
}