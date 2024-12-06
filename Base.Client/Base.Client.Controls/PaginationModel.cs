using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Base.Client.Controls
{
    public class PaginationModel : INotifyPropertyChanged
    {
        //public PaginationModel(ICommand navCommand, ICommand countChangeCommand)
        //{
        //    NavCommand = navCommand;
        //    CountPerPageChangeCommand = countChangeCommand;
        //}

        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand NavCommand { get; set; }
        public ICommand CountPerPageChangeCommand { get; set; }

        public ObservableCollection<PageNumberItemModel> PageNumbers { get; set; } = new ObservableCollection<PageNumberItemModel>();


        private bool _isCanPrevious = true;
        public bool IsCanPrevious
        {
            get => _isCanPrevious;
            set
            {
                this._isCanPrevious = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsCanPrevious"));
            }
        }

        private bool _isCanNext = true;
        public bool IsCanNext
        {
            get => _isCanNext;
            set
            {
                this._isCanNext = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsCanNext"));
            }
        }

        private int _countPerPage = 10;
        /// <summary>
        /// 每页的显示数据条数
        /// </summary>
        public int CountPerPage
        {
            get => _countPerPage;
            set
            {
                this._countPerPage = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CountPerPage"));
            }
        }

        private int _previousIndex;
        /// <summary>
        /// 前一条数据的Index     如果当前Index=2  1   3
        /// </summary>
        public int PreviousIndex
        {
            get => _previousIndex;
            set
            {
                this._previousIndex = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PreviousIndex"));
            }
        }

        private int _nextIndex;
        public int NextIndex
        {
            get => _nextIndex;
            set
            {
                this._nextIndex = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NextIndex"));
            }
        }

        // 填充页码
        public void FillPages(int sumCount, int pageCode)
        {
            IsCanPrevious = true;
            IsCanNext = true;
            PreviousIndex = pageCode - 1;
            NextIndex = pageCode + 1;

            // 总页数
            // 向上取整    13  总      10      1.3   2
            int pageCount = (int)Math.Ceiling(sumCount * 1.0 / CountPerPage);
            if (pageCode > pageCount) pageCode = pageCount;

            // 处理前一页和后一页按钮的可用性
            if (pageCode == 1)
                IsCanPrevious = false;
            if (pageCode == pageCount)
                IsCanNext = false;


            List<object> temp = new List<object>();
            //[1]2 3 4 5 6 7 8 9 ... 15
            // 1 2 3 4 [5] 6 7 8 9 ... 15
            // 1 ... 3 4 5 [6] 7 8 9 ... 15
            // 1 ... 7 8 9 [10] 11 12 13 ... 15
            // 1 ... 8 9 10 [11] 12 13 14 15      10条    50条
            // 省略首页和尾页

            int min = pageCode - 4;
            if (min <= 1) min = 1;
            else min = pageCode - 3;

            int max = pageCode + 4;
            if (pageCode <= 5)
                max = Math.Min(9, pageCount);
            else
            {
                if (max >= pageCount) max = pageCount;
                else max = pageCode + 3;
            }
            if (pageCode >= pageCount - 4)
                min = Math.Max(1, pageCount - 8);


            if (min > 1)
            {
                temp.Add(1);
                temp.Add("...");
            }
            for (int i = min; i <= max; i++)
                temp.Add(i);
            if (max < pageCount)
            {
                temp.Add("...");
                temp.Add(pageCount);
            }

            int index;
            PageNumbers.Clear();
            foreach (var item in temp)
            {
                // 将object值 转换成数字
                bool state = int.TryParse(item.ToString(), out index);
                PageNumbers.Add(new PageNumberItemModel
                {
                    Index = (state ? index : item),
                    IsCurrent = index == pageCode,
                    IsEnabled = state
                });
            }
        }
    }

    public class PageNumberItemModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        /// <summary>
        /// 页码（都是数字，不一定，需要对不能显示的页码进行隐藏：...  ）  
        /// </summary>
        public object Index { get; set; }
        public bool IsEnabled { get; set; } = true;
        /// <summary>
        /// 选中状态
        /// </summary>
        private bool _isCurrent;
        public bool IsCurrent
        {
            get => _isCurrent;
            set
            {
                this._isCurrent = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsCurrent"));
            }
        }

    }
}
