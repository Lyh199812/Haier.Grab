using Base.Client.Entity;
using Base.Client.IBLL;
using Base.Client.IDAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Client.BLL
{
    public class RunLogBLL : BindableBase, IRunLogBLL
    {
        private readonly IRunLogDAL _runLogDal;

        public RunLogBLL(IRunLogDAL runLogDal)
        {
            _runLogDal = runLogDal;
            Logs = new ObservableCollection<RunLogEntity>();
        }

        private ObservableCollection<RunLogEntity> _logs;

        public ObservableCollection<RunLogEntity> Logs
        {
            get => _logs;
            set
            {
                if (_logs != value)
                {
                    _logs = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<RunLogEntity> GetAllLogs()
        {
            return Logs;
        }

        public void AddInfoLog(string info)
        {
            _runLogDal.AddRunLog(0, info, Logs);
        }

        public void AddSuccessLog(string info)
        {
            _runLogDal.AddRunLog(1, info, Logs);
        }

        public void AddWarningLog(string info)
        {
            _runLogDal.AddRunLog(2, info, Logs);
        }

        public void AddErrorLog(string info)
        {
            _runLogDal.AddRunLog(3, info, Logs);
        }

        public void ClearAllLogs()
        {
            _runLogDal.CleanOldLogs(Logs);
        }
    }


}
