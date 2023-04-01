using Manager.App.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.App.Abstract
{
    public interface IService<T>
    {
        public List<T> Someone { get; set; }

        OperationResult Add(T person);

        OperationResult Remove(T person);

        OperationResult Edit(T person);

    }
}
