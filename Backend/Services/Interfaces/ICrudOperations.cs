using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICrudOperations<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetBy(object param);
        Task<T> Create(T body);
        Task<T> Update(T body);
        Task<T> Delete(object param);
    }
}
